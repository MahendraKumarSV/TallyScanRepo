using SQLite;

namespace TallySoftShared.Model
{
    /// <summary>
    /// ScanDataTable
    /// </summary>
	[Table ("scandatatable")]
	public class ScanDataTable
	{
		[PrimaryKey, AutoIncrement]
		public int rowid { get; set; } //rowid

        public string filename { get; set; } //filename

        public string quantity { get; set; } //quantity

        public string sku { get; set; } //sku

        public string firstTimeScan { get; set; } //firstTimeScan

        public string lastTimeScan { get; set; } //lastTimeScan
    }
}