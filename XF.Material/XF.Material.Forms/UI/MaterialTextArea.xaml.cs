using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Internals;
using XF.Material.Forms.Utilities;

namespace XF.Material.Forms.UI
{
    /// <inheritdoc />
    /// <summary>
    /// A control that let users enter and editor text.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialTextArea : ContentView, IMaterialElementConfiguration
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

        public static readonly BindableProperty InputTypeProperty = BindableProperty.Create(nameof(InputType), typeof(MaterialTextFieldInputType), typeof(MaterialTextField), MaterialTextFieldInputType.Default);

        public static readonly BindableProperty IsAutoCapitalizationEnabledProperty = BindableProperty.Create(nameof(IsAutoCapitalizationEnabled), typeof(bool), typeof(MaterialTextField), false);

        public static readonly BindableProperty IsMaxLengthCounterVisibleProperty = BindableProperty.Create(nameof(IsMaxLengthCounterVisible), typeof(bool), typeof(MaterialTextField), true);

        public static readonly BindableProperty IsSpellCheckEnabledProperty = BindableProperty.Create(nameof(IsSpellCheckEnabled), typeof(bool), typeof(MaterialTextField), false);

        public static readonly BindableProperty IsTextPredictionEnabledProperty = BindableProperty.Create(nameof(IsTextPredictionEnabled), typeof(bool), typeof(MaterialTextField), false);

        public static readonly BindableProperty LeadingIconProperty = BindableProperty.Create(nameof(LeadingIcon), typeof(string), typeof(MaterialTextField));

