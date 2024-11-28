using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Triml.Element
{
	[XmlRoot("graphic")]
	public class Graphic : TrimlNode
	{
		[XmlAttribute("style")]
		public string Style { get; set; }
	}
}
