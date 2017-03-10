using System;
using System.IO;
using Foundation;

namespace TallySoftShared.iOS
{
	public class FileAccessHelper
	{
		public static string GetLocalFilePath (string filename)
		{
			string docFolder = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			string libFolder = Path.Combine (docFolder, "..", "Library", "Databases");
			Console.WriteLine (libFolder);

			if (!Directory.Exists (libFolder)) {
				Directory.CreateDirectory (libFolder);
			}

			string dbPath = Path.Combine (libFolder, filename);
			Console.WriteLine (dbPath);

			CopyDatabaseIfNotExists (dbPath);

			return dbPath;
		}

		private static void CopyDatabaseIfNotExists (string dbPath)
		{
			if (!File.Exists (dbPath)) {
				var existingDb = NSBundle.MainBundle.PathForResource ("FileDataAndStrings", "sqlite");
				File.Copy (existingDb, dbPath);
			}
		}
	}
}