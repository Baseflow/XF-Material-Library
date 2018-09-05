using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XF.Material.Forms.Dialogs
{
    public class BaseMaterialModalPage : PopupPage, IMaterialModalPage
    {
        /// <summary>
        /// Raised only on Android when the back button was pressed.
        /// </summary>
        public event EventHandler BackButtonPressed;

        private bool _disposed;

        /// <summary>
        /// Disposes this modal dialog.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override bool OnBackButtonPressed()
        {
            BackButtonPressed?.Invoke(this, new EventArgs());
            return base.OnBackButtonPressed();
        }

        /// <summary>
        /// Shows this modal dialog.
        /// </summary>
        protected virtual async Task ShowAsync()
        {
            if(this.CanShowPopup())
            {
                await PopupNavigation.Instance.PushAsync(this, true);
            }

            else
            {
                this.Dispose();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await PopupNavigation.Instance.RemovePageAsync(this, true);
                    this.Content = null;
                });

                _disposed = true;
            }
        }

        private bool CanShowPopup()
        {
            return !PopupNavigation.Instance.PopupStack.ToList().Exists(p => p.GetType() == this.GetType());
        }
    }
}
