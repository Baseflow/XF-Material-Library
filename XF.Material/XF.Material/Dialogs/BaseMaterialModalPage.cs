using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace XF.Material.Dialogs
{
#pragma warning disable S3881 // "IDisposable" should be implemented correctly
    public class BaseMaterialModalPage : PopupPage, IMaterialModalPage
#pragma warning restore S3881 // "IDisposable" should be implemented correctly
    {
        /// <summary>
        /// Raised only on Android when the back button was pressed.
        /// </summary>
        public event EventHandler BackButtonPressed;

        private bool _disposed;

        /// <summary>
        /// Called to hide this modal view.
        /// </summary>
        public virtual async void Dispose()
        {
            if(!_disposed)
            {
                await PopupNavigation.Instance.RemovePageAsync(this, true);
                this.Content = null;
                _disposed = true;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            BackButtonPressed?.Invoke(this, new EventArgs());
            return base.OnBackButtonPressed();
        }

        protected virtual async Task ShowAsync()
        {
            if(this.CanShowPopup())
            {
                await PopupNavigation.Instance.PushAsync(this, true);
            }
        }

        private bool CanShowPopup()
        {
            return !PopupNavigation.Instance.PopupStack.ToList().Exists(p => p.GetType() == this.GetType());
        }
    }
}
