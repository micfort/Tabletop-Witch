using System.Xml;

namespace micfort.dndroid.DnD4.MonsterReader
{
	public class MonsterAttackEntry: BasicXmlComplexType
	{
		public string Name;
		public Aftereffects Aftereffects;
		public Sustains Sustains;
		public Damage Damage;
		public string Description;
		public NormalList<MonsterAttack> Attacks;
		public FailedSavingThrows FailedSavingThrows;
		#region Overrides of BasicXmlComplexType

		protected override void CheckTag(XmlReader reader)
		{
			if (reader.Name == "Name")
				Name = Helper.ReadString(reader);
			else if (reader.Name == "Aftereffects")
				Aftereffects = Helper.Parse<Aftereffects>(reader);
			else if (reader.Name == "Sustains")
				Sustains = Helper.Parse<Sustains>(reader);
			else if (reader.Name == "Damage")
				Damage = Helper.Parse<Damage>(reader);
			else if (reader.Name == "Description")
				Description = Helper.ReadString(reader);
			else if (reader.Name == "Attacks")
				Attacks = Helper.ParseNormalList(reader, new DefaultItemFactory<MonsterAttack>(), "MonsterAttack");
			else if (reader.Name == "FailedSavingThrows")
				FailedSavingThrows = Helper.Parse<FailedSavingThrows>(reader);
		}

		protected override void CheckAttributes(XmlReader reader)
		{
		}

		#endregion
	}
}
