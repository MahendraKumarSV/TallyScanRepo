using System;
using System.Threading.Tasks;

namespace TallySoftShared
{
	/// <summary>
	/// Define an API for loading and saving a text file. Reference this interface
	/// in the common code, and implement this interface in the app projects for
	/// iOS, Android and WinPhone. Remember to use the 
	///     [assembly: Dependency (typeof (SaveAndLoad_IMPLEMENTATION_CLASSNAME))]
	/// attribute on each of the implementations.
	/// </summary>
	public interface ISaveAndLoad
	{
		Task SaveFileToFolderAsync(string filename, string text);
		bool FileExists (string filename);
	}
}

