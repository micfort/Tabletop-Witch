using System.Xml;
using System.Linq;

namespace micfort.dndroid.DnD4.MonsterReader
{
	public class ValuesList<T> : NormalList<T>
		where T: Value
	{
		public ValuesList(IItemFactory<T> factory, string tagName): base(factory, tagName)
		{
		}

		public float GetFinalValue(string name)
		{
			var first = this.Values.First(x => x.Name == name);
			if(first != null)
			{
				return first.FinalValue;
			}
			else
			{
				return 0;
			}
		}

		public override void ReadXml(XmlReader reader)
		{
			string StartTagName = reader.Name;

			CheckAttributes(reader);

			if(!reader.IsEmptyElement)
			{
				while (reader.Read() && reader.Name != StartTagName)
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						if (reader.Name == "Values")
						{
							base.ReadXml(reader);
						}
					}
				}
			}
		}
	}
}
