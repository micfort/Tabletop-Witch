using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using micfort.GMDroid.PF.Feats;

namespace micfort.GMDroid.PF
{
	class Program
	{
		static void Main(string[] args)
		{
			//string filename = @"D:\S120397\Documents\DnDroid\Pathfinder\monster_stat_blocks_full - Updated 23Mar2014-Michiel.csv";
			//string filename = @"/home/michiel/TabletopWitch/Pathfinder/monster_stat_blocks_full - Updated 23Mar2014-Michiel.csv";
			string filename = @"/home/michiel/TabletopWitch/Pathfinder/Feats.csv";
			string input = File.ReadAllText(filename);

			IProcess feats = new ProcessFeats();
			feats.LoadInput(input);
			List<JObject> data = feats.GenerateJson();

			ParseFormatter formatter = new ParseFormatter();
			string json = formatter.GenerateParseJson(data, "L0hSHZDtsd", "RDsO1rPZ41").ToString(Formatting.Indented);

			File.WriteAllText("output.json", json);
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
			/*Console.Out.WriteLine("Writing information");

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
			}*/
		}


	}


}
