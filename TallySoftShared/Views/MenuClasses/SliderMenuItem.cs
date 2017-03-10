using System;
using Xamarin.Forms;
using System.ComponentModel;

namespace TallySoftShared
{
    /// <summary>
    /// This class properties are being binded with menucell
    /// slider menu item notify class.
    /// </summary>
	public class SliderMenuItem : INotifyPropertyChanged
	{
		public string _Title;
		public string _IconSource;
		public bool _Active;
		public Color _TitleColor;
		public Type _TargetType;

		public string Title
		{
			get { return _Title; }
			set
			{
				_Title = value;
				OnPropertyChanged("Title");
			}
		}

		public string IconSource
		{
			get { return _IconSource; }
			set
			{
				_IconSource = value;
				OnPropertyChanged("IconSource");
			}
		}

		public bool Active
		{
			get { return _Active; }
			set
			{
				_Active = value;
				OnPropertyChanged("Active");
			}
		}

		public Color TitleColor
		{
			get { return _TitleColor; }
			set
			{
				_TitleColor = value;
				OnPropertyChanged("TitleColor");
			}
		}

		public Type TargetType
		{
			get { return _TargetType; }
			set
			{
				_TargetType = value;
				OnPropertyChanged("TargetType");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string Property)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(Property));
			}
		}
	}
}