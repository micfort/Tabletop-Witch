using System.Collections.Generic;
using System.Xml;

namespace micfort.dndroid.DnD4.MonsterReader
{
	public class Powers: BasicXmlComplexType
	{
		public List<MonsterPower> Power = new List<MonsterPower>();
		public List<MonsterTrait> Trait = new List<MonsterTrait>(); 

		#region Overrides of BasicXmlComplexType

		protected override void CheckTag(XmlReader reader)
		{
			if (reader.Name == "MonsterPower")
			{
				Power.Add(Helper.Parse<MonsterPower>(reader));
			}
			else if (reader.Name == "MonsterTrait")
			{
				Trait.Add(Helper.Parse<MonsterTrait>(reader));
			}
		}

		protected override void CheckAttributes(XmlReader reader) { }

		#endregion
	}
}
