using System;
using System.Collections.Generic;
using Xamarin.Forms;
using TallySoftShared;
using TallySoftShared.Model;

namespace TallySoftShared
{
	public class DataSource
	{
        public string Quantity { get; set; }

        public string Sku { get; set; }

        public string rowid { get; set; }

        public string fileid { get; set; }

        public DataSource(string quantity, string sku, string row, string fileID)
        {
            Quantity = quantity;
            Sku = sku;
            rowid = row;
            fileid = fileID;
        }

        public static List<DataSource> GetList(List<ScanDataTable> arrayObj)
        {
            var l = new List<DataSource>();

            if (arrayObj.Count > 0)
            {
                for (int i = 0; i < arrayObj.Count; i++)
                {
                    //Insert data into datasource along with row id
                    l.Add(new DataSource(arrayObj[i].quantity, arrayObj[i].sku, Convert.ToString(arrayObj[i].rowid), Convert.ToString(arrayObj[i].fileid)));
                }
            }

            return l;
        }
    }
}
