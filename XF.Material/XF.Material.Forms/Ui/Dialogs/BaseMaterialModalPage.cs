using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XF.Material.Forms.UI.Dialogs
{
    /// <summary>
    /// Base class of Material modal dialogs.
    /// </summary>
    public abstract class BaseMaterialModalPage : PopupPage, IMaterialModalPage
    {
        private bool _disposed;
        private bool _dismissing;

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

            this.DisplayOrientation = Plugin.DeviceOrientation.CrossDeviceOrientation.Current.CurrentOrientation;
        }

        public virtual bool Dismissable => true;

        public virtual string MessageText
        {
            get { return ""; }
            set { }
        }

        protected DeviceOrientations DisplayOrientation { get; private set; }

        /// <summary>
        /// Dismisses this modal dialog asynchronously.
        /// </summary>
        public async Task DismissAsync()
        {
            await Device.InvokeOnMainThreadAsync(async () => await PopupNavigation.Instance.RemovePageAsync(this, true)).ConfigureAwait(false);
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// For internal use only when running on the Android platform.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void RaiseOnBackButtonDismissed()
        {
            if (_dismissing || Device.RuntimePlatform == Device.iOS) return;

            _dismissing = true;

            this.OnBackButtonDismissed();
        }

        protected virtual void OnBackButtonDismissed() { }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || !disposing) return;
            Device.InvokeOnMainThreadAsync(async () =>
            {
                try
                {
                    await this.DismissAsync();
                    this.Content = null;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            });

            _disposed = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            CrossDeviceOrientation.Current.OrientationChanged += this.CurrentOnOrientationChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            CrossDeviceOrientation.Current.OrientationChanged -= this.CurrentOnOrientationChanged;
        }

        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
            this.Dispose();
        }

        protected virtual void OnOrientationChanged(DeviceOrientations orientation)
        {
        }

        /// <summary>
        /// Shows this modal dialog.
        /// </summary>
        protected virtual async Task ShowAsync()
        {
            if (this.CanShowPopup())
            {
                await Device.InvokeOnMainThreadAsync(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(this, true);
                }).ConfigureAwait(false);

            }
            else
            {
                await this.DismissAsync();
            }
        }

        private bool CanShowPopup()
        {
            return !PopupNavigation
                .Instance
                .PopupStack
                .ToList()
                .Exists(p => p.GetType() == this.GetType());
        }

        private void CurrentOnOrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            if (this.DisplayOrientation == e.Orientation) return;
            this.DisplayOrientation = e.Orientation;
            this.OnOrientationChanged(this.DisplayOrientation);
        }
    }
}