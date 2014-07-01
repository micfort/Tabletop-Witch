using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using micfort.dndroid.DnD4.MonsterReader;

namespace micfort.dndroid.DnD4.Latex
{
	class ConvertToLatex
	{
		public static string PowerToLatex(MonsterPower power)
		{
			string monsterLatex = Properties.Resources.PowerTemplate;

			monsterLatex = monsterLatex.Replace("<Name>", power.Name);
			monsterLatex = monsterLatex.Replace("<Action>", power.Action);
			monsterLatex = monsterLatex.Replace("<Usage>", power.Usage);

			return monsterLatex;
		}

		public static string PowerListToLatex(List<MonsterPower> powers)
		{
			List<string> powersLatex = powers.ConvertAll(PowerToLatex);
			string output = powersLatex.Aggregate("", (s, s1) => s + "\r\n" + s1);

			return output;
		}

		public static string MonsterToLatex(Monster monster)
		{
			string monsterLatex = Properties.Resources.MonsterTemplate;

			monsterLatex = monsterLatex.Replace("<Name>", monster.Name);
			monsterLatex = monsterLatex.Replace("<XP>", monster.Experience.FinalValue.ToString());
			monsterLatex = monsterLatex.Replace("<Level>", monster.Level.ToString());
			monsterLatex = monsterLatex.Replace("<Type>", monster.MonsterType.Name);
			monsterLatex = monsterLatex.Replace("<Role>", monster.Role.Name);
			monsterLatex = monsterLatex.Replace("<GroupRole>", monster.GroupRole.Name);
			monsterLatex = monsterLatex.Replace("<Size>", monster.Size.Name);
			monsterLatex = monsterLatex.Replace("<Origin>", monster.Origin.Name);
			monsterLatex = monsterLatex.Replace("<Initiative>", monster.Initiative.FinalValue.ToString());
			monsterLatex = monsterLatex.Replace("<Perception>", monster.Skills.GetFinalValue("Perception").ToString());
			monsterLatex = monsterLatex.Replace("<HP>", monster.HitPoints.FinalValue.ToString());
			monsterLatex = monsterLatex.Replace("<AC>", monster.Defenses.GetFinalValue("AC").ToString());
			monsterLatex = monsterLatex.Replace("<Fort>", monster.Defenses.GetFinalValue("Fortitude").ToString());
			monsterLatex = monsterLatex.Replace("<Refl>", monster.Defenses.GetFinalValue("Reflex").ToString());
			monsterLatex = monsterLatex.Replace("<Will>", monster.Defenses.GetFinalValue("Will").ToString());
			monsterLatex = monsterLatex.Replace("<Speed>", monster.LandSpeed.Speed.FinalValue.ToString());
			monsterLatex = monsterLatex.Replace("<Keywords>", monster.Keywords.Values.Aggregate("", (s, o) => s + " " + o.Name));
			monsterLatex = monsterLatex.Replace("<Alignment>", monster.Alignment.Name);

			monsterLatex = monsterLatex.Replace("<Powers>", PowerListToLatex(monster.Powers.Power));

			monsterLatex = monsterLatex.Replace("<Str>", monster.AbilityScores.GetFinalValue("Strength").ToString());
			monsterLatex = monsterLatex.Replace("<Con>", monster.AbilityScores.GetFinalValue("Constitution").ToString());
			monsterLatex = monsterLatex.Replace("<Dex>", monster.AbilityScores.GetFinalValue("Dexterity").ToString());
			monsterLatex = monsterLatex.Replace("<Int>", monster.AbilityScores.GetFinalValue("Intelligence").ToString());
			monsterLatex = monsterLatex.Replace("<Wis>", monster.AbilityScores.GetFinalValue("Wisdom").ToString());
			monsterLatex = monsterLatex.Replace("<Cha>", monster.AbilityScores.GetFinalValue("Charisma").ToString());

			return monsterLatex;
		}

		public static string MonsterDatabaseToLatex(List<Monster> monster)
		{
			List<string> monsterLatex = monster.ConvertAll(MonsterToLatex);
			string monsters = monsterLatex.Aggregate("", (s, s1) => s + "\r\n" + s1);
			
			string databaseLatex = Properties.Resources.MonsterDatabaseTemplate;
			databaseLatex = databaseLatex.Replace("<MonsterTemplate>", monsters);
			return databaseLatex;
		}
	}
}
