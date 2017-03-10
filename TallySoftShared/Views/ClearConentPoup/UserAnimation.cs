using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace TallySoftShared
{
    /// <summary>
    /// User Animation Popup class
    /// </summary>
	class UserAnimation : IPopupAnimation
	{
        /// <summary>
        /// Preparing
        /// </summary>
        /// <param name="content"></param>
        /// <param name="page"></param>
		public void Preparing(View content, PopupPage page)
		{
			content.Opacity = 0;
		}
        /// <summary>
        /// Disposing
        /// </summary>
        /// <param name="content"></param>
        /// <param name="page"></param>
		public void Disposing(View content, PopupPage page)
		{

		}
        /// <summary>
        /// Appearing
        /// </summary>
        /// <param name="content"></param>
        /// <param name="page"></param>
        /// <returns></returns>
		public async Task Appearing(View content, PopupPage page)
		{
			var topOffset = GetTopOffset(content, page);
			content.TranslationY = topOffset;
			content.Opacity = 1;

			await content.TranslateTo(0, 0, easing: Easing.CubicOut);
		}
        /// <summary>
        /// Disappearing
        /// </summary>
        /// <param name="content"></param>
        /// <param name="page"></param>
        /// <returns></returns>
		public async Task Disappearing(View content, PopupPage page)
		{
			var topOffset = GetTopOffset(content, page);

			await content.TranslateTo(0, topOffset, easing: Easing.CubicIn);
		}

        /// <summary>
        /// GetTopOffset
        /// </summary>
        /// <param name="content"></param>
        /// <param name="page"></param>
        /// <returns></returns>
		private int GetTopOffset(View content, Page page)
		{
			return (int)(content.Height + page.Height) / 2;
		}
	}
}


