using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Triml.Element
{
    [XmlRoot("input")]
	public class Input : TrimlElement
	{
		[XmlAttribute("label")]
		public string Label { get; set; }

		[XmlAttribute("options")]
		public string Options { get; set; }


		public Input() { }


		public Input(string name, params string[] optionValues)
		{
			Name = name;

			foreach (var v in optionValues)
			{
				ChildElements.Add(new Option { Value = v });
			}
		}
	}
}
