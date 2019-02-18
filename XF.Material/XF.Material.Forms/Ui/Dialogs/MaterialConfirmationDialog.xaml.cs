using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace XF.Material.Forms.UI.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialConfirmationDialog : BaseMaterialModalPage, IMaterialAwaitableDialog<object>
    {
        private MaterialCheckboxGroup _checkboxGroup;
        private bool _isMultiChoice;
        private MaterialConfirmationDialogConfiguration _preferredConfig;
        private MaterialRadioButtonGroup _radioButtonGroup;

        internal MaterialConfirmationDialog(MaterialConfirmationDialogConfiguration configuration)
        {
            this.InitializeComponent();
            this.Configure(configuration);
        }

        public TaskCompletionSource<object> InputTaskCompletionSource { get; set; }

        internal static MaterialConfirmationDialogConfiguration GlobalConfiguration { get; set; }

        public static async Task<object> ShowSelectChoiceAsync(string title, IList<string> choices, MaterialConfirmationDialogConfiguration configuration)
        {
            var dialog = new MaterialConfirmationDialog(configuration)
            {
                InputTaskCompletionSource = new TaskCompletionSource<object>(),
                _radioButtonGroup = new MaterialRadioButtonGroup
                {
                    HorizontalSpacing = 20,
                    Choices = choices ?? throw new ArgumentNullException(nameof(choices)),
                }
            };

            if (dialog._preferredConfig != null)
            {
                dialog._radioButtonGroup.SelectedColor = dialog._preferredConfig.ControlSelectedColor;
                dialog._radioButtonGroup.UnselectedColor = dialog._preferredConfig.ControlUnselectedColor;
                dialog._radioButtonGroup.FontFamily = dialog._preferredConfig.TextFontFamily;
                dialog._radioButtonGroup.TextColor = dialog._preferredConfig.TextColor;
            }

            dialog._radioButtonGroup.ShouldShowScrollbar = true;
            dialog.DialogTitle.Text = !string.IsNullOrEmpty(title) ? title : throw new ArgumentNullException(nameof(title));
            dialog.container.Content = dialog._radioButtonGroup;
            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        public static async Task<object> ShowSelectChoiceAsync(string title, IList<string> choices, int selectedIndex, MaterialConfirmationDialogConfiguration configuration)
        {
            var dialog = new MaterialConfirmationDialog(configuration)
            {
                InputTaskCompletionSource = new TaskCompletionSource<object>(),
                _radioButtonGroup = new MaterialRadioButtonGroup
                {
                    HorizontalSpacing = 20,
                    Choices = choices ?? throw new ArgumentNullException(nameof(choices)),
                    SelectedIndex = selectedIndex
                }
            };

            if (dialog._preferredConfig != null)
            {
                dialog._radioButtonGroup.SelectedColor = dialog._preferredConfig.ControlSelectedColor;
                dialog._radioButtonGroup.UnselectedColor = dialog._preferredConfig.ControlUnselectedColor;
                dialog._radioButtonGroup.FontFamily = dialog._preferredConfig.TextFontFamily;
                dialog._radioButtonGroup.TextColor = dialog._preferredConfig.TextColor;
            }

            dialog._radioButtonGroup.ShouldShowScrollbar = true;
            dialog.DialogTitle.Text = !string.IsNullOrEmpty(title) ? title : throw new ArgumentNullException(nameof(title));
            dialog.container.Content = dialog._radioButtonGroup;
            dialog.PositiveButton.IsEnabled = true;
            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        public static async Task<object> ShowSelectChoicesAsync(string title, IList<string> choices, MaterialConfirmationDialogConfiguration configuration)
        {
            var dialog = new MaterialConfirmationDialog(configuration)
            {
                InputTaskCompletionSource = new TaskCompletionSource<object>(),
                _checkboxGroup = new MaterialCheckboxGroup
                {
                    HorizontalSpacing = 20,
                    Choices = choices ?? throw new ArgumentNullException(nameof(choices))
                }
            };

            if (dialog._preferredConfig != null)
            {
                dialog._checkboxGroup.SelectedColor = dialog._preferredConfig.ControlSelectedColor;
                dialog._checkboxGroup.UnselectedColor = dialog._preferredConfig.ControlUnselectedColor;
                dialog._checkboxGroup.FontFamily = dialog._preferredConfig.TextFontFamily;
                dialog._checkboxGroup.TextColor = dialog._preferredConfig.TextColor;
            }

            dialog._checkboxGroup.ShouldShowScrollbar = true;
            dialog._isMultiChoice = true;
            dialog.DialogTitle.Text = !string.IsNullOrEmpty(title) ? title : throw new ArgumentNullException(nameof(title));
            dialog.container.Content = dialog._checkboxGroup;
            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        public static async Task<object> ShowSelectChoicesAsync(string title, IList<string> choices, IList<int> selectedIndices, MaterialConfirmationDialogConfiguration configuration)
        {
            var dialog = new MaterialConfirmationDialog(configuration)
            {
                InputTaskCompletionSource = new TaskCompletionSource<object>(),
                _checkboxGroup = new MaterialCheckboxGroup
                {
                    HorizontalSpacing = 20,
                    Choices = choices ?? throw new ArgumentNullException(nameof(choices)),
                    SelectedIndices = selectedIndices
                }
            };

            if (dialog._preferredConfig != null)
            {
                dialog._checkboxGroup.SelectedColor = dialog._preferredConfig.ControlSelectedColor;
                dialog._checkboxGroup.UnselectedColor = dialog._preferredConfig.ControlUnselectedColor;
                dialog._checkboxGroup.FontFamily = dialog._preferredConfig.TextFontFamily;
                dialog._checkboxGroup.TextColor = dialog._preferredConfig.TextColor;
            }

            dialog._checkboxGroup.ShouldShowScrollbar = true;
            dialog._isMultiChoice = true;
            dialog.DialogTitle.Text = !string.IsNullOrEmpty(title) ? title : throw new ArgumentNullException(nameof(title));
            dialog.container.Content = dialog._checkboxGroup;
            dialog.PositiveButton.IsEnabled = true;
            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        public override void OnBackButtonDismissed()
        {
            this.InputTaskCompletionSource?.SetResult(_isMultiChoice ? null : -1 as object);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_radioButtonGroup?.Choices != null && _radioButtonGroup.Choices.Any())
            {
                _radioButtonGroup.SelectedIndexChanged += this.DialogActionList_SelectedIndexChanged;
            }
            else if (_checkboxGroup?.Choices != null && _checkboxGroup.Choices.Any())
            {
                _checkboxGroup.SelectedIndicesChanged += this.CheckboxGroup_SelectedIndicesChanged;
            }

            PositiveButton.Clicked += this.PositiveButton_Clicked;
            NegativeButton.Clicked += this.NegativeButton_Clicked;

            this.ChangeLayout();
        }

        protected override bool OnBackgroundClicked()
        {
            this.InputTaskCompletionSource?.SetResult(_isMultiChoice ? null : -1 as object);

            return base.OnBackgroundClicked();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (_radioButtonGroup?.Choices != null && _radioButtonGroup.Choices.Any())
            {
                _radioButtonGroup.SelectedIndexChanged -= this.DialogActionList_SelectedIndexChanged;
            }
            else if (_checkboxGroup?.Choices != null && _checkboxGroup.Choices.Any())
            {
                _checkboxGroup.SelectedIndicesChanged -= this.CheckboxGroup_SelectedIndicesChanged;
            }

            PositiveButton.Clicked -= this.PositiveButton_Clicked;
            NegativeButton.Clicked -= this.NegativeButton_Clicked;
        }

        protected override void OnOrientationChanged(DisplayOrientation orientation)
        {
            base.OnOrientationChanged(orientation);

            this.ChangeLayout();
        }

        private void ChangeLayout()
        {
            switch (this.DisplayOrientation)
            {
                case DisplayOrientation.Landscape when Device.Idiom == TargetIdiom.Phone:
                    Container.WidthRequest = 560;
                    break;
                case DisplayOrientation.Portrait when Device.Idiom == TargetIdiom.Phone:
                    Container.WidthRequest = 280;
                    break;
            }
        }

        private void CheckboxGroup_SelectedIndicesChanged(object sender, SelectedIndicesChangedEventArgs e)
        {
            PositiveButton.IsEnabled = e.Indices.Any();
        }

        private void Configure(MaterialConfirmationDialogConfiguration configuration)
        {
            _preferredConfig = configuration ?? GlobalConfiguration;

            if (_preferredConfig == null) return;
            this.BackgroundColor = _preferredConfig.ScrimColor;
            Container.CornerRadius = _preferredConfig.CornerRadius;
            Container.BackgroundColor = _preferredConfig.BackgroundColor;
            DialogTitle.TextColor = _preferredConfig.TitleTextColor;
            DialogTitle.FontFamily = _preferredConfig.TitleFontFamily;
            PositiveButton.TextColor = NegativeButton.TextColor = _preferredConfig.TintColor;
            PositiveButton.AllCaps = NegativeButton.AllCaps = _preferredConfig.ButtonAllCaps;
            PositiveButton.FontFamily = NegativeButton.FontFamily = _preferredConfig.ButtonFontFamily;
        }

        private void DialogActionList_SelectedIndexChanged(object sender, SelectedIndexChangedEventArgs e)
        {
            PositiveButton.IsEnabled = e.Index >= 0;
        }

        private async void NegativeButton_Clicked(object sender, EventArgs e)
        {
            await this.DismissAsync();
            this.InputTaskCompletionSource.SetResult(_isMultiChoice ? null : -1 as object);
            _checkboxGroup?.SelectedIndices.Clear();
        }

        private async void PositiveButton_Clicked(object sender, EventArgs e)
        {
            await this.DismissAsync();
            var result = (_radioButtonGroup?.SelectedIndex) ?? _checkboxGroup?.SelectedIndices.ToArray() as object;
            this.InputTaskCompletionSource.SetResult(result);
            _checkboxGroup?.SelectedIndices.Clear();
        }
    }
}