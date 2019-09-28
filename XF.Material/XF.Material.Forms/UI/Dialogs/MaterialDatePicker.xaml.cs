using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.DeviceOrientation.Abstractions;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace XF.Material.Forms.UI.Dialogs
{
    public partial class MaterialDatePicker : BaseMaterialModalPage, IMaterialAwaitableDialog<DateTime?>
    {
        private bool _disposed;

        internal MaterialDatePicker(MaterialConfirmationDialogConfiguration configuration = null)
        {
            this.InitializeComponent();
            this.Configure(configuration);
            this.Container.Margin = 25;
        }

        internal MaterialDatePicker(string title = "Select Date", string confirmingText = "Ok", string dismissiveText = "Cancel", MaterialConfirmationDialogConfiguration configuration = null) : this(configuration)
        {
            this.InputTaskCompletionSource = new TaskCompletionSource<DateTime?>();

            this.Calendar.Title = title;
            this.PositiveButton.Text = confirmingText;
            this.NegativeButton.Text = dismissiveText;
            this.PositiveButton.Command = new Command(async () =>
            {
                await this.DismissAsync();
                this.InputTaskCompletionSource?.TrySetResult(this.Calendar.SelectedDate);
            });
            this.NegativeButton.Command = new Command(async () =>
            {
                await this.DismissAsync();
                this.InputTaskCompletionSource?.TrySetResult(null);
            });
        }

        public TaskCompletionSource<DateTime?> InputTaskCompletionSource { get; set; }

        internal static MaterialConfirmationDialogConfiguration GlobalConfiguration { get; set; }

        public static async Task<DateTime?> Show(string title = "Select Date", string confirmingText = "Ok", string dismissiveText = "Cancel", MaterialConfirmationDialogConfiguration configuration = null)
        {
            using (MaterialDatePicker dialog = new MaterialDatePicker(title, confirmingText, dismissiveText, configuration) { PositiveButton = { IsEnabled = false } })
            {
                await dialog.ShowAsync();

                return await dialog.InputTaskCompletionSource.Task;
            }
        }

        protected override void OnBackButtonDismissed()
        {
            this.InputTaskCompletionSource?.TrySetResult(null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.Calendar.SelectionChanged += this.Calendar_OnSelectionChanged;

            this.ChangeLayout();
        }

        protected override bool OnBackgroundClicked()
        {
            this.InputTaskCompletionSource?.TrySetResult(null);

            return base.OnBackgroundClicked();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            this.Calendar.SelectionChanged -= this.Calendar_OnSelectionChanged;
        }

        protected override void OnOrientationChanged(DeviceOrientations orientation)
        {
            base.OnOrientationChanged(orientation);

            this.ChangeLayout();
        }

        private void ChangeLayout()
        {
            switch (this.DisplayOrientation)
            {
                case DeviceOrientations.Landscape:// when Device.Idiom == TargetIdiom.Phone:
                case DeviceOrientations.LandscapeFlipped:
                    this.Container.WidthRequest = 512;
                    this.Container.HorizontalOptions = LayoutOptions.Center;
                    break;
                case DeviceOrientations.Portrait:// when Device.Idiom == TargetIdiom.Phone:
                case DeviceOrientations.PortraitFlipped:
                    this.Container.WidthRequest = 328;
                    this.Container.HorizontalOptions = LayoutOptions.Center;
                    break;
            }

            this.Calendar.OnOrientationChanged(this.DisplayOrientation);
        }

        private void Configure(MaterialConfirmationDialogConfiguration configuration)
        {
            MaterialConfirmationDialogConfiguration preferredConfig = configuration ?? GlobalConfiguration;

            if (preferredConfig == null) return;
            this.BackgroundColor = preferredConfig.ScrimColor;
            this.Container.CornerRadius = preferredConfig.CornerRadius;
            this.Container.BackgroundColor = preferredConfig.BackgroundColor;
            this.PositiveButton.TextColor = this.NegativeButton.TextColor = preferredConfig.TintColor;
            this.PositiveButton.AllCaps = this.NegativeButton.AllCaps = preferredConfig.ButtonAllCaps;
            this.PositiveButton.FontFamily = this.NegativeButton.FontFamily = preferredConfig.ButtonFontFamily;
            this.Container.Margin = preferredConfig.Margin == default ? Material.GetResource<Thickness>("Material.Dialog.Margin") : preferredConfig.Margin;
        }

        private void Calendar_OnSelectionChanged(object sender, UI.Internals.CalendarSelectionChangedEventArgs e)
        {
            this.PositiveButton.IsEnabled = e.NewSelection != null;
        }

        protected override void Dispose(bool disposing)
        {
            if (this._disposed || !disposing) return;

            try
            {
                this.Content = null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            this._disposed = true;
        }

        /// <summary>
        /// Shows this modal dialog.
        /// </summary>
        protected override async Task ShowAsync()
        {
            if (this.CanShowPopup())
            {
                await Device.InvokeOnMainThreadAsync(async () =>
                {
                    await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(this, true);
                }).ConfigureAwait(false);

            }
            else
            {
                try
                {
                    await this.DismissAsync();
                }
                catch
                {
                    var mutable = Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack as List<Rg.Plugins.Popup.Pages.PopupPage>;
                    mutable?.Clear();
                }
            }
        }
    }
}