        public static readonly BindableProperty LeadingIconTintColorProperty = BindableProperty.Create(nameof(LeadingIconTintColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#99000000"));

        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(MaterialTextField), 0);

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#99000000"));

        public static readonly BindableProperty PlaceholderFontFamilyProperty = BindableProperty.Create(nameof(PlaceholderFontFamily), typeof(string), typeof(MaterialTextField));

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialTextField), string.Empty);

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
        private bool _counterEnabled;
        private DeviceOrientations DisplayOrientation;
        private List<int> _selectedIndicies = new List<int>();
        private bool _wasFocused;

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialTextArea"/>.
        /// </summary>
        public MaterialTextArea()
        {
            this.InitializeComponent();
            this.SetPropertyChangeHandler(ref _propertyChangeActions);
            this.SetControl();
            this.DisplayOrientation = Plugin.DeviceOrientation.CrossDeviceOrientation.Current.CurrentOrientation;
        }

        public event EventHandler<SelectedItemChangedEventArgs> ChoiceSelected;

        /// <summary>
        /// Raised when this text area receives focus.
        /// </summary>
        public new event EventHandler<FocusEventArgs> Focused;

        /// <summary>
        /// Raised when this text area loses focus.
        /// </summary>
        public new event EventHandler<FocusEventArgs> Unfocused;

        /// <summary>
        /// Raised when the input text of this text area has changed.
        /// </summary>
        public event EventHandler<TextChangedEventArgs> TextChanged;

        /// <summary>
        /// Raised when the user finalizes the input on this text area using the return key.
        /// </summary>
        public event EventHandler Completed;

        /// <summary>
        /// Gets or sets whether the underline accent of this text area should always show or not.
        /// </summary>
        public bool AlwaysShowUnderline
        {
            get => (bool)this.GetValue(AlwaysShowUnderlineProperty);
            set => this.SetValue(AlwaysShowUnderlineProperty, value);
        }

        /// <summary>
        /// Gets or sets the background color of this text area.
        /// </summary>
        public new Color BackgroundColor
        {
            get => (Color)this.GetValue(BackgroundColorProperty);
            set => this.SetValue(BackgroundColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the collection of objects which the user will choose from. This is required when <see cref="InputType"/> is set to <see cref="MaterialTextFieldInputType.Choice"/>.
        /// </summary>
        public IList Choices
        {
            get => (IList)this.GetValue(ChoicesProperty);
            set => this.SetValue(ChoicesProperty, value);
        }

        /// <summary>
        /// Gets or sets the name of the property to display of each object in the <see cref="Choices"/> property. This will be ignored if the objects are strings.
        /// </summary>
        public string ChoicesBindingName { get; set; }

        /// <summary>
        /// Gets or sets the command that will execute if a choice was selected when the <see cref="InputType"/> is set to <see cref="MaterialTextFieldInputType.Choice"/>.
        /// </summary>
        public ICommand ChoiceSelectedCommand
        {
            get => (ICommand)this.GetValue(ChoiceSelectedCommandProperty);
            set => this.SetValue(ChoiceSelectedCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the color to indicate an error in this text area.
        /// The default value is the color of <see cref="MaterialColorConfiguration.Error"/>.
        /// </summary>
        public Color ErrorColor
        {
            get => (Color)this.GetValue(ErrorColorProperty);
            set => this.SetValue(ErrorColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the error text of this text area.
        /// </summary>
        public string ErrorText
        {
            get => (string)this.GetValue(ErrorTextProperty);
            set => this.SetValue(ErrorTextProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the floating placeholder.
        /// </summary>
        public Color FloatingPlaceholderColor
        {
            get => (Color)this.GetValue(FloatingPlaceholderColorProperty);
            set => this.SetValue(FloatingPlaceholderColorProperty, value);
        }

        /// <summary>
        /// Gets or sets whether the placeholder label will float at top of the text area when focused or when it has text.
        /// </summary>
        public bool FloatingPlaceholderEnabled
        {
            get => (bool)this.GetValue(FloatingPlaceholderEnabledProperty);
            set => this.SetValue(FloatingPlaceholderEnabledProperty, value);
        }

        /// <summary>
        /// Gets or sets the font size of the floating placeholder.
        /// </summary>
        public double FloatingPlaceholderFontSize
        {
            get => (double)this.GetValue(FloatingPlaceholderFontSizeProperty);
            set => this.SetValue(FloatingPlaceholderFontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that will be executed when this text area receives or loses focus.
        /// </summary>
        public Command<bool> FocusCommand
        {
            get => (Command<bool>)this.GetValue(FocusCommandProperty);
            set => this.SetValue(FocusCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the boolean value whether this text area has an error, and if it will show the its error text.
        /// </summary>
        public bool HasError
        {
            get => (bool)this.GetValue(HasErrorProperty);
            set => this.SetValue(HasErrorProperty, value);
        }

        /// <summary>
        /// Gets or sets the helper text of this text area.
        /// </summary>
        public string HelperText
        {
            get => (string)this.GetValue(HelperTextProperty);
            set => this.SetValue(HelperTextProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of this text area's helper text.
        /// </summary>
        public Color HelperTextColor
        {
            get => (Color)this.GetValue(HelperTextColorProperty);
            set => this.SetValue(HelperTextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family of this text area's helper text.
        /// </summary>
        public string HelperTextFontFamily
        {
            get => (string)this.GetValue(HelperTextFontFamilyProperty);
            set => this.SetValue(HelperTextFontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets the horizontal padding of the text area.
        /// </summary>
        public MaterialHorizontalThickness HorizontalPadding
        {
            get => (MaterialHorizontalThickness)this.GetValue(HorizontalPaddingProperty);
            set => this.SetValue(HorizontalPaddingProperty, value);
        }

        /// <summary>
        /// Gets or sets the keyboard input type of this text area.
        /// </summary>
        public MaterialTextFieldInputType InputType
        {
            get => (MaterialTextFieldInputType)this.GetValue(InputTypeProperty);
            set => this.SetValue(InputTypeProperty, value);
        }

        /// <summary>
        /// Gets or sets whether auto capitialization is enabled.
        /// </summary>
        public bool IsAutoCapitalizationEnabled
        {
            get => (bool)this.GetValue(IsAutoCapitalizationEnabledProperty);
            set => this.SetValue(IsAutoCapitalizationEnabledProperty, value);
        }

        /// <summary>
        /// Gets or sets whether the counter for the max input length of this text area is visible or not.
        /// </summary>
        public bool IsMaxLengthCounterVisible
        {
            get => (bool)this.GetValue(IsMaxLengthCounterVisibleProperty);
            set => this.SetValue(IsMaxLengthCounterVisibleProperty, value);
        }

        /// <summary>
        /// Gets or sets whether spell checking is enabled.
        /// </summary>
        public bool IsSpellCheckEnabled
        {
            get => (bool)this.GetValue(IsSpellCheckEnabledProperty);
            set => this.SetValue(IsSpellCheckEnabledProperty, value);
        }

        /// <summary>
        /// Gets or sets whether text prediction is enabled.
        /// </summary>
        public bool IsTextPredictionEnabled
        {
            get => (bool)this.GetValue(IsTextPredictionEnabledProperty);
            set => this.SetValue(IsTextPredictionEnabledProperty, value);
        }

        /// <summary>
        /// Gets or sets the image source of the icon to be showed at the left side of this text area.
        /// </summary>
        public string LeadingIcon
        {
            get => (string)this.GetValue(LeadingIconProperty);
            set => this.SetValue(LeadingIconProperty, value);
        }

        /// <summary>
        /// Gets or sets the tint color of the icon of this text area.
        /// </summary>
        public Color LeadingIconTintColor
        {
            get => (Color)this.GetValue(LeadingIconTintColorProperty);
            set => this.SetValue(LeadingIconTintColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the maximum allowed number of characters in this text area.
        /// </summary>
        public int MaxLength
        {
            get => (int)this.GetValue(MaxLengthProperty);
            set => this.SetValue(MaxLengthProperty, value);
        }

        /// <summary>
        /// Gets or sets the text of this text area's placeholder.
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
        /// Gets or sets the color of this text area's placeholder.
        /// </summary>
        public Color PlaceholderColor
        {
            get => (Color)this.GetValue(PlaceholderColorProperty);
            set => this.SetValue(PlaceholderColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family of this text area's placeholder
        /// </summary>
        public string PlaceholderFontFamily
        {
            get => (string)this.GetValue(PlaceholderFontFamilyProperty);
            set => this.SetValue(PlaceholderFontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets whether the underline indicator will be animated. If set to false, the underline will not be shown.
        /// </summary>
        public bool ShouldAnimateUnderline
        {
            get => (bool)this.GetValue(ShouldAnimateUnderlineProperty);
            set => this.SetValue(ShouldAnimateUnderlineProperty, value);
        }

        /// <summary>
        /// Gets or sets the input text of this text area.
        /// </summary>
        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that will execute if there is a change in this text area's input text.
        /// </summary>
        public Command<string> TextChangeCommand
        {
            get => (Command<string>)this.GetValue(TextChangeCommandProperty);
            set => this.SetValue(TextChangeCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of this text area's input text.
        /// </summary>
        public Color TextColor
        {
            get => (Color)this.GetValue(TextColorProperty);
            set => this.SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family of this text area's input text.
        /// </summary>
        public string TextFontFamily
        {
            get => (string)this.GetValue(TextFontFamilyProperty);
            set => this.SetValue(TextFontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets the text's font size.
        /// </summary>
        public double TextFontSize
        {
            get => (double)this.GetValue(TextFontSizeProperty);
            set => this.SetValue(TextFontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the tint color of the underline and the placeholder of this text area when focused.
        /// The default value is the color of <see cref="MaterialColorConfiguration.Secondary"/>.
        /// </summary>
        public Color TintColor
        {
            get => (Color)this.GetValue(TintColorProperty);
            set => this.SetValue(TintColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the underline when this text area is activated. <see cref="AlwaysShowUnderline"/> is set to true.
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
                editor.PropertyChanged += this.Entry_PropertyChanged;
                editor.TextChanged += this.Entry_TextChanged;
                editor.SizeChanged += this.Entry_SizeChanged;
                editor.Focused += this.Entry_Focused;
                editor.Unfocused += this.Entry_Unfocused;
                editor.Completed += this.Entry_Completed;
                CrossDeviceOrientation.Current.OrientationChanged += this.CurrentOnOrientationChanged;
            }
            else
            {
                editor.PropertyChanged -= this.Entry_PropertyChanged;
                editor.TextChanged -= this.Entry_TextChanged;
                editor.SizeChanged -= this.Entry_SizeChanged;
                editor.Focused -= this.Entry_Focused;
                editor.Unfocused -= this.Entry_Unfocused;
                editor.Completed += this.Entry_Completed;
                CrossDeviceOrientation.Current.OrientationChanged -= this.CurrentOnOrientationChanged;
            }
        }

        /// <summary>
        /// Requests to set focus on this text area.
        /// </summary>
        public new void Focus() => editor.Focus();

        /// <summary>
        /// Requests to unset the focus on this text area.
        /// </summary>
        public new void Unfocus() => editor.Unfocus();

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

            if (editor.IsFocused)
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
            double preferredStartFont = this.FloatingPlaceholderFontSize == 0 ? editor.FontSize * 0.75 : this.FloatingPlaceholderFontSize;
            double preferredEndFont = this.FloatingPlaceholderFontSize == 0 ? editor.FontSize * 0.75 : this.FloatingPlaceholderFontSize;
            double startFont = editor.IsFocused ? editor.FontSize : preferredStartFont;
            double endFOnt = editor.IsFocused ? preferredEndFont : editor.FontSize;
            var startY = placeholder.TranslationY;
            double endY = editor.IsFocused ? -(editor.FontSize * 0.8) : 0;

            if (this.HasError)
            {
                tintColor = editor.IsFocused ? this.ErrorColor : this.PlaceholderColor;
            }
            else
            {
                tintColor = editor.IsFocused ? this.TintColor : this.PlaceholderColor;
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
                        if(this.HasError && editor.IsFocused)
                        {
                            placeholder.TextColor = this.ErrorColor;
                        }

                         placeholder.TextColor = tintColor;
                    })
                }
            } : new Animation();

            if (editor.IsFocused)
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
            var placeholderEndY = -(editor.FontSize * 0.8);
            var placeholderEndFont = editor.FontSize * 0.75;

            if (!this.FloatingPlaceholderEnabled && string.IsNullOrEmpty(editor.Text))
            {
                placeholder.TextColor = this.PlaceholderColor;
            }

            if (startObject != null && !string.IsNullOrEmpty(this.Text) && !_wasFocused)
            {
                if (placeholder.TranslationY == placeholderEndY)
                {
                    return;
                }
                editor.Opacity = 0;

                Device.BeginInvokeOnMainThread(() =>
                {
                    var anim = new Animation();

                    if (this.FloatingPlaceholderEnabled)
                    {
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.FontSize = v, editor.FontSize, placeholderEndFont, _animationCurve));
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.TranslationY = v, placeholder.TranslationY, placeholderEndY, _animationCurve, () =>
                        {
                            placeholder.TextColor = this.HasError ? this.ErrorColor : this.FloatingPlaceholderColor;
                            editor.Opacity = 1;
                        }));
                    }

                    if (this.ShouldAnimateUnderline)
                    {
                        underline.Color = this.HasError ? this.ErrorColor : this.TintColor;
                        underline.HeightRequest = 1;
                        anim.Add(0.0, AnimationDuration, new Animation(v => underline.WidthRequest = v, 0, this.Width, _animationCurve, () => underline.HorizontalOptions = LayoutOptions.FillAndExpand));
                    }

                    anim.Commit(this, "Anim2", rate: 2, length: (uint)(AnimationDuration * 1000), easing: _animationCurve);
                });

                editor.Opacity = 1;

                return;
            }

            if (startObject != null && string.IsNullOrEmpty(this.Text) && placeholder.TranslationY == placeholderEndY)
            {
                if (editor.IsFocused)
                {
                    return;
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    var anim = new Animation();

                    if (this.FloatingPlaceholderEnabled)
                    {
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.FontSize = v, placeholderEndFont, editor.FontSize, _animationCurve));
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.TranslationY = v, placeholder.TranslationY, 0, _animationCurve, () =>
                        {
                            placeholder.TextColor = this.PlaceholderColor;
                            editor.Opacity = 1;
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
            placeholder.TextColor = (this.FloatingPlaceholderEnabled && editor.IsFocused) || (this.FloatingPlaceholderEnabled && !string.IsNullOrEmpty(this.Text)) ? this.ErrorColor : this.PlaceholderColor;
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
            editor.Opacity = opactiy;
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

        private void CurrentOnOrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            if (e.Orientation != this.DisplayOrientation)
            {
                if (!string.IsNullOrEmpty(editor.Text) && this.ShouldAnimateUnderline)
                {
                    underline.WidthRequest = -1;
                    underline.HorizontalOptions = LayoutOptions.FillAndExpand;
                }

                this.DisplayOrientation = e.Orientation;
            }
        }

        private void Entry_Completed(object sender, EventArgs e) => this.Completed?.Invoke(this, EventArgs.Empty);

        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            _wasFocused = true;
            this.FocusCommand?.Execute(editor.IsFocused);
            this.Focused?.Invoke(this, e);
            this.UpdateCounter();
        }

        private void Entry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.IsFocused) when string.IsNullOrEmpty(editor.Text):
                    this.AnimateToInactiveOrFocusedState();
                    break;

                case nameof(this.IsFocused) when !string.IsNullOrEmpty(editor.Text):
                    this.AnimateToActivatedState();
                    break;

                case nameof(Entry.Text):
                    this.Text = editor.Text;
                    this.UpdateCounter();
                    break;
            }
        }

        private void Entry_SizeChanged(object sender, EventArgs e)
        {
            var baseHeight = this.FloatingPlaceholderEnabled ? 56 : 40;
            var diff = editor.Height - 20;
            var rawRowHeight = baseHeight + diff;
            _autoSizingRow.Height = new GridLength(rawRowHeight);

            double iconVerticalMargin = (_autoSizingRow.Height.Value - 24) / 2;

            if (leadingIcon.IsVisible)
            {
                leadingIcon.Margin = new Thickness(this.HorizontalPadding.Left, iconVerticalMargin, 0, iconVerticalMargin);
                editor.Margin = new Thickness(12, editor.Margin.Top, this.HorizontalPadding.Right, editor.Margin.Bottom);
            }
            else
            {
                editor.Margin = new Thickness(this.HorizontalPadding.Left, editor.Margin.Top, this.HorizontalPadding.Right, editor.Margin.Bottom);
            }

            if (trailingIcon.IsVisible)
            {
                var entryPaddingLeft = leadingIcon.IsVisible ? 12 : this.HorizontalPadding;
                trailingIcon.Margin = new Thickness(12, iconVerticalMargin, this.HorizontalPadding.Right, iconVerticalMargin);
                editor.Margin = new Thickness(entryPaddingLeft.Left, editor.Margin.Top, 0, editor.Margin.Bottom);
            }

            helper.Margin = new Thickness(this.HorizontalPadding.Left, helper.Margin.Top, 12, 0);
            counter.Margin = new Thickness(0, counter.Margin.Top, this.HorizontalPadding.Right, 0);

            var placeholderLeftMargin = this.FloatingPlaceholderEnabled ? this.HorizontalPadding.Left : editor.Margin.Left;
            placeholder.Margin = new Thickness(placeholderLeftMargin, 0, 0, 0);

            if (this.HasError)
            {
                underline.Color = this.ErrorColor;
            }
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.TextChangeCommand?.Execute(editor.Text);
            this.TextChanged?.Invoke(this, e);
        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            this.FocusCommand?.Execute(editor.IsFocused);
            this.Unfocused?.Invoke(this, e);
            this.UpdateCounter();
        }

        private IList<string> GetChoices()
        {
            var choiceStrings = new List<string>(this.Choices.Count);

            for (int i = 0; i < this.Choices.Count; i++)
            {
                this.GetChoiceString(i);
            }

            return choiceStrings;
        }

        private string GetChoiceString(int index)
        {
            if (index < 0)
            {
                return "";
            }
            else
            {
                //Ok
            }

            var choice = this.Choices[index];

            var listType = this.Choices[0].GetType();

            if (!string.IsNullOrEmpty(this.ChoicesBindingName))
            {
                var propInfo = listType.GetProperty(this.ChoicesBindingName);

                if (propInfo == null)
                {
                    System.Diagnostics.Debug.WriteLine($"Property {this.ChoicesBindingName} was not found for item in {this.Choices}.");
                    return choice.ToString();
                }
                else
                {
                    var propValue = propInfo.GetValue(choice);
                    return propValue.ToString();
                }
            }
            else
            {
                return choice.ToString();
            }
        }

        private object GetSelectedChoice(int index)
        {
            if (index < 0)
            {
                return null;
            }

            return this.Choices[index];
        }

        private IList GetSelectedChoices(List<int> indicies)
        {
            if (indicies.Count() < 0)
            {
                return null;
            }

            return this.Choices.Subset(indicies.ToArray());
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
            //_choices = choices?.Count > 0 ? this.GetChoices() : null;
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
            editor.Margin = isEnabled ? new Thickness(editor.Margin.Left, 24, editor.Margin.Right, 0) : new Thickness(editor.Margin.Left, marginTopVariation - 9, editor.Margin.Right, 0);

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

        private void OnInputTypeChanged(MaterialTextFieldInputType inputType)
        {
            switch (inputType)
            {
                case MaterialTextFieldInputType.Chat:
                    editor.Keyboard = Keyboard.Chat;
                    break;

                case MaterialTextFieldInputType.Default:
                    editor.Keyboard = Keyboard.Default;
                    break;

                case MaterialTextFieldInputType.Email:
                    editor.Keyboard = Keyboard.Email;
                    break;

                case MaterialTextFieldInputType.Numeric:
                    editor.Keyboard = Keyboard.Numeric;
                    break;

                case MaterialTextFieldInputType.Plain:
                    editor.Keyboard = Keyboard.Plain;
                    break;

                case MaterialTextFieldInputType.Telephone:
                    editor.Keyboard = Keyboard.Telephone;
                    break;

                case MaterialTextFieldInputType.Text:
                case MaterialTextFieldInputType.MultiLineText:
                    editor.Keyboard = Keyboard.Text;
                    break;

                case MaterialTextFieldInputType.Url:
                    editor.Keyboard = Keyboard.Url;
                    break;

                case MaterialTextFieldInputType.NumericPassword:
                    editor.Keyboard = Keyboard.Numeric;
                    break;

                case MaterialTextFieldInputType.Password:
                    editor.Keyboard = Keyboard.Text;
                    break;

                case MaterialTextFieldInputType.Choice:
                    break;
            }

            // Hint: Will use this for MaterialTextArea
            editor.AutoSize = inputType == MaterialTextFieldInputType.MultiLineText ? EditorAutoSizeOption.TextChanges : EditorAutoSizeOption.Disabled;
            _gridContainer.InputTransparent = inputType == MaterialTextFieldInputType.Choice;
            trailingIcon.IsVisible = inputType == MaterialTextFieldInputType.Choice;

        }

        private void OnKeyboardFlagsChanged(bool isAutoCapitalizationEnabled, bool isSpellCheckEnabled, bool isTextPredictionEnabled)
        {
            var flags = KeyboardFlags.CapitalizeWord | KeyboardFlags.Spellcheck | KeyboardFlags.Suggestions;

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

            editor.Keyboard = Keyboard.Create(flags);
        }

        private void OnLeadingIconChanged(string icon)
        {
            leadingIcon.Source = icon;
            this.OnLeadingIconTintColorChanged(this.LeadingIconTintColor);
        }

        private void OnLeadingIconTintColorChanged(Color tintColor)
        {
            leadingIcon.TintColor = tintColor;
        }

        private void OnMaxLengthChanged(int maxLength, bool isMaxLengthCounterVisible)
        {
            _counterEnabled = maxLength > 0 && isMaxLengthCounterVisible;
            editor.MaxLength = maxLength > 0 ? maxLength : (int)InputView.MaxLengthProperty.DefaultValue;
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


        private async Task OnSelectChoices()
        {
            if (this.Choices == null || this.Choices?.Count <= 0)
            {
                throw new InvalidOperationException("The property `Choices` is null or empty");
            }

            var title = MaterialConfirmationDialog.GetDialogTitle(this);
            var confirmingText = MaterialConfirmationDialog.GetDialogConfirmingText(this);
            var dismissiveText = MaterialConfirmationDialog.GetDialogDismissiveText(this);
            var configuration = MaterialConfirmationDialog.GetDialogConfiguration(this);
            List<int> result = new List<int>();

            if (this.InputType == MaterialTextFieldInputType.Choice)
            {
                if (_selectedIndicies.Count > 0)
                {
                    int choiceIndicies = await MaterialDialog.Instance.SelectChoiceAsync(title, this.Choices, _selectedIndicies[0], this.ChoicesBindingName, confirmingText, dismissiveText, configuration);
                    result.Add(choiceIndicies);
                }
                else
                {
                    int choiceIndicies = await MaterialDialog.Instance.SelectChoiceAsync(title, this.Choices, this.ChoicesBindingName, confirmingText, dismissiveText, configuration);
                    result.Add(choiceIndicies);
                }

                if (result.Count > 0)
                {
                    _selectedIndicies = result;
                    this.Text = this.GetChoiceString(_selectedIndicies[0]);
                }
            }
            else //MultiChoice
            {
                if (_selectedIndicies.Count > 0)
                {
                    IEnumerable<int> choiceIndicies = await MaterialDialog.Instance.SelectChoicesAsync(title, this.Choices, _selectedIndicies, this.ChoicesBindingName, confirmingText, dismissiveText, configuration);
                    if (choiceIndicies != null)
                    {
                        result = choiceIndicies.ToList();
                    }
                    else
                    {
                        //retain empty list from above
                    }
                }
                else
                {
                    IEnumerable<int> choiceIndicies = await MaterialDialog.Instance.SelectChoicesAsync(title, this.Choices, this.ChoicesBindingName, confirmingText, dismissiveText, configuration);
                    if (choiceIndicies != null)
                    {
                        result = choiceIndicies.ToList();
                    }
                    else
                    {
                        //retain empty list from above
                    }
                }

                if (result.Count > 0)
                {
                    _selectedIndicies = result;
                    var selectedChoices = this.GetSelectedChoices(_selectedIndicies);
                    this.ChoiceSelected?.Invoke(this, new SelectedItemChangedEventArgs(selectedChoices));
                    this.ChoiceSelectedCommand?.Execute(selectedChoices);
                }
                else
                {
                    _selectedIndicies.Clear();
                }

            }

            if (result.Count > 0)
            {
                this.Text = result.Count > 1 ? "Multiple" : this.GetChoiceString(_selectedIndicies[0]);
            }
        }

        private void OnTextChanged(string text) 
        {
            if (!string.IsNullOrEmpty(text) && !this.FloatingPlaceholderEnabled)
            {
                placeholder.IsVisible = false;
            }
            else if (string.IsNullOrEmpty(text) && !this.FloatingPlaceholderEnabled)
            {
                placeholder.IsVisible = true;
            }

            if (this.InputType == MaterialTextFieldInputType.Choice && !string.IsNullOrEmpty(text) && this.Choices?.Contains(text) == false)
            {
                Debug.WriteLine($"The `Text` property value `{this.Text}` does not match any item in the collection `Choices`.");
                this.Text = null;
                return;
            }

            if (this.InputType == MaterialTextFieldInputType.Choice && !string.IsNullOrEmpty(text))
            {
                var selectedChoice = this.GetSelectedChoice(_selectedIndicies[0]);
                this.ChoiceSelected?.Invoke(this, new SelectedItemChangedEventArgs(selectedChoice));
                this.ChoiceSelectedCommand?.Execute(selectedChoice);
            }
            else if (this.InputType == MaterialTextFieldInputType.Choice && string.IsNullOrEmpty(text))
            {
                _selectedIndicies.Clear();
            }

            editor.Text = text;

            this.AnimateToInactiveOrFocusedStateOnStart(this);
            this.UpdateCounter();
        }

        private void OnTextColorChanged(Color textColor)
        {
            editor.TextColor = trailingIcon.TintColor = textColor;
        }

        private void OnTextFontFamilyChanged(string fontFamily)
        {
            editor.FontFamily = fontFamily;
        }

        private void OnTextFontSizeChanged(double fontSize)
        {
            placeholder.FontSize = editor.FontSize = fontSize;
        }

        private void OnTintColorChanged(Color tintColor)
        {
            editor.TintColor = tintColor;
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
                if (!editor.IsFocused)
                {
                    editor.Focus();
                }
            });

            mainTapGesture.Command = new Command(async () => await this.OnSelectChoices());
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
                { nameof(this.MaxLength), () => this.OnMaxLengthChanged(this.MaxLength, this.IsMaxLengthCounterVisible) },
                { nameof(this.ErrorColor), () => this.OnErrorColorChanged(this.ErrorColor) },
                { nameof(this.UnderlineColor), () => this.OnUnderlineColorChanged(this.UnderlineColor) },
                { nameof(this.HasError), () => this.OnHasErrorChanged() },
                { nameof(this.FloatingPlaceholderEnabled), () => this.OnFloatingPlaceholderEnabledChanged(this.FloatingPlaceholderEnabled) },
                { nameof(this.Choices), () => this.OnChoicesChanged(this.Choices) },
                { nameof(this.LeadingIcon), () => this.OnLeadingIconChanged(this.LeadingIcon) },
                { nameof(this.LeadingIconTintColor), () => this.OnLeadingIconTintColorChanged(this.LeadingIconTintColor) },
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
            var count = editor.Text?.Length ?? 0;
            counter.Text = editor.IsFocused ? $"{count}/{this.MaxLength}" : string.Empty;
        }
    }
}