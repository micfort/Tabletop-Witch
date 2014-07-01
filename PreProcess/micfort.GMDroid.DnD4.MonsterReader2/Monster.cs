using System;
using System.IO;
using System.Xml;

namespace micfort.dndroid.DnD4.MonsterReader
{
	public class Monster : BasicXmlComplexType
	{
		public ValuesList<Value> AbilityScores;
		public ValuesList<Value> Defenses;
		public ValuesList<Value> AttackBonuses;
		public ValuesList<SkillNumber> Skills;
		public ReferencedObject Size;
		public ReferencedObject Origin;
		public ReferencedObject MonsterType;
		public bool IsLeader;
		public ReferencedObject GroupRole;
		public NormalList<ItemAndQuantity> Items;
		public NormalList<ReferencedObject> Languages;
		public ReferencedObject Alignment;
		public NormalList<SenseReference> Senses;
		public Value Regeneration;
		public String Description;
		public NormalList<ReferencedObject> Keywords;
		public Powers Powers;
		public Value Initiative;
		public Value HitPoints;
		public Value ActionPoints;
		public CreatureSpeed LandSpeed;
		public bool Phasing;
		public NormalList<CreatureSpeed> Speeds;
		public NormalList<MonsterSavingThrow> SavingThrows;
		public NormalList<CreatureSusceptibility> Weaknesses;
		public NormalList<ReferencedObject> Immunities;
		public NormalList<CreatureSusceptibility> Resistances;
		public string EliteUpgradeID;
		public int Level;
		public Value Experience;
		public string CompendiumUrl;
		public ReferencedObject Role;
		public string Name;

		#region Overrides of BasicXmlComplexType

		protected override void CheckTag(XmlReader reader)
		{
			if (reader.Name == "AbilityScores")
				AbilityScores = Helper.ParseValuesList(reader, new DefaultItemFactory<Value>(), "AbilityScoreNumber");
			else if (reader.Name == "Defenses")
				Defenses = Helper.ParseValuesList(reader, new DefaultItemFactory<Value>(), "SimpleAdjustableNumber");
			else if (reader.Name == "AttackBonuses")
				AttackBonuses = Helper.ParseValuesList(reader, new DefaultItemFactory<Value>(), "CalculatedNumber");
			else if (reader.Name == "Skills")
				Skills = Helper.ParseValuesList(reader, new DefaultItemFactory<SkillNumber>(), "SkillNumber");
			else if (reader.Name == "Size")
				Size = Helper.Parse<ReferencedObject>(reader);
			else if (reader.Name == "Origin")
				Origin = Helper.Parse<ReferencedObject>(reader);
			else if (reader.Name == "Type")
				MonsterType = Helper.Parse<ReferencedObject>(reader);
			else if (reader.Name == "IsLeader")
				IsLeader = Helper.ReadBool(reader);
			else if (reader.Name == "GroupRole")
				GroupRole = Helper.Parse<ReferencedObject>(reader);
			else if (reader.Name == "Items")
				Items = Helper.ParseNormalList(reader, new DefaultItemFactory<ItemAndQuantity>(), "ItemAndQuantity");
			else if (reader.Name == "Languages")
				Languages = Helper.ParseNormalList(reader, new DefaultItemFactory<ReferencedObject>(), "ObjectReference");
			else if (reader.Name == "Alignment")
				Alignment = Helper.Parse<ReferencedObject>(reader);
			else if (reader.Name == "Senses")
				Senses = Helper.ParseNormalList(reader, new DefaultItemFactory<SenseReference>(), "SenseReference");
			else if (reader.Name == "Regeneration")
				Regeneration = Helper.Parse<Value>(reader);
			else if (reader.Name == "Description")
				Description = Helper.ReadString(reader);
			else if (reader.Name == "Keywords")
				Keywords = Helper.ParseNormalList(reader, new DefaultItemFactory<ReferencedObject>(), "ObjectReference");
			else if (reader.Name == "Race")
				Helper.Skip(reader);
			else if (reader.Name == "Powers")
				Powers = Helper.Parse<Powers>(reader);
			else if (reader.Name == "Initiative")
				Initiative = Helper.Parse<Value>(reader);
			else if (reader.Name == "HitPoints")
				HitPoints = Helper.Parse<Value>(reader);
			else if (reader.Name == "ActionPoints")
				ActionPoints = Helper.Parse<Value>(reader);
			else if (reader.Name == "LandSpeed")
				LandSpeed = Helper.Parse<CreatureSpeed>(reader);
			else if (reader.Name == "Phasing")
				Phasing = Helper.ReadBool(reader);
			else if (reader.Name == "Speeds")
				Speeds = Helper.ParseNormalList(reader, new DefaultItemFactory<CreatureSpeed>(), "CreatureSpeed");
			else if (reader.Name == "SavingThrows")
				SavingThrows = Helper.ParseNormalList(reader, new DefaultItemFactory<MonsterSavingThrow>(), "MonsterSavingThrow");
			else if (reader.Name == "Weaknesses")
				Weaknesses = Helper.ParseNormalList(reader, new DefaultItemFactory<CreatureSusceptibility>(),
				                                    "CreatureSusceptibility");
			else if (reader.Name == "Immunities")
				Immunities = Helper.ParseNormalList(reader, new DefaultItemFactory<ReferencedObject>(), "ObjectReference");
			else if (reader.Name == "Resistances")
				Weaknesses = Helper.ParseNormalList(reader, new DefaultItemFactory<CreatureSusceptibility>(),
													"CreatureSusceptibility");
			else if (reader.Name == "TemplateApplications")
				Helper.Skip(reader);
			else if (reader.Name == "EliteUpgradeID")
				EliteUpgradeID = Helper.ReadString(reader);
			else if (reader.Name == "FullPortrait")
				Helper.Skip(reader);
			else if (reader.Name == "SourceBook")
				Helper.Skip(reader);
			else if (reader.Name == "SourceBooks")
				Helper.Skip(reader);
			else if (reader.Name == "Level")
				Level = Helper.ReadInt(reader);
			else if (reader.Name == "Experience")
				Experience = Helper.Parse<Value>(reader);
			else if (reader.Name == "CompendiumUrl")
				CompendiumUrl = Helper.ReadString(reader);
			else if (reader.Name == "Role")
				Role = Helper.Parse<ReferencedObject>(reader);
			else if (reader.Name == "ID")
				Helper.Skip(reader);
			else if (reader.Name == "Name")
				Name = Helper.ReadString(reader);
		}

		protected override void CheckAttributes(XmlReader reader)
		{
			
		}

		#endregion


		public static Monster ReadMonster(Stream stream)
		{
			XmlReaderSettings settings = new XmlReaderSettings { NameTable = new NameTable() };
			XmlNamespaceManager xmlns = new XmlNamespaceManager(settings.NameTable);
			xmlns.AddNamespace(Constants.XmlSchemaPrefix, Constants.XmlSchemaNamespace);
			XmlParserContext context = new XmlParserContext(null, xmlns, "", XmlSpace.Default);
			XmlReader reader = XmlReader.Create(stream, settings, context);
			while (reader.NodeType != XmlNodeType.Element)
			{
				reader.Read();
			}

			return Helper.Parse<Monster>(reader);
		}

		
	}
}
