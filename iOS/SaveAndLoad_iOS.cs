using System;
using Xamarin.Forms;
using TallySoftShared.iOS;
using System.IO;
using System.Threading.Tasks;
using TallySoftShared;
using Foundation;
using System.Linq;
using System.Collections.Generic;
using TallySoftShared.Model;

[assembly: Dependency (typeof(SaveAndLoad_iOS))]

namespace TallySoftShared.iOS
{
	public class SchemaWriter 
	{
		private StreamWriter sw;
		private string path;

		/// <summary>
		/// Creates an object to handle writing Schema information
		/// </summary>
		/// <param name="Path">Path to place Schema file in</param>
		public SchemaWriter(string Path)
		{
			path = Path;
			sw = new StreamWriter(File.Open(Path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None));		
		}

		/// <summary>
		/// Writes Schema information about the supplied file name
		/// </summary>
		public void Write()
		{
			try
			{
				List<ScanDataTable> scanRecordsList = App.DatabaseRepo.GetAllScanDataRecords(App.fileName);
				for (int i = 0; i < scanRecordsList.Count; i++)
				{
					sw.WriteLine(string.Format("{0},{1},{2},{3}", scanRecordsList[i].quantity, scanRecordsList[i].sku, scanRecordsList[i].firstTimeScan, scanRecordsList[i].lastTimeScan));
				}
			}

			finally
			{
				sw.Flush();
				sw.Close();
			}
		}

		/// <summary>
		/// Closes StreamWriter
		/// </summary>
		~SchemaWriter()
		{
			/*if (sw.BaseStream != null)
			{
				sw.Close();
			}*/
		}
	}

	public class SaveAndLoad_iOS : ISaveAndLoad
	{
		public static string DocumentsPath {
			get {
				var documentsDirUrl = NSFileManager.DefaultManager.GetUrls (NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User).Last ();
				return documentsDirUrl.Path;
			}
		}

		#region ISaveAndLoad implementation

		public bool FileExists(string filename)
		{
			return File.Exists(CreatePathToFile(filename));
		}

		public async Task SaveFileToFolderAsync(string filename, string text)
		{
			var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			string path = Path.Combine(string.Concat(folderPath, "/ScanData"), filename);
			if (Directory.Exists(string.Concat(folderPath, "/ScanData")))
			{
				using (StreamWriter streamWriter = File.CreateText(path))
				{
					await streamWriter.WriteAsync(text);
					streamWriter.Dispose();
					streamWriter.Close();
				}
			}

			else
			{
				DirectoryInfo di = Directory.CreateDirectory(string.Concat(folderPath, "/ScanData"));

				using (StreamWriter streamWriter = File.CreateText(path))
				{
					await streamWriter.WriteAsync(text);
					streamWriter.Dispose();
					streamWriter.Close();
				}
			}
		}

		#endregion

		static string CreatePathToFile(string fileName)
		{
			return Path.Combine (DocumentsPath, fileName);
		}
	}
}