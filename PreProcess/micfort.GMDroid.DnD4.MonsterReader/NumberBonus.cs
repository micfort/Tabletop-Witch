using System;
using System.Xml;

namespace micfort.dndroid.DnD4.MonsterReader
{
	public abstract class NumberBonus: BasicXmlComplexType
	{
		public String Name;
		public String ID;

		public override string ToString()
		{
			return string.Format(Name);
		}

		#region Overrides of BasicXmlComplexType

		protected override void CheckTag(XmlReader reader)
		{
			if (reader.Name == "ID")
			{
				reader.Read();
				this.ID = reader.ReadContentAsString();
			}
			else if (reader.Name == "Name")
			{
				reader.Read();
				this.Name = reader.ReadContentAsString();
			}
		}

		protected override void CheckAttributes(XmlReader reader)
		{
			
		}

		#endregion
	}
}
