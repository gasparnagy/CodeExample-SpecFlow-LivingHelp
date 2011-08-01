using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using TechTalk.SpecFlow.Parser.SyntaxElements;

namespace SpecOverflow.LivingHelp
{
    public class HelpDescription
    {
        [XmlElement("Page")]
        public List<Page> Pages { get; set; }

        public HelpDescription()
        {
            Pages = new List<Page>();
        }
    }

    public class Page
    {
        [XmlAttribute("path")]
        public string Path { get; set; }

        [XmlElement("Scope")]
        public List<Scope> Scopes { get; set; }

        public Page()
        {
            Scopes = new List<Scope>();
        }
    }

    public enum ScopeType
    {
        [XmlEnum("submit")]
        FormSubmit,
        [XmlEnum("navigate")]
        Navigate,
        [XmlEnum("click")]
        Click
    }

    public class Scope
    {
        [XmlAttribute("type")]
        public ScopeType Type { get; set; }

        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlElement("Entry")]
        public List<Entry> Entries { get; set; }

        public Scope()
        {
            Entries = new List<Entry>();
        }
    }

    public class Entry
    {
        [XmlAttribute("feature")]
        public string FeatureTitle { get; set; }
        [XmlAttribute("scenario")]
        public string ScenarioTitle { get; set; }

        public string Text { get; set; }
        public Scenario Scenario { get; set; }
    }

    public class HelpDescriptionParser
    {
        public HelpDescription Parse(string filePath)
        {
            XmlDocument document = new XmlDocument();
            document.Load(filePath);

            var serizlizer = new XmlSerializer(typeof (HelpDescription));
            var helpDescription = (HelpDescription)serizlizer.Deserialize(new XmlNodeReader(document));

            return helpDescription;
        }

        public void SaveTo(HelpDescription helpDescription, string filePath)
        {
            var serizlizer = new XmlSerializer(typeof(HelpDescription));
            using(var fileStream = new FileStream(filePath, FileMode.Create))
            {
                serizlizer.Serialize(fileStream, helpDescription);
            }
        }
    }
}
