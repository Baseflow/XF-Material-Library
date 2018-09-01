using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;

namespace XF.Material.Dialogs
{
#pragma warning disable S3881 // "IDisposable" should be implemented correctly
    public abstract class BaseMaterialModalPage : PopupPage, IMaterialModalPage
#pragma warning restore S3881 // "IDisposable" should be implemented correctly
    {
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

        protected virtual async Task ShowAsync()
        {
            await PopupNavigation.Instance.PushAsync(this, true);
        }
    }
}
