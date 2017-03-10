using SQLite;

namespace TallySoftShared.Model
{
	[Table("currentfilename")]
	public class CurrentFileName
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; } //id

		public string filename { get; set; } //filename
    }
}
