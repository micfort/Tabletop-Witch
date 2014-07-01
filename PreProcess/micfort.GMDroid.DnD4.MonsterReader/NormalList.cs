using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace micfort.dndroid.DnD4.MonsterReader
{
	public class NormalList<T>: BasicXmlComplexType
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly IItemFactory<T> _factory;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly string _tagName;
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public List<T> Values { get; private set; }

		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public int Count { get { return Values.Count; } }

		public NormalList(IItemFactory<T> factory, string tagName)
		{
			_factory = factory;
			_tagName = tagName;
			Values = new List<T>();
		}

		#region Overrides of BasicXmlComplexType

		protected override void CheckTag(XmlReader reader)
		{
			if (reader.Name == _tagName)
			{
				Values.Add(_factory.Create(reader));
			}
		}

		protected override void CheckAttributes(XmlReader reader)
		{
		}

		#endregion
	}

	public interface IItemFactory<T>
	{
		T Create(XmlReader reader);
	}
	public class DefaultItemFactory<T> : IItemFactory<T> where T : IXmlSerializable, new()
	{
		public T Create(XmlReader reader)
		{
			return Helper.Parse<T>(reader);
		}
	}
}
