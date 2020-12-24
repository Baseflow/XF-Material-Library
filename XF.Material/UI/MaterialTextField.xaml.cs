using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Internals;
using Keyboard = Xamarin.Forms.Keyboard;

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

        public static readonly BindableProperty CardBackgroundColorProperty = BindableProperty.Create(nameof(CardBackgroundColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#DCDCDC"));
        
        public static readonly BindableProperty CardCornerRadiusProperty = BindableProperty.Create(nameof(CardCornerRadius), typeof(CornerRadius), typeof(MaterialTextField), new CornerRadius(4, 4, 0, 0));

        public static readonly BindableProperty ChoiceSelectedCommandProperty = BindableProperty.Create(nameof(ChoiceSelectedCommand), typeof(ICommand), typeof(MaterialTextField));

        public static readonly BindableProperty ChoiceSelectedCommandParameterProperty = BindableProperty.Create(nameof(ChoiceSelectedCommandParameter), typeof(object), typeof(MaterialTextField));

        public static readonly BindableProperty TrailingIconCommandProperty = BindableProperty.Create(nameof(TrailingIconCommand), typeof(ICommand), typeof(MaterialTextField));

        public static readonly BindableProperty TrailingIconCommandParameterProperty = BindableProperty.Create(nameof(TrailingIconCommandParameter), typeof(object), typeof(MaterialTextField));

        public static readonly BindableProperty ChoicesProperty = BindableProperty.Create(nameof(Choices), typeof(IList), typeof(MaterialTextField));

        public static readonly BindableProperty SelectedChoiceProperty = BindableProperty.Create(nameof(SelectedChoice), typeof(object), typeof(MaterialTextField), null, BindingMode.TwoWay, null, propertyChanged: SelectedChoicePropertyChange);

        private static void SelectedChoicePropertyChange(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MaterialTextField control)
                control.SetSelectedChoice(newValue);
        }

        private void SetSelectedChoice(object selectedChoice)
        {
            var choices = Choices;
            if (choices?.Count > 0)
            {
                // 1. Get selected value index
                // 1.1 Current selected index and get selected index should not same.
                // 2. set control selected index

                var index = choices.IndexOf(selectedChoice);
                if (_selectedIndex != index)
                {
                    _selectedIndex = index;
                    Text = index >= 0 ? _choicesResults[index] : null;
                    AnimateToInactiveOrFocusedStateOnStart(this, false);

                    UpdateCounter();
                }
            }
            else
            {
                Text = null;
            }
        }

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

        public static readonly BindableProperty IsTextAllCapsProperty = BindableProperty.Create(nameof(IsTextAllCaps), typeof(bool), typeof(MaterialTextField), false);

        public static readonly BindableProperty IsMaxLengthCounterVisibleProperty = BindableProperty.Create(nameof(IsMaxLengthCounterVisible), typeof(bool), typeof(MaterialTextField), true);

        public static readonly BindableProperty IsSpellCheckEnabledProperty = BindableProperty.Create(nameof(IsSpellCheckEnabled), typeof(bool), typeof(MaterialTextField), false);

        public static readonly BindableProperty IsTextPredictionEnabledProperty = BindableProperty.Create(nameof(IsTextPredictionEnabled), typeof(bool), typeof(MaterialTextField), false);

        public static readonly BindableProperty LeadingIconProperty = BindableProperty.Create(nameof(LeadingIcon), typeof(ImageSource), typeof(MaterialTextField));

        public static readonly BindableProperty LeadingIconTintColorProperty = BindableProperty.Create(nameof(LeadingIconTintColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#99000000"));

        public static readonly BindableProperty TrailingIconTintColorProperty = BindableProperty.Create(nameof(TrailingIconTintColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#99000000"));

        public static readonly BindableProperty ErrorIconProperty = BindableProperty.Create(nameof(ErrorIcon), typeof(ImageSource), typeof(MaterialDateField), new FileImageSource { File = "xf_error" });

        public static readonly BindableProperty TrailingIconProperty = BindableProperty.Create(nameof(TrailingIcon), typeof(ImageSource), typeof(MaterialDateField), null);

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

        public static readonly BindableProperty PlaceholderLineBreakModeProperty = BindableProperty.Create(nameof(PlaceholderLineBreakMode), typeof(LineBreakMode), typeof(MaterialTextField), LineBreakMode.WordWrap);

        public static readonly BindableProperty LeadingIconSizeProperty = BindableProperty.Create(nameof(LeadingIconSize), typeof(double), typeof(MaterialTextField), 24.0);

        public static readonly BindableProperty TrailingIconSizeProperty = BindableProperty.Create(nameof(TrailingIconSize), typeof(double), typeof(MaterialTextField), 24.0);

        //public static readonly BindableProperty ChoicesBindingNameProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialTextField), string.Empty, BindingMode.TwoWay);

        private const double AnimationDuration = 0.35;
        private readonly Easing _animationCurve = Easing.SinOut;
        private readonly Dictionary<string, Action> _propertyChangeActions;
        private IList<string> _choices;
        private IList<string> _choicesResults;
        private bool _counterEnabled;
        private DisplayInfo _lastDeviceDisplay;
        private int _selectedIndex = -1;

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialTextField"/>.
        /// </summary>
        public MaterialTextField()
        {
            InitializeComponent();
            SetPropertyChangeHandler(ref _propertyChangeActions);
            SetControl();
            _lastDeviceDisplay = DeviceDisplay.MainDisplayInfo;
        }

        public event EventHandler<SelectedItemChangedEventArgs> ChoiceSelected;
        public event EventHandler<EventArgs> TrailingIconSelected;

        /// <summary>
        /// Raised when this text field receives focus.
        /// </summary>
        public new event EventHandler<FocusEventArgs> Focused;

        /// <summary>
        /// Raised when this text field loses focus.
        /// </summary>
        public new event EventHandler<FocusEventArgs> Unfocused;

        /// <summary>
        /// Raised when the input text of this text field has changed.
        /// </summary>
        public event EventHandler<TextChangedEventArgs> TextChanged;

        /// <summary>
        /// Raised when the user finalizes the input on this text field using the return key.
        /// </summary>
        public event EventHandler Completed;

        /// <summary>
        /// Gets or sets whether the underline accent of this text field should always show or not.
        /// </summary>
        public bool AlwaysShowUnderline
        {
            get => (bool)GetValue(AlwaysShowUnderlineProperty);
            set => SetValue(AlwaysShowUnderlineProperty, value);
        }

        /// <summary>
        /// Gets or sets the background color of this text field.
        /// </summary>
        public Color CardBackgroundColor
        {
            get => (Color)GetValue(CardBackgroundColorProperty);
            set => SetValue(CardBackgroundColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the corner radius of this text field.
        /// </summary>
        public CornerRadius CardCornerRadius
        {
            get => (CornerRadius)GetValue(CardCornerRadiusProperty);
            set => SetValue(CardCornerRadiusProperty, value);
        }

        /// <summary>
        /// Gets or sets the collection of objects which the user will choose from. This is required when <see cref="InputType"/> is set to <see cref="MaterialTextFieldInputType.Choice"/> or <see cref="MaterialTextFieldInputType.SingleImmediateChoice"/>.
        /// </summary>
        public IList Choices
        {
            get => (IList)GetValue(ChoicesProperty);
            set => SetValue(ChoicesProperty, value);
        }

        public object SelectedChoice
        {
            get => (object)GetValue(SelectedChoiceProperty);
            set => SetValue(SelectedChoiceProperty, value);
        }


        /// <summary>
        /// Gets or sets the name of the property to display of each object in the <see cref="Choices"/> property. This will be ignored if the objects are strings.
        /// </summary>


        /// <summary>
        /// Gets or sets the command that will execute if a choice was selected when the <see cref="InputType"/> is set to <see cref="MaterialTextFieldInputType.Choice"/> or <see cref="MaterialTextFieldInputType.SingleImmediateChoice"/>.
        /// </summary>
        public ICommand ChoiceSelectedCommand
        {
            get => (ICommand)GetValue(ChoiceSelectedCommandProperty);
            set => SetValue(ChoiceSelectedCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the command for TrailingIcon />.
        /// </summary>
        public ICommand TrailingIconCommand
        {
            get => (ICommand)GetValue(TrailingIconCommandProperty);
            set => SetValue(TrailingIconCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the color to indicate an error in this text field.
        /// The default value is the color of <see cref="MaterialColorConfiguration.Error"/>.
        /// </summary>
        public Color ErrorColor
        {
            get => (Color)GetValue(ErrorColorProperty);
            set => SetValue(ErrorColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the error text of this text field.
        /// </summary>
        public string ErrorText
        {
            get => (string)GetValue(ErrorTextProperty);
            set => SetValue(ErrorTextProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the floating placeholder.
        /// </summary>
        public Color FloatingPlaceholderColor
        {
            get => (Color)GetValue(FloatingPlaceholderColorProperty);
            set => SetValue(FloatingPlaceholderColorProperty, value);
        }

        /// <summary>
        /// Gets or sets whether the placeholder label will float at top of the text field when focused or when it has text.
        /// </summary>
        public bool FloatingPlaceholderEnabled
        {
            get => (bool)GetValue(FloatingPlaceholderEnabledProperty);
            set => SetValue(FloatingPlaceholderEnabledProperty, value);
        }

        /// <summary>
        /// Gets or sets the font size of the floating placeholder.
        /// </summary>
        public double FloatingPlaceholderFontSize
        {
            get => (double)GetValue(FloatingPlaceholderFontSizeProperty);
            set => SetValue(FloatingPlaceholderFontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that will be executed when this text field receives or loses focus.
        /// </summary>
        public Command<bool> FocusCommand
        {
            get => (Command<bool>)GetValue(FocusCommandProperty);
            set => SetValue(FocusCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the boolean value whether this text field has an error, and if it will show the its error text.
        /// </summary>
        public bool HasError
        {
            get => (bool)GetValue(HasErrorProperty);
            set => SetValue(HasErrorProperty, value);
        }

        /// <summary>
        /// Gets or sets the helper text of this text field.
        /// </summary>
        public string HelperText
        {
            get => (string)GetValue(HelperTextProperty);
            set => SetValue(HelperTextProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of this text field's helper text.
        /// </summary>
        public Color HelperTextColor
        {
            get => (Color)GetValue(HelperTextColorProperty);
            set => SetValue(HelperTextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family of this text field's helper text.
        /// </summary>
        public string HelperTextFontFamily
        {
            get => (string)GetValue(HelperTextFontFamilyProperty);
            set => SetValue(HelperTextFontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets the horizontal padding of the text field.
        /// </summary>
        public MaterialHorizontalThickness HorizontalPadding
        {
            get => (MaterialHorizontalThickness)GetValue(HorizontalPaddingProperty);
            set => SetValue(HorizontalPaddingProperty, value);
        }

        /// <summary>
        /// Gets or sets the keyboard input type of this text field.
        /// </summary>
        public MaterialTextFieldInputType InputType
        {
            get => (MaterialTextFieldInputType)GetValue(InputTypeProperty);
            set => SetValue(InputTypeProperty, value);
        }

        /// <summary>
        /// Gets or sets whether auto capitialization is enabled.
        /// </summary>
        public bool IsAutoCapitalizationEnabled
        {
            get => (bool)GetValue(IsAutoCapitalizationEnabledProperty);
            set => SetValue(IsAutoCapitalizationEnabledProperty, value);
        }

        /// <summary>
        /// Gets or sets whether the input text should be in All Caps.
        /// </summary>
        public bool IsTextAllCaps
        {
            get => (bool)GetValue(IsTextAllCapsProperty);
            set => SetValue(IsTextAllCapsProperty, value);
        }

        /// <summary>
        /// Gets or sets whether the counter for the max input length of this text field is visible or not.
        /// </summary>
        public bool IsMaxLengthCounterVisible
        {
            get => (bool)GetValue(IsMaxLengthCounterVisibleProperty);
            set => SetValue(IsMaxLengthCounterVisibleProperty, value);
        }

        /// <summary>
        /// Gets or sets whether spell checking is enabled.
        /// </summary>
        public bool IsSpellCheckEnabled
        {
            get => (bool)GetValue(IsSpellCheckEnabledProperty);
            set => SetValue(IsSpellCheckEnabledProperty, value);
        }

        /// <summary>
        /// Gets or sets whether text prediction is enabled.
        /// </summary>
        public bool IsTextPredictionEnabled
        {
            get => (bool)GetValue(IsTextPredictionEnabledProperty);
            set => SetValue(IsTextPredictionEnabledProperty, value);
        }

        /// <summary>
        /// Gets or sets the image source of the icon to be showed at the left side of this text field.
        /// </summary>
        public ImageSource LeadingIcon
        {
            get => (ImageSource)GetValue(LeadingIconProperty);
            set => SetValue(LeadingIconProperty, value);
        }

        /// <summary>
        /// Gets or sets the tint color of the icon of this text field.
        /// </summary>
        public Color LeadingIconTintColor
        {
            get => (Color)GetValue(LeadingIconTintColorProperty);
            set => SetValue(LeadingIconTintColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the image source of the icon to be showed at the left side of this text field.
        /// </summary>
        [Xamarin.Forms.TypeConverter(typeof(ImageSourceConverter))]
        public ImageSource ErrorIcon
        {
            get => (ImageSource)GetValue(ErrorIconProperty);
            set => SetValue(ErrorIconProperty, value);
        }

        /// <summary>
        /// Gets or sets the image source of the icon to be showed at the left side of this text field.
        /// </summary>
        [Xamarin.Forms.TypeConverter(typeof(ImageSourceConverter))]
        public ImageSource TrailingIcon
        {
            get => (ImageSource)GetValue(TrailingIconProperty);
            set => SetValue(TrailingIconProperty, value);
        }


        /// <summary>
        /// Gets or sets the tint color of the trailing icon of this text field.
        /// </summary>
        public Color TrailingIconTintColor
        {
            get => (Color)GetValue(TrailingIconTintColorProperty);
            set => SetValue(TrailingIconTintColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the maximum allowed number of characters in this text field.
        /// </summary>
        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        /// <summary>
        /// Gets or sets the text of this text field's placeholder.
        /// </summary>
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of this text field's placeholder.
        /// </summary>
        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family of this text field's placeholder
        /// </summary>
        public string PlaceholderFontFamily
        {
            get => (string)GetValue(PlaceholderFontFamilyProperty);
            set => SetValue(PlaceholderFontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that will run when the user returns the input in this textfield.
        /// </summary>
        public ICommand ReturnCommand
        {
            get => (ICommand)GetValue(ReturnCommandProperty);
            set => SetValue(ReturnCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the parameter of <see cref="ReturnCommand"/>.
        /// </summary>
        public object ReturnCommandParameter
        {
            get => GetValue(ReturnCommandParameterProperty);
            set => SetValue(ReturnCommandParameterProperty, value);
        }

        /// <summary>
        /// Gets or sets the parameter of <see cref="ChoiceSelectedCommand"/>.
        /// </summary>
        public object ChoiceSelectedCommandParameter
        {
            get => GetValue(ChoiceSelectedCommandParameterProperty);
            set => SetValue(ChoiceSelectedCommandParameterProperty, value);
        }

        /// <summary>
        /// Gets or sets the parameter of <see cref="ChoiceSelectedCommand"/>.
        /// </summary>
        public object TrailingIconCommandParameter
        {
            get => GetValue(TrailingIconCommandParameterProperty);
            set => SetValue(TrailingIconCommandParameterProperty, value);
        }

        /// <summary>
        /// Gets or sets the return type of this textfield.
        /// </summary>
        public ReturnType ReturnType
        {
            get => (ReturnType)GetValue(ReturnTypeProperty);
            set => SetValue(ReturnTypeProperty, value);
        }

        /// <summary>
        /// Gets or sets whether the underline indicator will be animated. If set to false, the underline will not be shown.
        /// </summary>
        public bool ShouldAnimateUnderline
        {
            get => (bool)GetValue(ShouldAnimateUnderlineProperty);
            set => SetValue(ShouldAnimateUnderlineProperty, value);
        }

        public string ChoicesBindingName { get; set; }

        public string ChoicesResultBindingName { get; set; }

        //public string ChoicesBindingName
        //{
        //    get => (string)this.GetValue(ChoicesBindingNameProperty);
        //    set => this.SetValue(ChoicesBindingNameProperty, value);
        //}



        /// <summary>
        /// Gets or sets the input text of this text field.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that will execute if there is a change in this text field's input text.
        /// </summary>
        public Command<string> TextChangeCommand
        {
            get => (Command<string>)GetValue(TextChangeCommandProperty);
            set => SetValue(TextChangeCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of this text field's input text.
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family of this text field's input text.
        /// </summary>
        public string TextFontFamily
        {
            get => (string)GetValue(TextFontFamilyProperty);
            set => SetValue(TextFontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets the text's font size.
        /// </summary>
        public double TextFontSize
        {
            get => (double)GetValue(TextFontSizeProperty);
            set => SetValue(TextFontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the tint color of the underline and the placeholder of this text field when focused.
        /// The default value is the color of <see cref="MaterialColorConfiguration.Secondary"/>.
        /// </summary>
        public Color TintColor
        {
            get => (Color)GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the underline when this text field is activated. <see cref="AlwaysShowUnderline"/> is set to true.
        /// </summary>
        public Color UnderlineColor
        {
            get => (Color)GetValue(UnderlineColorProperty);
            set => SetValue(UnderlineColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the line break mode for the placeholder
        /// </summary>
        public LineBreakMode PlaceholderLineBreakMode
        {
            get { return (LineBreakMode)GetValue(PlaceholderLineBreakModeProperty); }
            set { SetValue(PlaceholderLineBreakModeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the icon size. Maximum value should be 24.0
        /// </summary>
        public double LeadingIconSize
        {
            get { return (double)GetValue(LeadingIconSizeProperty); }
            set { SetValue(LeadingIconSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the icon size. Maximum value should be 24.0
        /// </summary>
        public double TrailingIconSize
        {
            get { return (double)GetValue(TrailingIconSizeProperty); }
            set { SetValue(TrailingIconSizeProperty, value); }
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
                entry.PropertyChanged += Entry_PropertyChanged;
                entry.TextChanged += Entry_TextChanged;
                entry.SizeChanged += Entry_SizeChanged;
                entry.Focused += Entry_Focused;
                entry.Unfocused += Entry_Unfocused;
                entry.Completed += Entry_Completed;
                DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
            }
            else
            {
                entry.PropertyChanged -= Entry_PropertyChanged;
                entry.TextChanged -= Entry_TextChanged;
                entry.SizeChanged -= Entry_SizeChanged;
                entry.Focused -= Entry_Focused;
                entry.Unfocused -= Entry_Unfocused;
                entry.Completed -= Entry_Completed;
                DeviceDisplay.MainDisplayInfoChanged -= DeviceDisplay_MainDisplayInfoChanged;
            }
        }

        /// <summary>
        /// Requests to set focus on this text field.
        /// </summary>
        public new void Focus() => entry.Focus();

        /// <summary>
        /// Requests to unset the focus on this text field.
        /// </summary>
        public new void Unfocus() => entry.Unfocus();

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            AnimateToInactiveOrFocusedStateOnStart(BindingContext, true);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            OnBackgroundColorChanged(); //For global styles
            AnimateToInactiveOrFocusedStateOnStart(Parent, true);
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
            var hasText = !string.IsNullOrEmpty(Text);

            if (entry.IsFocused)
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

                var endHeight = hasText ? 1 : 0;

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
            var preferredStartFont = FloatingPlaceholderFontSize == 0 ? entry.FontSize * 0.75 : FloatingPlaceholderFontSize;
            var preferredEndFont = FloatingPlaceholderFontSize == 0 ? entry.FontSize * 0.75 : FloatingPlaceholderFontSize;
            var startFont = entry.IsFocused ? entry.FontSize : preferredStartFont;
            var endFOnt = entry.IsFocused ? preferredEndFont : entry.FontSize;
            var startY = placeholder.TranslationY;
            var endY = entry.IsFocused ? -(entry.FontSize * 0.8) : 0;

            if (HasError)
            {
                tintColor = entry.IsFocused ? ErrorColor : PlaceholderColor;
            }
            else
            {
                tintColor = entry.IsFocused ? TintColor : PlaceholderColor;
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
                        if(HasError && entry.IsFocused)
                        {
                            placeholder.TextColor = ErrorColor;
                        }

                        placeholder.TextColor = tintColor;
                    })
                }
            } : new Animation();

            if (entry.IsFocused)
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
                    underline.Color = HasError ? ErrorColor : UnderlineColor;

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

        private void AnimateToInactiveOrFocusedStateOnStart(object startObject, bool isStart)
        {
            var placeholderEndY = -(entry.FontSize * 0.8);
            var placeholderEndFont = entry.FontSize * 0.75;

            if (!FloatingPlaceholderEnabled && string.IsNullOrEmpty(entry.Text))
            {
                placeholder.TextColor = PlaceholderColor;
            }

            if (isStart && startObject != null)
                SetTrailingIcon();

            if (startObject != null && !string.IsNullOrEmpty(Text))
            {
                if (placeholder.TranslationY == placeholderEndY)
                {
                    return;
                }

                entry.Opacity = 0;

                Device.BeginInvokeOnMainThread(() =>
                {
                    var anim = new Animation();


                    if (FloatingPlaceholderEnabled)
                    {

                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.FontSize = v, entry.FontSize, placeholderEndFont, _animationCurve));

                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.TranslationY = v, 0, placeholderEndY, _animationCurve, () =>
                       {
                           placeholder.TextColor = HasError ? ErrorColor : FloatingPlaceholderColor;
                           entry.Opacity = 1;
                           UpdateCounter();
                       }));

                    }

                    if (ShouldAnimateUnderline)
                    {
                        underline.Color = HasError ? ErrorColor : UnderlineColor;
                        underline.HeightRequest = 1;
                        underline.HorizontalOptions = LayoutOptions.FillAndExpand;
                    }

                    anim.Commit(this, "Anim1", rate: 2, length: (uint)(AnimationDuration * 1000), easing: _animationCurve);

                });

                entry.Opacity = 1;

                return;
            }

            if (startObject != null && string.IsNullOrEmpty(Text) && placeholder.TranslationY == placeholderEndY)
            {
                if (entry.IsFocused)
                {
                    return;
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    var anim = new Animation();

                    if (FloatingPlaceholderEnabled)
                    {
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.FontSize = v, placeholderEndFont, entry.FontSize, _animationCurve));
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.TranslationY = v, placeholder.TranslationY, 0, _animationCurve, () =>
                        {
                            placeholder.TextColor = PlaceholderColor;
                            entry.Opacity = 1;
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
            placeholder.TextColor = (FloatingPlaceholderEnabled && entry.IsFocused) || (FloatingPlaceholderEnabled && !string.IsNullOrEmpty(Text)) ? ErrorColor : PlaceholderColor;
            counter.TextColor = ErrorColor;
            underline.Color = ShouldAnimateUnderline ? ErrorColor : Color.Transparent;
            persistentUnderline.Color = AlwaysShowUnderline ? ErrorColor : Color.Transparent;
            trailingIcon.IsVisible = true;
            trailingIcon.Source = ErrorIcon;
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
            entry.Opacity = opactiy;
            placeholder.Opacity = opactiy;
            helper.Opacity = opactiy;
            underline.Opacity = opactiy;

            Device.BeginInvokeOnMainThread(async () =>
            {
                SetTrailingIcon();


                var accentColor = TintColor;
                placeholder.TextColor = accentColor;
                counter.TextColor = HelperTextColor;
                underline.Color = accentColor;
                persistentUnderline.Color = UnderlineColor;

                if (string.IsNullOrEmpty(ErrorText))
                {
                    helper.TextColor = HelperTextColor;
                    helper.Text = HelperText;
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

        private void SetTrailingIcon()
        {

            if (TrailingIcon == null)
            {

                if (InputType == MaterialTextFieldInputType.Choice || InputType == MaterialTextFieldInputType.SingleImmediateChoice)
                {
                    trailingIcon.Source = "xf_arrow_dropdown";
                    trailingIcon.TintColor = TrailingIconTintColor;
                }
                if (InputType == MaterialTextFieldInputType.CommandChoice)
                {
                    trailingIcon.Source = "xf_arrow_right";
                    trailingIcon.TintColor = TrailingIconTintColor;
                }
                else
                {
                    trailingIcon.IsVisible = false;
                }
            }
            else
            {
                trailingIcon.Source = TrailingIcon;
                trailingIcon.TintColor = TrailingIconTintColor;
                trailingIcon.IsVisible = true;
            }
        }

        private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            if (e.DisplayInfo.Orientation != _lastDeviceDisplay.Orientation)
            {
                if (!string.IsNullOrEmpty(entry.Text) && ShouldAnimateUnderline)
                {
                    underline.WidthRequest = -1;
                    underline.HorizontalOptions = LayoutOptions.FillAndExpand;
                }

                _lastDeviceDisplay = e.DisplayInfo;
            }
        }

        private void Entry_Completed(object sender, EventArgs e) => Completed?.Invoke(this, EventArgs.Empty);

        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            if (InputType == MaterialTextFieldInputType.Choice || InputType == MaterialTextFieldInputType.SingleImmediateChoice
                                                               || InputType == MaterialTextFieldInputType.CommandChoice)
            {
                ((View)sender).Unfocus();
            }
            else
            {
                FocusCommand?.Execute(entry.IsFocused);
                Focused?.Invoke(this, e);
                UpdateCounter();
            }
        }

        private void Entry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IsFocused) when string.IsNullOrEmpty(entry.Text):
                    AnimateToInactiveOrFocusedState();
                    break;

                case nameof(IsFocused) when !string.IsNullOrEmpty(entry.Text):
                    AnimateToActivatedState();
                    break;

                case nameof(Entry.Text):
                    Text = entry.Text;
                    UpdateCounter();
                    break;


            }
        }

        private void Entry_SizeChanged(object sender, EventArgs e)
        {
            var baseHeight = FloatingPlaceholderEnabled ? 56 : 40;
            var diff = entry.Height - 20;
            var rawRowHeight = baseHeight + diff;
            _autoSizingRow.Height = new GridLength(rawRowHeight);

            var iconVerticalMargin = (_autoSizingRow.Height.Value - 24) / 2;

            if (leadingIcon.IsVisible)
            {
                leadingIcon.Margin = new Thickness(HorizontalPadding.Left, iconVerticalMargin, 0, iconVerticalMargin);
                entry.Margin = new Thickness(12, entry.Margin.Top, HorizontalPadding.Right, entry.Margin.Bottom);
            }
            else
            {
                entry.Margin = new Thickness(HorizontalPadding.Left, entry.Margin.Top, HorizontalPadding.Right, entry.Margin.Bottom);
            }

            if (trailingIcon.IsVisible)
            {
                var entryPaddingLeft = leadingIcon.IsVisible ? 12 : HorizontalPadding;
                trailingIcon.Margin = new Thickness(12, iconVerticalMargin, HorizontalPadding.Right, iconVerticalMargin);
                entry.Margin = new Thickness(entryPaddingLeft.Left, entry.Margin.Top, 0, entry.Margin.Bottom);
            }

            helper.Margin = new Thickness(HorizontalPadding.Left, helper.Margin.Top, 12, 0);
            counter.Margin = new Thickness(0, counter.Margin.Top, HorizontalPadding.Right, 0);

            var placeholderLeftMargin = FloatingPlaceholderEnabled ? HorizontalPadding.Left : entry.Margin.Left;
            placeholder.Margin = new Thickness(placeholderLeftMargin, 0, 0, 0);

            if (HasError)
            {
                underline.Color = ErrorColor;
            }
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChangeCommand?.Execute(entry.Text);
            TextChanged?.Invoke(this, e);
        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            FocusCommand?.Execute(entry.IsFocused);
            Unfocused?.Invoke(this, e);
            UpdateCounter();
        }

        private IList<string> GetChoices(out IList<string> choicesResults)
        {
            var choiceStrings = new List<string>(Choices.Count);
            choicesResults = new List<string>(Choices.Count);
            var listType = Choices[0].GetType();
            foreach (var item in Choices)
            {
                string choice = item.ToString();
                if (!string.IsNullOrEmpty(ChoicesBindingName))
                {
                    var propInfo = listType.GetProperty(ChoicesBindingName);

                    if (propInfo != null)
                    {
                        var propValue = propInfo.GetValue(item);
                        choice = propValue?.ToString();
                    }
                }

                choiceStrings.Add(choice);

                string choiceResult = choice;
                if (!string.IsNullOrEmpty(ChoicesResultBindingName))
                {
                    var propInfo = listType.GetProperty(ChoicesResultBindingName);

                    if (propInfo != null)
                    {
                        var propValue = propInfo.GetValue(item);
                        choiceResult = propValue.ToString();
                    }
                }

                choicesResults.Add(choiceResult);
            }

            return choiceStrings;
        }

        private object GetSelectedChoice(int index)
        {
            if (index < 0)
            {
                return null;
            }

            return Choices[index];
        }

        private void OnAlwaysShowUnderlineChanged(bool isShown)
        {
            persistentUnderline.IsVisible = isShown;
            persistentUnderline.Color = UnderlineColor;
        }

        private void OnTrailingIconSelected(object sender, EventArgs e)
        {
            TrailingIconSelected?.Invoke(this, e);
            TrailingIconCommand?.Execute(TrailingIconCommandParameter);
        }


        private void OnBackgroundColorChanged()
        {
            if (base.BackgroundColor != Color.Transparent)
            {
                if (base.BackgroundColor != Color.Default)
                    CardBackgroundColor = base.BackgroundColor;
                base.BackgroundColor = Color.Transparent;
            }
        }

        private void OnChoicesChanged(ICollection choices)
        {
            if (choices?.Count > 0)
            {
                _choices = GetChoices(out _choicesResults);
            }
            else
            {
                _choices = null;
                _choicesResults = null;
            }

            SetSelectedChoice(SelectedChoice);
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

        private void OnPlaceholderLineBreakModeChanged()
        {
            placeholder.LineBreakMode = PlaceholderLineBreakMode;
        }

        private void OnLeadingIconSizeChanged()
        {
            leadingIcon.WidthRequest = LeadingIconSize;
            leadingIcon.HeightRequest = LeadingIconSize;
        }

        private void OnTrailingIconSizeChanged()
        {
            trailingIcon.WidthRequest = TrailingIconSize;
            trailingIcon.HeightRequest = TrailingIconSize;
        }

        private void OnFloatingPlaceholderEnabledChanged(bool isEnabled)
        {
          
            entry.Margin = isEnabled ? new Thickness(entry.Margin.Left, 24, entry.Margin.Right, 0) : new Thickness(entry.Margin.Left, 0, entry.Margin.Right, 0);

            var iconMargin = leadingIcon.Margin;
            leadingIcon.Margin = isEnabled ? new Thickness(iconMargin.Left, 16, iconMargin.Right, 16) : new Thickness(iconMargin.Left, 8, iconMargin.Right, 8);

            var trailingIconMargin = trailingIcon.Margin;
            trailingIcon.Margin = isEnabled ? new Thickness(trailingIconMargin.Left, 16, trailingIconMargin.Right, 16) : new Thickness(trailingIconMargin.Left, 8, trailingIconMargin.Right, 8);

            UpdatePlaceholderVisibility();
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

        private void OnInputTypeChanged()
        {
            switch (InputType)
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

                //Only when plain type of keyboard we need consider the keyboard flags
                //Same as Xamarin.Forms.Entry control
                case MaterialTextFieldInputType.Plain:
                    var flags = KeyboardFlags.None;
                    if (IsTextAllCaps)
                    {
                        flags |= KeyboardFlags.CapitalizeCharacter;
                    }

                    if (IsAutoCapitalizationEnabled && !IsTextAllCaps)
                    {
                        flags |= KeyboardFlags.CapitalizeWord;
                    }

                    if (IsSpellCheckEnabled)
                    {
                        flags |= KeyboardFlags.Spellcheck;
                    }

                    if (IsTextPredictionEnabled)
                    {
                        flags |= KeyboardFlags.Suggestions;
                    }

                    entry.Keyboard = Keyboard.Create(flags);
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
                    entry.Keyboard = Keyboard.Plain;
                    break;

                case MaterialTextFieldInputType.Choice:
                case MaterialTextFieldInputType.SingleImmediateChoice:
                case MaterialTextFieldInputType.CommandChoice:
                    break;
            }



            // Hint: Will use this for MaterialTextArea
            // entry.AutoSize = inputType == MaterialTextFieldInputType.MultiLine ? EditorAutoSizeOption.TextChanges : EditorAutoSizeOption.Disabled;
            var isChoice = InputType == MaterialTextFieldInputType.Choice || InputType == MaterialTextFieldInputType.SingleImmediateChoice || InputType == MaterialTextFieldInputType.CommandChoice;

            if (isChoice)
            {
                _gridContainer.InputTransparent = isChoice;
                trailingIcon.IsVisible = isChoice;
            }

            entry.IsNumericKeyboard = InputType == MaterialTextFieldInputType.Telephone || InputType == MaterialTextFieldInputType.Numeric;
            entry.IsPassword = InputType == MaterialTextFieldInputType.Password || InputType == MaterialTextFieldInputType.NumericPassword;
        }

        private void OnLeadingIconChanged(ImageSource imageSource)
        {
            leadingIcon.Source = imageSource;
            OnLeadingIconTintColorChanged(LeadingIconTintColor);
        }

        private void OnLeadingIconTintColorChanged(Color tintColor)
        {
            leadingIcon.TintColor = tintColor;
        }

        private void OnTrailingIconChanged(ImageSource imageSource)
        {
            trailingIcon.Source = imageSource;
            OnTrailingIconTintColorChanged(TrailingIconTintColor);
        }

        private void OnTrailingIconTintColorChanged(Color tintColor)
        {
            trailingIcon.TintColor = tintColor;
        }

        private void OnMaxLengthChanged(int maxLength, bool isMaxLengthCounterVisible)
        {
            _counterEnabled = maxLength > 0 && isMaxLengthCounterVisible;
            entry.MaxLength = maxLength > 0 ? maxLength : (int)InputView.MaxLengthProperty.DefaultValue;
        }

        private void OnPlaceholderChanged(string placeholderText)
        {
            placeholder.Text = placeholderText ?? string.Empty;
            UpdatePlaceholderVisibility();
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

        private void OnReturnTypeChanged(ReturnType returnType)
        {
            entry.ReturnType = returnType;
        }

        private async Task OnSelectChoices()
        {
            if (InputType == MaterialTextFieldInputType.CommandChoice)
            {
                ChoiceSelected?.Invoke(this, new SelectedItemChangedEventArgs(ChoiceSelectedCommandParameter, -1));
                ChoiceSelectedCommand?.Execute(ChoiceSelectedCommandParameter);
                return;
            }

            if (Choices == null || Choices?.Count <= 0)
            {
                //throw new InvalidOperationException("The property `Choices` is null or empty");
                Console.WriteLine("Error in MaterialTextField: the property `Choices` is null or empty");
                return;
            }
            _choices = GetChoices(out _choicesResults);

            var title = MaterialConfirmationDialog.GetDialogTitle(this);
            var confirmingText = MaterialConfirmationDialog.GetDialogConfirmingText(this);
            var dismissiveText = MaterialConfirmationDialog.GetDialogDismissiveText(this);
            var configuration = MaterialConfirmationDialog.GetDialogConfiguration(this);
            int result;

            if (_selectedIndex >= 0)
            {
                result = await MaterialDialog.Instance.SelectChoiceAsync(title, _choices, _selectedIndex, confirmingText, dismissiveText, configuration, InputType == MaterialTextFieldInputType.SingleImmediateChoice);
            }
            else
            {
                result = await MaterialDialog.Instance.SelectChoiceAsync(title, _choices, confirmingText, dismissiveText, configuration, InputType == MaterialTextFieldInputType.SingleImmediateChoice);
            }

            if (result >= 0)
            {
                _selectedIndex = result;
                Text = _choicesResults[result];
                // entry.Text = Text;
            }
        }

        private void OnTextChanged(string text)
        {
            var isChoice = InputType == MaterialTextFieldInputType.Choice || InputType == MaterialTextFieldInputType.SingleImmediateChoice;

            if (isChoice)
            {
                if (!string.IsNullOrEmpty(text) && _choicesResults?.Contains(text) == false)
                {
                    Debug.WriteLine($"The `Text` property value `{Text}` does not match any item in the collection `Choices`.");
                    Text = null;
                    return;
                }

                if (!string.IsNullOrEmpty(text))
                {
                    var selectedChoice = GetSelectedChoice(_selectedIndex);
                    SelectedChoice = selectedChoice;
                    ChoiceSelected?.Invoke(this, new SelectedItemChangedEventArgs(selectedChoice, _selectedIndex));
                    ChoiceSelectedCommand?.Execute(selectedChoice);
                }
                else if (string.IsNullOrEmpty(text))
                {
                    _selectedIndex = -1;
                }
            }

            entry.Text = text;

            AnimateToInactiveOrFocusedStateOnStart(this, false);
            UpdatePlaceholderVisibility();
            UpdateCounter();
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
            if (AlwaysShowUnderline)
            {
                persistentUnderline.Color = underlineColor;
            }
        }

        private void SetControl()
        {
            trailingIcon.TintColor = TextColor;
            persistentUnderline.Color = UnderlineColor;
            tapGesture.Command = new Command(() =>
            {
                if (!entry.IsFocused)
                {
                    entry.Focus();
                }
            });

            mainTapGesture.Command = new Command(async () => await OnSelectChoices());
        }

        private void SetPropertyChangeHandler(ref Dictionary<string, Action> propertyChangeActions)
        {
            propertyChangeActions = new Dictionary<string, Action>
            {
                { nameof(Text), () => OnTextChanged(Text) },
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
                { nameof(BackgroundColor), () => OnBackgroundColorChanged() },
                { nameof(AlwaysShowUnderline), () => OnAlwaysShowUnderlineChanged(AlwaysShowUnderline) },
                { nameof(MaxLength), () => OnMaxLengthChanged(MaxLength, IsMaxLengthCounterVisible) },
                { nameof(ReturnCommand), () => OnReturnCommandChanged(ReturnCommand) },
                { nameof(ReturnCommandParameter), () => OnReturnCommandParameterChanged(ReturnCommandParameter) },
                { nameof(ReturnType), () => OnReturnTypeChanged(ReturnType) },
                { nameof(ErrorColor), () => OnErrorColorChanged(ErrorColor) },
                { nameof(UnderlineColor), () => OnUnderlineColorChanged(UnderlineColor) },
                { nameof(HasError), () => OnHasErrorChanged() },
                { nameof(FloatingPlaceholderEnabled), () => OnFloatingPlaceholderEnabledChanged(FloatingPlaceholderEnabled) },
                { nameof(Choices), () => OnChoicesChanged(Choices) },
                { nameof(LeadingIcon), () => OnLeadingIconChanged(LeadingIcon) },
                { nameof(LeadingIconTintColor), () => OnLeadingIconTintColorChanged(LeadingIconTintColor) },
                { nameof(TrailingIcon), () => OnTrailingIconChanged(TrailingIcon) },
                { nameof(TrailingIconTintColor), () => OnTrailingIconTintColorChanged(TrailingIconTintColor) },
                { nameof(InputType), () => OnInputTypeChanged() },
                { nameof(IsSpellCheckEnabled), () => OnInputTypeChanged() },
                { nameof(IsTextPredictionEnabled), () => OnInputTypeChanged() },
                { nameof(IsAutoCapitalizationEnabled), () => OnInputTypeChanged() },
                { nameof(IsTextAllCaps), () => OnInputTypeChanged() },
                { nameof(TextFontSize), () => OnTextFontSizeChanged(TextFontSize) },
                { nameof(ErrorText), () => OnErrorTextChanged() },
                { nameof(PlaceholderLineBreakMode), () => OnPlaceholderLineBreakModeChanged()},
                { nameof(LeadingIconSize), () => OnLeadingIconSizeChanged()},
                { nameof(TrailingIconSize), () => OnTrailingIconSizeChanged()}
            };
        }

        private void UpdatePlaceholderVisibility()
        {
            if (!string.IsNullOrEmpty(Text) && !FloatingPlaceholderEnabled)
            {
                placeholder.IsVisible = false;
            }
            else if (string.IsNullOrEmpty(Text) && !FloatingPlaceholderEnabled)
            {
                placeholder.IsVisible = true;
            }
        }

        private void UpdateCounter()
        {
            if (!_counterEnabled)
            {
                return;
            }


            var count = entry.Text?.Length ?? 0;
            counter.Text = entry.IsFocused ? $"{count}/{MaxLength}" : string.Empty;

            if (!string.IsNullOrEmpty(counter.Text))
            {
                counter.IsVisible = true;
            }
            else
            {
                counter.IsVisible = false;
            }
        }
    }
}
