using System.IO;
using Xamarin.Forms;

namespace TallyScan.Windows8_1
{
    public class FileAccessHelper
    {
        public static string GetLocalFilePath(string filename)
        {
            string path =  Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            string dbPath = Path.Combine(path, filename);
            //CopyDatabaseIfNotExists(dbPath);
            return dbPath;
        }
        private static void CopyDatabaseIfNotExists(string dbPath)
        {
           
        }
    }
}
