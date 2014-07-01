namespace micfort.dndroid.DnD4.MonsterReader
{
	public class CreatureSusceptibility: ReferencedObject
	{
		public Value Amount;

		protected override void CheckTag(System.Xml.XmlReader reader)
		{
			if (reader.Name == "Amount")
			{
				Amount = Helper.Parse<Value>(reader);
			}
			else
			{
				base.CheckTag(reader);
			}
		}
	}
}
