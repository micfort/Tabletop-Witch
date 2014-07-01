namespace micfort.dndroid.DnD4.MonsterReader
{
	public class SenseReference: ReferencedObject
	{
		public int Range;

		protected override void CheckTag(System.Xml.XmlReader reader)
		{
			if (reader.Name == "Range")
			{
				reader.Read();
				this.Range = reader.ReadContentAsInt();
			}
			else
			{
				base.CheckTag(reader);
			}
		}
	}
}
