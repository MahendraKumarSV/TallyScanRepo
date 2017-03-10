
namespace TallyScan.UniversalApp
{
    /// <summary>
    /// FileAccessHelper
    /// </summary>
    public class FileAccessHelper
    {
        /// <summary>
        /// GetLocalFilePath
        /// </summary>
        /// <param name="filename">string</param>
        /// <returns></returns>
        public static string GetLocalFilePath(string filename)
        {
            string path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            string dbPath = System.IO.Path.Combine(path, filename);
            return dbPath;
        }
    }
}
