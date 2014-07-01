using System.Xml;

namespace micfort.dndroid.DnD4.MonsterReader
{
	public class MonsterPower:BasicXmlComplexType
	{
		public string Action;
		public string Trigger;
		public string Usage;
		public string UsageDetails;
		public string FlavorText;
		public NormalList<MonsterAttack> Attacks;
		public string Name;
		public string Type;
		public bool IsBasic;
		public NormalList<ReferencedObject> Keywords;
		public int Tier;
		

		#region Overrides of BasicXmlComplexType

		protected override void CheckTag(XmlReader reader)
		{
			if (reader.Name == "Action")
				Action = Helper.ReadString(reader);
			else if (reader.Name == "Trigger")
				Trigger = Helper.ReadString(reader);
			else if (reader.Name == "Usage")
				Usage = Helper.ReadString(reader);
			else if (reader.Name == "UsageDetails")
				UsageDetails = Helper.ReadString(reader);
			else if (reader.Name == "FlavorText")
				FlavorText = Helper.ReadString(reader);
			else if (reader.Name == "Attacks")
				Attacks = Helper.ParseNormalList(reader, new DefaultItemFactory<MonsterAttack>(), "MonsterAttack");
			else if (reader.Name == "Name")
				Name = Helper.ReadString(reader);
			else if (reader.Name == "Type")
				Type = Helper.ReadString(reader);
			else if (reader.Name == "IsBasic")
				IsBasic = Helper.ReadBool(reader);
			else if (reader.Name == "Keywords")
				Keywords = Helper.ParseNormalList(reader, new DefaultItemFactory<ReferencedObject>(), "ObjectReference");
			else if (reader.Name == "Tier")
				Tier = Helper.ReadInt(reader);
		}

		protected override void CheckAttributes(XmlReader reader)
		{
		}

		#endregion

		public override string ToString()
		{
			return Name;
		}
	}
}
