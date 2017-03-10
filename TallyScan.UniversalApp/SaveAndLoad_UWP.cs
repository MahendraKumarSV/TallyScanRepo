using System;
using System.Collections.Generic;
using TallyScan.UniversalApp;
using TallySoftShared.Model;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: Xamarin.Forms.Dependency(typeof(TallyScan.UniversalApp.SaveAndLoad_UWP))]
namespace TallyScan.UniversalApp
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

    public class SaveAndLoad_UWP : TallySoftShared.ISaveAndLoad
    {

        /// <summary>
        /// Check File exists or not
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool FileExists(string filename)
        {
            try
            {
                bool IsFileExists = false;
                var allfiles = Windows.Storage.ApplicationData.Current.LocalFolder.GetFilesAsync().AsTask().GetAwaiter().GetResult();
                for (int i = 0; i < allfiles.Count; i++)
                {
                    if (allfiles[i].Name.ToLower() == filename.ToLower())
                    {
                        IsFileExists = true;
                    }
                }
                return IsFileExists;
            }
            catch
            {
                return false;
            }
        }


       

        ///// <summary>
        ///// save to text file
        ///// </summary>
        ///// <param name="filename"></param>
        ///// <param name="text"></param>
        ///// <returns></returns>
        //public async Task SaveTextAsync(string filename, string text)
        //{

        //    if (FileExists(filename))
        //    {

        //        var af = Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(filename).AsTask().GetAwaiter().GetResult();
        //        var dotindex = af.Name.IndexOf(".");
        //        var name = af.Name;
        //        var extension = name.Substring(dotindex, af.Name.Length - dotindex);
        //        if (extension == ".tsx")
        //        {
        //            await af.DeleteAsync(Windows.Storage.StorageDeleteOption.PermanentDelete);
        //        }
        //    }

        //    var file = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(filename, Windows.Storage.CreationCollisionOption.ReplaceExisting);
        //    using (StreamWriter sw = new StreamWriter(await file.OpenStreamForWriteAsync()))
        //    {
        //        await sw.WriteAsync(text);
        //        sw.Flush();
        //    };

        //}


        /// <summary>
        /// SaveFileToFolderAsync
        /// </summary>
        /// <param name="filename">string</param>
        /// <param name="text">string</param>
        /// <returns></returns>
        public async Task SaveFileToFolderAsync(string filename, string text)
        {
            var dataFolder = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFolderAsync("ScanData", Windows.Storage.CreationCollisionOption.OpenIfExists);
            var file = await dataFolder.CreateFileAsync(filename, Windows.Storage.CreationCollisionOption.ReplaceExisting);
            using (StreamWriter sw = new StreamWriter(await file.OpenStreamForWriteAsync()))
            {
                await sw.WriteAsync(text);
                sw.Flush();
            };
        }
        /// <summary>
        /// CreatePathToFile
        /// </summary>
        /// <param name="filename">string</param>
        /// <returns></returns>
        string CreatePathToFile(string filename)
        {
            var docsPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path);
            return Path.Combine(docsPath, filename);
        }
    }
}
