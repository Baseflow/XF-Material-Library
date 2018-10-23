using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Resources;

namespace XF.Material.Forms.Views.Internals
{
    /// <summary>
    /// Internal base class of selection controls such as <see cref="MaterialCheckbox"/> and <see cref="MaterialRadioButton"/>.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseMaterialSelectionControl : ContentView
    {
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(BaseMaterialSelectionControl), string.Empty);

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(BaseMaterialSelectionControl), Material.GetResource<double>(MaterialConstants.MATERIAL_FONTSIZE_BODY1));

        public static readonly BindableProperty HorizontalSpacingProperty = BindableProperty.Create(nameof(HorizontalSpacing), typeof(double), typeof(BaseMaterialSelectionControl), 0.0);

        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(BaseMaterialSelectionControl), false, BindingMode.TwoWay);

        public static readonly BindableProperty SelectedChangeCommandProperty = BindableProperty.Create(nameof(SelectedChangeCommand), typeof(Command<bool>), typeof(BaseMaterialSelectionControl));

        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(BaseMaterialSelectionControl), Material.Color.Secondary);

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(BaseMaterialSelectionControl), Color.FromHex("#DE000000"));

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(BaseMaterialSelectionControl), string.Empty);

        public static readonly BindableProperty UnselectedColorProperty = BindableProperty.Create(nameof(UnselectedColor), typeof(Color), typeof(BaseMaterialSelectionControl), Color.FromHex("#84000000"));

        public static readonly BindableProperty VerticalSpacingProperty = BindableProperty.Create(nameof(VerticalSpacing), typeof(double), typeof(BaseMaterialSelectionControlGroup), 0.0);

        private string _selectedSource;
        private string _unselectedSource;
        private readonly MaterialSelectionControlType _controlType;

        internal BaseMaterialSelectionControl(MaterialSelectionControlType controlType)
        {
            this.InitializeComponent();

            switch (controlType)
            {
                case MaterialSelectionControlType.Checkbox:
                    _selectedSource = "ic_checkbox_selected";
                    _unselectedSource = "ic_checkbox_unselected";
                    break;
                case MaterialSelectionControlType.RadioButton:
                    _selectedSource = "ic_radio_button_selected";
                    _unselectedSource = "ic_radio_button_unselected";
                    selectionIcon.WidthRequest = selectionIcon.HeightRequest = 20;
                    break;
            }

            _controlType = controlType;
            selectionIcon.Source = this.IsSelected ? _selectedSource : _unselectedSource;
            selectionIcon.TintColor = this.IsSelected ? this.SelectedColor : this.UnselectedColor;
            selectionButton.CornerRadius = Device.RuntimePlatform == Device.Android ? 22 : 18;
            selectionButton.Command = new Command(() =>
            {
                this.IsSelected = !this.IsSelected;
            });

            selectionText.Text = this.Text;
        }

        /// <summary>
        /// Raised when this selection control was selected or deselected.
        /// </summary>
        public event EventHandler<SelectedChangedEventArgs> SelectedChanged;

        public string FontFamily
        {
            get => (string)this.GetValue(FontFamilyProperty);
            set => this.SetValue(FontFamilyProperty, value);
        }

        public double FontSize
        {
            get => (double)this.GetValue(FontSizeProperty);
            set => this.SetValue(FontSizeProperty, value);
        }

        public double HorizontalSpacing
        {
            get => (double)this.GetValue(HorizontalSpacingProperty);
            set => this.SetValue(HorizontalSpacingProperty, value);
        }

        public bool IsSelected
        {
            get => (bool)this.GetValue(IsSelectedProperty);
            set => this.SetValue(IsSelectedProperty, value);
        }

        public Command<bool> SelectedChangeCommand
        {
            get => (Command<bool>)this.GetValue(SelectedChangeCommandProperty);
            set => this.SetValue(SelectedChangeCommandProperty, value);
        }

        public Color SelectedColor
        {
            get => (Color)this.GetValue(SelectedColorProperty);
            set => this.SetValue(SelectedColorProperty, value);
        }

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        public Color TextColor
        {
            get => (Color)this.GetValue(TextColorProperty);
            set => this.SetValue(TextColorProperty, value);
        }

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

            if (propertyName == nameof(this.IsSelected))
            {
                selectionIcon.Source = this.IsSelected ? _selectedSource : _unselectedSource;
                selectionIcon.TintColor = this.IsSelected ? this.SelectedColor : this.UnselectedColor;
                this.SelectedChanged?.Invoke(this, new SelectedChangedEventArgs(this.IsSelected));
                this.SelectedChangeCommand?.Execute(this.IsSelected);
            }

            else if (propertyName == nameof(this.SelectedColor) || propertyName == nameof(this.UnselectedColor))
            {
                selectionIcon.TintColor = this.IsSelected ? this.SelectedColor : this.UnselectedColor;
            }

            else if (propertyName == nameof(this.IsEnabled))
            {
                this.Opacity = this.IsEnabled ? 1.0 : 0.38;
            }

            else if(propertyName == nameof(this.Text))
            {
                selectionText.Text = this.Text;
            }

            else if(propertyName == nameof(this.FontFamily))
            {
                selectionText.FontFamily = this.FontFamily;
            }

            else if(propertyName == nameof(this.HorizontalSpacing))
            {
                selectionText.Margin = new Thickness(this.HorizontalSpacing, 0, 0, 0);
            }

            else if (propertyName == nameof(this.VerticalSpacing))
            {
                this.Margin = new Thickness(this.Margin.Left, this.Margin.Top, this.Margin.Right, this.VerticalSpacing);
            }

            else if(propertyName == nameof(this.FontSize))
            {
                selectionText.FontSize = this.FontSize;
            }

            else if(propertyName == nameof(this.TextColor))
            {
                selectionText.TextColor = this.TextColor;
            }
        }
    }
}