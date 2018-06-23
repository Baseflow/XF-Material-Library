using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace XF.Material.Dialogs
{
    public abstract class BaseMaterialModalPage : PopupPage
    {
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();

            this.Content = null;
        }
    }
}
