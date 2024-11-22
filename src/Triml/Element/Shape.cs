﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Triml.Element
{
	[XmlRoot("shape")]
	public class Shape : TrimlElement
	{
		[XmlAttribute("type")]
		public string Type { get; set; }

		[XmlAttribute("data")]
		public string Data { get; set; }
	}
}
