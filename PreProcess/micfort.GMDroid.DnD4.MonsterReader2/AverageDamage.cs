namespace micfort.dndroid.DnD4.MonsterReader
{
	public class AverageDamage: Value
	{
		public string Type;
		public string Modifier;

		protected override void CheckTag(System.Xml.XmlReader reader)
		{
			if (reader.Name == "Type")
				Type = Helper.ReadString(reader);
			else if (reader.Name == "Modifier")
				Modifier = Helper.ReadString(reader);
			else
				base.CheckTag(reader);
		}
	}
}
