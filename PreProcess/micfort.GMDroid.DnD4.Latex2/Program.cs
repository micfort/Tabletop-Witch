using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using micfort.dndroid.DnD4.MonsterReader;

namespace micfort.dndroid.DnD4.Latex
{
	static class Program
	{
		static void Main()
		{
			string outputFile = @"D:\S120397\Dropbox\DnD School DM\Campaign notes\Latex\git\OrbisInsularum\MonsterDatabase.sty";
			string inputDir = @"D:\S120397\Documents\DnDroid\xml\";

			List<Monster> monsters = new List<Monster>();
			var files = Directory.EnumerateFiles(inputDir, "*.monster.xml");
			foreach (string file in files)
			{
				using (FileStream stream = File.OpenRead(file))
				{
					Monster monster = Monster.ReadMonster(stream);
					monsters.Add(monster);
				}
			}

			if (File.Exists(outputFile))
				File.Delete(outputFile);
			File.WriteAllText(outputFile, ConvertToLatex.MonsterDatabaseToLatex(monsters));
		}
	}
}
