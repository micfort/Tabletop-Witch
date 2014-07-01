using System.Xml;

namespace micfort.dndroid.DnD4.MonsterReader
{
	public class Damage : BasicXmlComplexType
	{
		public AverageDamage AverageDamage;
		public int DiceQuantity;
		public int DamageConstant;
		public int DiceSides;
		public string Expression;

		#region Overrides of BasicXmlComplexType

		protected override void CheckTag(XmlReader reader)
		{
			if (reader.Name == "AverageDamage")
				AverageDamage = Helper.Parse<AverageDamage>(reader);
			else if (reader.Name == "DiceQuantity")
				DiceQuantity = Helper.ReadInt(reader);
			else if (reader.Name == "DamageConstant")
			{
				Value v = Helper.Parse<Value>(reader);
				DamageConstant = (v.DefaultBonus as AddNumberBonus).Value;
			}
			else if (reader.Name == "DiceSides")
				DiceSides = Helper.ReadInt(reader);
			else if (reader.Name == "Expression")
				Expression = Helper.ReadString(reader);
		}

		#endregion
	}
}
