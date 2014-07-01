using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using MyCouch;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace micfort.dndroid.PF.Json
{
	class ErrorInfo
	{
		public int count = 0;
		public List<int> line = new List<int>();
	}

	class Program
	{
		public static bool succes = true;
		public static int position = -1;
		public static Dictionary<string, ErrorInfo> histogram = new Dictionary<string, ErrorInfo>();
		public static List<JObject> Monsters = new List<JObject>();


		static void Main(string[] args)
		{
			string filename = @"D:\S120397\Documents\DnDroid\Pathfinder\monster_stat_blocks_full - Updated 23Mar2014-Michiel.csv";
			string[] monsterLines = File.ReadAllLines(filename);

			int monsterCount = monsterLines.Length - 1;
			int correct = 0;

			JsonCreater creater = new JsonCreater();

			for (int i = 1; i < monsterLines.Length; i++)
			{
				//Console.Out.WriteLine("Parsing monster: {0}", i);
				AntlrInputStream input = new AntlrInputStream(monsterLines[i] + "\r\n");
				PFLexer lexer = new PFLexer(input);
				lexer.RemoveErrorListeners();
				CommonTokenStream tokens = new CommonTokenStream(lexer);
				PFParser parser = new PFParser(tokens);
				parser.RemoveErrorListeners();
				parser.AddErrorListener(new ErrorHandler());
				succes = true;
				//PFParser.MonsterContext tree = parser.monster();
				RuleContext tree = parser.monster();
				if(succes)
				{
					correct++;
					JObject monster = (JObject)creater.Visit(tree);
					monster["_id"] = i.ToString();
					Monsters.Add(monster);
				}
				else
				{

					IParseTree error = FindErrorNode(tree);
					List<IParseTree> path = GetPathToRoot(error);
					//PrintPath(path);
					//Console.WriteLine();
					if(error != null)
					{
						//Console.Out.WriteLine("------- This was monster: {0} with element {1}", i, path[path.Count-2].GetType().Name);
						Add(path[path.Count - 2].GetType().Name, i);
					}
					else
					{
						//Console.Out.WriteLine("------- This was monster: {0} (couldn't find element)", i);
						Add("Unkown", i);
					}
				}
			}
			PrintHistogram();
			Console.WriteLine("Correct: {0}", correct/(float)monsterCount);

			OutputJsonFilesToDisk(Monsters, "output");

			//Console.WriteLine(tree.ToStringTree());
			Console.ReadKey();
		}

		public static void OutputJsonFilesToDisk(List<JObject> monsters, string outputDirectory)
		{
			Console.Out.WriteLine("Writing information");

			if (!Directory.Exists(outputDirectory))
				Directory.CreateDirectory(outputDirectory);

			foreach (JObject monster in monsters)
			{
				string ID = monster["_id"].ToString();
				string filename = ID + ".json";

				File.WriteAllText(outputDirectory + Path.DirectorySeparatorChar + filename, monster.ToString(Formatting.Indented));
			}
		}


		public static void OutputJsonFilesToServer(List<JObject> monsters)
		{
			Console.Out.WriteLine("Writing information");

			string APIUser = "wherstanothelostristonsi";
			string APIPass = "abwKS6TWoopSwTrLybk4YKUh";
			string DB = "micfort_gmdroid_pf_monsters_r1";
			string Username = "micfort";

			using (var client = new MyCouchClient(string.Format("https://{0}:{1}@{2}.cloudant.com/{3}", APIUser, APIPass, Username, DB)))
			{
				foreach (JObject monster in monsters)
				{
					var task = client.Documents.PostAsync(monster.ToString());
					task.Wait();
					if(!task.Result.IsSuccess)
					{
						Console.WriteLine("Monster {0} didn't post", monster["_id"]);
					}
				}
				
			}
		}

		public static void Add(string name, int line)
		{
			if(!histogram.ContainsKey(name))
			{
				histogram[name] = new ErrorInfo();
				
			}
			histogram[name].count++;
			histogram[name].line.Add(line);
		}

		private static void PrintHistogram()
		{
			List<KeyValuePair<string, ErrorInfo>> hist = histogram.ToList();
			hist.Sort((pair, valuePair) => pair.Value.count.CompareTo(valuePair.Value.count));
			for (int i = 0; i < hist.Count; i++)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append("[");
				for (int j = 0; j < hist[i].Value.line.Count; j++)
				{
					sb.Append(hist[i].Value.line[j]);
					sb.Append(" ");
				}
				sb.Append("]");
				Console.Out.WriteLine("{0}: {1} {2}", hist[i].Value.count, hist[i].Key, sb.ToString());
			}
		}

		private static void PrintPath(List<IParseTree> path)
		{
			for (int i = path.Count - 1; i >= 0; i--)
			{
				Console.Write(path[i].GetType().Name + " ");
			}
		}

		private static List<IParseTree> GetPathToRoot(IParseTree node)
		{
			List<IParseTree> result = new List<IParseTree>();
			IParseTree current = node;
			result.Add(current);
			do
			{
				result.Add(current);
				current = current.Parent;
			} while (current != null);

			return result;
		} 

		private static IParseTree FindErrorNode(IParseTree root)
		{
			for (int i = 0; i < root.ChildCount; i++)
			{
				var child = root.GetChild(i);
				if(child is IErrorNode)
				{
					return child;
				}
				else
				{
					var result = FindErrorNode(child);
					if(result != null)
					{
						return result;
					}
				}
			}
			return null;
		}
	}

	class ErrorHandler : BaseErrorListener
	{
		public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
		{
			if(Program.succes == true)
			{
				Program.position = charPositionInLine;
				Program.succes = false;
			}
			base.SyntaxError(recognizer, offendingSymbol, line, charPositionInLine, msg, e);
		}	
	}

	class JsonCreater: PFBaseVisitor<JToken>
	{
		
		public int CurrentID = 0;

		public override JToken VisitMonster(PFParser.MonsterContext context)
		{
			JObject monster = new JObject();
			monster["Name"] = Visit(context.nameCell());
			monster["Alignment"] = Visit(context.alignmentCell());
			monster["html"] = Visit(context.fulltextCell());
			return monster;
		}

		public override JToken VisitFulltextCell(PFParser.FulltextCellContext context)
		{
			return Visit(context.cell());
		}

		public override JToken VisitNameCell(PFParser.NameCellContext context)
		{
			return Visit(context.cell());
		}

		public override JToken VisitAlignmentCell(PFParser.AlignmentCellContext context)
		{
			var child = context.GetChild(0);
			if(child != null)
			{
				return child.GetText();
			}
			return base.VisitAlignmentCell(context);
		}

		public override JToken VisitCell(PFParser.CellContext context)
		{
			if(context.quotedText() != null)
			{
				return Visit(context.quotedText());
			}
			else if(context.text() != null)
			{
				return Visit(context.text());
			}
			else
			{
				return string.Empty;
			}
		}

		public override JToken VisitQuotedText(PFParser.QuotedTextContext context)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < context.ChildCount; i++)
			{
				sb.Append(context.GetChild(i).GetText());
			}
			return sb.ToString();
		}

		public override JToken VisitText(PFParser.TextContext context)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < context.ChildCount; i++)
			{
				sb.Append(context.GetChild(i).GetText());
			}
			return sb.ToString();
		}
	}
}
