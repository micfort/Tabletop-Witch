using System.Xml;

namespace micfort.dndroid.DnD4.MonsterReader
{
	public class ItemAndQuantity: BasicXmlComplexType
	{
		public int Quantity;
		public ReferencedObject Item;

		#region Overrides of BasicXmlComplexType

		protected override void CheckTag(XmlReader reader)
		{
			if (reader.Name == "Quantity")
			{
				this.Quantity = Helper.ReadInt(reader);
			}
			else if (reader.Name == "Item")
			{
				this.Item = Helper.Parse<ReferencedObject>(reader);
			}
		}

		protected override void CheckAttributes(XmlReader reader)
		{
		}

		#endregion
	}
}
