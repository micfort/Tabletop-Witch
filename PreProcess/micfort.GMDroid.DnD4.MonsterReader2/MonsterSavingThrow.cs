namespace micfort.dndroid.DnD4.MonsterReader
{
	public class MonsterSavingThrow:Value
	{
		public string Details;
		protected override void CheckTag(System.Xml.XmlReader reader)
		{
			if (reader.Name == "Details")
			{
				this.Details = Helper.ReadString(reader);
			}
			else
			{
				base.CheckTag(reader);
			}
		}
	}
}
