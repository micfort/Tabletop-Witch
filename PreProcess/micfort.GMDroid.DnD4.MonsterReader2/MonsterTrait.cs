using System.Xml;

namespace micfort.dndroid.DnD4.MonsterReader
{
	public class MonsterTrait:BasicXmlComplexType
	{
		public Value Range;
		public string Details;
		public string Name;
		public string Type;
		public bool IsBasic;
		public NormalList<ReferencedObject> Keywords;
		public int Tier;

		#region Overrides of BasicXmlComplexType

		protected override void CheckTag(XmlReader reader)
		{
			if (reader.Name == "Range")
				Range = Helper.Parse<Value>(reader);
			else if (reader.Name == "Details")
				Details = Helper.ReadString(reader);
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
	}
}
