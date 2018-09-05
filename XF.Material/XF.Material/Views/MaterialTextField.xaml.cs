using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Resources;

namespace XF.Material.Views
{
    /// <summary>
    /// Enumeration used in determining the type of keyboard input to use for <see cref="MaterialTextField"/>
    /// </summary>
    public enum MaterialTextFieldInputType
    {
        Default,
        Plain,
        Chat,
        Email,
        Numeric,
        Telephone,
        Text,
        Url,
        Password,
        NumericPassword
    }

    /// <summary>
    /// A control that allows the user to input text.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialTextField : ContentView
    {
        public static readonly BindableProperty AlwaysShowUnderlineProperty = BindableProperty.Create(nameof(AlwaysShowUnderline), typeof(bool), typeof(MaterialTextField), false);

        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#DCDCDC"));

        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(MaterialTextField), Material.GetMaterialResource<Color>(MaterialConstants.Color.ERROR));

        public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(nameof(ErrorText), typeof(string), typeof(MaterialTextField), string.Empty);

        public static readonly BindableProperty FocusCommandProperty = BindableProperty.Create(nameof(FocusCommand), typeof(Command<bool>), typeof(MaterialTextField));

        public static readonly BindableProperty HasErrorProperty = BindableProperty.Create(nameof(HasError), typeof(bool), typeof(MaterialTextField), false);

        public static readonly BindableProperty HelperTextColorProperty = BindableProperty.Create(nameof(HelperTextColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#99000000"));

        public static readonly BindableProperty HelperTextFontFamilyProperty = BindableProperty.Create(nameof(HelperTextFontFamily), typeof(string), typeof(MaterialTextField));

        public static readonly BindableProperty HelperTextProperty = BindableProperty.Create(nameof(HelperText), typeof(string), typeof(MaterialTextField), string.Empty);

        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(MaterialTextField));

        public static readonly BindableProperty IconTintColorProperty = BindableProperty.Create(nameof(IconTintColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#99000000"));

        public static readonly BindableProperty InputTypeProperty = BindableProperty.Create(nameof(InputType), typeof(MaterialTextFieldInputType), typeof(MaterialTextField), MaterialTextFieldInputType.Default);

        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(MaterialTextField), 0);

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#99000000"));

        public static readonly BindableProperty PlaceholderFontFamilyProperty = BindableProperty.Create(nameof(PlaceholderFontFamily), typeof(string), typeof(MaterialTextField));

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialTextField), string.Empty);

        public static readonly BindableProperty TextChangeCommandProperty = BindableProperty.Create(nameof(TextChangeCommand), typeof(Command<string>), typeof(MaterialTextField));

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#D0000000"));

