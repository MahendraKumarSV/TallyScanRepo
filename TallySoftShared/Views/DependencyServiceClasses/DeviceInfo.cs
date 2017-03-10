using System;
using System.Collections.Generic;
using System.Text;

namespace TallySoftShared
{
    /// <summary>
    /// Device orientations
    /// </summary>
    public enum DeviceOrientations
    {
        Undefined,
        Landscape,
        Portrait
    }
    public class DeviceInfo
    {
        protected static DeviceInfo _instance;
        double width;
        double height;

        static DeviceInfo()
        {
            _instance = new DeviceInfo();
        }
        protected DeviceInfo()
        {
        }

		/// <summary>
		/// Observe device orientation
		/// </summary>
		public static DeviceOrientations IsOrientationPortrait()
        {
            if (_instance.height == 0 && _instance.width == 0)
            {
                return DeviceOrientations.Undefined;
            }
            else if (_instance.height > _instance.width)
            {
                return DeviceOrientations.Portrait;
            }
            else
            {
                return DeviceOrientations.Landscape;
                
            }
        }

        public static void SetSize(double width, double height)
        {
            _instance.width = width;
            _instance.height = height;
        }
    }
}
