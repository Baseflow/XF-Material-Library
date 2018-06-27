using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;

namespace XF.Material.Dialogs
{
    public abstract class BaseMaterialModalPage : PopupPage, IDisposable
    {
        private bool _disposed;

        public virtual async void Dispose()
        {
            if(!_disposed)
            {
                this.Content = null;
                await PopupNavigation.Instance.PopAsync(true);
                _disposed = true;
            }
        }
    }
}
