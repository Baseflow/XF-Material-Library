using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Xaml;
using XF.Material.Maui.Resources;
using Keyboard = Microsoft.Maui.Keyboard;

namespace XF.Material.Maui.UI.Internals
{
    /// <summary>
    /// For internal use only.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialInputDialogTextField : ContentView, IMaterialElementConfiguration
    {
        public static readonly BindableProperty AlwaysShowUnderlineProperty = BindableProperty.Create(nameof(AlwaysShowUnderline), typeof(bool), typeof(MaterialInputDialogTextField), false);

        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialInputDialogTextField), Color.FromArgb("#DCDCDC"));

        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(MaterialInputDialogTextField), Material.Color.Error);

        public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(nameof(ErrorText), typeof(string), typeof(MaterialInputDialogTextField), "Error");

        public static readonly BindableProperty FocusCommandProperty = BindableProperty.Create(nameof(FocusCommand), typeof(Command<bool>), typeof(MaterialInputDialogTextField));
        public static readonly BindableProperty HasErrorProperty = BindableProperty.Create(nameof(HasError), typeof(bool), typeof(MaterialInputDialogTextField), false);
        public static readonly BindableProperty HelperTextColorProperty = BindableProperty.Create(nameof(HelperTextColor), typeof(Color), typeof(MaterialInputDialogTextField), Color.FromArgb("#99000000"));
        public static readonly BindableProperty HelperTextFontFamilyProperty = BindableProperty.Create(nameof(HelperTextFontFamily), typeof(string), typeof(MaterialInputDialogTextField));
        public static readonly BindableProperty HelperTextProperty = BindableProperty.Create(nameof(HelperText), typeof(string), typeof(MaterialInputDialogTextField), string.Empty);
        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(MaterialInputDialogTextField));
        public static readonly BindableProperty IconTintColorProperty = BindableProperty.Create(nameof(IconTintColor), typeof(Color), typeof(MaterialInputDialogTextField), Color.FromArgb("#99000000"));
        public static readonly BindableProperty InputTypeProperty = BindableProperty.Create(nameof(InputType), typeof(MaterialTextFieldInputType), typeof(MaterialInputDialogTextField), MaterialTextFieldInputType.Default);
        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(MaterialInputDialogTextField), 0);
        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(MaterialInputDialogTextField), Color.FromArgb("#99000000"));
        public static readonly BindableProperty PlaceholderFontFamilyProperty = BindableProperty.Create(nameof(PlaceholderFontFamily), typeof(string), typeof(MaterialInputDialogTextField));
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialInputDialogTextField), string.Empty);
        public static readonly BindableProperty ReturnCommandParameterProperty = BindableProperty.Create(nameof(ReturnCommand), typeof(object), typeof(MaterialInputDialogTextField));
        public static readonly BindableProperty ReturnCommandProperty = BindableProperty.Create(nameof(ReturnCommand), typeof(ICommand), typeof(MaterialInputDialogTextField));
        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(MaterialInputDialogTextField), ReturnType.Default);
        public static readonly BindableProperty TextChangeCommandProperty = BindableProperty.Create(nameof(TextChangeCommand), typeof(Command<string>), typeof(MaterialInputDialogTextField));
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialInputDialogTextField), Color.FromArgb("#D0000000"));
        public static readonly BindableProperty TextFontFamilyProperty = BindableProperty.Create(nameof(TextFontFamily), typeof(string), typeof(MaterialInputDialogTextField));
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialInputDialogTextField), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialInputDialogTextField), Material.Color.Secondary);
        public static readonly BindableProperty UnderlineColorProperty = BindableProperty.Create(nameof(UnderlineColor), typeof(Color), typeof(MaterialInputDialogTextField), Color.FromArgb("#99000000"));
        internal static readonly BindableProperty FloatingPlaceholderEnabledProperty = BindableProperty.Create(nameof(FloatingPlaceholderEnabled), typeof(bool), typeof(MaterialInputDialogTextField), true);
        private const double AnimationDuration = 0.35;
        private readonly Dictionary<string, Action> _propertyChangeActions;
        private readonly Easing animationCurve = Easing.SinOut;
        private bool _counterEnabled;
        private bool _wasFocused;

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialInputDialogTextField"/>.
        /// </summary>
        internal MaterialInputDialogTextField()
        {
            InitializeComponent();
            SetPropertyChangeHandler(ref _propertyChangeActions);
            SetControl();
        }

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
            get => (bool)GetValue(AlwaysShowUnderlineProperty);
            set => SetValue(AlwaysShowUnderlineProperty, value);
        }

        /// <summary>
        /// Gets or sets the background color of this text field.
        /// </summary>
        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
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
        /// Gets or sets the image source of the icon to be showed at the left side of this text field.
        /// </summary>
        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        /// <summary>
        /// Gets or sets the tint color of the icon of this text field.
        /// </summary>
        public Color IconTintColor
        {
            get => (Color)GetValue(IconTintColorProperty);
            set => SetValue(IconTintColorProperty, value);
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
            get => (Command<object>)GetValue(ReturnCommandParameterProperty);
            set => SetValue(ReturnCommandParameterProperty, value);
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
        /// Gets or sets the input text of this text field.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set
            {
                if (!string.IsNullOrEmpty(value) && !FloatingPlaceholderEnabled)
                {
                    placeholder.IsVisible = false;
                }
                else if (string.IsNullOrEmpty(value) && !FloatingPlaceholderEnabled)
                {
                    placeholder.IsVisible = true;
                }

                TextChangeCommand?.Execute(value);
                TextChanged?.Invoke(this, new TextChangedEventArgs((string)GetValue(TextProperty), value));
                SetValue(TextProperty, value);
            }
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
        /// Gets or sets the tint color of the underline accent, and the placeholder of this text field when focused.
        /// The default value is the color of <see cref="MaterialColorConfiguration.Secondary"/>.
        /// </summary>
        public Color TintColor
        {
            get => (Color)GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the underline. This will only work if <see cref="AlwaysShowUnderline"/> is set to true.
        /// </summary>
        public Color UnderlineColor
        {
            get => (Color)GetValue(UnderlineColorProperty);
            set => SetValue(UnderlineColorProperty, value);
        }

        internal bool FloatingPlaceholderEnabled
        {
            get => (bool)GetValue(FloatingPlaceholderEnabledProperty);
            set => SetValue(FloatingPlaceholderEnabledProperty, value);
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        /// <param name="created"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ElementChanged(bool created)
        {
            if (created)
            {
                entry.PropertyChanged += Entry_PropertyChanged;
            }
            else
            {
                entry.PropertyChanged -= Entry_PropertyChanged;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            AnimatePlaceHolderOnStart(BindingContext);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            AnimatePlaceHolderOnStart(Parent);
        }

        /// <summary>
        /// Method that is called when a bound property has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (_propertyChangeActions != null && _propertyChangeActions.TryGetValue(propertyName, out var handlePropertyChange))
            {
                handlePropertyChange();
            }
        }

        private void AnimatePlaceHolder()
        {
            var startFont = entry.IsFocused ? 16 : 12;
            var endFOnt = entry.IsFocused ? 12 : 16;
            var startY = placeholder.TranslationY;
            var endY = entry.IsFocused ? -20 : 0;
            var color = entry.IsFocused ? TintColor : PlaceholderColor;
            var anim = new Animation();

            if (FloatingPlaceholderEnabled)
            {
                anim = new Animation
                {
                    {
                        0.0,
                        AnimationDuration,
                        new Animation(v => placeholder.FontSize = v, startFont, endFOnt, animationCurve)
                    },
                    {
                        0.0,
                        AnimationDuration,
                        new Animation(v => placeholder.TranslationY = v, startY, endY, animationCurve, () => placeholder.TextColor = HasError && entry.IsFocused ? ErrorColor : color)
                    }
                };
            }

            if (entry.IsFocused)
            {
                anim.Add(0.0, AnimationDuration, new Animation(v => underline.WidthRequest = v, 0, Width, animationCurve, () => underline.HorizontalOptions = LayoutOptions.FillAndExpand));
            }
            else
            {
                anim.Add(0.0, AnimationDuration, new Animation(v => underline.HeightRequest = v, underline.HeightRequest, 0, animationCurve, () =>
                {
                    underline.WidthRequest = 0;
                    underline.HeightRequest = 2;
                    underline.HorizontalOptions = LayoutOptions.Center;
                }));
            }

            anim.Commit(this, "FocusAnimation", rate: 2, length: (uint)(Device.RuntimePlatform == Device.iOS ? 500 : AnimationDuration * 1000), easing: animationCurve);
        }

        private void AnimatePlaceHolderOnStart(object startObject)
        {
            if (startObject != null && !string.IsNullOrEmpty(Text) && !_wasFocused)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(250);
                    var anim = new Animation();

                    if (FloatingPlaceholderEnabled)
                    {
                        entry.Opacity = 0;
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.FontSize = v, 16, 12, animationCurve));
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.TranslationY = v, placeholder.TranslationY, -12, animationCurve, () =>
                        {
                            placeholder.TextColor = TintColor;
                            entry.Opacity = 1;
                        }));
                    }

                    anim.Add(0.0, AnimationDuration, new Animation(v => underline.WidthRequest = v, 0, Width, animationCurve, () => underline.HorizontalOptions = LayoutOptions.FillAndExpand));
                    anim.Commit(this, "Anim2", rate: 2, length: (uint)(AnimationDuration * 1000), easing: animationCurve);
                });
            }
        }

        private async Task ChangeToErrorState()
        {
            const int animDuration = 250;
            var shakeAnimTask = Task.CompletedTask;
            placeholder.TextColor = string.IsNullOrEmpty(Text) ? PlaceholderColor : ErrorColor;
            counter.TextColor = ErrorColor;
            underline.Color = ErrorColor;

            if (!string.IsNullOrEmpty(Text))
            {
                shakeAnimTask = Task.Run(() =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        const uint duration = (uint)animDuration / 6;

                        await placeholder.TranslateTo(3, placeholder.TranslationY, duration, animationCurve);
                        await placeholder.TranslateTo(-2, placeholder.TranslationY, duration, animationCurve);
                        await placeholder.TranslateTo(0, placeholder.TranslationY, duration, animationCurve);
                    });
                });
            }

            await Task.WhenAll
            (
                helper.FadeTo(0, animDuration / 2, animationCurve).ContinueWith(delegate
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        helper.TranslationY = -4;
                        helper.TextColor = ErrorColor;
                        helper.Text = ErrorText;
                        await Task.WhenAll(helper.FadeTo(1, animDuration / 2, animationCurve), helper.TranslateTo(0, 0, animDuration / 2, animationCurve));
                    });
                }),
                shakeAnimTask
            );
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
                var accentColor = TintColor;
                placeholder.TextColor = accentColor;
                counter.TextColor = HelperTextColor;
                underline.Color = accentColor;
                await helper.FadeTo(0, 150, animationCurve);
                helper.TranslationY = -4;
                helper.TextColor = HelperTextColor;
                helper.Text = HelperText;
                await Task.WhenAll(helper.FadeTo(1, 150, animationCurve), helper.TranslateTo(0, 0, 150, animationCurve));
            });
        }

        private void Entry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Entry.IsFocused))
            {
                _wasFocused = true;
                FocusCommand?.Execute(entry.IsFocused);
                Focused?.Invoke(this, new FocusEventArgs(entry, entry.IsFocused));
                UpdateCounter();
            }

            if (e.PropertyName == nameof(Entry.IsFocused) && string.IsNullOrEmpty(entry.Text))
            {
                AnimatePlaceHolder();
            }
            else if (e.PropertyName == nameof(Entry.Text))
            {
                Text = entry.Text;
                UpdateCounter();
            }
        }

        private void OnAlwaysShowUnderlineChanged(bool isShown)
        {
            persistentUnderline.IsVisible = isShown;
            persistentUnderline.Color = UnderlineColor.MultiplyAlpha((float)0.6);
        }

        private void OnBackgroundColorChanged(Color backgroundColor)
        {
            backgroundCard.BackgroundColor = cardCut.Color = backgroundColor;
        }

        private void OnEnabledChanged(bool isEnabled)
        {
            Opacity = isEnabled ? 1 : 0.33;
            helper.IsVisible = isEnabled && !string.IsNullOrEmpty(HelperText);
        }

        private void OnErrorColorChanged(Color errorColor)
        {
            errorIcon.TintColor = errorColor;
        }

        private async Task OnErrorTextChanged()
        {
            if (HasError)
            {
                await ChangeToErrorState();
            }
        }

        private async Task OnHasErrorChanged(bool hasError)
        {
            errorIcon.IsVisible = hasError;

            if (HasError)
            {
                await ChangeToErrorState();
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

        private void OnIconChanged(string source)
        {
            icon.IsVisible = !string.IsNullOrEmpty(source);
            icon.Source = source;
            icon.TintColor = IconTintColor;
        }

        private void OnIconTintColorChanged(Color tintColor)
        {
            icon.TintColor = tintColor;
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
            }

            entry.IsPassword = inputType == MaterialTextFieldInputType.Password || inputType == MaterialTextFieldInputType.NumericPassword;
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
            placeholder.TextColor = persistentUnderline.Color = placeholderColor;
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
            entry.Text = text;
            AnimatePlaceHolderOnStart(this);
            UpdateCounter();
        }

        private void OnTextColorChanged(Color textColor)
        {
            entry.TextColor = textColor;
        }

        private void OnTextFontFamilyChanged(string fontFamily)
        {
            entry.FontFamily = fontFamily;
        }

        private void OnTintColorChanged(Color tintColor)
        {
            entry.TintColor = underline.Color = tintColor;
        }

        private void OnUnderlineColorChanged(Color underlineColor)
        {
            if (AlwaysShowUnderline)
            {
                persistentUnderline.Color = underlineColor.MultiplyAlpha((float)0.6);
            }
        }

        private void SetControl()
        {
            errorIcon.TintColor = ErrorColor;
            persistentUnderline.Color = UnderlineColor;
            tapGesture.Command = new Command(() =>
            {
                if (!entry.IsFocused)
                {
                    entry.Focus();
                }
            });
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
                { nameof(InputType), () => OnInputTypeChanged(InputType) },
                { nameof(IsEnabled), () => OnEnabledChanged(IsEnabled) },
                { nameof(BackgroundColor), () => OnBackgroundColorChanged(BackgroundColor) },
                { nameof(AlwaysShowUnderline), () => OnAlwaysShowUnderlineChanged(AlwaysShowUnderline) },
                { nameof(MaxLength), () => OnMaxLengthChanged(MaxLength) },
                { nameof(Icon), () => OnIconChanged(Icon) },
                { nameof(IconTintColor), () => OnIconTintColorChanged(IconTintColor) },
                { nameof(ReturnCommand), () => OnReturnCommandChanged(ReturnCommand) },
                { nameof(ReturnCommandParameter), () => OnReturnCommandParameterChanged(ReturnCommandParameter) },
                { nameof(ReturnType), () => OnReturnTypeChangedd(ReturnType) },
                { nameof(ErrorColor), () => OnErrorColorChanged(ErrorColor) },
                { nameof(UnderlineColor), () => OnUnderlineColorChanged(UnderlineColor) },
                { nameof(ErrorText), async () => await OnErrorTextChanged() },
                { nameof(HasError), async () => await OnHasErrorChanged(HasError) },
            };
        }

        private void UpdateCounter()
        {
            if (_counterEnabled)
            {
                var count = entry.Text != null ? entry.Text.Length : 0;
                counter.Text = entry.IsFocused ? $"{count}/{MaxLength}" : string.Empty;
            }
        }
    }
}
