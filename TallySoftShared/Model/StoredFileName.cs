using SQLite;

namespace TallySoftShared.Model
{
    /// <summary>
    /// StoredFileName
    /// </summary>
	[Table("storedfilename")]
	public class StoredFileName
	{
		[PrimaryKey, AutoIncrement]
		public int fileid { get; set; }//fileid

        public string name { get; set; } //name
    }
}