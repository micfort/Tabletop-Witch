namespace micfort.dndroid.DnD4.MonsterReader
{
	public class CreatureSpeed: ReferencedObject
	{
		public Value Speed;
		public string Details;

		protected override void CheckTag(System.Xml.XmlReader reader)
		{
			if (reader.Name == "Speed")
			{
				Speed = Helper.Parse<Value>(reader);
			}
			else if (reader.Name == "Details")
			{
				Details = Helper.ReadString(reader);
			}
			else
			{
				base.CheckTag(reader);
			}
		}
	}
}
