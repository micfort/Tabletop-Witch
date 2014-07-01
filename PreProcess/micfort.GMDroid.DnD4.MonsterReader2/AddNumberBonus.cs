namespace micfort.dndroid.DnD4.MonsterReader
{
	public class AddNumberBonus: NumberBonus
	{
		public int Value;

		protected override void CheckTag(System.Xml.XmlReader reader)
		{
			if (reader.Name == "Value")
			{
				reader.Read();
				this.Value = reader.ReadContentAsInt();
			}
			else
			{
				base.CheckTag(reader);
			}
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
