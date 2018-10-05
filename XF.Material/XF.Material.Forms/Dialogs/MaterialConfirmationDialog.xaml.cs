using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Dialogs.Configurations;
using XF.Material.Forms.Views;

namespace XF.Material.Forms.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MaterialConfirmationDialog : BaseMaterialModalPage, IMaterialAwaitableDialog<object>
	{
        internal static MaterialConfirmationDialogConfiguration GlobalConfiguration { get; set; }

        private MaterialRadioButtonGroup _radioButtonGroup;
        private MaterialCheckboxGroup _checkboxGroup;
        private MaterialConfirmationDialogConfiguration _preferredConfig;
        
		internal MaterialConfirmationDialog (MaterialConfirmationDialogConfiguration configuration)
		{
            this.InitializeComponent();
            this.Configure(configuration);
		}

        public TaskCompletionSource<object> InputTaskCompletionSource { get; set; }

        public static async Task<object> ShowSelectChoiceAsync(string title, IList<string> choices, MaterialConfirmationDialogConfiguration configuration)
        {
            var dialog = new MaterialConfirmationDialog(configuration) { InputTaskCompletionSource = new TaskCompletionSource<object>() };
            dialog._radioButtonGroup = new MaterialRadioButtonGroup
            {
                HorizontalSpacing = 20,
                Choices = choices ?? throw new ArgumentNullException(nameof(choices)),
            };

            if(dialog._preferredConfig != null)
            {
                dialog._radioButtonGroup.SelectedColor = dialog._preferredConfig.ControlSelectedColor;
                dialog._radioButtonGroup.UnselectedColor = dialog._preferredConfig.ControlUnselectedColor;
                dialog._radioButtonGroup.FontFamily = dialog._preferredConfig.TextFontFamily;
                dialog._radioButtonGroup.TextColor = dialog._preferredConfig.TextColor;
            }

            dialog.DialogTitle.Text = !string.IsNullOrEmpty(title) ? title : throw new ArgumentNullException(nameof(title));
            dialog.container.Content = dialog._radioButtonGroup;
            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        public static async Task<object> ShowSelectChoiceAsync(string title, IList<string> choices, int selectedIndex, MaterialConfirmationDialogConfiguration configuration)
        {
            var dialog = new MaterialConfirmationDialog(configuration) { InputTaskCompletionSource = new TaskCompletionSource<object>() };
            dialog._radioButtonGroup = new MaterialRadioButtonGroup
            {
                HorizontalSpacing = 20,
                Choices = choices ?? throw new ArgumentNullException(nameof(choices)),
                SelectedIndex = selectedIndex
            };     

            if (dialog._preferredConfig != null)
            {
                dialog._radioButtonGroup.SelectedColor = dialog._preferredConfig.ControlSelectedColor;
                dialog._radioButtonGroup.UnselectedColor = dialog._preferredConfig.ControlUnselectedColor;
                dialog._radioButtonGroup.FontFamily = dialog._preferredConfig.TextFontFamily;
                dialog._radioButtonGroup.TextColor = dialog._preferredConfig.TextColor;
            }

            dialog.DialogTitle.Text = !string.IsNullOrEmpty(title) ? title : throw new ArgumentNullException(nameof(title));
            dialog.container.Content = dialog._radioButtonGroup;
            dialog.PositiveButton.IsEnabled = true;
            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        public static async Task<object> ShowSelectChoicesAsync(string title, IList<string> choices, MaterialConfirmationDialogConfiguration configuration)
        {
            var dialog = new MaterialConfirmationDialog(configuration) { InputTaskCompletionSource = new TaskCompletionSource<object>() };
            dialog._checkboxGroup = new MaterialCheckboxGroup
            {
                HorizontalSpacing = 20,
                Choices = choices ?? throw new ArgumentNullException(nameof(choices))
            };

            if (dialog._preferredConfig != null)
            {
                dialog._checkboxGroup.SelectedColor = dialog._preferredConfig.ControlSelectedColor;
                dialog._checkboxGroup.UnselectedColor = dialog._preferredConfig.ControlUnselectedColor;
                dialog._checkboxGroup.FontFamily = dialog._preferredConfig.TextFontFamily;
                dialog._checkboxGroup.TextColor = dialog._preferredConfig.TextColor;
            }

            dialog.DialogTitle.Text = !string.IsNullOrEmpty(title) ? title : throw new ArgumentNullException(nameof(title));
            dialog.container.Content = dialog._checkboxGroup;
            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        public static async Task<object> ShowSelectChoicesAsync(string title, IList<string> choices, IList<int> selectedIndices, MaterialConfirmationDialogConfiguration configuration)
        {
            var dialog = new MaterialConfirmationDialog(configuration) { InputTaskCompletionSource = new TaskCompletionSource<object>() };
            dialog._checkboxGroup = new MaterialCheckboxGroup
            {
                HorizontalSpacing = 20,
                Choices = choices ?? throw new ArgumentNullException(nameof(choices)),
                SelectedIndices = selectedIndices
            };

            if (dialog._preferredConfig != null)
            {
                dialog._checkboxGroup.SelectedColor = dialog._preferredConfig.ControlSelectedColor;
                dialog._checkboxGroup.UnselectedColor = dialog._preferredConfig.ControlUnselectedColor;
                dialog._checkboxGroup.FontFamily = dialog._preferredConfig.TextFontFamily;
                dialog._checkboxGroup.TextColor = dialog._preferredConfig.TextColor;
            }

            dialog.DialogTitle.Text = !string.IsNullOrEmpty(title) ? title : throw new ArgumentNullException(nameof(title));
            dialog.container.Content = dialog._checkboxGroup;
            dialog.PositiveButton.IsEnabled = true;
            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(_radioButtonGroup != null && _radioButtonGroup.Choices != null && _radioButtonGroup.Choices.Any())
            {
                _radioButtonGroup.SelectedIndexChanged += this.DialogActionList_SelectedIndexChanged;
            }

            else if (_checkboxGroup != null && _checkboxGroup.Choices != null && _checkboxGroup.Choices.Any())
            {
                _checkboxGroup.SelectedIndicesChanged += this.CheckboxGroup_SelectedIndicesChanged;
            }

            PositiveButton.Clicked += this.PositiveButton_Clicked;
            NegativeButton.Clicked += this.NegativeButton_Clicked;
        }

        private void CheckboxGroup_SelectedIndicesChanged(object sender, SelectedIndicesChangedEventArgs e)
        {
            PositiveButton.IsEnabled = e.Indices.Any();
        }

        private void NegativeButton_Clicked(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void PositiveButton_Clicked(object sender, EventArgs e)
        {
            var result = (_radioButtonGroup?.SelectedIndex) ?? _checkboxGroup?.SelectedIndices.ToArray() as object;
            this.InputTaskCompletionSource.SetResult(result);
            this.Dispose();
            _checkboxGroup?.SelectedIndices.Clear();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (_radioButtonGroup != null && _radioButtonGroup.Choices != null && _radioButtonGroup.Choices.Any())
            {
                _radioButtonGroup.SelectedIndexChanged -= this.DialogActionList_SelectedIndexChanged;
            }

            else if (_checkboxGroup != null && _checkboxGroup.Choices != null && _checkboxGroup.Choices.Any())
            {
                _checkboxGroup.SelectedIndicesChanged -= this.CheckboxGroup_SelectedIndicesChanged;
            }

            PositiveButton.Clicked -= this.PositiveButton_Clicked;
            NegativeButton.Clicked -= this.NegativeButton_Clicked;
        }

        private void DialogActionList_SelectedIndexChanged(object sender, Views.SelectedIndexChangedEventArgs e)
        {
            PositiveButton.IsEnabled = e.Index >= 0;
        }

        private void Configure(MaterialConfirmationDialogConfiguration configuration)
        {
            _preferredConfig = configuration ?? GlobalConfiguration;

            if(_preferredConfig != null)
            {
                this.BackgroundColor = _preferredConfig.ScrimColor;
                Container.CornerRadius = _preferredConfig.CornerRadius;
                Container.BackgroundColor = _preferredConfig.BackgroundColor;
                DialogTitle.TextColor = _preferredConfig.TitleTextColor;
                DialogTitle.FontFamily = _preferredConfig.TitleFontFamily;
                PositiveButton.TextColor = NegativeButton.TextColor = _preferredConfig.TintColor;
                PositiveButton.AllCaps = NegativeButton.AllCaps = _preferredConfig.ButtonAllCaps;
                PositiveButton.FontFamily = NegativeButton.FontFamily = _preferredConfig.ButtonFontFamily;
            }
        }
    }
}