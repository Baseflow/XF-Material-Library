using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Xaml;
using XF.Material.Maui.Resources;

namespace XF.Material.Maui.UI.Internals
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
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(BaseMaterialSelectionControl), Color.FromArgb("#DE000000"));

        /// <summary>
        /// Backing field for the bindable property <see cref="Text"/>.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(BaseMaterialSelectionControl), string.Empty);

        /// <summary>
        /// Backing field for the bindable property <see cref="UnselectedColor"/>.
        /// </summary>
        public static readonly BindableProperty UnselectedColorProperty = BindableProperty.Create(nameof(UnselectedColor), typeof(Color), typeof(BaseMaterialSelectionControl), Color.FromArgb("#84000000"));

        /// <summary>
        /// Backing field for the bindable property <see cref="VerticalSpacing"/>.
        /// </summary>
        public static readonly BindableProperty VerticalSpacingProperty = BindableProperty.Create(nameof(VerticalSpacing), typeof(double), typeof(BaseMaterialSelectionControlGroup), 0.0);

        private readonly Dictionary<string, Action> _propertyChangeActions;
        private string _selectedSource;
        private string _unselectedSource;

        internal BaseMaterialSelectionControl(MaterialSelectionControlType controlType)
        {
            InitializeComponent();
            SetPropertyChangeHandler(ref _propertyChangeActions);
            SetControlType(controlType);
            SetControl();
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
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets the font size of the selection control's text.
        /// </summary>
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the spacing between the selection control's image and text.
        /// </summary>
        public double HorizontalSpacing
        {
            get => (double)GetValue(HorizontalSpacingProperty);
            set => SetValue(HorizontalSpacingProperty, value);
        }

        /// <summary>
        /// Gets or sets the boolean value whether this selection control was selected or not.
        /// </summary>
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that will execute when this selection control was selected or not.
        /// </summary>
        public Command<bool> SelectedChangeCommand
        {
            get => (Command<bool>)GetValue(SelectedChangeCommandProperty);
            set => SetValue(SelectedChangeCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the selection control image if it was selected.
        /// </summary>
        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the selection control's text.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// Gets or sets the selection control's text color.
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the selection control image if it was not selected.
        /// </summary>
        public Color UnselectedColor
        {
            get => (Color)GetValue(UnselectedColorProperty);
            set => SetValue(UnselectedColorProperty, value);
        }

        internal double VerticalSpacing
        {
            get => (double)GetValue(VerticalSpacingProperty);
            set => SetValue(VerticalSpacingProperty, value);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == null)
            {
                return;
            }

            if (_propertyChangeActions != null && _propertyChangeActions.TryGetValue(propertyName, out var handlePropertyChange))
            {
                handlePropertyChange();
            }
        }

        private void OnEnableChanged(bool isEnabled)
        {
            Opacity = isEnabled ? 1.0 : 0.38;
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
            selectionIcon.TintColor = isSelected ? SelectedColor : UnselectedColor;
            SelectedChanged?.Invoke(this, new SelectedChangedEventArgs(isSelected));
            SelectedChangeCommand?.Execute(isSelected);
        }

        private void OnStateColorChanged(bool isSelected)
        {
            selectionIcon.TintColor = isSelected ? SelectedColor : UnselectedColor;
        }

        private void OnTextChanged()
        {
            selectionText.Text = Text;
        }

        private void OnTextColorChanged()
        {
            selectionText.TextColor = TextColor;
        }

        private void OnVerticalSpacingChanged(double value)
        {
            Margin = new Thickness(Margin.Left, Margin.Top, Margin.Right, value);
        }

        private void SetControl()
        {
            selectionIcon.Source = IsSelected ? _selectedSource : _unselectedSource;
            selectionIcon.TintColor = IsSelected ? SelectedColor : UnselectedColor;
            iconButton.Command = selectionButton.Command = new Command(() =>
            {
                IsSelected = !IsSelected;
            });

            selectionText.Text = Text;
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
                { nameof(Text), OnTextChanged },
                { nameof(TextColor), OnTextColorChanged },
                { nameof(IsSelected), () => OnSelectedChanged(IsSelected) },
                { nameof(SelectedColor), () => OnStateColorChanged(IsSelected) },
                { nameof(UnselectedColor), () => OnStateColorChanged(IsSelected) },
                { nameof(IsEnabled), () => OnEnableChanged(IsEnabled) },
                { nameof(FontFamily), () => OnFontFamilyChanged(FontFamily) },
                { nameof(HorizontalSpacing), () => OnHorizontalSpacingChanged(HorizontalSpacing) },
                { nameof(VerticalSpacing), () => OnVerticalSpacingChanged(VerticalSpacing) },
                { nameof(FontSize), () => OnFontSizeChanged(FontSize) },
            };
        }
    }
}
