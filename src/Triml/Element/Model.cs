using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Triml.Element
{
    [XmlRoot("model")]
	public class Model : TrimlElement
	{
		public void AddSet(string name, params string[] values)
		{
			ChildElements.Add(new Input(name, values));
		}
	}
}
