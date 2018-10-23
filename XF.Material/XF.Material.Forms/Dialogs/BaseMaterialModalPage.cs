using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XF.Material.Forms.Dialogs
{
    /// <summary>
    /// Base class of Material modal dialogs.
    /// </summary>
    public abstract class BaseMaterialModalPage : PopupPage, IMaterialModalPage
    {
        private bool _disposed;

        protected BaseMaterialModalPage()
        {
            this.Animation = new ScaleAnimation()
            {
                DurationIn = 150,
                DurationOut = 100,
                EasingIn = Easing.SinOut,
                EasingOut = Easing.Linear,
                HasBackgroundAnimation = true,
                PositionIn = Rg.Plugins.Popup.Enums.MoveAnimationOptions.Center,
                PositionOut = Rg.Plugins.Popup.Enums.MoveAnimationOptions.Center,
                ScaleIn = 0.9,
                ScaleOut = 1
            };
        }

        /// <summary>
        /// Disposes this modal dialog.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        await PopupNavigation.Instance.RemovePageAsync(this, true);
                        this.Content = null;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                });

                _disposed = true;
            }
        }

        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
            this.Dispose();
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
        private bool CanShowPopup()
        {
            return !PopupNavigation.Instance.PopupStack.ToList().Exists(p => p.GetType() == this.GetType());
        }
    }
}
