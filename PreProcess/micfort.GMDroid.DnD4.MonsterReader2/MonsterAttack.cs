using System.Xml;

namespace micfort.dndroid.DnD4.MonsterReader
{
	public class MonsterAttack: BasicXmlComplexType
	{
		public string Range;
		public MonsterAttackEntry Hit;
		public MonsterAttackEntry Miss;
		public MonsterAttackEntry Effect;
		public NormalList<MonsterPowerAttackNumber> AttackBonuses;

		#region Overrides of BasicXmlComplexType

		protected override void CheckTag(XmlReader reader)
		{
			if (reader.Name == "Range")
				Range = Helper.ReadString(reader);
			else if (reader.Name == "Hit")
				Hit = Helper.Parse<MonsterAttackEntry>(reader);
			else if (reader.Name == "Miss")
				Miss = Helper.Parse<MonsterAttackEntry>(reader);
			else if (reader.Name == "Effect")
				Effect = Helper.Parse<MonsterAttackEntry>(reader);
			else if (reader.Name == "AttackBonuses")
				AttackBonuses = Helper.ParseNormalList(reader, new DefaultItemFactory<MonsterPowerAttackNumber>(), "MonsterPowerAttackNumber");
		}

		protected override void CheckAttributes(XmlReader reader)
		{
		}

		#endregion
	}
}
