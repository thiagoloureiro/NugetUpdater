using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NugetUpdater
{
	public interface IUpdate
	{
		string Url { get; set; }
		NetworkCredential Credentials { get; set; }
		bool ReleaseVersion { get; set; }
		bool LatestVersion { get; set; }
		bool UseNugetDefaultApi { get; set; }

		void UpdatePackageList(List<string> list);

		void UpdatePackage(string package);
	}
}