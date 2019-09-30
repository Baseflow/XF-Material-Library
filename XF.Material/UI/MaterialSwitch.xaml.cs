using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Material.Forms.UI
{
    public class ActivatedEventArgs : ToggledEventArgs
    {
        public ActivatedEventArgs(bool value) : base(value)
        {
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialSwitch : ContentView
    {
        public static readonly BindableProperty ActiveTrackColorProperty = BindableProperty.Create(nameof(ActiveTrackColor), typeof(Color), typeof(MaterialSwitch), Material.Color.Secondary.MultiplyAlpha(0.54));

        public static readonly BindableProperty ActiveThumbColorProperty = BindableProperty.Create(nameof(ActiveThumbColor), typeof(Color), typeof(MaterialSwitch), Material.Color.Secondary);

        public static readonly BindableProperty InactiveTrackColorProperty = BindableProperty.Create(nameof(InactiveTrackColor), typeof(Color), typeof(MaterialSwitch), Color.LightGray);

        public static readonly BindableProperty InactiveThumColorProperty = BindableProperty.Create(nameof(InactiveThumbColor), typeof(Color), typeof(MaterialSwitch), Color.FromHex("#FFFFFF"));

        public static readonly BindableProperty IsActivatedProperty = BindableProperty.Create(nameof(IsActivated), typeof(bool), typeof(MaterialSwitch), false, BindingMode.TwoWay);

        public MaterialSwitch()
        {
            InitializeComponent();
            _background.Color = IsActivated ? ActiveTrackColor : InactiveTrackColor;
        }

        public event EventHandler<ActivatedEventArgs> Activated;

        public Color ActiveTrackColor
        {
            get => (Color)GetValue(ActiveTrackColorProperty);
            set => SetValue(ActiveTrackColorProperty, value);
        }

        public Color ActiveThumbColor
        {
            get => (Color)GetValue(ActiveThumbColorProperty);
            set => SetValue(ActiveThumbColorProperty, value);
        }

        public Color InactiveTrackColor
        {
            get => (Color)GetValue(InactiveTrackColorProperty);
            set => SetValue(InactiveTrackColorProperty, value);
        }

        public Color InactiveThumbColor
        {
            get => (Color)GetValue(InactiveThumColorProperty);
            set => SetValue(InactiveThumColorProperty, value);
        }

        public bool IsActivated
        {
            get => (bool)GetValue(IsActivatedProperty);
            set => SetValue(IsActivatedProperty, value);
        }

        protected virtual void OnActivatedChanged(bool isActivated)
        {
            Activated?.Invoke(this, new ActivatedEventArgs(IsActivated));

            Device.BeginInvokeOnMainThread(async () => await AnimateSwitchAsync(IsActivated));
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(IsActivated):
                    OnActivatedChanged(IsActivated);
                    break;
                case nameof(ActiveTrackColor):
                case nameof(InactiveTrackColor):
                    _background.Color = IsActivated ? ActiveTrackColor : InactiveTrackColor;
                    break;
            }
        }

        private async Task AnimateSwitchAsync(bool isActivated)
        {
            _background.Color = IsActivated ? ActiveTrackColor : InactiveTrackColor;

            if (isActivated)
            {
                await AnimateToActivatedState();
            }
            else
            {
                await AnimateToUnactivatedState();
            }
        }

        private async Task AnimateToActivatedState()
        {
            _thumb.BackgroundColor = ActiveThumbColor;
            await _thumb.TranslateTo(16, 0, 150, Easing.SinOut);
        }

        private async Task AnimateToUnactivatedState()
        {
            _thumb.BackgroundColor = InactiveThumbColor;
            await _thumb.TranslateTo(0, 0, 100, Easing.SinOut);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            IsActivated = !IsActivated;
        }
    }
}