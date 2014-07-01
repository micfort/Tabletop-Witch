using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace micfort.dndroid.DnD4.MonsterReader
{
	public abstract class BasicXmlComplexType: IXmlSerializable
	{
		protected abstract void CheckTag(XmlReader reader);

		protected virtual void CheckAttributes(XmlReader reader) { }

		#region Implementation of IXmlSerializable

		public XmlSchema GetSchema()
		{
			return null;
		}

		public virtual void ReadXml(XmlReader reader)
		{
			string StartTagName = reader.Name;

			CheckAttributes(reader);

			if(!reader.IsEmptyElement)
			{
				bool result = reader.Read();
				while (result && !(reader.NodeType == XmlNodeType.EndElement && reader.Name == StartTagName))
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						CheckTag(reader);
					}
					result = reader.Read();
				}
			}
		}

		public void WriteXml(XmlWriter writer)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
