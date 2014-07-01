using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace micfort.dndroid.DnD4.Downloader
{
	class Program
	{
		static void Main(string[] args)
		{
			CookieAwareWebClient client = new CookieAwareWebClient();
			client.Login("http://www.wizards.com/dndinsider/compendium/login.aspx?page=monster&id=115848", new NameValueCollection()
				                                                                                       {
					                                                                                       {
						                                                                                       "email",
						                                                                                       "michiel.fortuin@gmail.com"
					                                                                                       },
					                                                                                       {"password", "rover7gom"},
																										   {"InsiderSignin","Sign In"}
				                                                                                       });
			//Console.WriteLine(Encoding.UTF8.GetString(result));
			//client.DownloadFile("http://www.wizards.com/dndinsider/compendium/login.aspx?page=monster&id=115848", "output/115848.html");


			//string filename = @"D:\S120397\Documents\DnDroid\DnD4E\MonsterList";
			//string monsterIndex = File.ReadAllText(filename);

			//AntlrInputStream input = new AntlrInputStream(monsterIndex + "\r\n");
			//KeywordsSearchLexer lexer = new KeywordsSearchLexer(input);
			//CommonTokenStream tokens = new CommonTokenStream(lexer);
			//KeywordsSearchParser parser = new KeywordsSearchParser(tokens);
			//RuleContext tree = parser.search();

			//KeywordSearchVisitor visitor = new KeywordSearchVisitor();
			//visitor.Visit(tree);

			//WebClient client = new WebClient();
			//client.UploadValues("http://www.wizards.com/dndinsider/compendium/login.aspx", "POST", new NameValueCollection()
			//																						   {
			//																							   {
			//																								   "email",
			//																								   "michiel.fortuin@gmail.com"
			//																							   },
			//																							   {"password", "rover7gom"},
			//																							   {"InsiderSignin","Sign In"}
			//																						   });
			//Directory.CreateDirectory("output");
			//for (int i = 0; i < 1; i++)
			//{
			//	client.DownloadFile(visitor.urls[i].url, "output/" + visitor.urls[i].id+".html");
			//}

			Console.ReadKey();
		}
	}

	class KeywordSearchVisitor: KeywordsSearchBaseVisitor<object>
	{
		public class Info
		{
			public int id;
			public string name;
			public string url;
		}
		public List<Info> urls = new List<Info>();
		 
		public override object VisitMonster(KeywordsSearchParser.MonsterContext context)
		{
			int id = (int) Visit(context.id());
			string url = string.Format("http://www.wizards.com/dndinsider/compendium/{0}.aspx?id={1}", "monster", id);
			string name = Visit(context.name()).ToString();
			Console.WriteLine("{0} ({1}, url={2})", name, Visit(context.level()), url);
			urls.Add(new Info()
				         {
					         name = name,
							 url = url,
							 id = id
				         });
			return 0;
		}

		public override object VisitId(KeywordsSearchParser.IdContext context)
		{
			return int.Parse(context.NUMBER().GetText());
		}

		public override object VisitName(KeywordsSearchParser.NameContext context)
		{
			return Visit(context.text());
		}

		public override object VisitLevel(KeywordsSearchParser.LevelContext context)
		{
			return int.Parse(context.NUMBER().GetText());
		}

		public override object VisitText(KeywordsSearchParser.TextContext context)
		{
			if(context.children.Count == 0)
				return "";

			StringBuilder sb = new StringBuilder();
			sb.Append(context.children[0].GetText());
			for (int i = 1; i < context.children.Count; i++)
			{
				sb.Append(" ");
				sb.Append(context.children[i].GetText());
			}
			return sb.ToString();
		}

		
	}
}
