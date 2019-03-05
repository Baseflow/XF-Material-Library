using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Internals;

namespace XF.Material.Forms.UI
{
    /// <inheritdoc />
    /// <summary>
    /// A control that let users enter and edit text.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialTextField : ContentView, IMaterialElementConfiguration
    {
        public static readonly BindableProperty AlwaysShowUnderlineProperty = BindableProperty.Create(nameof(AlwaysShowUnderline), typeof(bool), typeof(MaterialTextField), false);

        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#DCDCDC"));

        public static readonly BindableProperty ChoiceSelectedCommandProperty = BindableProperty.Create(nameof(ChoiceSelectedCommand), typeof(ICommand), typeof(MaterialTextField));

        public static readonly BindableProperty ChoicesProperty = BindableProperty.Create(nameof(Choices), typeof(IList), typeof(MaterialTextField));

        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(MaterialTextField), Material.Color.Error);

        public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(nameof(ErrorText), typeof(string), typeof(MaterialTextField));

        public static readonly BindableProperty FloatingPlaceholderColorProperty = BindableProperty.Create(nameof(FloatingPlaceholderColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#99000000"));

        public static readonly BindableProperty FloatingPlaceholderEnabledProperty = BindableProperty.Create(nameof(FloatingPlaceholderEnabled), typeof(bool), typeof(MaterialTextField), true);

        public static readonly BindableProperty FloatingPlaceholderFontSizeProperty = BindableProperty.Create(nameof(FloatingPlaceholderFontSize), typeof(double), typeof(MaterialTextField), 0d);

        public static readonly BindableProperty FocusCommandProperty = BindableProperty.Create(nameof(FocusCommand), typeof(Command<bool>), typeof(MaterialTextField));

        public static readonly BindableProperty HasErrorProperty = BindableProperty.Create(nameof(HasError), typeof(bool), typeof(MaterialTextField), false);

        public static readonly BindableProperty HelperTextColorProperty = BindableProperty.Create(nameof(HelperTextColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#99000000"));

        public static readonly BindableProperty HelperTextFontFamilyProperty = BindableProperty.Create(nameof(HelperTextFontFamily), typeof(string), typeof(MaterialTextField));

        public static readonly BindableProperty HelperTextProperty = BindableProperty.Create(nameof(HelperText), typeof(string), typeof(MaterialTextField), string.Empty);

        public static readonly BindableProperty HorizontalPaddingProperty = BindableProperty.Create(nameof(HorizontalPadding), typeof(MaterialHorizontalThickness), typeof(MaterialTextField), new MaterialHorizontalThickness(12d), defaultBindingMode: BindingMode.OneTime);

        public static readonly BindableProperty LeadingIconProperty = BindableProperty.Create(nameof(LeadingIcon), typeof(string), typeof(MaterialTextField));

        public static readonly BindableProperty IconTintColorProperty = BindableProperty.Create(nameof(IconTintColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#99000000"));

        public static readonly BindableProperty InputTypeProperty = BindableProperty.Create(nameof(InputType), typeof(MaterialTextFieldInputType), typeof(MaterialTextField), MaterialTextFieldInputType.Default);

        public static readonly BindableProperty IsAutoCapitalizationEnabledProperty = BindableProperty.Create(nameof(IsAutoCapitalizationEnabled), typeof(bool), typeof(MaterialTextField), false);

        public static readonly BindableProperty IsSpellCheckEnabledProperty = BindableProperty.Create(nameof(IsSpellCheckEnabled), typeof(bool), typeof(MaterialTextField), false);

        public static readonly BindableProperty IsTextPredictionEnabledProperty = BindableProperty.Create(nameof(IsTextPredictionEnabled), typeof(bool), typeof(MaterialTextField), false);

        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(MaterialTextField), 0);

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#99000000"));

        public static readonly BindableProperty PlaceholderFontFamilyProperty = BindableProperty.Create(nameof(PlaceholderFontFamily), typeof(string), typeof(MaterialTextField));

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialTextField), string.Empty);

        public static readonly BindableProperty ReturnCommandParameterProperty = BindableProperty.Create(nameof(ReturnCommandParameter), typeof(object), typeof(MaterialTextField));

        public static readonly BindableProperty ReturnCommandProperty = BindableProperty.Create(nameof(ReturnCommand), typeof(ICommand), typeof(MaterialTextField));

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(MaterialTextField), ReturnType.Default);

        public static readonly BindableProperty ShouldAnimateUnderlineProperty = BindableProperty.Create(nameof(ShouldAnimateUnderline), typeof(bool), typeof(MaterialTextField), true);

        public static readonly BindableProperty TextChangeCommandProperty = BindableProperty.Create(nameof(TextChangeCommand), typeof(Command<string>), typeof(MaterialTextField));

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#D0000000"));

        public static readonly BindableProperty TextFontFamilyProperty = BindableProperty.Create(nameof(TextFontFamily), typeof(string), typeof(MaterialTextField));

        public static readonly BindableProperty TextFontSizeProperty = BindableProperty.Create(nameof(TextFontSize), typeof(double), typeof(MaterialTextField), 16d);

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialTextField), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialTextField), Material.Color.Secondary);

        public static readonly BindableProperty UnderlineColorProperty = BindableProperty.Create(nameof(UnderlineColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#99000000"));

        

        private const double AnimationDuration = 0.35;

        private readonly Easing _animationCurve = Easing.SinOut;

        private readonly Dictionary<string, Action> _propertyChangeActions;

        private IList<string> _choices;

        private bool _counterEnabled;

        private DisplayInfo _lastDeviceDisplay;

        private bool _wasFocused;

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialTextField"/>.
        /// </summary>
        public MaterialTextField()
        {
            this.InitializeComponent();
            this.SetPropertyChangeHandler(ref _propertyChangeActions);
            this.SetControl();
            _lastDeviceDisplay = DeviceDisplay.MainDisplayInfo;
        }

        public event EventHandler<SelectedItemChangedEventArgs> ChoiceSelected;

        /// <summary>
        /// Raised when this text field receives or loses focus.
        /// </summary>
        public new event EventHandler<FocusEventArgs> Focused;

        /// <summary>
        /// Raised when the input text of this text field has changed.
        /// </summary>
        public event EventHandler<TextChangedEventArgs> TextChanged;

        /// <summary>
        /// Gets or sets whether the underline accent of this text field should always show or not.
        /// </summary>
        public bool AlwaysShowUnderline
        {
            get => (bool)this.GetValue(AlwaysShowUnderlineProperty);
            set => this.SetValue(AlwaysShowUnderlineProperty, value);
        }

        /// <summary>
        /// Gets or sets the background color of this text field.
        /// </summary>
        public new Color BackgroundColor
        {
            get => (Color)this.GetValue(BackgroundColorProperty);
            set => this.SetValue(BackgroundColorProperty, value);
        }

        public IList Choices
        {
            get => (IList)this.GetValue(ChoicesProperty);
            set => this.SetValue(ChoicesProperty, value);
        }

        public string ChoicesBindingName { get; set; }

        public ICommand ChoiceSelectedCommand
        {
            get => (ICommand)this.GetValue(ChoiceSelectedCommandProperty);
            set => this.SetValue(ChoiceSelectedCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the color to indicate an error in this text field.
        /// The default value is the color of <see cref="MaterialColorConfiguration.Error"/>.
        /// </summary>
        public Color ErrorColor
        {
            get => (Color)this.GetValue(ErrorColorProperty);
            set => this.SetValue(ErrorColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the error text of this text field.
        /// </summary>
        public string ErrorText
        {
            get => (string)this.GetValue(ErrorTextProperty);
            set => this.SetValue(ErrorTextProperty, value);
        }

        public Color FloatingPlaceholderColor
        {
            get => (Color)this.GetValue(FloatingPlaceholderColorProperty);
            set => this.SetValue(FloatingPlaceholderColorProperty, value);
        }

        /// <summary>
        /// Gets or sets whether the placeholder label will float at top of the text field when focused or when it has text.
        /// </summary>
        public bool FloatingPlaceholderEnabled
        {
            get => (bool)this.GetValue(FloatingPlaceholderEnabledProperty);
            set => this.SetValue(FloatingPlaceholderEnabledProperty, value);
        }

        public double FloatingPlaceholderFontSize
        {
            get => (double)this.GetValue(FloatingPlaceholderFontSizeProperty);
            set => this.SetValue(FloatingPlaceholderFontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that will be executed when this text field receives or loses focus.
        /// </summary>
        public Command<bool> FocusCommand
        {
            get => (Command<bool>)this.GetValue(FocusCommandProperty);
            set => this.SetValue(FocusCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the boolean value whether this text field has an error, and if it will show the its error text.
        /// </summary>
        public bool HasError
        {
            get => (bool)this.GetValue(HasErrorProperty);
            set => this.SetValue(HasErrorProperty, value);
        }

        /// <summary>
        /// Gets or sets the helper text of this text field.
        /// </summary>
        public string HelperText
        {
            get => (string)this.GetValue(HelperTextProperty);
            set => this.SetValue(HelperTextProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of this text field's helper text.
        /// </summary>
        public Color HelperTextColor
        {
            get => (Color)this.GetValue(HelperTextColorProperty);
            set => this.SetValue(HelperTextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family of this text field's helper text.
        /// </summary>
        public string HelperTextFontFamily
        {
            get => (string)this.GetValue(HelperTextFontFamilyProperty);
            set => this.SetValue(HelperTextFontFamilyProperty, value);
        }

        public MaterialHorizontalThickness HorizontalPadding
        {
            get => (MaterialHorizontalThickness)this.GetValue(HorizontalPaddingProperty);
            set => this.SetValue(HorizontalPaddingProperty, value);
        }

        /// <summary>
        /// Gets or sets the image source of the icon to be showed at the left side of this text field.
        /// </summary>
        public string LeadingIcon
        {
            get => (string)this.GetValue(LeadingIconProperty);
            set => this.SetValue(LeadingIconProperty, value);
        }

        /// <summary>
        /// Gets or sets the tint color of the icon of this text field.
        /// </summary>
        public Color IconTintColor
        {
            get => (Color)this.GetValue(IconTintColorProperty);
            set => this.SetValue(IconTintColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the keyboard input type of this text field.
        /// </summary>
        public MaterialTextFieldInputType InputType
        {
            get => (MaterialTextFieldInputType)this.GetValue(InputTypeProperty);
            set => this.SetValue(InputTypeProperty, value);
        }

        public bool IsAutoCapitalizationEnabled
        {
            get => (bool)this.GetValue(IsAutoCapitalizationEnabledProperty);
            set => this.SetValue(IsAutoCapitalizationEnabledProperty, value);
        }

        public bool IsSpellCheckEnabled
        {
            get => (bool)this.GetValue(IsSpellCheckEnabledProperty);
            set => this.SetValue(IsSpellCheckEnabledProperty, value);
        }

        public bool IsTextPredictionEnabled
        {
            get => (bool)this.GetValue(IsTextPredictionEnabledProperty);
            set => this.SetValue(IsTextPredictionEnabledProperty, value);
        }

        /// <summary>
        /// Gets or sets the maximum allowed number of characters in this text field.
        /// </summary>
        public int MaxLength
        {
            get => (int)this.GetValue(MaxLengthProperty);
            set => this.SetValue(MaxLengthProperty, value);
        }

        /// <summary>
        /// Gets or sets the text of this text field's placeholder.
        /// </summary>
        public string Placeholder
        {
            get => (string)this.GetValue(PlaceholderProperty);
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
            get => (Color)this.GetValue(PlaceholderColorProperty);
            set => this.SetValue(PlaceholderColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family of this text field's placeholder
        /// </summary>
        public string PlaceholderFontFamily
        {
            get => (string)this.GetValue(PlaceholderFontFamilyProperty);
            set => this.SetValue(PlaceholderFontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that will run when the user returns the input in this textfield.
        /// </summary>
        public ICommand ReturnCommand
        {
            get => (ICommand)this.GetValue(ReturnCommandProperty);
            set => this.SetValue(ReturnCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the parameter of <see cref="ReturnCommand"/>.
        /// </summary>
        public object ReturnCommandParameter
        {
            get => this.GetValue(ReturnCommandParameterProperty);
            set => this.SetValue(ReturnCommandParameterProperty, value);
        }

        /// <summary>
        /// Gets or sets the return type of this textfield.
        /// </summary>
        public ReturnType ReturnType
        {
            get => (ReturnType)this.GetValue(ReturnTypeProperty);
            set => this.SetValue(ReturnTypeProperty, value);
        }

        public bool ShouldAnimateUnderline
        {
            get => (bool)this.GetValue(ShouldAnimateUnderlineProperty);
            set => this.SetValue(ShouldAnimateUnderlineProperty, value);
        }

        /// <summary>
        /// Gets or sets the input text of this text field.
        /// </summary>
        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set
            {
                if (!string.IsNullOrEmpty(value) && !this.FloatingPlaceholderEnabled)
                {
                    placeholder.IsVisible = false;
                }
                else if (string.IsNullOrEmpty(value) && !this.FloatingPlaceholderEnabled)
                {
                    placeholder.IsVisible = true;
                }

                this.SetValue(TextProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the command that will execute if there is a change in this text field's input text.
        /// </summary>
        public Command<string> TextChangeCommand
        {
            get => (Command<string>)this.GetValue(TextChangeCommandProperty);
            set => this.SetValue(TextChangeCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of this text field's input text.
        /// </summary>
        public Color TextColor
        {
            get => (Color)this.GetValue(TextColorProperty);
            set => this.SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family of this text field's input text.
        /// </summary>
        public string TextFontFamily
        {
            get => (string)this.GetValue(TextFontFamilyProperty);
            set => this.SetValue(TextFontFamilyProperty, value);
        }

        public double TextFontSize
        {
            get => (double)this.GetValue(TextFontSizeProperty);
            set => this.SetValue(TextFontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the tint color of the underline and the placeholder of this text field when focused.
        /// The default value is the color of <see cref="MaterialColorConfiguration.Secondary"/>.
        /// </summary>
        public Color TintColor
        {
            get => (Color)this.GetValue(TintColorProperty);
            set => this.SetValue(TintColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the underline when this text field is activated. <see cref="AlwaysShowUnderline"/> is set to true.
        /// </summary>
        public Color UnderlineColor
        {
            get => (Color)this.GetValue(UnderlineColorProperty);
            set => this.SetValue(UnderlineColorProperty, value);
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
                entry.PropertyChanged += this.Entry_PropertyChanged;
                entry.TextChanged += this.Entry_TextChanged;
                entry.SizeChanged += this.Entry_SizeChanged;
                entry.Focused += this.Entry_Focused;
                DeviceDisplay.MainDisplayInfoChanged += this.DeviceDisplay_MainDisplayInfoChanged;
            }
            else
            {
                entry.PropertyChanged -= this.Entry_PropertyChanged;
                entry.TextChanged -= this.Entry_TextChanged;
                entry.SizeChanged -= this.Entry_SizeChanged;
                entry.Focused -= this.Entry_Focused;
                DeviceDisplay.MainDisplayInfoChanged -= this.DeviceDisplay_MainDisplayInfoChanged;
            }
        }

        /// <summary>
        /// Requests to focus this text field.
        /// </summary>
        public new void Focus() => entry.Focus();

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

            if (propertyName == null) return;

            if (_propertyChangeActions != null && _propertyChangeActions.TryGetValue(propertyName, out var handlePropertyChange))
            {
                handlePropertyChange();
            }
        }

        private void AnimateToActivatedState()
        {
            var anim = new Animation();
            var hasText = !string.IsNullOrEmpty(this.Text);

            if (entry.IsFocused)
            {
                var tintColor = this.HasError ? this.ErrorColor : this.TintColor;

                if (this.ShouldAnimateUnderline)
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
                var underlineColor = this.HasError ? this.ErrorColor : this.UnderlineColor;
                var placeholderColor = this.HasError ? this.ErrorColor : this.FloatingPlaceholderColor;

                var endHeight = hasText ? 1 : 0;

                if (this.ShouldAnimateUnderline)
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
            double preferredStartFont = this.FloatingPlaceholderFontSize == 0 ? entry.FontSize * 0.75 : this.FloatingPlaceholderFontSize;
            double preferredEndFont = this.FloatingPlaceholderFontSize == 0 ? entry.FontSize * 0.75 : this.FloatingPlaceholderFontSize;
            double startFont = entry.IsFocused ? entry.FontSize : preferredStartFont;
            double endFOnt = entry.IsFocused ? preferredEndFont : entry.FontSize;
            var startY = placeholder.TranslationY;
            double endY = entry.IsFocused ? -(entry.FontSize * 0.8) : 0;

            if (this.HasError)
            {
                tintColor = entry.IsFocused ? this.ErrorColor : this.PlaceholderColor;
            }
            else
            {
                tintColor = entry.IsFocused ? this.TintColor : this.PlaceholderColor;
            }

            var anim = this.FloatingPlaceholderEnabled ? new Animation
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
                        if(this.HasError && entry.IsFocused)
                        {
                            placeholder.TextColor = this.ErrorColor;
                        }

                        placeholder.TextColor = tintColor;
                    })
                }
            } : new Animation();

            if (entry.IsFocused)
            {
                if (this.ShouldAnimateUnderline)
                {
                    underline.Color = this.HasError ? this.ErrorColor : this.TintColor;

                    anim.Add(0.0, AnimationDuration, new Animation(v => underline.WidthRequest = v, 0, this.Width, _animationCurve, () =>
                    {
                        underline.WidthRequest = -1;
                        underline.HorizontalOptions = LayoutOptions.FillAndExpand;
                    }));
                }
            }
            else
            {
                if (this.ShouldAnimateUnderline)
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
            var placeholderEndY = -(entry.FontSize * 0.8);
            var placeholderEndFont = entry.FontSize * 0.75;

            if (!this.FloatingPlaceholderEnabled && string.IsNullOrEmpty(entry.Text))
            {
                placeholder.TextColor = this.PlaceholderColor;
            }

            if (startObject != null && !string.IsNullOrEmpty(this.Text) && !_wasFocused)
            {
                if (placeholder.TranslationY == placeholderEndY)
                {
                    return;
                }
                entry.Opacity = 0;

                Device.BeginInvokeOnMainThread(() =>
                {
                    var anim = new Animation();

                    if (this.FloatingPlaceholderEnabled)
                    {
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.FontSize = v, entry.FontSize, placeholderEndFont, _animationCurve));
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.TranslationY = v, placeholder.TranslationY, placeholderEndY, _animationCurve, () =>
                        {
                            placeholder.TextColor = this.HasError ? this.ErrorColor : this.FloatingPlaceholderColor;
                            entry.Opacity = 1;
                        }));
                    }

                    if (this.ShouldAnimateUnderline)
                    {
                        underline.HeightRequest = 1;
                        anim.Add(0.0, AnimationDuration, new Animation(v => underline.WidthRequest = v, 0, this.Width, _animationCurve, () => underline.HorizontalOptions = LayoutOptions.FillAndExpand));
                    }

                    anim.Commit(this, "Anim2", rate: 2, length: (uint)(AnimationDuration * 1000), easing: _animationCurve);
                });

                entry.Opacity = 1;

                return;
            }

            if (startObject != null && string.IsNullOrEmpty(this.Text) && placeholder.TranslationY == placeholderEndY)
            {
                if (entry.IsFocused)
                {
                    return;
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    var anim = new Animation();

                    if (this.FloatingPlaceholderEnabled)
                    {
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.FontSize = v, placeholderEndFont, entry.FontSize, _animationCurve));
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.TranslationY = v, placeholder.TranslationY, 0, _animationCurve, () =>
                        {
                            placeholder.TextColor = this.PlaceholderColor;
                            entry.Opacity = 1;
                        }));
                    }

                    if (this.ShouldAnimateUnderline)
                    {
                        anim.Add(0.0, AnimationDuration, new Animation(v => underline.WidthRequest = v, this.Width, 0, _animationCurve, () => underline.HorizontalOptions = LayoutOptions.Center));
                    }

                    anim.Commit(this, "Anim2", rate: 2, length: (uint)(AnimationDuration * 1000), easing: _animationCurve);
                });
            }
        }

        private void ChangeToErrorState()
        {
            const int animDuration = 250;
            placeholder.TextColor = (this.FloatingPlaceholderEnabled && entry.IsFocused) || (this.FloatingPlaceholderEnabled && !string.IsNullOrEmpty(this.Text)) ? this.ErrorColor : this.PlaceholderColor;
            counter.TextColor = this.ErrorColor;
            underline.Color = this.ShouldAnimateUnderline ? this.ErrorColor : Color.Transparent;
            persistentUnderline.Color = this.AlwaysShowUnderline ? this.ErrorColor : Color.Transparent;
            trailingIcon.IsVisible = true;
            trailingIcon.Source = "xf_error";
            trailingIcon.TintColor = this.ErrorColor;

            if (string.IsNullOrEmpty(this.ErrorText))
            {
                helper.TextColor = this.ErrorColor;
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await helper.FadeTo(0, animDuration / 2, _animationCurve);
                    helper.TranslationY = -4;
                    helper.TextColor = this.ErrorColor;
                    helper.Text = this.ErrorText;
                    await Task.WhenAll(helper.FadeTo(1, animDuration / 2, _animationCurve), helper.TranslateTo(0, 0, animDuration / 2, _animationCurve));
                });
            }
        }

        private void ChangeToNormalState()
        {
            const double opactiy = 1;
            this.IsEnabled = true;
            entry.Opacity = opactiy;
            placeholder.Opacity = opactiy;
            helper.Opacity = opactiy;
            underline.Opacity = opactiy;

            Device.BeginInvokeOnMainThread(async () =>
            {
                if (this.InputType == MaterialTextFieldInputType.Choice)
                {
                    trailingIcon.Source = "xf_arrow_dropdown";
                    trailingIcon.TintColor = this.TextColor;
                }
                else
                {
                    trailingIcon.IsVisible = false;
                }

                var accentColor = this.TintColor;
                placeholder.TextColor = accentColor;
                counter.TextColor = this.HelperTextColor;
                underline.Color = accentColor;
                persistentUnderline.Color = this.UnderlineColor;

                if (string.IsNullOrEmpty(this.ErrorText))
                {
                    helper.TextColor = this.HelperTextColor;
                }
                else
                {
                    await helper.FadeTo(0, 150, _animationCurve);
                    helper.TranslationY = -4;
                    helper.TextColor = this.HelperTextColor;
                    helper.Text = this.HelperText;
                    await Task.WhenAll(helper.FadeTo(1, 150, _animationCurve), helper.TranslateTo(0, 0, 150, _animationCurve));
                }
            });
        }

        private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            if (e.DisplayInfo.Orientation != _lastDeviceDisplay.Orientation)
            {
                if (!string.IsNullOrEmpty(entry.Text) && this.ShouldAnimateUnderline)
                {
                    underline.WidthRequest = -1;
                    underline.HorizontalOptions = LayoutOptions.FillAndExpand;
                }

                _lastDeviceDisplay = e.DisplayInfo;
            }
        }

        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            _wasFocused = true;
            this.FocusCommand?.Execute(entry.IsFocused);
            this.Focused?.Invoke(this, e);
            this.UpdateCounter();
        }

        private void Entry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.IsFocused) when string.IsNullOrEmpty(entry.Text):
                    this.AnimateToInactiveOrFocusedState();
                    break;

                case nameof(this.IsFocused) when !string.IsNullOrEmpty(entry.Text):
                    this.AnimateToActivatedState();
                    break;

                case nameof(Entry.Text):
                    this.Text = entry.Text;
                    this.UpdateCounter();
                    break;
            }
        }

        private void Entry_SizeChanged(object sender, EventArgs e)
        {
            var baseHeight = this.FloatingPlaceholderEnabled ? 56 : 40;
            var diff = entry.Height - 20;
            var rawRowHeight = baseHeight + diff;
            _autoSizingRow.Height = new GridLength(rawRowHeight);

            double iconVerticalMargin = (_autoSizingRow.Height.Value - 24) / 2;

            if (leadingIcon.IsVisible)
            {
                leadingIcon.Margin = new Thickness(this.HorizontalPadding.Left, iconVerticalMargin, 0, iconVerticalMargin);
                entry.Margin = new Thickness(12, entry.Margin.Top, this.HorizontalPadding.Right, entry.Margin.Bottom);
            }
            else
            {
                entry.Margin = new Thickness(this.HorizontalPadding.Left, entry.Margin.Top, this.HorizontalPadding.Right, entry.Margin.Bottom);
            }

            if (trailingIcon.IsVisible)
            {
                var entryPaddingLeft = leadingIcon.IsVisible ? 12 : this.HorizontalPadding;
                trailingIcon.Margin = new Thickness(12, iconVerticalMargin, this.HorizontalPadding.Right, iconVerticalMargin);
                entry.Margin = new Thickness(entryPaddingLeft.Left, entry.Margin.Top, 0, entry.Margin.Bottom);
            }

            helper.Margin = new Thickness(this.HorizontalPadding.Left, helper.Margin.Top, 12, 0);
            counter.Margin = new Thickness(0, counter.Margin.Top, this.HorizontalPadding.Right, 0);

            var placeholderLeftMargin = this.FloatingPlaceholderEnabled ? this.HorizontalPadding.Left : entry.Margin.Left;
            placeholder.Margin = new Thickness(placeholderLeftMargin, 0, 0, 0);

            if (this.HasError)
            {
                underline.Color = this.ErrorColor;
            }
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.TextChangeCommand?.Execute(entry.Text);
            this.TextChanged?.Invoke(this, e);
        }

        private IList<string> GetChoices()
        {
            var choiceStrings = new List<string>(this.Choices.Count);
            var listType = this.Choices[0].GetType();
            foreach (var item in this.Choices)
            {
                if (!string.IsNullOrEmpty(this.ChoicesBindingName))
                {
                    var propInfo = listType.GetProperty(this.ChoicesBindingName);

                    if (propInfo == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Property {this.ChoicesBindingName} was not found for item in {this.Choices}.");
                        choiceStrings.Add(item.ToString());
                    }
                    else
                    {
                        var propValue = propInfo.GetValue(item);
                        choiceStrings.Add(propValue.ToString());
                    }
                }
                else
                {
                    choiceStrings.Add(item.ToString());
                }
            }

            return choiceStrings;
        }

        private object GetSelectedChoice(string text)
        {
            if (string.IsNullOrEmpty(this.ChoicesBindingName))
            {
                return null;
            }

            return (from object item in this.Choices let value = item.GetType().GetProperty(this.ChoicesBindingName)?.GetValue(item, null) where value?.ToString() == text select item).FirstOrDefault();
        }

        private void OnAlwaysShowUnderlineChanged(bool isShown)
        {
            persistentUnderline.IsVisible = isShown;
            persistentUnderline.Color = this.UnderlineColor;
        }

        private void OnBackgroundColorChanged(Color backgroundColor)
        {
            backgroundCard.BackgroundColor = backgroundColor;
        }

        private void OnChoicesChanged(ICollection choices)
        {
            _choices = choices?.Count > 0 ? this.GetChoices() : null;
        }

        private void OnEnabledChanged(bool isEnabled)
        {
            this.Opacity = isEnabled ? 1 : 0.33;
            helper.IsVisible = isEnabled && !string.IsNullOrEmpty(this.HelperText);
        }

        private void OnErrorColorChanged(Color errorColor)
        {
            trailingIcon.TintColor = errorColor;
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
            entry.Margin = isEnabled ? new Thickness(entry.Margin.Left, 24, entry.Margin.Right, 0) : new Thickness(entry.Margin.Left, marginTopVariation - 9, entry.Margin.Right, 0);

            var iconMargin = leadingIcon.Margin;
            leadingIcon.Margin = isEnabled ? new Thickness(iconMargin.Left, 16, iconMargin.Right, 16) : new Thickness(iconMargin.Left, 8, iconMargin.Right, 8);

            var trailingIconMargin = trailingIcon.Margin;
            trailingIcon.Margin = isEnabled ? new Thickness(trailingIconMargin.Left, 16, trailingIconMargin.Right, 16) : new Thickness(trailingIconMargin.Left, 8, trailingIconMargin.Right, 8);
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
            this.OnLeadingIconTintColorChanged(this.IconTintColor);
        }

        private void OnLeadingIconTintColorChanged(Color tintColor)
        {
            leadingIcon.TintColor = tintColor;
        }

        private void OnInputTypeChanged(MaterialTextFieldInputType inputType)
        {
            switch (inputType)
            {
                case MaterialTextFieldInputType.Chat:
                    entry.Keyboard = Keyboard.Chat;
                    break;

                case MaterialTextFieldInputType.Default:
                    entry.Keyboard = Keyboard.Default;
                    break;

                case MaterialTextFieldInputType.Email:
                    entry.Keyboard = Keyboard.Email;
                    break;

                case MaterialTextFieldInputType.Numeric:
                    entry.Keyboard = Keyboard.Numeric;
                    break;

                case MaterialTextFieldInputType.Plain:
                    entry.Keyboard = Keyboard.Plain;
                    break;

                case MaterialTextFieldInputType.Telephone:
                    entry.Keyboard = Keyboard.Telephone;
                    break;

                case MaterialTextFieldInputType.Text:
                    entry.Keyboard = Keyboard.Text;
                    break;

                case MaterialTextFieldInputType.Url:
                    entry.Keyboard = Keyboard.Url;
                    break;

                case MaterialTextFieldInputType.NumericPassword:
                    entry.Keyboard = Keyboard.Numeric;
                    break;

                case MaterialTextFieldInputType.Password:
                    entry.Keyboard = Keyboard.Text;
                    break;

                case MaterialTextFieldInputType.Choice:

                    break;
            }

            //entry.AutoSize = inputType == MaterialTextFieldInputType.MultiLine ? EditorAutoSizeOption.TextChanges : EditorAutoSizeOption.Disabled;
            _gridContainer.InputTransparent = inputType == MaterialTextFieldInputType.Choice;
            trailingIcon.IsVisible = inputType == MaterialTextFieldInputType.Choice;

            entry.IsPassword = inputType == MaterialTextFieldInputType.Password || inputType == MaterialTextFieldInputType.NumericPassword;
        }

        private void OnKeyboardFlagsChanged(bool isAutoCapitalizationEnabled, bool isSpellCheckEnabled, bool isTextPredictionEnabled)
        {
            KeyboardFlags flags = KeyboardFlags.CapitalizeWord | KeyboardFlags.Spellcheck | KeyboardFlags.Suggestions;

            if (!isAutoCapitalizationEnabled)
            {
                flags &= ~KeyboardFlags.CapitalizeWord;
            }

            if (!isSpellCheckEnabled)
            {
                flags &= ~KeyboardFlags.Spellcheck;
            }

            if (!isTextPredictionEnabled)
            {
                flags &= ~KeyboardFlags.Suggestions;
            }

            entry.Keyboard = Keyboard.Create(flags);
            Debug.WriteLine(flags);
        }

        private void OnMaxLengthChanged(int maxLength)
        {
            _counterEnabled = maxLength > 0;
            entry.MaxLength = _counterEnabled ? maxLength : (int)InputView.MaxLengthProperty.DefaultValue;
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

        private void OnReturnCommandChanged(ICommand returnCommand)
        {
            entry.ReturnCommand = returnCommand;
        }

        private void OnReturnCommandParameterChanged(object parameter)
        {
            entry.ReturnCommandParameter = parameter;
        }

        private void OnReturnTypeChangedd(ReturnType returnType)
        {
            entry.ReturnType = returnType;
        }

        private void OnTextChanged(string text)
        {
            if (this.InputType == MaterialTextFieldInputType.Choice && !string.IsNullOrEmpty(text) && _choices?.Contains(text) == false)
            {
                throw new InvalidOperationException($"The `Text` property value `{this.Text}` does not match any item in the collection `Choices`.");
            }

            if (this.InputType == MaterialTextFieldInputType.Choice && !string.IsNullOrEmpty(text))
            {
                var selectedChoice = this.GetSelectedChoice(text) ?? text;
                this.ChoiceSelected?.Invoke(this, new SelectedItemChangedEventArgs(selectedChoice));
                this.ChoiceSelectedCommand?.Execute(selectedChoice);
            }

            entry.Text = text;
            this.AnimateToInactiveOrFocusedStateOnStart(this);
            this.UpdateCounter();
        }

        private void OnTextColorChanged(Color textColor)
        {
            entry.TextColor = trailingIcon.TintColor = textColor;
        }

        private void OnTextFontFamilyChanged(string fontFamily)
        {
            entry.FontFamily = fontFamily;
        }

        private void OnTextFontSizeChanged(double fontSize)
        {
            placeholder.FontSize = entry.FontSize = fontSize;
        }

        private void OnTintColorChanged(Color tintColor)
        {
            entry.TintColor = tintColor;
        }

        private void OnUnderlineColorChanged(Color underlineColor)
        {
            if (this.AlwaysShowUnderline)
            {
                persistentUnderline.Color = underlineColor;
            }
        }

        private void SetControl()
        {
            trailingIcon.TintColor = this.TextColor;
            persistentUnderline.Color = this.UnderlineColor;
            tapGesture.Command = new Command(() =>
            {
                if (!entry.IsFocused)
                {
                    entry.Focus();
                }
            });

            mainTapGesture.Command = new Command(async () =>
            {
                if (this.Choices == null || this.Choices?.Count <= 0)
                {
                    throw new InvalidOperationException("The property `Choices` is null or empty");
                }
                _choices = this.GetChoices();

                var result = await MaterialDialog.Instance.SelectChoiceAsync("Select an item", _choices);

                if (result >= 0)
                {
                    this.Text = _choices[result];
                }
            });
        }

        private void SetPropertyChangeHandler(ref Dictionary<string, Action> propertyChangeActions)
        {
            propertyChangeActions = new Dictionary<string, Action>
            {
                { nameof(this.Text), () => this.OnTextChanged(this.Text) },
                { nameof(this.TextColor), () => this.OnTextColorChanged(this.TextColor) },
                { nameof(this.TextFontFamily), () => this.OnTextFontFamilyChanged(this.TextFontFamily) },
                { nameof(this.TintColor), () => this.OnTintColorChanged(this.TintColor) },
                { nameof(this.Placeholder), () => this.OnPlaceholderChanged(this.Placeholder) },
                { nameof(this.PlaceholderColor), () => this.OnPlaceholderColorChanged(this.PlaceholderColor) },
                { nameof(this.PlaceholderFontFamily), () => this.OnPlaceholderFontFamilyChanged(this.PlaceholderFontFamily) },
                { nameof(this.HelperText), () => this.OnHelperTextChanged(this.HelperText) },
                { nameof(this.HelperTextFontFamily), () => this.OnHelpertTextFontFamilyChanged(this.HelperTextFontFamily) },
                { nameof(this.HelperTextColor), () => this.OnHelperTextColorChanged(this.HelperTextColor) },
                { nameof(this.InputType), () => this.OnInputTypeChanged(this.InputType) },
                { nameof(this.IsEnabled), () => this.OnEnabledChanged(this.IsEnabled) },
                { nameof(this.BackgroundColor), () => this.OnBackgroundColorChanged(this.BackgroundColor) },
                { nameof(this.AlwaysShowUnderline), () => this.OnAlwaysShowUnderlineChanged(this.AlwaysShowUnderline) },
                { nameof(this.MaxLength), () => this.OnMaxLengthChanged(this.MaxLength) },
                { nameof(this.ReturnCommand), () => this.OnReturnCommandChanged(this.ReturnCommand) },
                { nameof(this.ReturnCommandParameter), () => this.OnReturnCommandParameterChanged(this.ReturnCommandParameter) },
                { nameof(this.ReturnType), () => this.OnReturnTypeChangedd(this.ReturnType) },
                { nameof(this.ErrorColor), () => this.OnErrorColorChanged(this.ErrorColor) },
                { nameof(this.UnderlineColor), () => this.OnUnderlineColorChanged(this.UnderlineColor) },
                { nameof(this.HasError), () => this.OnHasErrorChanged() },
                { nameof(this.FloatingPlaceholderEnabled), () => this.OnFloatingPlaceholderEnabledChanged(this.FloatingPlaceholderEnabled) },
                { nameof(this.Choices), () => this.OnChoicesChanged(this.Choices) },
                { nameof(this.LeadingIcon), () => this.OnLeadingIconChanged(this.LeadingIcon) },
                { nameof(this.IconTintColor), () => this.OnLeadingIconTintColorChanged(this.IconTintColor) },
                { nameof(this.IsSpellCheckEnabled), () => this.OnKeyboardFlagsChanged(this.IsAutoCapitalizationEnabled, this.IsSpellCheckEnabled, this.IsTextPredictionEnabled) },
                { nameof(this.IsTextPredictionEnabled), () => this.OnKeyboardFlagsChanged(this.IsAutoCapitalizationEnabled, this.IsSpellCheckEnabled, this.IsTextPredictionEnabled) },
                { nameof(this.IsAutoCapitalizationEnabled), () => this.OnKeyboardFlagsChanged(this.IsAutoCapitalizationEnabled, this.IsSpellCheckEnabled, this.IsTextPredictionEnabled) },
                { nameof(this.TextFontSize), () => this.OnTextFontSizeChanged(this.TextFontSize) },
                { nameof(this.ErrorText), () => this.OnErrorTextChanged() }
            };
        }

        private void UpdateCounter()
        {
            if (!_counterEnabled) return;
            var count = entry.Text?.Length ?? 0;
            counter.Text = entry.IsFocused ? $"{count}/{this.MaxLength}" : string.Empty;
        }
    }
}