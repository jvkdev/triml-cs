using System;
using System.Collections.Generic;
using System.Text;
using Triml.Element;

namespace Triml.Json
{
    public class TrimlJsonNode
	{
		public string NodeType { get; set; }

		public TrimlJsonProperty[] Properties { get; set; }

		public TrimlJsonNode[] Nodes { get; set; }


		public TrimlNode ToTrimlRecursive()
		{
			TrimlNode node;
			switch ((NodeType ?? "").ToLower())
			{				
				case "model": node = new Model(); break;
				case "input": node = new Input(); break;
				case "option": node = new Option(); break;
				case "graphic": node = new Graphic(); break;
				case "data": node = new Data(); break;
				default: node = new TrimlNode(); break;
			}

			Dictionary<string, string> propLookup = new Dictionary<string, string>();
			foreach (var p in Properties)
			{
				string key = (p.Name ?? "").ToLower();
				if (!propLookup.ContainsKey(key))
				{
					propLookup.Add(key, p.Value);
				}
			}

			foreach (var prop in node.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
			{
				if (prop.PropertyType == typeof(string))
				{
					string key = prop.Name.ToLower();
					string val = null;

					if (propLookup.ContainsKey(key)) { val = propLookup[key]; }
					if (val != null) { prop.SetValue(node, val); }
				}
			}

			foreach (var n in Nodes)
			{
				var trimlChild = n.ToTrimlRecursive();
				if (trimlChild != null)
				{
					node.Nodes.Add(trimlChild);
				}
			}

			return node;
		}
	}


	public class TrimlJsonProperty
	{
		public string Name { get; set; }

		public string Value { get; set; }


		public TrimlJsonProperty()
		{

		}

		public TrimlJsonProperty(string name, string value)
		{
			Name = name;
			Value = value;
		}
	}
}
