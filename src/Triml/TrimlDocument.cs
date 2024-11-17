using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Triml
{
    [XmlRoot("triml")]
    public class TrimlDocument : TrimlElement
    {
        public const string XML_NAMESPACE = "https://triml.org/schema/1.0";

    }
}
