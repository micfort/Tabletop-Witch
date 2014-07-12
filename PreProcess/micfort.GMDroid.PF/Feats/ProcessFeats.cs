using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using micfort.GMDroid.PF.TypeConverter;

namespace micfort.GMDroid.PF.Feats
{
	public class ProcessFeats: IProcess
	{
		public string input;
		public RuleContext tree;

		public void LoadInput(string input)
		{
			ErrorHandler errorChecker = new ErrorHandler ();

			AntlrInputStream inputStream = new AntlrInputStream(input);
			CSVLexer lexer = new CSVLexer(inputStream);
			CommonTokenStream tokens = new CommonTokenStream(lexer);
			CSVParser parser = new CSVParser(tokens);
			parser.AddErrorListener (errorChecker);

			tree = parser.file ();

			if (!errorChecker.Success)
			{
				throw errorChecker.Exception;
			}
		}

		public List<JObject> GenerateJson()
		{
			JsonCreator creator = new JsonCreator();
			creator.converters = new Dictionary<string, ITypeConverter>() {
				{"teamwork", new BoolConverter()},
				{"critical", new BoolConverter()},
				{"grit", new BoolConverter()},
				{"style", new BoolConverter()},
				{"performance", new BoolConverter()},
				{"racial", new BoolConverter()},
				{"companion_familiar", new BoolConverter()},
				{"multiples", new BoolConverter()},
			};
			JArray array = (JArray)creator.Visit(tree);
			List<JObject> output = new List<JObject>();
			for(int i = 0; i < array.Count; i++)
			{
				output.Add((JObject)array[i]);
			}
			return output;
		}
	}

	class ErrorHandler : BaseErrorListener
	{
		public bool Success{ get; set; }
		public RecognitionException Exception { get; set; }

		public ErrorHandler()
		{
			this.Success = true;
		}

		public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
		{
			if(this.Success)
			{
				this.Success = false;
				this.Exception = e;
			}
			base.SyntaxError(recognizer, offendingSymbol, line, charPositionInLine, msg, e);
		}	
	}

	class JsonCreator: CSVBaseVisitor<JToken>
	{
		public List<string> headers;
		public Dictionary<string, ITypeConverter> converters = new Dictionary<string, ITypeConverter>();

		public override JToken VisitFile(CSVParser.FileContext context)
		{
			Visit(context.hdr());
			JArray array = new JArray();
			foreach(var row in context.row())
			{
				array.Add(Visit(row));
			}

			return array;
		}

		public override JToken VisitHdr(CSVParser.HdrContext context)
		{
			this.headers = new List<string>();
			IReadOnlyList<CSVParser.FieldContext> list = context.field();
			for (int i = 0; i < list.Count; i++)
			{
				headers.Add(list[i].GetText());
			}
			return base.VisitHdr (context);
		}

		public override JToken VisitRow(CSVParser.RowContext context)
		{
			JObject output = new JObject();
			int i = 0;
			foreach(var item in context.field())
			{
				string text = item.GetText();
				if(text.StartsWith("\""))
				{
					text = text.Substring(1, text.Length - 2);
					text.Replace("\"\"", "\"");
				}
				if(converters.ContainsKey(headers[i]))
				{
					output[headers[i]] = converters[headers[i]].Convert(text);
				}
				else
				{
					output[headers[i]] = text;
				}

				i++;
			}
			return output;
		}
	}
}

