using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Internals;

namespace XF.Material.Forms.UI
{
    /// <inheritdoc />
    /// <summary>
    /// A control that let users enter and edit text.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialDateField : ContentView, IMaterialElementConfiguration
    {
        public static readonly BindableProperty AlwaysShowUnderlineProperty = BindableProperty.Create(nameof(AlwaysShowUnderline), typeof(bool), typeof(MaterialDateField), false);

        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialDateField), Color.FromHex("#DCDCDC"));

        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(MaterialDateField), Material.Color.Error);

        public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(nameof(ErrorText), typeof(string), typeof(MaterialDateField));

        public static readonly BindableProperty FloatingPlaceholderColorProperty = BindableProperty.Create(nameof(FloatingPlaceholderColor), typeof(Color), typeof(MaterialDateField), Color.FromHex("#99000000"));

        public static readonly BindableProperty FloatingPlaceholderEnabledProperty = BindableProperty.Create(nameof(FloatingPlaceholderEnabled), typeof(bool), typeof(MaterialDateField), true);

        public static readonly BindableProperty FloatingPlaceholderFontSizeProperty = BindableProperty.Create(nameof(FloatingPlaceholderFontSize), typeof(double), typeof(MaterialDateField), 0d);

        public static readonly BindableProperty FocusCommandProperty = BindableProperty.Create(nameof(FocusCommand), typeof(Command<bool>), typeof(MaterialDateField));

        public static readonly BindableProperty HasErrorProperty = BindableProperty.Create(nameof(HasError), typeof(bool), typeof(MaterialDateField), false);

        public static readonly BindableProperty HelperTextColorProperty = BindableProperty.Create(nameof(HelperTextColor), typeof(Color), typeof(MaterialDateField), Color.FromHex("#99000000"));

        public static readonly BindableProperty HelperTextFontFamilyProperty = BindableProperty.Create(nameof(HelperTextFontFamily), typeof(string), typeof(MaterialDateField));

        public static readonly BindableProperty HelperTextProperty = BindableProperty.Create(nameof(HelperText), typeof(string), typeof(MaterialDateField), string.Empty);

        public static readonly BindableProperty HorizontalPaddingProperty = BindableProperty.Create(nameof(HorizontalPadding), typeof(MaterialHorizontalThickness), typeof(MaterialDateField), new MaterialHorizontalThickness(12d), defaultBindingMode: BindingMode.OneTime);

        public static readonly BindableProperty LeadingIconProperty = BindableProperty.Create(nameof(LeadingIcon), typeof(string), typeof(MaterialDateField));

        public static readonly BindableProperty LeadingIconTintColorProperty = BindableProperty.Create(nameof(LeadingIconTintColor), typeof(Color), typeof(MaterialDateField), Color.FromHex("#99000000"));

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(MaterialDateField), Color.FromHex("#99000000"));

        public static readonly BindableProperty PlaceholderFontFamilyProperty = BindableProperty.Create(nameof(PlaceholderFontFamily), typeof(string), typeof(MaterialDateField));

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialDateField), string.Empty);

        public static readonly BindableProperty ShouldAnimateUnderlineProperty = BindableProperty.Create(nameof(ShouldAnimateUnderline), typeof(bool), typeof(MaterialDateField), true);

        public static readonly BindableProperty DateChangeCommandProperty = BindableProperty.Create(nameof(DateChangeCommand), typeof(Command<DateTime?>), typeof(MaterialDateField));

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialDateField), Color.FromHex("#D0000000"));

        public static readonly BindableProperty TextFontFamilyProperty = BindableProperty.Create(nameof(TextFontFamily), typeof(string), typeof(MaterialDateField));

        public static readonly BindableProperty TextFontSizeProperty = BindableProperty.Create(nameof(TextFontSize), typeof(double), typeof(MaterialDateField), 16d);

        public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime?), typeof(MaterialDateField), null, BindingMode.TwoWay);

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialDateField), Material.Color.Secondary);

        public static readonly BindableProperty ClearIconProperty = BindableProperty.Create(nameof(ClearIcon), typeof(string), typeof(MaterialDateField), "xf_clear");

        public static readonly BindableProperty UnderlineColorProperty = BindableProperty.Create(nameof(UnderlineColor), typeof(Color), typeof(MaterialDateField), Color.FromHex("#99000000"));

        private const double AnimationDuration = 0.35;
        private readonly Easing _animationCurve = Easing.SinOut;
        private readonly Dictionary<string, Action> _propertyChangeActions;
        private readonly IList<string> _choices;
        private readonly bool _counterEnabled;
        private DisplayInfo _lastDeviceDisplay;
        private readonly int _selectedIndex = -1;
        private bool _wasFocused;

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialDateField"/>.
        /// </summary>
        public MaterialDateField()
        {
            InitializeComponent();
            SetPropertyChangeHandler(ref _propertyChangeActions);
            SetControl();

            datePicker.PropertyChanged += DatePicker_PropertyChanged;
            datePicker.DateSelected += DatePicker_DateChanged;
            datePicker.SizeChanged += Entry_SizeChanged;
            datePicker.Focused += DatePicker_Focused;
            datePicker.Unfocused += DatePicker_Unfocused;

            DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
            _lastDeviceDisplay = DeviceDisplay.MainDisplayInfo;
        }

        public event EventHandler<SelectedItemChangedEventArgs> ChoiceSelected;

        /// <summary>
        /// Raised when this text field receives focus.
        /// </summary>
        public new event EventHandler<FocusEventArgs> Focused;

        /// <summary>
        /// Raised when this text field loses focus.
        /// </summary>
        public new event EventHandler<FocusEventArgs> Unfocused;

        /// <summary>
        /// Raised when the date has changed.
        /// </summary>
        public event EventHandler<NullableDateChangedEventArgs> DateChanged;

        /// <summary>
        /// Raised when the user finalizes the input on this text field using the return key.
        /// </summary>
        public event EventHandler Completed;

        /// <summary>
        /// Gets or sets whether the underline accent of this text field should always show or not.
        /// </summary>
        public bool AlwaysShowUnderline
        {
            get
            {
                return (bool)GetValue(AlwaysShowUnderlineProperty);
            }

            set
            {
                SetValue(AlwaysShowUnderlineProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the background color of this text field.
        /// </summary>
        public new Color BackgroundColor
        {
            get
            {
                return (Color)GetValue(BackgroundColorProperty);
            }

            set
            {
                SetValue(BackgroundColorProperty, value);
            }
        }

        public ImageSource ClearIcon
        {
            get
            {
                return (string)GetValue(ClearIconProperty);
            }

            set
            {
                SetValue(ClearIconProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color to indicate an error in this text field.
        /// The default value is the color of <see cref="MaterialColorConfiguration.Error"/>.
        /// </summary>
        public Color ErrorColor
        {
            get
            {
                return (Color)GetValue(ErrorColorProperty);
            }

            set
            {
                SetValue(ErrorColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the error text of this text field.
        /// </summary>
        public string ErrorText
        {
            get
            {
                return (string)GetValue(ErrorTextProperty);
            }

            set
            {
                SetValue(ErrorTextProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the floating placeholder.
        /// </summary>
        public Color FloatingPlaceholderColor
        {
            get
            {
                return (Color)GetValue(FloatingPlaceholderColorProperty);
            }

            set
            {
                SetValue(FloatingPlaceholderColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets whether the placeholder label will float at top of the text field when focused or when it has text.
        /// </summary>
        public bool FloatingPlaceholderEnabled
        {
            get
            {
                return (bool)GetValue(FloatingPlaceholderEnabledProperty);
            }

            set
            {
                SetValue(FloatingPlaceholderEnabledProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the font size of the floating placeholder.
        /// </summary>
        public double FloatingPlaceholderFontSize
        {
            get
            {
                return (double)GetValue(FloatingPlaceholderFontSizeProperty);
            }

            set
            {
                SetValue(FloatingPlaceholderFontSizeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when this text field receives or loses focus.
        /// </summary>
        public Command<bool> FocusCommand
        {
            get
            {
                return (Command<bool>)GetValue(FocusCommandProperty);
            }

            set
            {
                SetValue(FocusCommandProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the boolean value whether this text field has an error, and if it will show the its error text.
        /// </summary>
        public bool HasError
        {
            get
            {
                return (bool)GetValue(HasErrorProperty);
            }

            set
            {
                SetValue(HasErrorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the helper text of this text field.
        /// </summary>
        public string HelperText
        {
            get
            {
                return (string)GetValue(HelperTextProperty);
            }

            set
            {
                SetValue(HelperTextProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of this text field's helper text.
        /// </summary>
        public Color HelperTextColor
        {
            get
            {
                return (Color)GetValue(HelperTextColorProperty);
            }

            set
            {
                SetValue(HelperTextColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the font family of this text field's helper text.
        /// </summary>
        public string HelperTextFontFamily
        {
            get
            {
                return (string)GetValue(HelperTextFontFamilyProperty);
            }

            set
            {
                SetValue(HelperTextFontFamilyProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the horizontal padding of the text field.
        /// </summary>
        public MaterialHorizontalThickness HorizontalPadding
        {
            get
            {
                return (MaterialHorizontalThickness)GetValue(HorizontalPaddingProperty);
            }

            set
            {
                SetValue(HorizontalPaddingProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the image source of the icon to be showed at the left side of this text field.
        /// </summary>
        public string LeadingIcon
        {
            get
            {
                return (string)GetValue(LeadingIconProperty);
            }

            set
            {
                SetValue(LeadingIconProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the tint color of the icon of this text field.
        /// </summary>
        public Color LeadingIconTintColor
        {
            get
            {
                return (Color)GetValue(LeadingIconTintColorProperty);
            }

            set
            {
                SetValue(LeadingIconTintColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the text of this text field's placeholder.
        /// </summary>
        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException($"{nameof(Placeholder)} must not be null, empty, or a white space.");
                }

                SetValue(PlaceholderProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of this text field's placeholder.
        /// </summary>
        public Color PlaceholderColor
        {
            get
            {
                return (Color)GetValue(PlaceholderColorProperty);
            }

            set
            {
                SetValue(PlaceholderColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the font family of this text field's placeholder
        /// </summary>
        public string PlaceholderFontFamily
        {
            get
            {
                return (string)GetValue(PlaceholderFontFamilyProperty);
            }

            set
            {
                SetValue(PlaceholderFontFamilyProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets whether the underline indicator will be animated. If set to false, the underline will not be shown.
        /// </summary>
        public bool ShouldAnimateUnderline
        {
            get
            {
                return (bool)GetValue(ShouldAnimateUnderlineProperty);
            }

            set
            {
                SetValue(ShouldAnimateUnderlineProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime? Date
        {
            get
            {
                return (DateTime?)GetValue(DateProperty);
            }

            set
            {
                if (value.HasValue && !FloatingPlaceholderEnabled)
                {
                    placeholder.IsVisible = false;
                }
                else if (!value.HasValue && !FloatingPlaceholderEnabled)
                {
                    placeholder.IsVisible = true;
                }

                SetValue(DateProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the command that will execute if there is a change in this text field's input text.
        /// </summary>
        public Command<DateTime?> DateChangeCommand
        {
            get
            {
                return (Command<DateTime?>)GetValue(DateChangeCommandProperty);
            }

            set
            {
                SetValue(DateChangeCommandProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of this text field's input text.
        /// </summary>
        public Color TextColor
        {
            get
            {
                return (Color)GetValue(TextColorProperty);
            }

            set
            {
                SetValue(TextColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the font family of this text field's input text.
        /// </summary>
        public string TextFontFamily
        {
            get
            {
                return (string)GetValue(TextFontFamilyProperty);
            }

            set
            {
                SetValue(TextFontFamilyProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the text's font size.
        /// </summary>
        public double TextFontSize
        {
            get
            {
                return (double)GetValue(TextFontSizeProperty);
            }

            set
            {
                SetValue(TextFontSizeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the tint color of the underline and the placeholder of this text field when focused.
        /// The default value is the color of <see cref="MaterialColorConfiguration.Secondary"/>.
        /// </summary>
        public Color TintColor
        {
            get
            {
                return (Color)GetValue(TintColorProperty);
            }

            set
            {
                SetValue(TintColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the underline when this text field is activated. <see cref="AlwaysShowUnderline"/> is set to true.
        /// </summary>
        public Color UnderlineColor
        {
            get
            {
                return (Color)GetValue(UnderlineColorProperty);
            }

            set
            {
                SetValue(UnderlineColorProperty, value);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ElementChanged(bool created)
        {

        }

        /// <summary>
        /// Requests to set focus on this text field.
        /// </summary>
        public new void Focus()
        {
            datePicker.Focus();
        }

        /// <summary>
        /// Requests to unset the focus on this text field.
        /// </summary>
        public new void Unfocus()
        {
            datePicker.Unfocus();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            AnimateToInactiveOrFocusedStateOnStart(BindingContext);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            AnimateToInactiveOrFocusedStateOnStart(Parent);
        }

        /// <inheritdoc />
        /// <summary>
        /// Method that is called when a bound property has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
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

        private void AnimateToActivatedState()
        {
            var anim = new Animation();
            var hasDate = Date.HasValue;

            if (datePicker.IsFocused)
            {
                var tintColor = HasError ? ErrorColor : TintColor;

                if (ShouldAnimateUnderline)
                {
                    anim.Add(0.0, AnimationDuration, new Animation(v => underline.HeightRequest = v, 1, 2, _animationCurve, () =>
                    {
                        underline.Color = tintColor;
                    }));
                }

                placeholder.TextColor = tintColor;
            }
            else
            {
                var underlineColor = HasError ? ErrorColor : UnderlineColor;
                var placeholderColor = HasError ? ErrorColor : FloatingPlaceholderColor;

                var endHeight = hasDate ? 1 : 0;

                if (ShouldAnimateUnderline)
                {
                    anim.Add(0.0, AnimationDuration, new Animation(v => underline.HeightRequest = v, underline.HeightRequest, endHeight, _animationCurve, () =>
                    {
                        underline.Color = underlineColor;
                    }));
                }

                placeholder.TextColor = placeholderColor;
            }

            anim.Commit(this, "UnfocusAnimation", rate: 2, length: (uint)(Device.RuntimePlatform == Device.iOS ? 500 : AnimationDuration * 1000), easing: _animationCurve);
        }

        private void AnimateToInactiveOrFocusedState()
        {
            Color tintColor;
            var preferredStartFont = FloatingPlaceholderFontSize == 0 ? datePicker.FontSize * 0.75 : FloatingPlaceholderFontSize;
            var preferredEndFont = FloatingPlaceholderFontSize == 0 ? datePicker.FontSize * 0.75 : FloatingPlaceholderFontSize;
            var startFont = datePicker.IsFocused ? datePicker.FontSize : preferredStartFont;
            var endFOnt = datePicker.IsFocused ? preferredEndFont : datePicker.FontSize;
            var startY = placeholder.TranslationY;
            var endY = datePicker.IsFocused ? -(datePicker.FontSize * 0.8) : 0;

            if (HasError)
            {
                tintColor = datePicker.IsFocused ? ErrorColor : PlaceholderColor;
            }
            else
            {
                tintColor = datePicker.IsFocused ? TintColor : PlaceholderColor;
            }

            var anim = FloatingPlaceholderEnabled ? new Animation
            {
                {
                    0.0,
                    AnimationDuration,
                    new Animation(v => placeholder.FontSize = v, startFont, endFOnt, _animationCurve)
                },
                {
                    0.0,
                    AnimationDuration,
                    new Animation(v => placeholder.TranslationY = v, startY, endY, _animationCurve, () =>
                    {
                        if(HasError && datePicker.IsFocused)
                        {
                            placeholder.TextColor = ErrorColor;
                        }

                        placeholder.TextColor = tintColor;
                    })
                }
            } : new Animation();

            if (datePicker.IsFocused)
            {
                if (ShouldAnimateUnderline)
                {
                    underline.Color = HasError ? ErrorColor : TintColor;

                    anim.Add(0.0, AnimationDuration, new Animation(v => underline.WidthRequest = v, 0, Width, _animationCurve, () =>
                    {
                        underline.WidthRequest = -1;
                        underline.HorizontalOptions = LayoutOptions.FillAndExpand;
                    }));
                }
            }
            else
            {
                if (ShouldAnimateUnderline)
                {
                    anim.Add(0.0, AnimationDuration, new Animation(v => underline.HeightRequest = v, underline.HeightRequest, 0, _animationCurve, () =>
                    {
                        underline.WidthRequest = 0;
                        underline.HeightRequest = 2;
                        underline.HorizontalOptions = LayoutOptions.Center;
                    }));
                }
            }

            anim.Commit(this, "FocusAnimation", rate: 2, length: (uint)(Device.RuntimePlatform == Device.iOS ? 500 : AnimationDuration * 1000), easing: _animationCurve);
        }

        private void AnimateToInactiveOrFocusedStateOnStart(object startObject)
        {
            var placeholderEndY = -(datePicker.FontSize * 0.8);
            var placeholderEndFont = datePicker.FontSize * 0.75;

            if (!FloatingPlaceholderEnabled && !datePicker.NullableDate.HasValue)
            {
                placeholder.TextColor = PlaceholderColor;
            }

            if (startObject != null && Date.HasValue /*&& !this._wasFocused*/)
            {
                datePicker.Opacity = 0;

                Device.BeginInvokeOnMainThread(() =>
                {
                    var anim = new Animation();

                    if (FloatingPlaceholderEnabled)
                    {
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.FontSize = v, datePicker.FontSize, placeholderEndFont, _animationCurve));
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.TranslationY = v, placeholder.TranslationY, placeholderEndY, _animationCurve, () =>
                        {
                            placeholder.TextColor = HasError ? ErrorColor : FloatingPlaceholderColor;
                            datePicker.Opacity = 1;
                        }));
                    }

                    if (ShouldAnimateUnderline)
                    {
                        underline.Color = HasError ? ErrorColor : TintColor;
                        underline.HeightRequest = 1;
                        anim.Add(0.0, AnimationDuration, new Animation(v => underline.WidthRequest = v, 0, Width, _animationCurve, () => underline.HorizontalOptions = LayoutOptions.FillAndExpand));
                    }

                    anim.Commit(this, "Anim2", rate: 2, length: (uint)(AnimationDuration * 1000), easing: _animationCurve);
                });

                datePicker.Opacity = 1;

                return;
            }

            if (startObject != null && !Date.HasValue && placeholder.TranslationY == placeholderEndY)
            {
                if (datePicker.IsFocused)
                {
                    return;
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    var anim = new Animation();

                    if (FloatingPlaceholderEnabled)
                    {
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.FontSize = v, placeholderEndFont, datePicker.FontSize, _animationCurve));
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.TranslationY = v, placeholder.TranslationY, 0, _animationCurve, () =>
                        {
                            placeholder.TextColor = PlaceholderColor;
                            datePicker.Opacity = 1;
                        }));
                    }

                    if (ShouldAnimateUnderline)
                    {
                        anim.Add(0.0, AnimationDuration, new Animation(v => underline.WidthRequest = v, Width, 0, _animationCurve, () => underline.HorizontalOptions = LayoutOptions.Center));
                    }

                    anim.Commit(this, "Anim2", rate: 2, length: (uint)(AnimationDuration * 1000), easing: _animationCurve);
                });
            }
        }

        private void ChangeToErrorState()
        {
            const int animDuration = 250;

            placeholder.TextColor = (FloatingPlaceholderEnabled && datePicker.IsFocused) || (FloatingPlaceholderEnabled && datePicker.NullableDate.HasValue) ? ErrorColor : PlaceholderColor;
            counter.TextColor = ErrorColor;
            underline.Color = ShouldAnimateUnderline ? ErrorColor : Color.Transparent;
            persistentUnderline.Color = AlwaysShowUnderline ? ErrorColor : Color.Transparent;
            trailingIcon.IsVisible = true;
            trailingIcon.Source = "xf_error";
            trailingIcon.TintColor = ErrorColor;

            if (string.IsNullOrEmpty(ErrorText))
            {
                helper.TextColor = ErrorColor;
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await helper.FadeTo(0, animDuration / 2, _animationCurve);
                    helper.TranslationY = -4;
                    helper.TextColor = ErrorColor;
                    helper.Text = ErrorText;
                    await Task.WhenAll(helper.FadeTo(1, animDuration / 2, _animationCurve), helper.TranslateTo(0, 0, animDuration / 2, _animationCurve));
                });
            }
        }

        private void ChangeToNormalState()
        {
            const double opactiy = 1;

            IsEnabled = true;
            datePicker.Opacity = opactiy;
            placeholder.Opacity = opactiy;
            helper.Opacity = opactiy;
            underline.Opacity = opactiy;

            Device.BeginInvokeOnMainThread(async () =>
            {

                trailingIcon.IsVisible = false;

                var accentColor = TintColor;
                placeholder.TextColor = accentColor;
                counter.TextColor = HelperTextColor;
                underline.Color = accentColor;
                persistentUnderline.Color = UnderlineColor;

                if (string.IsNullOrEmpty(ErrorText))
                {
                    helper.TextColor = HelperTextColor;
                }
                else
                {
                    await helper.FadeTo(0, 150, _animationCurve);
                    helper.TranslationY = -4;
                    helper.TextColor = HelperTextColor;
                    helper.Text = HelperText;
                    await Task.WhenAll(helper.FadeTo(1, 150, _animationCurve), helper.TranslateTo(0, 0, 150, _animationCurve));
                }
            });
        }

        private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            if (e.DisplayInfo.Orientation != _lastDeviceDisplay.Orientation)
            {
                if (datePicker.NullableDate.HasValue && ShouldAnimateUnderline)
                {
                    underline.WidthRequest = -1;
                    underline.HorizontalOptions = LayoutOptions.FillAndExpand;
                }

                _lastDeviceDisplay = e.DisplayInfo;
            }
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            Completed?.Invoke(this, EventArgs.Empty);
        }

        private void DatePicker_Focused(object sender, FocusEventArgs e)
        {
            _wasFocused = true;
            FocusCommand?.Execute(datePicker.IsFocused);
            Focused?.Invoke(this, e);
        }

        private void DatePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IsFocused) when !datePicker.NullableDate.HasValue:
                    AnimateToInactiveOrFocusedState();
                    break;

                case nameof(IsFocused) when datePicker.NullableDate.HasValue:
                    AnimateToActivatedState();
                    break;
            }
        }

        private void Entry_SizeChanged(object sender, EventArgs e)
        {
            var baseHeight = FloatingPlaceholderEnabled ? 56 : 40;
            var diff = datePicker.Height - 20;
            var rawRowHeight = baseHeight + diff;
            _autoSizingRow.Height = new GridLength(rawRowHeight);

            var iconVerticalMargin = (_autoSizingRow.Height.Value - 24) / 2;

            if (leadingIcon.IsVisible)
            {
                leadingIcon.Margin = new Thickness(HorizontalPadding.Left, iconVerticalMargin, 0, iconVerticalMargin);
                datePicker.Margin = new Thickness(12, datePicker.Margin.Top, HorizontalPadding.Right, datePicker.Margin.Bottom);
            }
            else
            {
                datePicker.Margin = new Thickness(HorizontalPadding.Left, datePicker.Margin.Top, HorizontalPadding.Right, datePicker.Margin.Bottom);
            }

            if (trailingIcon.IsVisible)
            {
                var entryPaddingLeft = leadingIcon.IsVisible ? 12 : HorizontalPadding;
                trailingIcon.Margin = new Thickness(12, iconVerticalMargin, HorizontalPadding.Right, iconVerticalMargin);
                datePicker.Margin = new Thickness(entryPaddingLeft.Left, datePicker.Margin.Top, 0, datePicker.Margin.Bottom);
            }

            helper.Margin = new Thickness(HorizontalPadding.Left, helper.Margin.Top, 12, 0);
            counter.Margin = new Thickness(0, counter.Margin.Top, HorizontalPadding.Right, 0);

            var placeholderLeftMargin = FloatingPlaceholderEnabled ? HorizontalPadding.Left : datePicker.Margin.Left;
            placeholder.Margin = new Thickness(placeholderLeftMargin, 0, 0, 0);

            if (HasError)
            {
                underline.Color = ErrorColor;
            }
        }

        private void DatePicker_DateChanged(object sender, NullableDateChangedEventArgs e)
        {
            Date = datePicker.NullableDate;
            DateChangeCommand?.Execute(datePicker.NullableDate);
            DateChanged?.Invoke(this, e);
        }

        private void DatePicker_Unfocused(object sender, FocusEventArgs e)
        {
            FocusCommand?.Execute(datePicker.IsFocused);
            Unfocused?.Invoke(this, e);
        }


        private void OnAlwaysShowUnderlineChanged(bool isShown)
        {
            persistentUnderline.IsVisible = isShown;
            persistentUnderline.Color = UnderlineColor;
        }

        private void OnBackgroundColorChanged(Color backgroundColor)
        {
            backgroundCard.BackgroundColor = backgroundColor;
        }


        private void OnEnabledChanged(bool isEnabled)
        {
            Opacity = isEnabled ? 1 : 0.33;
            helper.IsVisible = isEnabled && !string.IsNullOrEmpty(HelperText);
        }

        private void OnErrorColorChanged(Color errorColor)
        {
            trailingIcon.TintColor = errorColor;
        }

        private void OnErrorTextChanged()
        {
            if (HasError)
            {
                ChangeToErrorState();
            }
        }

        private void OnFloatingPlaceholderEnabledChanged(bool isEnabled)
        {
            double marginTopVariation = Device.RuntimePlatform == Device.iOS ? 18 : 20;
            datePicker.Margin = isEnabled ? new Thickness(datePicker.Margin.Left, 24, datePicker.Margin.Right, 0) : new Thickness(datePicker.Margin.Left, marginTopVariation - 9, datePicker.Margin.Right, 0);

            var iconMargin = leadingIcon.Margin;
            leadingIcon.Margin = isEnabled ? new Thickness(iconMargin.Left, 16, iconMargin.Right, 16) : new Thickness(iconMargin.Left, 8, iconMargin.Right, 8);

            var trailingIconMargin = trailingIcon.Margin;
            trailingIcon.Margin = isEnabled ? new Thickness(trailingIconMargin.Left, 16, trailingIconMargin.Right, 16) : new Thickness(trailingIconMargin.Left, 8, trailingIconMargin.Right, 8);
        }

        private void OnHasErrorChanged()
        {
            if (HasError)
            {
                ChangeToErrorState();
            }
            else
            {
                ChangeToNormalState();
            }
        }

        private void OnHelperTextChanged(string helperText)
        {
            helper.Text = helperText;
            helper.IsVisible = !string.IsNullOrEmpty(helperText);
        }

        private void OnHelperTextColorChanged(Color textColor)
        {
            helper.TextColor = counter.TextColor = textColor;
        }

        private void OnHelpertTextFontFamilyChanged(string fontFamily)
        {
            helper.FontFamily = counter.FontFamily = fontFamily;
        }

        private void OnLeadingIconChanged(string icon)
        {
            leadingIcon.Source = icon;
            OnLeadingIconTintColorChanged(LeadingIconTintColor);
        }

        private void OnLeadingIconTintColorChanged(Color tintColor)
        {
            leadingIcon.TintColor = tintColor;
        }

        private void OnPlaceholderChanged(string placeholderText)
        {
            placeholder.Text = placeholderText;
        }

        private void OnPlaceholderColorChanged(Color placeholderColor)
        {
            placeholder.TextColor = placeholderColor;
        }

        private void OnPlaceholderFontFamilyChanged(string fontFamily)
        {
            placeholder.FontFamily = fontFamily;
        }

        private void OnDateChanged(DateTime? date)
        {
            datePicker.NullableDate = date;
            clearIcon.IsVisible = date.HasValue;
            AnimateToInactiveOrFocusedStateOnStart(this);
        }

        private void OnTextColorChanged(Color textColor)
        {
            datePicker.TextColor = trailingIcon.TintColor = textColor;
        }

        private void OnTextFontFamilyChanged(string fontFamily)
        {
            datePicker.FontFamily = fontFamily;
        }

        private void OnTextFontSizeChanged(double fontSize)
        {
            placeholder.FontSize = datePicker.FontSize = fontSize;
        }

        private void OnTintColorChanged(Color tintColor)
        {
            datePicker.TintColor = tintColor;
        }

        private void OnUnderlineColorChanged(Color underlineColor)
        {
            if (AlwaysShowUnderline)
            {
                persistentUnderline.Color = underlineColor;
            }
        }

        private void SetControl()
        {
            trailingIcon.TintColor = TextColor;

            clearIcon.TintColor = TextColor;
            clearIcon.Source = ClearIcon;
            clearIcon.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    if (Date != null)
                    {
                        Date = null;
                    }
                })
            });

            persistentUnderline.Color = UnderlineColor;

            tapGesture.Command = new Command(() =>
            {
                if (!datePicker.IsFocused)
                {
                    datePicker.Focus();
                }
            });
        }

        private void SetPropertyChangeHandler(ref Dictionary<string, Action> propertyChangeActions)
        {
            propertyChangeActions = new Dictionary<string, Action>
            {
                { nameof(Date), () => OnDateChanged(Date) },
                { nameof(TextColor), () => OnTextColorChanged(TextColor) },
                { nameof(TextFontFamily), () => OnTextFontFamilyChanged(TextFontFamily) },
                { nameof(TintColor), () => OnTintColorChanged(TintColor) },
                { nameof(Placeholder), () => OnPlaceholderChanged(Placeholder) },
                { nameof(PlaceholderColor), () => OnPlaceholderColorChanged(PlaceholderColor) },
                { nameof(PlaceholderFontFamily), () => OnPlaceholderFontFamilyChanged(PlaceholderFontFamily) },
                { nameof(HelperText), () => OnHelperTextChanged(HelperText) },
                { nameof(HelperTextFontFamily), () => OnHelpertTextFontFamilyChanged(HelperTextFontFamily) },
                { nameof(HelperTextColor), () => OnHelperTextColorChanged(HelperTextColor) },
                { nameof(IsEnabled), () => OnEnabledChanged(IsEnabled) },
                { nameof(BackgroundColor), () => OnBackgroundColorChanged(BackgroundColor) },
                { nameof(AlwaysShowUnderline), () => OnAlwaysShowUnderlineChanged(AlwaysShowUnderline) },
                { nameof(ErrorColor), () => OnErrorColorChanged(ErrorColor) },
                { nameof(UnderlineColor), () => OnUnderlineColorChanged(UnderlineColor) },
                { nameof(HasError), () => OnHasErrorChanged() },
                { nameof(FloatingPlaceholderEnabled), () => OnFloatingPlaceholderEnabledChanged(FloatingPlaceholderEnabled) },
                { nameof(LeadingIcon), () => OnLeadingIconChanged(LeadingIcon) },
                { nameof(LeadingIconTintColor), () => OnLeadingIconTintColorChanged(LeadingIconTintColor) },
                { nameof(TextFontSize), () => OnTextFontSizeChanged(TextFontSize) },
                { nameof(ErrorText), () => OnErrorTextChanged() }
            };
        }
    }
}