namespace micfort.dndroid.DnD4.MonsterReader
{
	class PercentageNumberBonus: NumberBonus
	{
		public float Value;

		protected override void CheckTag(System.Xml.XmlReader reader)
		{
			if (reader.Name == "Value")
			{
				Value = Helper.ReadFloat(reader);
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
