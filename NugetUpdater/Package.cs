using System;

namespace NugetUpdater
{
	[Serializable()]
	[System.Xml.Serialization.XmlTypeAttribute()]
	public class Package
	{
		[System.Xml.Serialization.XmlAttribute("id")]
		public string Id { get; set; }

		[System.Xml.Serialization.XmlAttribute("version")]
		public string Version { get; set; }
	}
}