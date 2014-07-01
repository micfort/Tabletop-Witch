using System.Xml;

namespace micfort.dndroid.DnD4.MonsterReader
{
	public class Value: BasicXmlComplexType
	{
		public string Name;
		public string ID;
		public float FinalValue;
		public NumberBonus DefaultBonus;

		public override string ToString()
		{
			return string.Format("{0}: {1}", Name, FinalValue.ToString());
		}

		#region Overrides of BasicXmlComplexType

		protected override void CheckTag(XmlReader reader)
		{
			if (reader.Name == "ID")
			{
				reader.Read();
				this.ID = reader.ReadContentAsString();
			}
			if (reader.Name == "Name")
			{
				reader.Read();
				this.Name = reader.ReadContentAsString();
			}
			else if (reader.Name == "DefaultBonus")
			{
				string numberBonus = reader.GetAttribute("type", Constants.XmlSchemaNamespace);
				if(numberBonus == "AddNumberBonus")
				{
					DefaultBonus = Helper.Parse<AddNumberBonus>(reader);
				}
				else
				{
					DefaultBonus = Helper.Parse<PercentageNumberBonus>(reader);
				}
			}
		}

		protected override void CheckAttributes(XmlReader reader)
		{
			if(reader.HasAttributes)
			{
				string finalValue = reader.GetAttribute("FinalValue");
				if(finalValue != null)
				{
					float.TryParse(finalValue, out this.FinalValue);
				}
			}
		}

		#endregion
	}
}
