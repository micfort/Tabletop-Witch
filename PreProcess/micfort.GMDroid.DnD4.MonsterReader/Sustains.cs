using System.Xml;

namespace micfort.dndroid.DnD4.MonsterReader
{
	public class Sustains: BasicXmlComplexType
	{
		public MonsterAttackEntry MonsterSustainEffect;

		#region Overrides of BasicXmlComplexType

		protected override void CheckTag(XmlReader reader)
		{
			if (reader.Name == "MonsterSustainEffect")
				MonsterSustainEffect = Helper.Parse<MonsterAttackEntry>(reader);
		}

		#endregion
	}
}
