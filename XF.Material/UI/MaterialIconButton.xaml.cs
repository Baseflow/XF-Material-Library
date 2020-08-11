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
        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialButton), Material.Color.Secondary);

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialIconButton), Color.Default);

        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(MaterialIconButton), 0.0);

        public static readonly BindableProperty ButtonTypeProperty = BindableProperty.Create(nameof(ButtonType), typeof(MaterialButtonType), typeof(MaterialIconButton), MaterialButtonType.Elevated);

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MaterialIconButton));

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialIconButton));

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(MaterialIconButton), 4);

        public static readonly BindableProperty DisabledBackgroundColorProperty = BindableProperty.Create(nameof(DisabledBackgroundColor), typeof(Color), typeof(MaterialIconButton), default(Color));

        public static readonly BindableProperty ElevationProperty = BindableProperty.Create(nameof(Elevation), typeof(MaterialElevation), typeof(MaterialIconButton), new MaterialElevation(2, 8));

        public static readonly BindableProperty ImageProperty = BindableProperty.Create(nameof(Image), typeof(ImageSource), typeof(MaterialIconButton));

        public static readonly BindableProperty PressedBackgroundColorProperty = BindableProperty.Create(nameof(PressedBackgroundColor), typeof(Color), typeof(MaterialIconButton), default(Color));

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialIconButton), default(Color));

        private readonly Dictionary<string, Action> _propertyChangeActions;

        public MaterialIconButton()
        {
            InitializeComponent();
            SetDynamicResource(WidthRequestProperty, MaterialConstants.MATERIAL_BUTTON_HEIGHT);
            SetDynamicResource(HeightRequestProperty, MaterialConstants.MATERIAL_BUTTON_HEIGHT);

            _button.Command = new Command(() => OnButtonClicked(true));
            _propertyChangeActions = new Dictionary<string, Action>
            {
                { nameof(BorderColor), () => OnBorderColorChanged(BorderColor) },
                { nameof(BorderWidth), () => OnBorderWidthChanged(BorderWidth) },
                { nameof(ButtonType), () => OnButtonTypeChanged(ButtonType) },
                { nameof(CornerRadius), () => OnCornerRadiusChanged(CornerRadius) },
                { nameof(DisabledBackgroundColor), () => OnDisabledBackgroundColorChanged(DisabledBackgroundColor) },
                { nameof(Image), () => OnImageChanged(Image) },
                { nameof(PressedBackgroundColor), () => OnPressedBackgroundColorChanged(PressedBackgroundColor) },
                { nameof(TintColor), () => OnTintColorChanged(TintColor) },
                { nameof(Elevation), () => OnElevationChanged(Elevation) },
            };
        }

        public event EventHandler Clicked;

        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public double BorderWidth
        {
            get => (double)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        public MaterialButtonType ButtonType
        {
            get => (MaterialButtonType)GetValue(ButtonTypeProperty);
            set => SetValue(ButtonTypeProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public Color DisabledBackgroundColor
        {
            get => (Color)GetValue(DisabledBackgroundColorProperty);
            set => SetValue(DisabledBackgroundColorProperty, value);
        }

        public MaterialElevation Elevation
        {
            get => (MaterialElevation)GetValue(ElevationProperty);
            set => SetValue(ElevationProperty, value);
        }

        public ImageSource Image
        {
            get => (ImageSource)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ICommand InternalCommand { get; set; }

        public Color PressedBackgroundColor
        {
            get => (Color)GetValue(PressedBackgroundColorProperty);
            set => SetValue(PressedBackgroundColorProperty, value);
        }

        public Color TintColor
        {
            get => (Color)GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }

        protected virtual void OnButtonClicked(bool handled)
        {
            Clicked?.Invoke(this, EventArgs.Empty);
            InternalCommand?.Execute(null);

            if (handled && Command?.CanExecute(CommandParameter) == true)
            {
                Command.Execute(CommandParameter);
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(BackgroundColor))
            {
                _button.BackgroundColor = BackgroundColor;

                return;
            }

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

        private void OnImageChanged(ImageSource image)
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