        public static readonly BindableProperty TextFontFamilyProperty = BindableProperty.Create(nameof(TextFontFamily), typeof(string), typeof(MaterialTextField));

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialTextField), string.Empty);

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialTextField), Material.GetMaterialResource<Color>(MaterialConstants.Color.SECONDARY));

        private const double ANIM_DURATION = 0.35;
        private readonly Easing animationCurve = Easing.SinOut;
        private bool _counterEnabled;

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialTextField"/>.
        /// </summary>
        public MaterialTextField()
        {
            InitializeComponent();
            entry.PropertyChanged += this.Entry_PropertyChanged;
            errorIcon.TintColor = this.ErrorColor;
            persistentUnderline.Color = this.PlaceholderColor;
            tapGesture.Command = new Command(() =>
            {
                if(!entry.IsFocused)
                {
                    entry.Focus();
                }
            });
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
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException($"{nameof(this.Placeholder)} must not be null, empty, or a white space.");
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
        /// Gets or sets the input text of this text field.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set
            {
                this.TextChangeCommand?.Execute(value);
                this.TextChanged?.Invoke(this, new TextChangedEventArgs((string)GetValue(TextProperty), value));
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

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (this.Parent != null && !string.IsNullOrEmpty(this.Text))
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    entry.Opacity = 0;
                    await Task.Delay(250);

                    var anim = new Animation();
                    anim.Add(0.0, ANIM_DURATION, new Animation(v => placeholder.FontSize = v, 16, 12, animationCurve));
                    anim.Add(0.0, ANIM_DURATION, new Animation(v => placeholder.TranslationY = v, placeholder.TranslationY, -12, animationCurve, () =>
                    {
                        placeholder.TextColor = this.TintColor;
                        entry.Opacity = 1;
                    }));
                    anim.Add(0.0, ANIM_DURATION, new Animation(v => underline.WidthRequest = v, 0, this.Width, animationCurve, () => underline.HorizontalOptions = LayoutOptions.CenterAndExpand));
                    anim.Commit(this, "Anim2", rate: 2, length: (uint)(/*Device.RuntimePlatform == Device.iOS ? 1000 : */ANIM_DURATION * 1000), easing: animationCurve);
                });
            }
        }

        /// <summary>
        /// Method that is called when a bound property has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        protected override async void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(this.Text))
            {
                entry.Text = this.Text;
                this.UpdateCounter();
            }

            else if (propertyName == nameof(this.TextColor))
            {
                entry.TextColor = this.TextColor;
            }

            else if (propertyName == nameof(this.TextFontFamily))
            {
                entry.FontFamily = this.TextFontFamily;
            }

            else if (propertyName == nameof(this.TintColor))
            {
                underline.Color = this.TintColor;
            }

            else if (propertyName == nameof(this.Placeholder))
            {
                placeholder.Text = this.Placeholder;
            }

            else if (propertyName == nameof(this.PlaceholderColor))
            {
                placeholder.TextColor = persistentUnderline.Color = this.PlaceholderColor;
            }

            else if (propertyName == nameof(this.PlaceholderFontFamily))
            {
                placeholder.FontFamily = this.PlaceholderFontFamily;
            }

            else if (propertyName == nameof(this.HelperText))
            {
                helper.Text = this.HelperText;
            }

            else if (propertyName == nameof(this.HelperTextFontFamily))
            {
                helper.FontFamily = counter.FontFamily = this.HelperTextFontFamily;
            }

            else if (propertyName == nameof(this.HelperTextColor))
            {
                helper.TextColor = counter.TextColor = this.HelperTextColor;
            }

            else if (propertyName == nameof(this.InputType))
            {
                this.SetInputType();
            }

            else if(propertyName == nameof(this.ErrorColor))
            {
                errorIcon.TintColor = this.ErrorColor;
            }

            else if (propertyName == nameof(this.HasError))
            {
                errorIcon.IsVisible = this.HasError;

                if (this.HasError)
                {
                    await this.ChangeToErrorState();
                }

                else
                {
                    this.ChangeToNormalState();
                }
            }

            else if (propertyName == nameof(this.IsEnabled))
            {
                this.Opacity = this.IsEnabled ? 1 : 0.33;
                helper.IsVisible = this.IsEnabled;
            }

            else if (propertyName == nameof(this.BackgroundColor))
            {
                backgroundCard.BackgroundColor = cardCut.Color = this.BackgroundColor;
            }

            else if (propertyName == nameof(this.AlwaysShowUnderline))
            {
                persistentUnderline.IsVisible = this.AlwaysShowUnderline;
            }

            else if(propertyName == nameof(this.MaxLength))
            {
                _counterEnabled = this.MaxLength > 0;
                entry.MaxLength = _counterEnabled ? this.MaxLength : (int)Entry.MaxLengthProperty.DefaultValue;
            }

            else if(propertyName == nameof(this.Icon))
            {
                icon.IsVisible = !string.IsNullOrEmpty(this.Icon);
                icon.Source = this.Icon;
                icon.TintColor = this.IconTintColor;
            }

            else if(propertyName == nameof(this.IconTintColor))
            {
                icon.TintColor = this.IconTintColor;
            }
        }

        private void AnimatePlaceHolder()
        {
            var startFont = entry.IsFocused ? 16 : 12;
            var endFOnt = entry.IsFocused ? 12 : 16;
            var startY = placeholder.TranslationY;
            var endY = entry.IsFocused ? -12 : 0;
            var color = entry.IsFocused ? this.TintColor : this.PlaceholderColor;
            var anim = new Animation
            {
                {
                    0.0,
                    ANIM_DURATION,
                    new Animation(v => placeholder.FontSize = v, startFont, endFOnt, animationCurve)
                },
                {
                    0.0,
                    ANIM_DURATION,
                    new Animation(v => placeholder.TranslationY = v, startY, endY, animationCurve, () => placeholder.TextColor = this.HasError && entry.IsFocused ? this.ErrorColor : color)
                }
            };

            if(entry.IsFocused)
            {
                anim.Add(0.0, ANIM_DURATION, new Animation(v => underline.WidthRequest = v, 0, this.Width, animationCurve, () => underline.HorizontalOptions = LayoutOptions.FillAndExpand));
            }

            else
            {
                anim.Add(0.0, ANIM_DURATION, new Animation(v => underline.HeightRequest = v, underline.HeightRequest, 0, animationCurve, () =>
                {
                    underline.WidthRequest = 0;
                    underline.HeightRequest = 2;
                    underline.HorizontalOptions = LayoutOptions.Center;
                }));
            }

            anim.Commit(this, "FocusAnimation", rate: 2, length: (uint)(Device.RuntimePlatform == Device.iOS ? 500 : ANIM_DURATION * 1000), easing: animationCurve);
        }

        private async Task ChangeToErrorState()
        {
            const int animDuration = 250;
            Task shakeAnimTask = null;
            placeholder.TextColor = this.ErrorColor;
            counter.TextColor = this.ErrorColor;
            underline.Color = this.ErrorColor;

            if (!string.IsNullOrEmpty(this.Text))
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
                        helper.TextColor = this.ErrorColor;
                        helper.Text = this.ErrorText;
                        await Task.WhenAll(helper.FadeTo(1, animDuration / 2, animationCurve), helper.TranslateTo(0, 0, animDuration / 2, animationCurve));
                    });
                }),
                shakeAnimTask
            );
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
                var accentColor = this.TintColor;
                placeholder.TextColor = accentColor;
                counter.TextColor = this.HelperTextColor;
                underline.Color = accentColor;
                await helper.FadeTo(0, 150, animationCurve);
                helper.TranslationY = -4;
                helper.TextColor = this.HelperTextColor;
                helper.Text = this.HelperText;
                await Task.WhenAll(helper.FadeTo(1, 150, animationCurve), helper.TranslateTo(0, 0, 150, animationCurve));
            });
        }

        private void Entry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Entry.IsFocused))
            {
                this.FocusCommand?.Execute(entry.IsFocused);
                this.Focused?.Invoke(this, new FocusEventArgs(entry, entry.IsFocused));
                this.UpdateCounter();
                counter.IsVisible = entry.IsFocused;
            }

            if (e.PropertyName == nameof(Entry.IsFocused) && string.IsNullOrEmpty(entry.Text))
            {
                this.AnimatePlaceHolder();
            }

            else if (e.PropertyName == nameof(Entry.Text))
            {
                this.Text = entry.Text;
                this.UpdateCounter();
            }
        }

        private void SetInputType()
        {
            switch (this.InputType)
            {
                case MaterialTextFieldInputType.Chat:
                    entry.Keyboard = Keyboard.Chat;
                    entry.IsPassword = false;
                    break;
                case MaterialTextFieldInputType.Default:
                    entry.Keyboard = Keyboard.Default;
                    entry.IsPassword = false;
                    break;
                case MaterialTextFieldInputType.Email:
                    entry.Keyboard = Keyboard.Email;
                    entry.IsPassword = false;
                    break;
                case MaterialTextFieldInputType.Numeric:
                    entry.Keyboard = Keyboard.Numeric;
                    entry.IsPassword = false;
                    break;
                case MaterialTextFieldInputType.Plain:
                    entry.Keyboard = Keyboard.Plain;
                    entry.IsPassword = false;
                    break;
                case MaterialTextFieldInputType.Telephone:
                    entry.Keyboard = Keyboard.Telephone;
                    entry.IsPassword = false;
                    break;
                case MaterialTextFieldInputType.Text:
                    entry.IsPassword = false;
                    entry.Keyboard = Keyboard.Text;
                    break;
                case MaterialTextFieldInputType.Url:
                    entry.Keyboard = Keyboard.Url;
                    entry.IsPassword = false;
                    break;
                case MaterialTextFieldInputType.NumericPassword:
                    entry.Keyboard = Keyboard.Numeric;
                    entry.IsPassword = true;
                    break;
                case MaterialTextFieldInputType.Password:
                    entry.Keyboard = Keyboard.Text;
                    entry.IsPassword = true;
                    break;
            }
        }

        private void UpdateCounter()
        {
            if (_counterEnabled)
            {
                var count = entry.Text != null ? entry.Text.Length : 0;
                counter.Text = $"{count}/{this.MaxLength}";
            }
        }
    }
}