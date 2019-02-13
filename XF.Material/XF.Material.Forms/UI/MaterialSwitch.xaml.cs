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
        public static readonly BindableProperty ActiveBackgroundColorProperty = BindableProperty.Create(nameof(ActiveBackgroundColor), typeof(Color), typeof(MaterialSwitch), Material.Color.Secondary.MultiplyAlpha(0.54));

        public static readonly BindableProperty ActiveThumbColorProperty = BindableProperty.Create(nameof(ActiveThumbColor), typeof(Color), typeof(MaterialSwitch), Material.Color.Secondary);

        public static readonly BindableProperty InactiveBackgroundColorProperty = BindableProperty.Create(nameof(InactiveBackgroundColor), typeof(Color), typeof(MaterialSwitch), Color.LightGray);

        public static readonly BindableProperty InactiveThumColorProperty = BindableProperty.Create(nameof(InactiveThumbColor), typeof(Color), typeof(MaterialSwitch), Color.FromHex("#FFFFFF"));

        public static readonly BindableProperty IsActivatedProperty = BindableProperty.Create(nameof(IsActivated), typeof(bool), typeof(MaterialSwitch), false, BindingMode.TwoWay);

        public MaterialSwitch()
        {
            this.InitializeComponent();
            _background.Color = this.IsActivated ? this.ActiveBackgroundColor : this.InactiveBackgroundColor;
        }

        public event EventHandler<ActivatedEventArgs> Activated;

        public Color ActiveBackgroundColor
        {
            get => (Color)this.GetValue(ActiveBackgroundColorProperty);
            set => this.SetValue(ActiveBackgroundColorProperty, value);
        }

        public Color ActiveThumbColor
        {
            get => (Color)this.GetValue(ActiveThumbColorProperty);
            set => this.SetValue(ActiveThumbColorProperty, value);
        }

        public Color InactiveBackgroundColor
        {
            get => (Color)this.GetValue(InactiveBackgroundColorProperty);
            set => this.SetValue(InactiveBackgroundColorProperty, value);
        }

        public Color InactiveThumbColor
        {
            get => (Color)this.GetValue(InactiveThumColorProperty);
            set => this.SetValue(InactiveThumColorProperty, value);
        }

        public bool IsActivated
        {
            get => (bool)this.GetValue(IsActivatedProperty);
            set => this.SetValue(IsActivatedProperty, value);
        }

        protected virtual void OnActivatedChanged(bool isActivated)
        {
            this.Activated?.Invoke(this, new ActivatedEventArgs(this.IsActivated));

            Device.BeginInvokeOnMainThread(async () => await this.AnimateSwitchAsync(this.IsActivated));
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(this.IsActivated))
            {
                this.OnActivatedChanged(this.IsActivated);
            }

            if (propertyName == nameof(this.ActiveBackgroundColor) || propertyName == nameof(this.InactiveBackgroundColor))
            {
                _background.Color = this.IsActivated ? this.ActiveBackgroundColor : this.InactiveBackgroundColor;
            }
        }

        private async Task AnimateSwitchAsync(bool isActivated)
        {
            _background.Color = this.IsActivated ? this.ActiveBackgroundColor : this.InactiveBackgroundColor;

            if (isActivated)
            {
                await this.AnimateToActivatedState();
            }
            else
            {
                await this.AnimateToUnactivatedState();
            }
        }

        private async Task AnimateToActivatedState()
        {
            _thumb.BackgroundColor = this.ActiveThumbColor;
            await _thumb.TranslateTo(16, 0, 150, Easing.SinOut);
        }

        private async Task AnimateToUnactivatedState()
        {
            _thumb.BackgroundColor = this.InactiveThumbColor;
            await _thumb.TranslateTo(0, 0, 100, Easing.SinOut);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            this.IsActivated = !this.IsActivated;
        }
    }
}