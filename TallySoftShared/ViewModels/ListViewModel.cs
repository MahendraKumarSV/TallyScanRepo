using System.Collections.Generic;
using System.ComponentModel;

namespace TallySoftShared
{
    /// <summary>
    /// ListViewModel
    /// </summary>
	public class ListViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string Property)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(Property));
			}
		}

        /// <summary>
        /// ListViewModel constructor
        /// </summary>
		public ListViewModel()
		{

		}

        /// <summary>
        /// fileID property
        /// int type
        /// </summary>
		int fileId;
        /// <summary>
        /// fileID Property
        /// </summary>
        public int fileID
		{
			get
			{
				return fileId;
			}
			set
			{
				fileId = value;
				OnPropertyChanged("fileID");
			}
		}
        
		string text;
        /// <summary>
        /// Text Property
        /// </summary>
		public string Text
		{
			get
			{
				return text;
			}

			set
			{
				text = value;
				OnPropertyChanged("Text");
			}
		}

		List<string> filedata;
        /// <summary>
        /// FileData Property
        /// </summary>
		public List<string> FileData
		{
			get
			{
				return filedata;
			}

			set
			{
				filedata = value;
				OnPropertyChanged("FileData");
			}
		}

		bool isselected;

        /// <summary>
        /// IsSelected Property
        /// </summary>
		public bool IsSelected
		{
			get
			{
				return isselected;
			}
			set
			{
				isselected = value;
				OnPropertyChanged("IsSelected");
				OnPropertyChanged("DetailImage");
			}
		}

		public string DetailImage
		{
			get
			{
				if (IsSelected)
				{
					return "ic_checkmark_active.png";
				}

				return "ic_checkmark_unactive.png";
			}
		}

		bool ishide;
        /// <summary>
        /// IsHide Property
        /// </summary>
		public bool IsHide
		{
			get
			{
				return ishide;
			}
			set
			{
				ishide = value;
				OnPropertyChanged("IsHide");
			}
		}
	}

    /// <summary>
    /// LoaderModel Notify Property
    /// </summary>
	public class LoaderModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string Property)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(Property));
			}
		}

		bool isloading;
        /// <summary>
        /// IsLoading Property
        /// </summary>
		public bool IsLoading
		{
			get
			{
				return isloading;
			}
			set
			{
				isloading = value;
				OnPropertyChanged("IsLoading");
			}
		}
	}
}