using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Resources;

namespace XF.Material.Forms.UI.Internals
{
    /// <summary>
    /// Internal base class of selection controls such as <see cref="MaterialCheckbox"/> and <see cref="MaterialRadioButton"/>.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseMaterialSelectionControl : ContentView
    {
        /// <summary>
        /// Backing field for the bindable property <see cref="FontFamily"/>.
        /// </summary>
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(BaseMaterialSelectionControl), string.Empty);

        /// <summary>
        /// Backing field for the bindable property <see cref="FontSize"/>.
        /// </summary>
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(BaseMaterialSelectionControl), Material.GetResource<double>(MaterialConstants.MATERIAL_FONTSIZE_BODY1));

        /// <summary>
        /// Backing field for the bindable property <see cref="HorizontalSpacing"/>.
        /// </summary>
        public static readonly BindableProperty HorizontalSpacingProperty = BindableProperty.Create(nameof(HorizontalSpacing), typeof(double), typeof(BaseMaterialSelectionControl), 0.0);

        /// <summary>
        /// Backing field for the bindable property <see cref="IsSelected"/>.
        /// </summary>
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(BaseMaterialSelectionControl), false, BindingMode.TwoWay);

        /// <summary>
        /// Backing field for the bindable property <see cref="SelectedChangeCommand"/>.
        /// </summary>
        public static readonly BindableProperty SelectedChangeCommandProperty = BindableProperty.Create(nameof(SelectedChangeCommand), typeof(Command<bool>), typeof(BaseMaterialSelectionControl));

        /// <summary>
        /// Backing field for the bindable property <see cref="SelectedColor"/>.
        /// </summary>
        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(BaseMaterialSelectionControl), Material.Color.Secondary);

        /// <summary>
        /// Backing field for the bindable property <see cref="TextColor"/>.
        /// </summary>
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(BaseMaterialSelectionControl), Color.FromHex("#DE000000"));

        /// <summary>
        /// Backing field for the bindable property <see cref="Text"/>.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(BaseMaterialSelectionControl), string.Empty);

        /// <summary>
        /// Backing field for the bindable property <see cref="UnselectedColor"/>.
        /// </summary>
        public static readonly BindableProperty UnselectedColorProperty = BindableProperty.Create(nameof(UnselectedColor), typeof(Color), typeof(BaseMaterialSelectionControl), Color.FromHex("#84000000"));

        /// <summary>
        /// Backing field for the bindable property <see cref="VerticalSpacing"/>.
        /// </summary>
        public static readonly BindableProperty VerticalSpacingProperty = BindableProperty.Create(nameof(VerticalSpacing), typeof(double), typeof(BaseMaterialSelectionControlGroup), 0.0);

        private readonly Dictionary<string, Action> _propertyChangeActions;
        private string _selectedSource;
        private string _unselectedSource;

        internal BaseMaterialSelectionControl(MaterialSelectionControlType controlType)
        {
            this.InitializeComponent();
            this.SetPropertyChangeHandler(ref _propertyChangeActions);
            this.SetControlType(controlType);
            this.SetControl();
        }

        /// <summary>
        /// Raised when this selection control was selected or deselected.
        /// </summary>
        public event EventHandler<SelectedChangedEventArgs> SelectedChanged;

        /// <summary>
        /// Gets or sets the font family of the selection control's text.
        /// </summary>
        public string FontFamily
        {
            get => (string)this.GetValue(FontFamilyProperty);
            set => this.SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets the font size of the selection control's text.
        /// </summary>
        public double FontSize
        {
            get => (double)this.GetValue(FontSizeProperty);
            set => this.SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the spacing between the selection control's image and text.
        /// </summary>
        public double HorizontalSpacing
        {
            get => (double)this.GetValue(HorizontalSpacingProperty);
            set => this.SetValue(HorizontalSpacingProperty, value);
        }

        /// <summary>
        /// Gets or sets the boolean value whether this selection control was selected or not.
        /// </summary>
        public bool IsSelected
        {
            get => (bool)this.GetValue(IsSelectedProperty);
            set => this.SetValue(IsSelectedProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that will execute when this selection control was selected or not.
        /// </summary>
        public Command<bool> SelectedChangeCommand
        {
            get => (Command<bool>)this.GetValue(SelectedChangeCommandProperty);
            set => this.SetValue(SelectedChangeCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the selection control image if it was selected.
        /// </summary>
        public Color SelectedColor
        {
            get => (Color)this.GetValue(SelectedColorProperty);
            set => this.SetValue(SelectedColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the selection control's text.
        /// </summary>
        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        /// <summary>
        /// Gets or sets the selection control's text color.
        /// </summary>
        public Color TextColor
        {
            get => (Color)this.GetValue(TextColorProperty);
            set => this.SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the selection control image if it was not selected.
        /// </summary>
        public Color UnselectedColor
        {
            get => (Color)this.GetValue(UnselectedColorProperty);
            set => this.SetValue(UnselectedColorProperty, value);
        }

        internal double VerticalSpacing
        {
            get => (double)this.GetValue(VerticalSpacingProperty);
            set => this.SetValue(VerticalSpacingProperty, value);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (_propertyChangeActions != null && _propertyChangeActions.TryGetValue(propertyName, out var handlePropertyChange))
            {
                handlePropertyChange();
            }
        }

        private void OnEnableChanged(bool isEnabled)
        {
            this.Opacity = isEnabled ? 1.0 : 0.38;
        }

        private void OnFontFamilyChanged(string fontFamily)
        {
            selectionText.FontFamily = fontFamily;
        }

        private void OnFontSizeChanged(double fontSize)
        {
            selectionText.FontSize = fontSize;
        }

        private void OnHorizontalSpacingChanged(double value)
        {
            selectionText.Margin = new Thickness(value, 16, 0, 16);
        }

        private void OnSelectedChanged(bool isSelected)
        {
            selectionIcon.Source = isSelected ? _selectedSource : _unselectedSource;
            selectionIcon.TintColor = isSelected ? this.SelectedColor : this.UnselectedColor;
            this.SelectedChanged?.Invoke(this, new SelectedChangedEventArgs(isSelected));
            this.SelectedChangeCommand?.Execute(isSelected);
        }

        private void OnStateColorChanged(bool isSelected)
        {
            selectionIcon.TintColor = isSelected ? this.SelectedColor : this.UnselectedColor;
        }

        private void OnTextChanged()
        {
            selectionText.Text = this.Text;
        }

        private void OnTextColorChanged()
        {
            selectionText.TextColor = this.TextColor;
        }

        private void OnVerticalSpacingChanged(double value)
        {
            this.Margin = new Thickness(this.Margin.Left, this.Margin.Top, this.Margin.Right, value);
        }

        private void SetControl()
        {
            selectionIcon.Source = this.IsSelected ? _selectedSource : _unselectedSource;
            selectionIcon.TintColor = this.IsSelected ? this.SelectedColor : this.UnselectedColor;
            iconButton.Command = selectionButton.Command = new Command(() =>
            {
                this.IsSelected = !this.IsSelected;
            });

            selectionText.Text = this.Text;
        }

        private void SetControlType(MaterialSelectionControlType controlType)
        {
            switch (controlType)
            {
                case MaterialSelectionControlType.Checkbox:
                    _selectedSource = "xf_checkbox_selected";
                    _unselectedSource = "xf_checkbox_unselected";
                    break;

                case MaterialSelectionControlType.RadioButton:
                    _selectedSource = "xf_radio_button_selected";
                    _unselectedSource = "xf_radio_button_unselected";
                    selectionIcon.WidthRequest = selectionIcon.HeightRequest = 20;
                    break;
            }
        }

        private void SetPropertyChangeHandler(ref Dictionary<string, Action> propertyChangeActions)
        {
            propertyChangeActions = new Dictionary<string, Action>
            {
                { nameof(this.Text), this.OnTextChanged },
                { nameof(this.TextColor), this.OnTextColorChanged },
                { nameof(this.IsSelected), () => this.OnSelectedChanged(this.IsSelected) },
                { nameof(this.SelectedColor), () => this.OnStateColorChanged(this.IsSelected) },
                { nameof(this.UnselectedColor), () => this.OnStateColorChanged(this.IsSelected) },
                { nameof(this.IsEnabled), () => this.OnEnableChanged(this.IsEnabled) },
                { nameof(this.FontFamily), () => this.OnFontFamilyChanged(this.FontFamily) },
                { nameof(this.HorizontalSpacing), () => this.OnHorizontalSpacingChanged(this.HorizontalSpacing) },
                { nameof(this.VerticalSpacing), () => this.OnVerticalSpacingChanged(this.VerticalSpacing) },
                { nameof(this.FontSize), () => this.OnFontSizeChanged(this.FontSize) },
            };
        }
    }
}