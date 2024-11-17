using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Triml.Element
{
    [XmlRoot("model")]
	public class Model : TrimlElement
	{
		[XmlAttribute("label")]
		public string Label { get; set; }

		[XmlAttribute("source")]
		public string Source { get; set; }

		public void AddSet(string name, params string[] values)
		{
			ChildElements.Add(new Input(name, values));
		}
	}
}
