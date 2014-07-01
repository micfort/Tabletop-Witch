using System.Xml;

namespace micfort.dndroid.DnD4.MonsterReader
{
	public class FailedSavingThrows: BasicXmlComplexType
	{
		public MonsterAttackEntry MonsterAttackEntry;
		#region Overrides of BasicXmlComplexType

		protected override void CheckTag(XmlReader reader)
		{
			if (reader.Name == "MonsterAttackEntry")
				MonsterAttackEntry = Helper.Parse<MonsterAttackEntry>(reader);
		}

		#endregion
	}
}
