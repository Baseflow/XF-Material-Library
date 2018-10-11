using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Material.Forms.Views
{
    /// <summary>
    /// A control that allow users to make selections from a range of values.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialSlider : ContentView
    {
        public static readonly BindableProperty MaxValueProperty = BindableProperty.Create(nameof(MaxValue), typeof(double), typeof(MaterialSlider), 0.0, BindingMode.TwoWay);

        public static readonly BindableProperty MinValueProperty = BindableProperty.Create(nameof(MinValue), typeof(double), typeof(MaterialSlider), 0.0, BindingMode.TwoWay);

        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(MaterialSlider), 0.0, BindingMode.TwoWay);

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialSlider), Material.Color.Secondary, BindingMode.TwoWay);

        private double _x;
        private bool _draggerTranslatedInitially;
        private double _lastWidth;
        private double _lastHeight;

        public double MinValue
        {
            get => (double)this.GetValue(MinValueProperty);
            set => this.SetValue(MinValueProperty, value);
        }

        public double MaxValue
        {
            get => (double)this.GetValue(MaxValueProperty);
            set => this.SetValue(MaxValueProperty, value);
        }

        public double Value
        {
            get => (double)this.GetValue(ValueProperty);
            set
            {
                var oldVal = (double)this.GetValue(ValueProperty);

                if(oldVal != value)
                {
                    this.OnValueChanged(oldVal, value);
                }

                this.SetValue(ValueProperty, value);
            }
        }

        public Color TintColor
        {
            get => (Color)this.GetValue(TintColorProperty);
            set => this.SetValue(TintColorProperty, value);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialSlider"/>.
        /// </summary>
        public MaterialSlider()
        {
            this.InitializeComponent();

            var pan = new PanGestureRecognizer();
            pan.PanUpdated += this.Pan_PanUpdated;

            this.GestureRecognizers.Add(pan);
            TapContainer.Tapped += this.TapContainer_Tapped;
        }

        private void TapContainer_Tapped(object sender, Internals.TappedEventArgs e)
        {
            if(!this.IsEnabled)
            {
                return;
            }

            var newX = Math.Min(e.X, Placeholder.Width) >= 0 ? Math.Min(e.X, Placeholder.Width) : 0;
            var percentage = newX / Placeholder.Width;
            var newVal = percentage * (this.MaxValue - this.MinValue) + this.MinValue;

            this.Value = newVal;
            _x = Dragger.TranslationX;
        }

        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        private void Pan_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Running)
            {
                var newX = Math.Min(_x + e.TotalX, Placeholder.Width) >= 0 ? Math.Min(_x + e.TotalX, Placeholder.Width) : 0;
                var percentage = newX / Placeholder.Width;
                var newVal = percentage * (this.MaxValue - this.MinValue) + this.MinValue;

                this.Value = newVal;
            }

            else if (e.StatusType == GestureStatus.Completed)
            {
                _x = Dragger.TranslationX;
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(this.Value))
            {
                this.AnimateDragger();
            }

            if (propertyName == nameof(this.TintColor))
            {
                Dragger.BackgroundColor = Indicator.Color = Placeholder.Color = this.TintColor;
            }

            if(propertyName == nameof(this.IsEnabled))
            {
                this.Opacity = this.IsEnabled ? 1.0 : 0.24;
            }
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);

            if (width * height != 0 && this.Value > 0 && !_draggerTranslatedInitially)
            {
                this.AnimateDragger();
                _x = Dragger.TranslationX;
                _draggerTranslatedInitially = true;
            }
        }

        private void AnimateDragger()
        {
            var percentage = this.Value / (this.MaxValue - this.MinValue);
            Dragger.TranslationX = percentage * Placeholder.Width;
            Indicator.WidthRequest = Dragger.TranslationX;
        }

        protected virtual void OnValueChanged(double oldValue, double newValue)
        {
            this.ValueChanged?.Invoke(this, new ValueChangedEventArgs(oldValue, newValue));
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if(_lastWidth != width || _lastHeight != height)
            {
                _lastWidth = width;
                _lastHeight = height;

                this.AnimateDragger();
                _x = Dragger.TranslationX;
            }
        }
    }
}