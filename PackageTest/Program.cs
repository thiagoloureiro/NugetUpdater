using NuGet;
using System;
using System.Collections.Generic;
using System.Net;
using NugetUpdater;

namespace PackageTest
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var obj = new Update();
			obj.UseNugetDefaultApi = true;
			obj.LatestVersion = true;
			obj.ReleaseVersion = true;
			obj.UpdatePackage("WebImpact.WIFramework.SMSReceiver");

			//ID of the package to be looked up

			//List<string> packages = new List<string>();
			//packages.Add("PlaynGO.Firefly.GDK");
			//packages.Add("PlaynGO.Firefly.GameHelpers");
			//packages.Add("PlaynGO.Modules.Jackpots.Mystery");
			//packages.Add("PlaynGO.Modules.Random.Isaac64");

			////var credentials = new NetworkCredential("", "");
			////CredentialStore.Instance.Add(new Uri(""), credentials);

			////Connect to the official package repository
			//IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");

			//var pkgList = repo.FindPackages(packages);

			//foreach (var p in pkgList)
			//{
			//	if (p.IsAbsoluteLatestVersion && p.IsReleaseVersion())
			//		Console.WriteLine(p.GetFullName() + " - " + p.Version);
			//}

			//Console.ReadLine();
		}
	}
}