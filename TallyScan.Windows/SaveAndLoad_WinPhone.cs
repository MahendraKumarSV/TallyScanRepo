using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallyScan.Windows8_1;
using TallySoftShared;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveAndLoad_WinPhone))]
namespace TallyScan.Windows8_1
{
    public class SaveAndLoad_WinPhone : ISaveAndLoad
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
                var allfiles = ApplicationData.Current.LocalFolder.GetFilesAsync().AsTask().GetAwaiter().GetResult();
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



        /// <summary>
        /// load to text file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<string> LoadTextAsync(string filename)
        {
            if (ApplicationData.Current.LocalFolder != null)
            {
                var file = ApplicationData.Current.LocalFolder.GetFileAsync(filename).AsTask().GetAwaiter().GetResult();
                using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    using (var sr = new StreamReader(fileStream.AsStreamForRead()))
                    {
                        return await sr.ReadToEndAsync();
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// save to text file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task SaveTextAsync(string filename, string text)
        {
            try
            {
                if (FileExists(filename))
                {

                    var af = ApplicationData.Current.LocalFolder.GetFileAsync(filename).AsTask().GetAwaiter().GetResult();
                    var dotindex = af.Name.IndexOf(".");                   
                    var name = af.Name;
                    var extension = name.Substring(dotindex, af.Name.Length - dotindex);
                    if (extension == ".csv")
                    {
                        await af.DeleteAsync(StorageDeleteOption.PermanentDelete);
                    }
                }


                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                using (StreamWriter sw = new StreamWriter(await file.OpenStreamForWriteAsync()))
                {
                    await sw.WriteAsync(text);
                    sw.Flush();
                };
            }
            catch (Exception ex)
            {


            }


        }
        string CreatePathToFile(string filename)
        {
            var docsPath = Path.Combine(ApplicationData.Current.LocalFolder.Path);
            return Path.Combine(docsPath, filename);
        }



    }
}
