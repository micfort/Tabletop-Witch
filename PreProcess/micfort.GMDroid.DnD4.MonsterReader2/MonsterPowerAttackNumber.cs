namespace micfort.dndroid.DnD4.MonsterReader
{
	public class MonsterPowerAttackNumber: Value
	{
		public ReferencedObject Defense;

		protected override void CheckTag(System.Xml.XmlReader reader)
		{
			if (reader.Name == "Defense")
			{
				this.Defense = Helper.Parse<ReferencedObject>(reader);
			}
			else
			{
				base.CheckTag(reader);
			}
		}
	}
}
