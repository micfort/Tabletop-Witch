using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace micfort.dndroid.DnD4.MonsterReader
{
	public class Helper
	{
		public static T Parse<T>(XmlReader reader) where T: IXmlSerializable, new()
		{
			T result = new T();
			result.ReadXml(reader);
			return result;
		}

		public static ValuesList<T> ParseValuesList<T>(XmlReader reader, IItemFactory<T> factory, string tagName)
			where T:Value
		{
			ValuesList<T> result = new ValuesList<T>(factory, tagName);
			result.ReadXml(reader);
			return result;
		}

		public static NormalList<T> ParseNormalList<T>(XmlReader reader, IItemFactory<T> factory, string tagName)
		{
			NormalList<T> result = new NormalList<T>(factory, tagName);
			result.ReadXml(reader);
			return result;
		}

		public static void Skip(XmlReader reader)
		{
			if(reader.IsEmptyElement)
				return;
			string StartTagName = reader.Name;
			while (reader.Read() && !(reader.NodeType == XmlNodeType.EndElement && reader.Name == StartTagName))
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					Skip(reader);
				}
			}
		}

		public static string ReadString(XmlReader reader)
		{
			reader.Read();
			return reader.ReadContentAsString();
		}

		public static int ReadInt(XmlReader reader)
		{
			reader.Read();
			return reader.ReadContentAsInt();
		}

		public static float ReadFloat(XmlReader reader)
		{
			string text = ReadString(reader);
			float value;
			if(float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
			{
				return value;
			}
			else
			{
				text = text.Replace(',', '#').Replace('.', ',').Replace('#', '.');
				if (float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
				{
					return value;
				}
				else
				{
					return 0;
				}
			}
		}

		public static bool ReadBool(XmlReader reader)
		{
			reader.Read();
			return reader.ReadContentAsBoolean();
		}
	}
}
