namespace micfort.dndroid.DnD4.MonsterReader
{
	public class SkillNumber: Value
	{
		public bool Trained;

		protected override void CheckTag(System.Xml.XmlReader reader)
		{
			if (reader.Name == "Trained")
			{
				reader.Read();
				this.Trained = reader.ReadContentAsBoolean();
			}
			else
			{
				base.CheckTag(reader);
			}
		}
	}
}
