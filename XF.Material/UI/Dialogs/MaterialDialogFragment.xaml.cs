﻿using System;
using System.Threading.Tasks;

using Microsoft.Maui;
using Microsoft.Maui.Controls.Xaml;
using XF.Material.Maui.UI.Dialogs.Configurations;

namespace XF.Material.Maui.UI.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialDialogFragment : BaseMaterialModalPage, IMaterialAwaitableDialog<bool?>
    {
        internal MaterialDialogFragment(MaterialAlertDialogConfiguration configuration)
        {
            InitializeComponent();
            Configure(configuration);
            InputTaskCompletionSource = new TaskCompletionSource<bool?>();
        }

        public TaskCompletionSource<bool?> InputTaskCompletionSource { get; set; }

        public static async Task<bool?> ShowAsync(View view, string message, string title, string positiveButtonText, string negativeButtonText, MaterialAlertDialogConfiguration configuration)
        {
            var d = new MaterialDialogFragment(configuration)
            {
                container = { Content = view ?? throw new ArgumentNullException(nameof(view)) },
                DialogTitle = { Text = title },
                Message = { Text = message },
                PositiveButton = { Text = positiveButtonText },
                NegativeButton = { Text = negativeButtonText }
            };

            await d.ShowAsync();

            return await d.InputTaskCompletionSource.Task;
        }

        protected override void OnBackButtonDismissed()
        {
            InputTaskCompletionSource?.SetResult(null);
        }

        protected override bool OnBackgroundClicked()
        {
            InputTaskCompletionSource?.SetResult(null);
            return base.OnBackgroundClicked();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            PositiveButton.Clicked += PositiveButton_Clicked;
            NegativeButton.Clicked += NegativeButton_Clicked;

            ChangeLayout();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            PositiveButton.Clicked -= PositiveButton_Clicked;
            NegativeButton.Clicked -= NegativeButton_Clicked;
        }

        protected override void OnOrientationChanged(DisplayOrientation orientation)
        {
            base.OnOrientationChanged(orientation);

            ChangeLayout();
        }

        private void ChangeLayout()
        {
            switch (DisplayOrientation)
            {
                case DisplayOrientation.Landscape when Device.Idiom == TargetIdiom.Phone:
                    Container.WidthRequest = 560;
                    Container.HorizontalOptions = LayoutOptions.Center;
                    break;
                case DisplayOrientation.Portrait when Device.Idiom == TargetIdiom.Phone:
                    Container.WidthRequest = -1;
                    Container.HorizontalOptions = LayoutOptions.FillAndExpand;
                    break;
            }
        }

        internal static MaterialAlertDialogConfiguration GlobalConfiguration { get; set; }

        private void Configure(MaterialAlertDialogConfiguration configuration)
        {
            var preferredConfig = configuration ?? GlobalConfiguration;

            if (preferredConfig == null)
            {
                return;
            }

            BackgroundColor = preferredConfig.ScrimColor;
            Container.CornerRadius = preferredConfig.CornerRadius;
            Container.BackgroundColor = preferredConfig.BackgroundColor;
            DialogTitle.TextColor = preferredConfig.TitleTextColor;
            DialogTitle.FontFamily = preferredConfig.TitleFontFamily;
            Message.TextColor = preferredConfig.MessageTextColor;
            Message.FontFamily = preferredConfig.MessageFontFamily;
            PositiveButton.TextColor = NegativeButton.TextColor = preferredConfig.TintColor;
            PositiveButton.AllCaps = NegativeButton.AllCaps = preferredConfig.ButtonAllCaps;
            PositiveButton.FontFamily = NegativeButton.FontFamily = preferredConfig.ButtonFontFamily;
            Container.Margin = preferredConfig.Margin == default ? Material.GetResource<Thickness>("Material.Dialog.Margin") : preferredConfig.Margin;
        }

        private async void NegativeButton_Clicked(object sender, EventArgs e)
        {
            await DismissAsync();
            InputTaskCompletionSource?.SetResult(false);
        }

        private async void PositiveButton_Clicked(object sender, EventArgs e)
        {
            await DismissAsync();
            InputTaskCompletionSource?.SetResult(true);
        }
    }
}
