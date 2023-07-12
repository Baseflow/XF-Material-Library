using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Internals;

namespace XF.Material.Forms.UI
{
    /// <inheritdoc />
    /// <summary>
    /// A control that allow users to make selections from a range of values.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialSlider : ContentView, IMaterialElementConfiguration
    {
        /// <summary>
        /// Backing field for the bindable property <see cref="MaxValue"/>.
        /// </summary>
        public static readonly BindableProperty MaxValueProperty = BindableProperty.Create(nameof(MaxValue), typeof(double), typeof(MaterialSlider), 100.0, BindingMode.TwoWay);

        /// <summary>
        /// Backing field for the bindable property <see cref="MinValue"/>.
        /// </summary>
        public static readonly BindableProperty MinValueProperty = BindableProperty.Create(nameof(MinValue), typeof(double), typeof(MaterialSlider), 0.0, BindingMode.TwoWay);

        /// <summary>
        /// Backing field for the bindable property <see cref="ThumbColor"/>.
        /// </summary>
        public static readonly BindableProperty ThumbColorProperty = BindableProperty.Create(nameof(ThumbColor), typeof(Color), typeof(MaterialSlider), Material.Color.Secondary);

        /// <summary>
        /// Backing field for the bindable property <see cref="TrackColor"/>.
        /// </summary>
        public static readonly BindableProperty TrackColorProperty = BindableProperty.Create(nameof(TrackColor), typeof(Color), typeof(MaterialSlider), Material.Color.Secondary);

        /// <summary>
        /// Backing field for the bindable property <see cref="ValueChangedCommand"/>.
        /// </summary>
        public static readonly BindableProperty ValueChangedCommandProperty = BindableProperty.Create(nameof(ValueChangedCommand), typeof(Command<double>), typeof(MaterialSlider));

        /// <summary>
        /// Backing field for the bindable property <see cref="Value"/>.
        /// </summary>
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(MaterialSlider), 0.0, BindingMode.TwoWay);

        private bool _draggerTranslatedInitially;
        private double _lastHeight = -1;
        private double _lastWidth = -1;
        private PanGestureRecognizer _pan;
        private double _x;

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialSlider"/>.
        /// </summary>
        public MaterialSlider()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Raised when the <see cref="Value"/> property changes.
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        /// <summary>
        /// Gets or sets the maximum value allowed to select.
        /// </summary>
        public double MaxValue
        {
            get => (double)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        /// <summary>
        /// Gets or sets the minmimum value allowed to select.
        /// </summary>
        public double MinValue
        {
            get => (double)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        /// <summary>
        /// Gets or sets the thumb color of the slider.
        /// </summary>
        public Color ThumbColor
        {
            get => (Color)GetValue(ThumbColorProperty);
            set => SetValue(ThumbColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the track color of the slider.
        /// </summary>
        public Color TrackColor
        {
            get => (Color)GetValue(TrackColorProperty);
            set => SetValue(TrackColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the current value that is selected.
        /// </summary>
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set
            {
                var oldVal = (double)GetValue(ValueProperty);

                if (Math.Abs(oldVal - value) > float.MinValue)
                {
                    OnValueChanged(oldVal, value);
                }

                SetValue(ValueProperty, value);
            }
        }

        /// <summary>
        /// Command that will execute when the <see cref="Value"/> property has changed.
        /// </summary>
        public Command<double> ValueChangedCommand
        {
            get => (Command<double>)GetValue(ValueChangedCommandProperty);
            set => SetValue(ValueChangedCommandProperty, value);
        }

        /// <inheritdoc />
        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ElementChanged(bool created)
        {
            if (created)
            {
                _pan = new PanGestureRecognizer();
                _pan.PanUpdated += Pan_PanUpdated;

                GestureRecognizers.Add(_pan);
                TapContainer.Tapped += TapContainer_Tapped;
            }
            else
            {
                _pan.PanUpdated -= Pan_PanUpdated;
                GestureRecognizers.Remove(_pan);
                TapContainer.Tapped -= TapContainer_Tapped;
            }
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);

            if (Math.Abs(width * height) < float.MinValue || !(Value > 0) || _draggerTranslatedInitially)
            {
                return;
            }

            AnimateDragger();
            _x = Dragger.TranslationX;
            _draggerTranslatedInitially = true;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(Value):
                    AnimateDragger();
                    break;
                case nameof(ThumbColor):
                    Dragger.BackgroundColor = ThumbColor;
                    break;
                case nameof(TrackColor):
                    Indicator.Color = Placeholder.Color = TrackColor;
                    break;
                case nameof(IsEnabled):
                    Opacity = IsEnabled ? 1.0 : 0.24;
                    break;
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (Math.Abs(_lastWidth - width) < float.MinValue && Math.Abs(_lastHeight - height) < float.MinValue)
            {
                return;
            }

            _lastWidth = width;
            _lastHeight = height;

            AnimateDragger();
            _x = Dragger.TranslationX;
        }

        /// <summary>
        /// Called when the <see cref="MaterialSlider.Value"/> property changes.
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected virtual void OnValueChanged(double oldValue, double newValue)
        {
            ValueChanged?.Invoke(this, new ValueChangedEventArgs(oldValue, newValue));
            ValueChangedCommand?.Execute(newValue);
        }

        private void AnimateDragger()
        {
            var percentage = (Value - MinValue) / (MaxValue - MinValue);
            Dragger.TranslationX = percentage * Placeholder.Width;
            Indicator.WidthRequest = Dragger.TranslationX;
        }

        private void Pan_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    {
                        var newX = Math.Min(_x + e.TotalX, Placeholder.Width) >= 0 ? Math.Min(_x + e.TotalX, Placeholder.Width) : 0;
                        var percentage = newX / Placeholder.Width;
                        Value = (percentage * (MaxValue - MinValue)) + MinValue;
                        break;
                    }
                case GestureStatus.Completed:
                    _x = Dragger.TranslationX;
                    break;
            }
        }

        private void TapContainer_Tapped(object sender, Internals.TappedEventArgs e)
        {
            if (!IsEnabled)
            {
                return;
            }

            var newX = Math.Min(e.X, Placeholder.Width) >= 0 ? Math.Min(e.X, Placeholder.Width) : 0;
            var percentage = newX / Placeholder.Width;
            Value = (percentage * (MaxValue - MinValue)) + MinValue;
            _x = Dragger.TranslationX;
        }
    }
}