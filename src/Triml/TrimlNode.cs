using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using Triml.Element;
using Triml.Json;

namespace Triml
{
    [XmlInclude(typeof(TrimlDocument))]
	[XmlInclude(typeof(Model))]
	[XmlInclude(typeof(Input))]
    [XmlInclude(typeof(Option))]
	[XmlInclude(typeof(Graphic))]
	[XmlInclude(typeof(Data))]
	[XmlRoot("node")]
	public class TrimlNode
    {
		[XmlAttribute("type")]
		public string Type { get; set; }

		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("condition")]
		public string Condition { get; set; }

		[XmlAttribute("source")]
		public string Source { get; set; }

		[XmlAttribute("format")]
		public string Format { get; set; }

		[XmlText]
		public string Value { get; set; }

		[XmlElement("triml", typeof(TrimlDocument))]
        [XmlElement("model", typeof(Model))]
		[XmlElement("input", typeof(Input))]
		[XmlElement("option", typeof(Option))]
		[XmlElement("graphic", typeof(Graphic))]
		[XmlElement("data", typeof(Data))]
		[XmlElement("node", typeof(TrimlNode))]
		public List<TrimlNode> Nodes { get; set; } = new List<TrimlNode>();

        public virtual TrimlJsonNode ToJsonRecursive()
        {
            TrimlJsonNode node = new TrimlJsonNode
            {
                NodeType = GetType().GetCustomAttribute<XmlRootAttribute>()?.ElementName ?? GetType().Name.ToLower()
            };

            List<TrimlJsonProperty> props = new List<TrimlJsonProperty>();
            foreach (var prop in GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
            {
                if (prop.PropertyType == typeof(string))
                {
                    string val = prop.GetValue(this) as string;
                    if (val != null)
                    {
                        props.Add(new TrimlJsonProperty(prop.Name.ToLower(), val));
                    }
                }
            }

            List<TrimlJsonNode> childNodes = new List<TrimlJsonNode>();
            foreach (TrimlNode element in Nodes)
            {
                childNodes.Add(element.ToJsonRecursive());
            }

            node.Properties = props.ToArray();
            node.Nodes = childNodes.ToArray();

            return node;
        }

    }
}
