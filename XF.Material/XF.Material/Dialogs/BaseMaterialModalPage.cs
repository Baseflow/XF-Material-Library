using Rg.Plugins.Popup.Pages;
using System;

namespace XF.Material.Dialogs
{
    public abstract class BaseMaterialModalPage : PopupPage, IDisposable
    {
        public virtual void Dispose()
        {
            this.Content = null;
        }

        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();

            this.Dispose();
        }
    }
}
