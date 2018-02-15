using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using NuGet;

namespace NugetUpdater
{
	public class Update : IUpdate
	{
		public string Url { get; set; }
		public NetworkCredential Credentials { get; set; }
		public bool ReleaseVersion { get; set; }
		public bool LatestVersion { get; set; }
		public bool UseNugetDefaultApi { get; set; }

		public void UpdatePackageList(List<string> list)
		{
			throw new System.NotImplementedException();
		}

		public void UpdatePackage(string package)
		{
			var packageDetail = GetPackageDetail(package);
			var packageContents = ReadPackageFile(packageDetail);

			UpdateFile(packageDetail);
		}

		private IPackage GetPackageDetail(string package)
		{
			if (UseNugetDefaultApi)
				Url = "https://packages.nuget.org/api/v2";

			IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");

			return repo.FindPackage(package);
		}

		private IEnumerable<PackageReference> ReadPackageFile(IPackage package)
		{
			string fileName = @"..\..\packages.config";
			var fileExist = File.Exists(fileName);

			if (!fileExist) return null;
			var references = new PackageReferenceFile(fileName);
			return references.GetPackageReferences();

			//foreach (PackageReference packageReference in file.GetPackageReferences())
			//{
			//	Console.WriteLine("Id={0}, Version={1}", packageReference.Id, packageReference.Version);
			//}
		}

		private void UpdateFile(IPackage packageDetail)
		{
			string fileName = @"..\..\packages.config";

			var serializer = new System.Xml.Serialization.XmlSerializer(typeof(PackageCollection));
			StreamReader reader = new StreamReader(fileName);
			var packages2 = (PackageCollection)serializer.Deserialize(reader);

			foreach (var package in packages2.Packages)
			{
				if (package.Id == packageDetail.Id)
				{
					package.Version = packageDetail.Version.ToString();
				}
			}

			SaveChanges(packages2);
		}

		private void SaveChanges(object package)
		{
			string fileName = @"..\..\packages.config";

			using (StringWriter stringwriter = new System.IO.StringWriter())
			{
				var serializer = new XmlSerializer(package.GetType());
				serializer.Serialize(stringwriter, package);
				var ret = stringwriter.ToString();

				using (var sw = new StreamWriter(fileName))
				{
					sw.Write(ret);
					sw.Flush();
				}
			}
		}

		[Serializable()]
		[System.Xml.Serialization.XmlRoot("packages")]
		public class PackageCollection
		{
			[System.Xml.Serialization.XmlElement("package", typeof(Package))]
			public Package[] Packages { get; set; }
		}

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
}