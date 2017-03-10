using SQLite;

namespace TallySoftShared.Model
{
    /// <summary>
    /// LocalStrings
    /// </summary>
	[Table ("localstrings")]
	public class LocalStrings
	{
		[PrimaryKey, AutoIncrement]
		public int rowId { get; set; } // rowid

		public bool scannerOrDeviceCheckBoxBool { get; set; } //scannerOrDeviceCheckBoxBool

        public bool qtyCheckboxBool { get; set; } //qtyCheckboxBool

        public bool barcodeCheckBoxBool { get; set; } //barcodeCheckBoxBool

        public string emailId { get; set; } //emailId

        public bool testCheckBoxBool { get; set; } //testCheckBoxBool
    }
}
