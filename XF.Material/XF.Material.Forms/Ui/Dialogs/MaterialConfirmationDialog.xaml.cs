using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static readonly BindableProperty DialogConfigurationProperty = BindableProperty.Create("DialogConfiguration", typeof(MaterialConfirmationDialogConfiguration), typeof(MaterialConfirmationDialog));
        public static readonly BindableProperty DialogConfirmingTextProperty = BindableProperty.Create("DialogConfirmingText", typeof(string), typeof(MaterialConfirmationDialog), "Ok");
        public static readonly BindableProperty DialogDismissiveTextProperty = BindableProperty.Create("DialogDismissiveText", typeof(string), typeof(MaterialConfirmationDialog), "Cancel");
        public static readonly BindableProperty DialogTitleProperty = BindableProperty.Create("DialogTitle", typeof(string), typeof(MaterialConfirmationDialog), "Select an item");

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

        public static MaterialConfirmationDialogConfiguration GetDialogConfiguration(BindableObject bindable)
        {
            return (MaterialConfirmationDialogConfiguration)bindable.GetValue(DialogConfigurationProperty);
        }

        public static string GetDialogConfirmingText(BindableObject bindable)
        {
            return (string)bindable.GetValue(DialogConfirmingTextProperty);
        }

        public static string GetDialogDismissiveText(BindableObject bindable)
        {
            return (string)bindable.GetValue(DialogDismissiveTextProperty);
        }

        public static string GetDialogTitle(BindableObject bindable)
        {
            return (string)bindable.GetValue(DialogTitleProperty);
        }

        public static void SetDialogConfiguration(BindableObject bindable, MaterialConfirmationDialogConfiguration configuration)
        {
            bindable.SetValue(DialogConfigurationProperty, configuration);
        }

        public static void SetDialogConfirmingText(BindableObject bindable, string confirmingText)
        {
            bindable.SetValue(DialogConfirmingTextProperty, confirmingText);
        }

        public static void SetDialogDismissiveText(BindableObject bindable, string dismissiveText)
        {
            bindable.SetValue(DialogDismissiveTextProperty, dismissiveText);
        }

        public static void SetDialogTitle(BindableObject bindable, string title)
        {
            bindable.SetValue(DialogTitleProperty, title);
        }

        public static async Task<object> ShowSelectChoiceAsync(string title, IList choices, string choiceBindingName = null, string confirmingText = "Ok", string dismissiveText = "Cancel", MaterialConfirmationDialogConfiguration configuration = null)
        {
            var radioButtonGroup = new MaterialRadioButtonGroup
            {
                HorizontalSpacing = 20,
                ChoicesBindingName = choiceBindingName
            };
            radioButtonGroup.Choices = choices ?? throw new ArgumentNullException(nameof(choices));

            var dialog = new MaterialConfirmationDialog(configuration)
            {
                InputTaskCompletionSource = new TaskCompletionSource<object>(),
                _radioButtonGroup = radioButtonGroup
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
            dialog.PositiveButton.IsEnabled = false;
            dialog.PositiveButton.Text = confirmingText.ToUpper();
            dialog.NegativeButton.Text = dismissiveText.ToUpper();
            dialog.container.Content = dialog._radioButtonGroup;
            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        public static async Task<object> ShowSelectChoiceAsync(string title, IList choices, int selectedIndex, string choiceBindingName = null, string confirmingText = "Ok", string dismissiveText = "Cancel", MaterialConfirmationDialogConfiguration configuration = null)
        {
            var radioButtonGroup = new MaterialRadioButtonGroup
            {
                HorizontalSpacing = 20,
                SelectedIndex = selectedIndex,
                ChoicesBindingName = choiceBindingName
            };
            radioButtonGroup.Choices = choices ?? throw new ArgumentNullException(nameof(choices));
            radioButtonGroup.SelectedIndex = selectedIndex;

            var dialog = new MaterialConfirmationDialog(configuration)
            {
                InputTaskCompletionSource = new TaskCompletionSource<object>(),
                _radioButtonGroup = radioButtonGroup
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
            dialog.PositiveButton.Text = confirmingText.ToUpper();
            dialog.NegativeButton.Text = dismissiveText.ToUpper();
            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        public static async Task<object> ShowSelectChoicesAsync(string title, IList choices, string choiceBindingName = null, string confirmingText = "Ok", string dismissiveText = "Cancel", MaterialConfirmationDialogConfiguration configuration = null)
        {
            var checkboxGroup = new MaterialCheckboxGroup
            {
                HorizontalSpacing = 20,
                ChoicesBindingName = choiceBindingName
            };
            checkboxGroup.Choices = choices ?? throw new ArgumentNullException(nameof(choices));

            var dialog = new MaterialConfirmationDialog(configuration)
            {
                InputTaskCompletionSource = new TaskCompletionSource<object>(),
                _checkboxGroup = checkboxGroup
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
            dialog.PositiveButton.IsEnabled = false;
            dialog.PositiveButton.Text = confirmingText.ToUpper();
            dialog.NegativeButton.Text = dismissiveText.ToUpper();
            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        public static async Task<object> ShowSelectChoicesAsync(string title, IList choices, IList<int> selectedIndices, string choiceBindingName = null, string confirmingText = "Ok", string dismissiveText = "Cancel", MaterialConfirmationDialogConfiguration configuration = null)
        {
            var checkboxGroup = new MaterialCheckboxGroup
            {
                HorizontalSpacing = 20,
                ChoicesBindingName = choiceBindingName
            };
            checkboxGroup.Choices = choices ?? throw new ArgumentNullException(nameof(choices));
            checkboxGroup.SelectedIndices = selectedIndices;

            var dialog = new MaterialConfirmationDialog(configuration)
            {
                InputTaskCompletionSource = new TaskCompletionSource<object>(),
                _checkboxGroup = checkboxGroup
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
            dialog.PositiveButton.Text = confirmingText.ToUpper();
            dialog.NegativeButton.Text = dismissiveText.ToUpper();
            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_radioButtonGroup?.Choices != null && _radioButtonGroup.Choices.Count > 0)
            {
                _radioButtonGroup.SelectedIndexChanged += this.DialogActionList_SelectedIndexChanged;
            }
            else if (_checkboxGroup?.Choices != null && _checkboxGroup.Choices.Count > 0)
            {
                _checkboxGroup.SelectedIndicesChanged += this.CheckboxGroup_SelectedIndicesChanged;
            }

            PositiveButton.Clicked += this.PositiveButton_Clicked;
            NegativeButton.Clicked += this.NegativeButton_Clicked;

            this.ChangeLayout();
        }

        protected override void OnBackButtonDismissed()
        {
            this.InputTaskCompletionSource?.SetResult(_isMultiChoice ? null : -1 as object);
        }

        protected override bool OnBackgroundClicked()
        {
            this.InputTaskCompletionSource?.SetResult(_isMultiChoice ? null : -1 as object);

            return base.OnBackgroundClicked();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (_radioButtonGroup?.Choices != null && _radioButtonGroup.Choices.Count > 0)
            {
                _radioButtonGroup.SelectedIndexChanged -= this.DialogActionList_SelectedIndexChanged;
            }
            else if (_checkboxGroup?.Choices != null && _checkboxGroup.Choices.Count > 0)
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
                    Container.HorizontalOptions = LayoutOptions.Center;
                    break;

                case DisplayOrientation.Portrait when Device.Idiom == TargetIdiom.Phone:
                    Container.WidthRequest = -1;
                    Container.HorizontalOptions = LayoutOptions.FillAndExpand;
                    break;
            }
        }

        private void CheckboxGroup_SelectedIndicesChanged(object sender, SelectedIndicesChangedEventArgs e)
        {
            Debug.WriteLine($"CheckboxGroup_SelectedIndicesChanged: {(e.Indices.Any() ? string.Join(",", e.Indices) : "No indicies")}");
            PositiveButton.IsEnabled = e.Indices.Any();
        }

        private void Configure(MaterialConfirmationDialogConfiguration configuration)
        {
            _preferredConfig = configuration ?? GlobalConfiguration;

            if (_preferredConfig == null)
            {
                return;
            }
            this.BackgroundColor = _preferredConfig.ScrimColor;
            Container.CornerRadius = _preferredConfig.CornerRadius;
            Container.BackgroundColor = _preferredConfig.BackgroundColor;
            DialogTitle.TextColor = _preferredConfig.TitleTextColor;
            DialogTitle.FontFamily = _preferredConfig.TitleFontFamily;
            PositiveButton.TextColor = NegativeButton.TextColor = _preferredConfig.TintColor;
            PositiveButton.AllCaps = NegativeButton.AllCaps = _preferredConfig.ButtonAllCaps;
            PositiveButton.FontFamily = NegativeButton.FontFamily = _preferredConfig.ButtonFontFamily;
            Container.Margin = _preferredConfig.Margin == default ? Material.GetResource<Thickness>("Material.Dialog.Margin") : _preferredConfig.Margin;
        }

        private void DialogActionList_SelectedIndexChanged(object sender, SelectedIndexChangedEventArgs e)
        {
            Debug.WriteLine($"DialogActionList_SelectedIndexChanged: {e.Index}");
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