using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Resources;

namespace XF.Material.Forms.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialIconButton : ContentView, IMaterialButtonControl, IMaterialTintableControl
    {
        public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialButton), Material.Color.Secondary);

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialIconButton), Color.Default);

        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(MaterialIconButton), 0.0);

        public static readonly BindableProperty ButtonTypeProperty = BindableProperty.Create(nameof(ButtonType), typeof(MaterialButtonType), typeof(MaterialIconButton), MaterialButtonType.Elevated);

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MaterialIconButton));

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialIconButton));

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(MaterialIconButton), 4);

        public static readonly BindableProperty DisabledBackgroundColorProperty = BindableProperty.Create(nameof(DisabledBackgroundColor), typeof(Color), typeof(MaterialIconButton), default(Color));

        public static readonly BindableProperty ElevationProperty = BindableProperty.Create(nameof(Elevation), typeof(MaterialElevation), typeof(MaterialIconButton), new MaterialElevation(2, 8));

        public static readonly BindableProperty ImageProperty = BindableProperty.Create(nameof(Image), typeof(string), typeof(MaterialIconButton));

        public static readonly BindableProperty PressedBackgroundColorProperty = BindableProperty.Create(nameof(PressedBackgroundColor), typeof(Color), typeof(MaterialIconButton), default(Color));

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialIconButton), default(Color));

        private readonly Dictionary<string, Action> _propertyChangeActions;

        public MaterialIconButton()
        {
            this.InitializeComponent();
            this.SetDynamicResource(WidthRequestProperty, MaterialConstants.MATERIAL_BUTTON_HEIGHT);
            this.SetDynamicResource(HeightRequestProperty, MaterialConstants.MATERIAL_BUTTON_HEIGHT);

            _button.Command = new Command(() => this.OnButtonClicked(true));
            _propertyChangeActions = new Dictionary<string, Action>
            {
                { nameof(this.BorderColor), () => this.OnBorderColorChanged(this.BorderColor) },
                { nameof(this.BorderWidth), () => this.OnBorderWidthChanged(this.BorderWidth) },
                { nameof(this.ButtonType), () => this.OnButtonTypeChanged(this.ButtonType) },
                { nameof(this.CornerRadius), () => this.OnCornerRadiusChanged(this.CornerRadius) },
                { nameof(this.DisabledBackgroundColor), () => this.OnDisabledBackgroundColorChanged(this.DisabledBackgroundColor) },
                { nameof(this.Image), () => this.OnImageChanged(this.Image) },
                { nameof(this.PressedBackgroundColor), () => this.OnPressedBackgroundColorChanged(this.PressedBackgroundColor) },
                { nameof(this.TintColor), () => this.OnTintColorChanged(this.TintColor) },
                { nameof(this.Elevation), () => this.OnElevationChanged(this.Elevation) },
            };
        }

        public event EventHandler Clicked;

        public new Color BackgroundColor
        {
            get => (Color)this.GetValue(BackgroundColorProperty);
            set => this.SetValue(BackgroundColorProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)this.GetValue(BorderColorProperty);
            set => this.SetValue(BorderColorProperty, value);
        }

        public double BorderWidth
        {
            get => (double)this.GetValue(BorderWidthProperty);
            set => this.SetValue(BorderWidthProperty, value);
        }

        public MaterialButtonType ButtonType
        {
            get => (MaterialButtonType)this.GetValue(ButtonTypeProperty);
            set => this.SetValue(ButtonTypeProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => this.GetValue(CommandParameterProperty);
            set => this.SetValue(CommandParameterProperty, value);
        }

        public int CornerRadius
        {
            get => (int)this.GetValue(CornerRadiusProperty);
            set => this.SetValue(CornerRadiusProperty, value);
        }

        public Color DisabledBackgroundColor
        {
            get => (Color)this.GetValue(DisabledBackgroundColorProperty);
            set => this.SetValue(DisabledBackgroundColorProperty, value);
        }

        public MaterialElevation Elevation
        {
            get => (MaterialElevation)this.GetValue(ElevationProperty);
            set => this.SetValue(ElevationProperty, value);
        }

        public string Image
        {
            get => (string)this.GetValue(ImageProperty);
            set => this.SetValue(ImageProperty, value);
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ICommand InternalCommand { get; set; }

        public Color PressedBackgroundColor
        {
            get => (Color)this.GetValue(PressedBackgroundColorProperty);
            set => this.SetValue(PressedBackgroundColorProperty, value);
        }

        public Color TintColor
        {
            get => (Color)this.GetValue(TintColorProperty);
            set => this.SetValue(TintColorProperty, value);
        }

        protected virtual void OnButtonClicked(bool handled)
        {
            this.Clicked?.Invoke(this, EventArgs.Empty);
            this.InternalCommand?.Execute(null);

            if (handled && this.Command?.CanExecute(this.CommandParameter) == true)
            {
                this.Command.Execute(this.CommandParameter);
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(this.BackgroundColor))
            {
                _button.BackgroundColor = this.BackgroundColor;

                return;
            }

            base.OnPropertyChanged(propertyName);
            
            if(propertyName == null) return;

            if (_propertyChangeActions != null && _propertyChangeActions.TryGetValue(propertyName, out var handlePropertyChange))
            {
                handlePropertyChange();
            }
        }

        private void OnBorderColorChanged(Color color)
        {
            _button.BorderColor = color;
        }

        private void OnBorderWidthChanged(double borderWidth)
        {
            _button.BorderWidth = borderWidth;
        }

        private void OnButtonTypeChanged(MaterialButtonType buttonType)
        {
            _button.ButtonType = buttonType;
        }

        private void OnCornerRadiusChanged(int cornerRadius)
        {
            _button.CornerRadius = cornerRadius;
        }

        private void OnDisabledBackgroundColorChanged(Color color)
        {
            _button.DisabledBackgroundColor = color;
        }

        private void OnElevationChanged(MaterialElevation elevation)
        {
            _button.Elevation = elevation;
        }

        private void OnImageChanged(string image)
        {
            _icon.Source = image;
        }

        private void OnPressedBackgroundColorChanged(Color color)
        {
            _button.PressedBackgroundColor = color;
        }

        private void OnTintColorChanged(Color tintColor)
        {
            _icon.TintColor = tintColor;
        }
    }
}