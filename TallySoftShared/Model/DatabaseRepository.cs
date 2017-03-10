using TallySoftShared.Model;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;

namespace TallySoftShared
{
    /// <summary>
    /// DatabaseRepository for all sqlite database functions
    /// </summary>
	public class DatabaseRepository
	{
        /// <summary>
        /// sqlite transaction locker static instance
        /// </summary>
		static object locker = new object(); ///

		SQLiteConnection database;

        /// <summary>
        /// return database path as per os compiler check
        /// </summary>
		string DatabasePath
		{
			get
			{
				var sqliteFilename = "FileDataAndStrings.sqlite"; //database file name
				#if __IOS__
					string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
					string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
					var path = Path.Combine(libraryPath, sqliteFilename);
				#else
				#if __ANDROID__
					string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
					var path = Path.Combine(documentsPath, sqliteFilename);
				#else
					// WinPhone
					var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);
				#endif
				#endif
				return path;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		/// <param name='path'>
		/// Path.
		/// </param>
		public DatabaseRepository()
		{
			database = new SQLiteConnection(DatabasePath);
			// create the tables
			database.CreateTable<CurrentFileName>(); //table for current file name
			database.CreateTable<LocalStrings>(); //table for localstrings
			database.CreateTable<ScanDataTable>(); // table for scan data
			database.CreateTable<StoredFileName>(); //table for files
		}

        /// <summary>
        /// this method return the current file name 
        /// </summary>
        /// <returns></returns>
		public List<CurrentFileName> GetCurrentFileName()
		{
			lock (locker)
			{
				List<CurrentFileName> currentFileRecord = database.Table<CurrentFileName>().ToList();
				return currentFileRecord;
			}
		}

        /// <summary>
        /// this method handles the current file records count
        /// </summary>
        /// <returns></returns>
		public int CurrentStoredFileNameCount()
		{
			lock (locker)
			{
				//return a list of local string records saved to the Local Strings table in the database
				int currentStoredFileRecord = database.Table<StoredFileName>().Count();
				return currentStoredFileRecord;
			}
		}

        /// <summary>
        /// this method return the list of stored files
        /// </summary>
        /// <returns></returns>
		public List<StoredFileName> GetCurrentStoredFileName()
		{
			lock (locker)
			{
				//return a list of local string records saved to the Local Strings table in the database
				List<StoredFileName> currentStoredFileRecord = database.Table<StoredFileName>().ToList();
				return currentStoredFileRecord;
			}
		}

        /// <summary>
        /// AddNewLocalStringsRecord
        /// this method add/update configuration settings
        /// </summary>
        /// <param name="firstBool">bool</param>
        /// <param name="secondBool">bool</param>
        /// <param name="thirdBool">bool</param>
        /// <param name="email">string</param>
        /// <param name="fourthBool">bool</param>
		public void AddNewLocalStringsRecord(bool firstBool, bool secondBool, bool thirdBool, string email, bool fourthBool)
		{
			lock (locker)
			{
				int rowCount = database.Table<LocalStrings>().Count();

				if (rowCount == 0)
				{
					database.Insert(new LocalStrings { scannerOrDeviceCheckBoxBool = firstBool, qtyCheckboxBool = secondBool, barcodeCheckBoxBool = thirdBool, emailId = email, testCheckBoxBool = fourthBool });
				}

				else
				{
					database.Execute("update LocalStrings set scannerOrDeviceCheckBoxBool = ?, qtyCheckboxBool = ? , barcodeCheckBoxBool = ? , emailId = ?  , testCheckBoxBool = ? ", firstBool, secondBool, thirdBool, email, fourthBool);
				}
			}
		}

        /// <summary>
        /// DeleteSotredFileNameRecord
        /// this method delete the all records for current file from storedfilename table
        /// </summary>
        /// <param name="fileName">string</param>
		public void DeleteSotredFileNameRecord(string fileName)
		{
			lock (locker)
			{
				database.Execute("delete FROM storedfilename where name = ?", fileName);
			}
		}


        /// <summary>
        /// AddNewFilenameToTable
        /// </summary>
        /// <param name="name"></param>
		public void AddNewFilenameToTable(string name)
		{
			App.dataModifiedAndSaved = false; //save click flag
			App.dataModifiedAndSaveAndEnd = false; //saveandend click flag
			lock (locker)
			{
					database.Insert(new CurrentFileName { filename = name });
			}
		}

        /// <summary>
        /// AddNewFilenameToStoredFileNameTable
        /// This method add/update filename as per fileid
        /// </summary>
        /// <param name="filename">string</param>
        /// <param name="fileID">int</param>
		public void AddNewFilenameToStoredFileNameTable(string filename, int fileID)
		{
			lock (locker)
			{
				int rowCount = database.Table<StoredFileName>().Count();

				if (rowCount == 0)
				{
					database.Insert(new StoredFileName { name = filename, fileid = fileID });
				}

				else
				{
					database.Execute("update storedfilename set name = ?, fileid = ?", filename, fileID);
				}
			}
		}

        /// <summary>
        /// GetLocalStringRecord
        /// Return the list of scanned records
        /// </summary>
        /// <returns>List<LocalStrings></returns>
		public List<LocalStrings> GetLocalStringRecord()
		{
			lock (locker)
			{
				//return a list of local string records saved to the Local Strings table in the database
				List<LocalStrings> localStrings = database.Table<LocalStrings>().ToList();
				return localStrings;
			}
		}

        /// <summary>
        /// AddNewScanDataRecord
        /// </summary>
        /// <param name="qty">string(Quantity)</param>
        /// <param name="sku">string(SKU)</param>
        /// <param name="firstTime">string(FirstTime Scan)</param>
        /// <param name="lastTime">string(LastTime Scan)</param>
        /// <param name="fileName">string(File Name)</param>
        public void AddNewScanDataRecord(string qty, string sku, string firstTime, string lastTime, string fileName)
		{
			App.dataModifiedAndSaved = false; //save click flag
			App.dataModifiedAndSaveAndEnd = false; //saveandend click flag
			lock(locker)
			{
				//insert a new record into the Scan Data table
				database.Insert(new ScanDataTable { quantity = qty, sku = sku, firstTimeScan = firstTime, lastTimeScan = lastTime, filename =  fileName});
			}
		}

        /// <summary>
        /// Get list of  scan records as per file name
        /// </summary>
        /// <param name="fileName">string</param>
        /// <returns>List<ScanDataTable></returns>
		public List<ScanDataTable> GetAllScanDataRecords(string fileName)
		{
			lock (locker)
			{
				//return a list of Scan Data Records saved to the Scan Data table in the database
				List<ScanDataTable> scanDataRecords = database.Table<ScanDataTable>().Where(f => f.filename == fileName).Reverse().ToList();
				return scanDataRecords;
			}
		}

        /// <summary>
        /// GetSingleScanDataRecords
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>List<CurrentFileName></returns>
		public List<CurrentFileName> GetSingleScanDataRecords(string filename)
		{
			lock (locker)
			{
				List<CurrentFileName> scanDataRecords = database.Table<CurrentFileName>().Where(f => f.filename == filename).ToList();
				return scanDataRecords;
			}
		}

        /// <summary>
        /// Check file exist or not in table
        /// </summary>
        /// <param name="filename">string</param>
        /// <returns>List<CurrentFileName></returns>
		public List<CurrentFileName> IsfileExist(string filename)
		{
			lock (locker)
			{
				return database.Table<CurrentFileName>().Where(f => f.filename == filename).ToList();
			}
		}

        /// <summary>
        /// DeleteCurrentFileNameRecord
        /// Delete data from storedfilename,currentfilename,scandatatable tables as per file name
        /// </summary>
        /// <param name="fileName"></param>
		public void DeleteCurrentFileNameRecord(string fileName)
		{
			lock (locker)
			{
				database.Execute("delete FROM storedfilename where name = ?", fileName);
				database.Execute("delete FROM currentfilename where filename = ?", fileName);
				database.Execute("delete FROM scandatatable where filename = ?", fileName);
			}
		}

        /// <summary>
        /// ClearCurrentFileRecords
        /// delete data from scandatatable table as per file name
        /// </summary>
        /// <param name="fileName"></param>
		public void ClearCurrentFileRecords(string fileName)
		{
			lock (locker)
			{
				database.Execute("delete FROM scandatatable where filename = ?", fileName);
			}
		}

        /// <summary>
        /// DeleteMultipleFilesAndRecord
        /// delete data from storedfilename,currentfilename
        /// </summary>
        /// <param name="fileid">int[]</param>
		public void DeleteMultipleFilesAndRecord(int[] fileid)
		{
			lock (locker)
			{
				String args = String.Join(", ", fileid);
				database.Execute(String.Format("DELETE FROM storedfilename WHERE fileid IN (" + args + ");"));
				database.Execute(String.Format("DELETE FROM currentfilename WHERE id IN (" + args + ");"));
			}
		}

        /// <summary>
        /// AppendQuantityForOldScanRecord        
        /// </summary>
        /// <param name="newQty">newQty</param>
        /// <param name="updatedLastTime">updatedLastTime</param>
        /// <param name="sku">sku</param>
        /// <param name="row">row</param>
        /// <param name="fileName">fileName</param>
		public void AppendQuantityForOldScanRecord(int newQty, string updatedLastTime, string sku, string row, string fileName)
		{
			App.dataModifiedAndSaved = false;
			App.dataModifiedAndSaveAndEnd = false;

			lock (locker)
			{
				database.Execute("update scandatatable set quantity = ?, lastTimeScan = ? where sku = ? and filename = ? and rowid=?", newQty, updatedLastTime, sku, fileName, row);
			}
		}

        /// <summary>
        /// UpdateQuantityForOneScanRecord
        /// </summary>
        /// <param name="newQty">string</param>
        /// <param name="updatedTime">string</param>
        /// <param name="oldQty">string</param>
        /// <param name="sku">string</param>
        /// <param name="rowId">string</param>
        /// <param name="fileName">string</param>
		public void UpdateQuantityForOneScanRecord(string newQty, string updatedTime, string oldQty, string sku, string rowId, string fileName)
		{
			App.dataModifiedAndSaved = false; //save click flag
			App.dataModifiedAndSaveAndEnd = false; //saveandend click flag
			lock (locker)
			{
				database.Execute("update scandatatable set quantity = ?, lastTimeScan = ? where quantity = ? and filename = ? and sku = ? and rowid=?", newQty, updatedTime, oldQty, fileName, sku, rowId);
			}
		}

        /// <summary>
        /// DeleteOneScanRecord
        /// </summary>
        /// <param name="row">string</param>
		public void DeleteOneScanRecord(string row)
		{
			lock (locker)
			{
				database.Execute("delete FROM scandatatable where rowid = ?", row);
			}
		}
	}
}