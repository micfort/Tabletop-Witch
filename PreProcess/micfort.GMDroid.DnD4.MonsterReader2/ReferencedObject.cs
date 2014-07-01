using System.Xml;

namespace micfort.dndroid.DnD4.MonsterReader
{
	public class ReferencedObject: BasicXmlComplexType
	{
		public string Name;
		public string Description;
		public string ID;
		public string DefenseName;

		public override string ToString()
		{
			return string.Format(Name);
		}

		#region Overrides of BasicXmlComplexType

		protected override void CheckTag(XmlReader reader)
		{
			if(reader.Name == "id")
			{
				reader.Read();
				this.ID = reader.ReadContentAsString();
			}
			else if (reader.Name == "ReferencedObject")
			{
				bool eventType = reader.Read();
				while (eventType && reader.Name != "ReferencedObject")
				{
					if(reader.NodeType == XmlNodeType.Element)
					{
						if (reader.Name == "Name")
						{
							reader.Read();
							this.Name = reader.ReadContentAsString();
						}
						else if (reader.Name == "Description")
						{
							reader.Read();
							this.Description = reader.ReadContentAsString();
						}
						else if (reader.Name == "DefenseName")
						{
							reader.Read();
							this.DefenseName = reader.ReadContentAsString();
						}
					}
					eventType = reader.Read();
				}
			}
		}

		protected override void CheckAttributes(XmlReader reader)
		{
			
		}

		#endregion
	}
}
