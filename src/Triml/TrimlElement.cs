using System;
using System.Collections.Generic;
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
    [XmlInclude(typeof(Scope))]
	[XmlInclude(typeof(Group))]
	[XmlInclude(typeof(Shape))]
	[XmlInclude(typeof(Table))]
    public class TrimlElement
    {
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("condition")]
		public string Condition { get; set; }

		[XmlElement("triml", typeof(TrimlDocument))]		
		[XmlElement("model", typeof(Model))]
		[XmlElement("input", typeof(Input))]
		[XmlElement("option", typeof(Option))]
        [XmlElement("scope", typeof(Scope))]
		[XmlElement("group", typeof(Group))]
		[XmlElement("shape", typeof(Shape))]
		[XmlElement("table", typeof(Table))]
        public List<TrimlElement> ChildElements { get; set; } = new List<TrimlElement>();



        public virtual TrimlJsonNode ToJsonRecursive()
        {
            TrimlJsonNode node = new TrimlJsonNode
            {
                NodeType = GetType().Name.ToLower()
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
            foreach (TrimlElement element in ChildElements)
            {
                childNodes.Add(element.ToJsonRecursive());
            }

            if (this is Input input)
            {
                if (!string.IsNullOrEmpty(input.Options))
                {
                    string[] opts = input.Options.Split(';');
                    foreach (string o in opts)
                    {
                        childNodes.Add(new Option { Value = o }.ToJsonRecursive());
                    }
                    for (int i = props.Count - 1; i >= 0; i--)
                    {
                        var p = props[i];
                        if (p.Name == "options")
                        {
                            props.RemoveAt(i);
                        }
                    }

                }
            }

            node.Properties = props.ToArray();
            node.ChildNodes = childNodes.ToArray();

            return node;
        }

    }
}
