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
            this.InitializeComponent();
            this.SetPropertyChangeHandler(ref this._propertyChangeActions);
            this.SetControl();

            this.datePicker.PropertyChanged += this.DatePicker_PropertyChanged;
            this.datePicker.DateSelected += this.DatePicker_DateChanged;
            this.datePicker.SizeChanged += this.Entry_SizeChanged;
            this.datePicker.Focused += this.DatePicker_Focused;
            this.datePicker.Unfocused += this.DatePicker_Unfocused;

            DeviceDisplay.MainDisplayInfoChanged += this.DeviceDisplay_MainDisplayInfoChanged;
            this._lastDeviceDisplay = DeviceDisplay.MainDisplayInfo;
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
                return (bool)this.GetValue(AlwaysShowUnderlineProperty);
            }

            set
            {
                this.SetValue(AlwaysShowUnderlineProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the background color of this text field.
        /// </summary>
        public new Color BackgroundColor
        {
            get
            {
                return (Color)this.GetValue(BackgroundColorProperty);
            }

            set
            {
                this.SetValue(BackgroundColorProperty, value);
            }
        }

        public ImageSource ClearIcon
        {
            get
            {
                return (string)this.GetValue(ClearIconProperty);
            }

            set
            {
                this.SetValue(ClearIconProperty, value);
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
                return (Color)this.GetValue(ErrorColorProperty);
            }

            set
            {
                this.SetValue(ErrorColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the error text of this text field.
        /// </summary>
        public string ErrorText
        {
            get
            {
                return (string)this.GetValue(ErrorTextProperty);
            }

            set
            {
                this.SetValue(ErrorTextProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the floating placeholder.
        /// </summary>
        public Color FloatingPlaceholderColor
        {
            get
            {
                return (Color)this.GetValue(FloatingPlaceholderColorProperty);
            }

            set
            {
                this.SetValue(FloatingPlaceholderColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets whether the placeholder label will float at top of the text field when focused or when it has text.
        /// </summary>
        public bool FloatingPlaceholderEnabled
        {
            get
            {
                return (bool)this.GetValue(FloatingPlaceholderEnabledProperty);
            }

            set
            {
                this.SetValue(FloatingPlaceholderEnabledProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the font size of the floating placeholder.
        /// </summary>
        public double FloatingPlaceholderFontSize
        {
            get
            {
                return (double)this.GetValue(FloatingPlaceholderFontSizeProperty);
            }

            set
            {
                this.SetValue(FloatingPlaceholderFontSizeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when this text field receives or loses focus.
        /// </summary>
        public Command<bool> FocusCommand
        {
            get
            {
                return (Command<bool>)this.GetValue(FocusCommandProperty);
            }

            set
            {
                this.SetValue(FocusCommandProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the boolean value whether this text field has an error, and if it will show the its error text.
        /// </summary>
        public bool HasError
        {
            get
            {
                return (bool)this.GetValue(HasErrorProperty);
            }

            set
            {
                this.SetValue(HasErrorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the helper text of this text field.
        /// </summary>
        public string HelperText
        {
            get
            {
                return (string)this.GetValue(HelperTextProperty);
            }

            set
            {
                this.SetValue(HelperTextProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of this text field's helper text.
        /// </summary>
        public Color HelperTextColor
        {
            get
            {
                return (Color)this.GetValue(HelperTextColorProperty);
            }

            set
            {
                this.SetValue(HelperTextColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the font family of this text field's helper text.
        /// </summary>
        public string HelperTextFontFamily
        {
            get
            {
                return (string)this.GetValue(HelperTextFontFamilyProperty);
            }

            set
            {
                this.SetValue(HelperTextFontFamilyProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the horizontal padding of the text field.
        /// </summary>
        public MaterialHorizontalThickness HorizontalPadding
        {
            get
            {
                return (MaterialHorizontalThickness)this.GetValue(HorizontalPaddingProperty);
            }

            set
            {
                this.SetValue(HorizontalPaddingProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the image source of the icon to be showed at the left side of this text field.
        /// </summary>
        public string LeadingIcon
        {
            get
            {
                return (string)this.GetValue(LeadingIconProperty);
            }

            set
            {
                this.SetValue(LeadingIconProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the tint color of the icon of this text field.
        /// </summary>
        public Color LeadingIconTintColor
        {
            get
            {
                return (Color)this.GetValue(LeadingIconTintColorProperty);
            }

            set
            {
                this.SetValue(LeadingIconTintColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the text of this text field's placeholder.
        /// </summary>
        public string Placeholder
        {
            get
            {
                return (string)this.GetValue(PlaceholderProperty);
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException($"{nameof(this.Placeholder)} must not be null, empty, or a white space.");
                }

                this.SetValue(PlaceholderProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of this text field's placeholder.
        /// </summary>
        public Color PlaceholderColor
        {
            get
            {
                return (Color)this.GetValue(PlaceholderColorProperty);
            }

            set
            {
                this.SetValue(PlaceholderColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the font family of this text field's placeholder
        /// </summary>
        public string PlaceholderFontFamily
        {
            get
            {
                return (string)this.GetValue(PlaceholderFontFamilyProperty);
            }

            set
            {
                this.SetValue(PlaceholderFontFamilyProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets whether the underline indicator will be animated. If set to false, the underline will not be shown.
        /// </summary>
        public bool ShouldAnimateUnderline
        {
            get
            {
                return (bool)this.GetValue(ShouldAnimateUnderlineProperty);
            }

            set
            {
                this.SetValue(ShouldAnimateUnderlineProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime? Date
        {
            get
            {
                return (DateTime?)this.GetValue(DateProperty);
            }

            set
            {
                if (value.HasValue && !this.FloatingPlaceholderEnabled)
                {
                    this.placeholder.IsVisible = false;
                }
                else if (!value.HasValue && !this.FloatingPlaceholderEnabled)
                {
                    this.placeholder.IsVisible = true;
                }

                this.SetValue(DateProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the command that will execute if there is a change in this text field's input text.
        /// </summary>
        public Command<DateTime?> DateChangeCommand
        {
            get
            {
                return (Command<DateTime?>)this.GetValue(DateChangeCommandProperty);
            }

            set
            {
                this.SetValue(DateChangeCommandProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of this text field's input text.
        /// </summary>
        public Color TextColor
        {
            get
            {
                return (Color)this.GetValue(TextColorProperty);
            }

            set
            {
                this.SetValue(TextColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the font family of this text field's input text.
        /// </summary>
        public string TextFontFamily
        {
            get
            {
                return (string)this.GetValue(TextFontFamilyProperty);
            }

            set
            {
                this.SetValue(TextFontFamilyProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the text's font size.
        /// </summary>
        public double TextFontSize
        {
            get
            {
                return (double)this.GetValue(TextFontSizeProperty);
            }

            set
            {
                this.SetValue(TextFontSizeProperty, value);
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
                return (Color)this.GetValue(TintColorProperty);
            }

            set
            {
                this.SetValue(TintColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the underline when this text field is activated. <see cref="AlwaysShowUnderline"/> is set to true.
        /// </summary>
        public Color UnderlineColor
        {
            get
            {
                return (Color)this.GetValue(UnderlineColorProperty);
            }

            set
            {
                this.SetValue(UnderlineColorProperty, value);
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
            this.datePicker.Focus();
        }

        /// <summary>
        /// Requests to unset the focus on this text field.
        /// </summary>
        public new void Unfocus()
        {
            this.datePicker.Unfocus();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            this.AnimateToInactiveOrFocusedStateOnStart(this.BindingContext);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            this.AnimateToInactiveOrFocusedStateOnStart(this.Parent);
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

            if (this._propertyChangeActions != null && this._propertyChangeActions.TryGetValue(propertyName, out Action handlePropertyChange))
            {
                handlePropertyChange();
            }
        }

        private void AnimateToActivatedState()
        {
            Animation anim = new Animation();
            bool hasDate = this.Date.HasValue;

            if (this.datePicker.IsFocused)
            {
                Color tintColor = this.HasError ? this.ErrorColor : this.TintColor;

                if (this.ShouldAnimateUnderline)
                {
                    anim.Add(0.0, AnimationDuration, new Animation(v => this.underline.HeightRequest = v, 1, 2, this._animationCurve, () =>
                    {
                        this.underline.Color = tintColor;
                    }));
                }

                this.placeholder.TextColor = tintColor;
            }
            else
            {
                Color underlineColor = this.HasError ? this.ErrorColor : this.UnderlineColor;
                Color placeholderColor = this.HasError ? this.ErrorColor : this.FloatingPlaceholderColor;

                int endHeight = hasDate ? 1 : 0;

                if (this.ShouldAnimateUnderline)
                {
                    anim.Add(0.0, AnimationDuration, new Animation(v => this.underline.HeightRequest = v, this.underline.HeightRequest, endHeight, this._animationCurve, () =>
                    {
                        this.underline.Color = underlineColor;
                    }));
                }

                this.placeholder.TextColor = placeholderColor;
            }

            anim.Commit(this, "UnfocusAnimation", rate: 2, length: (uint)(Device.RuntimePlatform == Device.iOS ? 500 : AnimationDuration * 1000), easing: this._animationCurve);
        }

        private void AnimateToInactiveOrFocusedState()
        {
            Color tintColor;
            double preferredStartFont = this.FloatingPlaceholderFontSize == 0 ? this.datePicker.FontSize * 0.75 : this.FloatingPlaceholderFontSize;
            double preferredEndFont = this.FloatingPlaceholderFontSize == 0 ? this.datePicker.FontSize * 0.75 : this.FloatingPlaceholderFontSize;
            double startFont = this.datePicker.IsFocused ? this.datePicker.FontSize : preferredStartFont;
            double endFOnt = this.datePicker.IsFocused ? preferredEndFont : this.datePicker.FontSize;
            double startY = this.placeholder.TranslationY;
            double endY = this.datePicker.IsFocused ? -(this.datePicker.FontSize * 0.8) : 0;

            if (this.HasError)
            {
                tintColor = this.datePicker.IsFocused ? this.ErrorColor : this.PlaceholderColor;
            }
            else
            {
                tintColor = this.datePicker.IsFocused ? this.TintColor : this.PlaceholderColor;
            }

            Animation anim = this.FloatingPlaceholderEnabled ? new Animation
            {
                {
                    0.0,
                    AnimationDuration,
                    new Animation(v => this.placeholder.FontSize = v, startFont, endFOnt, this._animationCurve)
                },
                {
                    0.0,
                    AnimationDuration,
                    new Animation(v => this.placeholder.TranslationY = v, startY, endY, this._animationCurve, () =>
                    {
                        if(this.HasError && this.datePicker.IsFocused)
                        {
                            this.placeholder.TextColor = this.ErrorColor;
                        }

                        this.placeholder.TextColor = tintColor;
                    })
                }
            } : new Animation();

            if (this.datePicker.IsFocused)
            {
                if (this.ShouldAnimateUnderline)
                {
                    this.underline.Color = this.HasError ? this.ErrorColor : this.TintColor;

                    anim.Add(0.0, AnimationDuration, new Animation(v => this.underline.WidthRequest = v, 0, this.Width, this._animationCurve, () =>
                    {
                        this.underline.WidthRequest = -1;
                        this.underline.HorizontalOptions = LayoutOptions.FillAndExpand;
                    }));
                }
            }
            else
            {
                if (this.ShouldAnimateUnderline)
                {
                    anim.Add(0.0, AnimationDuration, new Animation(v => this.underline.HeightRequest = v, this.underline.HeightRequest, 0, this._animationCurve, () =>
                    {
                        this.underline.WidthRequest = 0;
                        this.underline.HeightRequest = 2;
                        this.underline.HorizontalOptions = LayoutOptions.Center;
                    }));
                }
            }

            anim.Commit(this, "FocusAnimation", rate: 2, length: (uint)(Device.RuntimePlatform == Device.iOS ? 500 : AnimationDuration * 1000), easing: this._animationCurve);
        }

        private void AnimateToInactiveOrFocusedStateOnStart(object startObject)
        {
            double placeholderEndY = -(this.datePicker.FontSize * 0.8);
            double placeholderEndFont = this.datePicker.FontSize * 0.75;

            if (!this.FloatingPlaceholderEnabled && !this.datePicker.NullableDate.HasValue)
            {
                this.placeholder.TextColor = this.PlaceholderColor;
            }

            if (startObject != null && this.Date.HasValue /*&& !this._wasFocused*/)
            {
                this.datePicker.Opacity = 0;

                Device.BeginInvokeOnMainThread(() =>
                {
                    Animation anim = new Animation();

                    if (this.FloatingPlaceholderEnabled)
                    {
                        anim.Add(0.0, AnimationDuration, new Animation(v => this.placeholder.FontSize = v, this.datePicker.FontSize, placeholderEndFont, this._animationCurve));
                        anim.Add(0.0, AnimationDuration, new Animation(v => this.placeholder.TranslationY = v, this.placeholder.TranslationY, placeholderEndY, this._animationCurve, () =>
                        {
                            this.placeholder.TextColor = this.HasError ? this.ErrorColor : this.FloatingPlaceholderColor;
                            this.datePicker.Opacity = 1;
                        }));
                    }

                    if (this.ShouldAnimateUnderline)
                    {
                        this.underline.Color = this.HasError ? this.ErrorColor : this.TintColor;
                        this.underline.HeightRequest = 1;
                        anim.Add(0.0, AnimationDuration, new Animation(v => this.underline.WidthRequest = v, 0, this.Width, this._animationCurve, () => this.underline.HorizontalOptions = LayoutOptions.FillAndExpand));
                    }

                    anim.Commit(this, "Anim2", rate: 2, length: (uint)(AnimationDuration * 1000), easing: this._animationCurve);
                });

                this.datePicker.Opacity = 1;

                return;
            }

            if (startObject != null && !this.Date.HasValue && this.placeholder.TranslationY == placeholderEndY)
            {
                if (this.datePicker.IsFocused)
                {
                    return;
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    Animation anim = new Animation();

                    if (this.FloatingPlaceholderEnabled)
                    {
                        anim.Add(0.0, AnimationDuration, new Animation(v => this.placeholder.FontSize = v, placeholderEndFont, this.datePicker.FontSize, this._animationCurve));
                        anim.Add(0.0, AnimationDuration, new Animation(v => this.placeholder.TranslationY = v, this.placeholder.TranslationY, 0, this._animationCurve, () =>
                        {
                            this.placeholder.TextColor = this.PlaceholderColor;
                            this.datePicker.Opacity = 1;
                        }));
                    }

                    if (this.ShouldAnimateUnderline)
                    {
                        anim.Add(0.0, AnimationDuration, new Animation(v => this.underline.WidthRequest = v, this.Width, 0, this._animationCurve, () => this.underline.HorizontalOptions = LayoutOptions.Center));
                    }

                    anim.Commit(this, "Anim2", rate: 2, length: (uint)(AnimationDuration * 1000), easing: this._animationCurve);
                });
            }
        }

        private void ChangeToErrorState()
        {
            const int animDuration = 250;

            this.placeholder.TextColor = (this.FloatingPlaceholderEnabled && this.datePicker.IsFocused) || (this.FloatingPlaceholderEnabled && this.datePicker.NullableDate.HasValue) ? this.ErrorColor : this.PlaceholderColor;
            this.counter.TextColor = this.ErrorColor;
            this.underline.Color = this.ShouldAnimateUnderline ? this.ErrorColor : Color.Transparent;
            this.persistentUnderline.Color = this.AlwaysShowUnderline ? this.ErrorColor : Color.Transparent;
            this.trailingIcon.IsVisible = true;
            this.trailingIcon.Source = "xf_error";
            this.trailingIcon.TintColor = this.ErrorColor;

            if (string.IsNullOrEmpty(this.ErrorText))
            {
                this.helper.TextColor = this.ErrorColor;
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.helper.FadeTo(0, animDuration / 2, this._animationCurve);
                    this.helper.TranslationY = -4;
                    this.helper.TextColor = this.ErrorColor;
                    this.helper.Text = this.ErrorText;
                    await Task.WhenAll(this.helper.FadeTo(1, animDuration / 2, this._animationCurve), this.helper.TranslateTo(0, 0, animDuration / 2, this._animationCurve));
                });
            }
        }

        private void ChangeToNormalState()
        {
            const double opactiy = 1;

            this.IsEnabled = true;
            this.datePicker.Opacity = opactiy;
            this.placeholder.Opacity = opactiy;
            this.helper.Opacity = opactiy;
            this.underline.Opacity = opactiy;

            Device.BeginInvokeOnMainThread(async () =>
            {

                this.trailingIcon.IsVisible = false;

                Color accentColor = this.TintColor;
                this.placeholder.TextColor = accentColor;
                this.counter.TextColor = this.HelperTextColor;
                this.underline.Color = accentColor;
                this.persistentUnderline.Color = this.UnderlineColor;

                if (string.IsNullOrEmpty(this.ErrorText))
                {
                    this.helper.TextColor = this.HelperTextColor;
                }
                else
                {
                    await this.helper.FadeTo(0, 150, this._animationCurve);
                    this.helper.TranslationY = -4;
                    this.helper.TextColor = this.HelperTextColor;
                    this.helper.Text = this.HelperText;
                    await Task.WhenAll(this.helper.FadeTo(1, 150, this._animationCurve), this.helper.TranslateTo(0, 0, 150, this._animationCurve));
                }
            });
        }

        private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            if (e.DisplayInfo.Orientation != this._lastDeviceDisplay.Orientation)
            {
                if (this.datePicker.NullableDate.HasValue && this.ShouldAnimateUnderline)
                {
                    this.underline.WidthRequest = -1;
                    this.underline.HorizontalOptions = LayoutOptions.FillAndExpand;
                }

                this._lastDeviceDisplay = e.DisplayInfo;
            }
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            this.Completed?.Invoke(this, EventArgs.Empty);
        }

        private void DatePicker_Focused(object sender, FocusEventArgs e)
        {
            this._wasFocused = true;
            this.FocusCommand?.Execute(this.datePicker.IsFocused);
            this.Focused?.Invoke(this, e);
        }

        private void DatePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.IsFocused) when !this.datePicker.NullableDate.HasValue:
                    this.AnimateToInactiveOrFocusedState();
                    break;

                case nameof(this.IsFocused) when this.datePicker.NullableDate.HasValue:
                    this.AnimateToActivatedState();
                    break;
            }
        }

        private void Entry_SizeChanged(object sender, EventArgs e)
        {
            int baseHeight = this.FloatingPlaceholderEnabled ? 56 : 40;
            double diff = this.datePicker.Height - 20;
            double rawRowHeight = baseHeight + diff;
            this._autoSizingRow.Height = new GridLength(rawRowHeight);

            double iconVerticalMargin = (this._autoSizingRow.Height.Value - 24) / 2;

            if (this.leadingIcon.IsVisible)
            {
                this.leadingIcon.Margin = new Thickness(this.HorizontalPadding.Left, iconVerticalMargin, 0, iconVerticalMargin);
                this.datePicker.Margin = new Thickness(12, this.datePicker.Margin.Top, this.HorizontalPadding.Right, this.datePicker.Margin.Bottom);
            }
            else
            {
                this.datePicker.Margin = new Thickness(this.HorizontalPadding.Left, this.datePicker.Margin.Top, this.HorizontalPadding.Right, this.datePicker.Margin.Bottom);
            }

            if (this.trailingIcon.IsVisible)
            {
                MaterialHorizontalThickness entryPaddingLeft = this.leadingIcon.IsVisible ? 12 : this.HorizontalPadding;
                this.trailingIcon.Margin = new Thickness(12, iconVerticalMargin, this.HorizontalPadding.Right, iconVerticalMargin);
                this.datePicker.Margin = new Thickness(entryPaddingLeft.Left, this.datePicker.Margin.Top, 0, this.datePicker.Margin.Bottom);
            }

            this.helper.Margin = new Thickness(this.HorizontalPadding.Left, this.helper.Margin.Top, 12, 0);
            this.counter.Margin = new Thickness(0, this.counter.Margin.Top, this.HorizontalPadding.Right, 0);

            double placeholderLeftMargin = this.FloatingPlaceholderEnabled ? this.HorizontalPadding.Left : this.datePicker.Margin.Left;
            this.placeholder.Margin = new Thickness(placeholderLeftMargin, 0, 0, 0);

            if (this.HasError)
            {
                this.underline.Color = this.ErrorColor;
            }
        }

        private void DatePicker_DateChanged(object sender, NullableDateChangedEventArgs e)
        {
            this.Date = this.datePicker.NullableDate;
            this.DateChangeCommand?.Execute(this.datePicker.NullableDate);
            this.DateChanged?.Invoke(this, e);
        }

        private void DatePicker_Unfocused(object sender, FocusEventArgs e)
        {
            this.FocusCommand?.Execute(this.datePicker.IsFocused);
            this.Unfocused?.Invoke(this, e);
        }


        private void OnAlwaysShowUnderlineChanged(bool isShown)
        {
            this.persistentUnderline.IsVisible = isShown;
            this.persistentUnderline.Color = this.UnderlineColor;
        }

        private void OnBackgroundColorChanged(Color backgroundColor)
        {
            this.backgroundCard.BackgroundColor = backgroundColor;
        }


        private void OnEnabledChanged(bool isEnabled)
        {
            this.Opacity = isEnabled ? 1 : 0.33;
            this.helper.IsVisible = isEnabled && !string.IsNullOrEmpty(this.HelperText);
        }

        private void OnErrorColorChanged(Color errorColor)
        {
            this.trailingIcon.TintColor = errorColor;
        }

        private void OnErrorTextChanged()
        {
            if (this.HasError)
            {
                this.ChangeToErrorState();
            }
        }

        private void OnFloatingPlaceholderEnabledChanged(bool isEnabled)
        {
            double marginTopVariation = Device.RuntimePlatform == Device.iOS ? 18 : 20;
            this.datePicker.Margin = isEnabled ? new Thickness(this.datePicker.Margin.Left, 24, this.datePicker.Margin.Right, 0) : new Thickness(this.datePicker.Margin.Left, marginTopVariation - 9, this.datePicker.Margin.Right, 0);

            Thickness iconMargin = this.leadingIcon.Margin;
            this.leadingIcon.Margin = isEnabled ? new Thickness(iconMargin.Left, 16, iconMargin.Right, 16) : new Thickness(iconMargin.Left, 8, iconMargin.Right, 8);

            Thickness trailingIconMargin = this.trailingIcon.Margin;
            this.trailingIcon.Margin = isEnabled ? new Thickness(trailingIconMargin.Left, 16, trailingIconMargin.Right, 16) : new Thickness(trailingIconMargin.Left, 8, trailingIconMargin.Right, 8);
        }

        private void OnHasErrorChanged()
        {
            if (this.HasError)
            {
                this.ChangeToErrorState();
            }
            else
            {
                this.ChangeToNormalState();
            }
        }

        private void OnHelperTextChanged(string helperText)
        {
            this.helper.Text = helperText;
            this.helper.IsVisible = !string.IsNullOrEmpty(helperText);
        }

        private void OnHelperTextColorChanged(Color textColor)
        {
            this.helper.TextColor = this.counter.TextColor = textColor;
        }

        private void OnHelpertTextFontFamilyChanged(string fontFamily)
        {
            this.helper.FontFamily = this.counter.FontFamily = fontFamily;
        }

        private void OnLeadingIconChanged(string icon)
        {
            this.leadingIcon.Source = icon;
            this.OnLeadingIconTintColorChanged(this.LeadingIconTintColor);
        }

        private void OnLeadingIconTintColorChanged(Color tintColor)
        {
            this.leadingIcon.TintColor = tintColor;
        }

        private void OnPlaceholderChanged(string placeholderText)
        {
            this.placeholder.Text = placeholderText;
        }

        private void OnPlaceholderColorChanged(Color placeholderColor)
        {
            this.placeholder.TextColor = placeholderColor;
        }

        private void OnPlaceholderFontFamilyChanged(string fontFamily)
        {
            this.placeholder.FontFamily = fontFamily;
        }

        private void OnDateChanged(DateTime? date)
        {
            this.datePicker.NullableDate = date;
            this.clearIcon.IsVisible = date.HasValue;
            this.AnimateToInactiveOrFocusedStateOnStart(this);
        }

        private void OnTextColorChanged(Color textColor)
        {
            this.datePicker.TextColor = this.trailingIcon.TintColor = textColor;
        }

        private void OnTextFontFamilyChanged(string fontFamily)
        {
            this.datePicker.FontFamily = fontFamily;
        }

        private void OnTextFontSizeChanged(double fontSize)
        {
            this.placeholder.FontSize = this.datePicker.FontSize = fontSize;
        }

        private void OnTintColorChanged(Color tintColor)
        {
            this.datePicker.TintColor = tintColor;
        }

        private void OnUnderlineColorChanged(Color underlineColor)
        {
            if (this.AlwaysShowUnderline)
            {
                this.persistentUnderline.Color = underlineColor;
            }
        }

        private void SetControl()
        {
            this.trailingIcon.TintColor = this.TextColor;

            this.clearIcon.TintColor = this.TextColor;
            this.clearIcon.Source = this.ClearIcon;
            this.clearIcon.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    if (this.Date != null)
                    {
                        this.Date = null;
                    }
                })
            });

            this.persistentUnderline.Color = this.UnderlineColor;

            this.tapGesture.Command = new Command(() =>
            {
                if (!this.datePicker.IsFocused)
                {
                    this.datePicker.Focus();
                }
            });
        }

        private void SetPropertyChangeHandler(ref Dictionary<string, Action> propertyChangeActions)
        {
            propertyChangeActions = new Dictionary<string, Action>
            {
                { nameof(this.Date), () => this.OnDateChanged(this.Date) },
                { nameof(this.TextColor), () => this.OnTextColorChanged(this.TextColor) },
                { nameof(this.TextFontFamily), () => this.OnTextFontFamilyChanged(this.TextFontFamily) },
                { nameof(this.TintColor), () => this.OnTintColorChanged(this.TintColor) },
                { nameof(this.Placeholder), () => this.OnPlaceholderChanged(this.Placeholder) },
                { nameof(this.PlaceholderColor), () => this.OnPlaceholderColorChanged(this.PlaceholderColor) },
                { nameof(this.PlaceholderFontFamily), () => this.OnPlaceholderFontFamilyChanged(this.PlaceholderFontFamily) },
                { nameof(this.HelperText), () => this.OnHelperTextChanged(this.HelperText) },
                { nameof(this.HelperTextFontFamily), () => this.OnHelpertTextFontFamilyChanged(this.HelperTextFontFamily) },
                { nameof(this.HelperTextColor), () => this.OnHelperTextColorChanged(this.HelperTextColor) },
                { nameof(this.IsEnabled), () => this.OnEnabledChanged(this.IsEnabled) },
                { nameof(this.BackgroundColor), () => this.OnBackgroundColorChanged(this.BackgroundColor) },
                { nameof(this.AlwaysShowUnderline), () => this.OnAlwaysShowUnderlineChanged(this.AlwaysShowUnderline) },
                { nameof(this.ErrorColor), () => this.OnErrorColorChanged(this.ErrorColor) },
                { nameof(this.UnderlineColor), () => this.OnUnderlineColorChanged(this.UnderlineColor) },
                { nameof(this.HasError), () => this.OnHasErrorChanged() },
                { nameof(this.FloatingPlaceholderEnabled), () => this.OnFloatingPlaceholderEnabledChanged(this.FloatingPlaceholderEnabled) },
                { nameof(this.LeadingIcon), () => this.OnLeadingIconChanged(this.LeadingIcon) },
                { nameof(this.LeadingIconTintColor), () => this.OnLeadingIconTintColorChanged(this.LeadingIconTintColor) },
                { nameof(this.TextFontSize), () => this.OnTextFontSizeChanged(this.TextFontSize) },
                { nameof(this.ErrorText), () => this.OnErrorTextChanged() }
            };
        }
    }
}