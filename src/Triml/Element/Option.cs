using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Triml.Element
{
    [XmlRoot("option")]
	public class Option : TrimlElement
	{
		[XmlAttribute("value")]
		public string Value { get; set; }

		[XmlAttribute("label")]
		public string Label { get; set; }
	}
}
