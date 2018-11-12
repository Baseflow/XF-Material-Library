using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Internals;

namespace XF.Material.Forms.UI
{
    /// <summary>
    /// A control that let users enter and edit text.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialTextField : ContentView, IMaterialElementConfiguration
    {
        public static readonly BindableProperty AlwaysShowUnderlineProperty = BindableProperty.Create(nameof(AlwaysShowUnderline), typeof(bool), typeof(MaterialTextField), false);

        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#DCDCDC"));

        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(MaterialTextField), Material.Color.Error);

        public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(nameof(ErrorText), typeof(string), typeof(MaterialTextField), "Error");

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

        public static readonly BindableProperty ReturnCommandProperty = BindableProperty.Create(nameof(ReturnCommand), typeof(ICommand), typeof(MaterialTextField));

        public static readonly BindableProperty ReturnCommandParameterProperty = BindableProperty.Create(nameof(ReturnCommand), typeof(object), typeof(MaterialTextField));

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(MaterialTextField), ReturnType.Default);

        public static readonly BindableProperty TextChangeCommandProperty = BindableProperty.Create(nameof(TextChangeCommand), typeof(Command<string>), typeof(MaterialTextField));

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#D0000000"));

        public static readonly BindableProperty TextFontFamilyProperty = BindableProperty.Create(nameof(TextFontFamily), typeof(string), typeof(MaterialTextField));

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialTextField), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialTextField), Material.Color.Secondary);

        public static readonly BindableProperty UnderlineColorProperty = BindableProperty.Create(nameof(UnderlineColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#99000000"));

        private const double ANIM_DURATION = 0.35;
        private readonly Easing animationCurve = Easing.SinOut;
        private bool _counterEnabled;

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialTextField"/>.
        /// </summary>
        public MaterialTextField()
        {
            this.InitializeComponent();
            errorIcon.TintColor = this.ErrorColor;
            persistentUnderline.Color = this.UnderlineColor;
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

        /// <summary>
        /// Gets or sets the image source of the icon to be showed at the left side of this text field.
        /// </summary>
        public string Icon
        {
            get => (string)this.GetValue(IconProperty);
            set => this.SetValue(IconProperty, value);
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
                if(string.IsNullOrWhiteSpace(value))
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
            get => (Command<object>)this.GetValue(ReturnCommandParameterProperty);
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

        /// <summary>
        /// Gets or sets the input text of this text field.
        /// </summary>
        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set
            {
                this.TextChangeCommand?.Execute(value);
                this.TextChanged?.Invoke(this, new TextChangedEventArgs((string)this.GetValue(TextProperty), value));
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

        /// <summary>
        /// Gets or sets the tint color of the underline accent, and the placeholder of this text field when focused.
        /// The default value is the color of <see cref="MaterialColorConfiguration.Secondary"/>.
        /// </summary>
        public Color TintColor
        {
            get => (Color)this.GetValue(TintColorProperty);
            set => this.SetValue(TintColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the underline. This will only work if <see cref="AlwaysShowUnderline"/> is set to true.
        /// </summary>
        public Color UnderlineColor
        {
            get => (Color)this.GetValue(UnderlineColorProperty);
            set => this.SetValue(UnderlineColorProperty, value);
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
                entry.PropertyChanged += this.Entry_PropertyChanged;
            }

            else
            {
                entry.PropertyChanged -= this.Entry_PropertyChanged;
            }
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            this.AnimatePlaceHolderOnStart(this.Parent);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            this.AnimatePlaceHolderOnStart(this.BindingContext);
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
                entry.TintColor = underline.Color = this.TintColor;
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
                helper.IsVisible = !string.IsNullOrEmpty(this.HelperText);
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

            else if(propertyName == nameof(this.ErrorText) && this.HasError)
            {
                await this.ChangeToErrorState();
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
                helper.IsVisible = this.IsEnabled && !string.IsNullOrEmpty(this.HelperText);
            }

            else if (propertyName == nameof(this.BackgroundColor))
            {
                backgroundCard.BackgroundColor = cardCut.Color = this.BackgroundColor;
            }

            else if (propertyName == nameof(this.AlwaysShowUnderline))
            {
                persistentUnderline.IsVisible = this.AlwaysShowUnderline;
                persistentUnderline.Color = this.UnderlineColor.MultiplyAlpha(0.6);
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

            else if(propertyName == nameof(this.ReturnCommand))
            {
                entry.ReturnCommand = this.ReturnCommand;
            }

            else if (propertyName == nameof(this.ReturnCommandParameter))
            {
                entry.ReturnCommandParameter = this.ReturnCommandParameter;
            }

            else if (propertyName == nameof(this.ReturnType))
            {
                entry.ReturnType = this.ReturnType;
            }

            else if(propertyName == nameof(this.UnderlineColor) && this.AlwaysShowUnderline)
            {
                persistentUnderline.Color = this.UnderlineColor.MultiplyAlpha(0.6);
            }
        }

        private void AnimatePlaceHolderOnStart(object startObject)
        {
            if (startObject != null && !string.IsNullOrEmpty(this.Text))
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
                    anim.Add(0.0, ANIM_DURATION, new Animation(v => underline.WidthRequest = v, 0, this.Width, animationCurve, () => underline.HorizontalOptions = LayoutOptions.FillAndExpand));
                    anim.Commit(this, "Anim2", rate: 2, length: (uint)(ANIM_DURATION * 1000), easing: animationCurve);
                });
            }
        }

        private void AnimatePlaceHolder()
        {
            var startFont = entry.IsFocused ? 16 : 12;
            var endFOnt = entry.IsFocused ? 12 : 16;
            var startY = placeholder.TranslationY;
            var endY = entry.IsFocused ? -14 : 0;
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
            var shakeAnimTask = Task.CompletedTask;
            placeholder.TextColor = string.IsNullOrEmpty(this.Text) ? this.PlaceholderColor : this.ErrorColor;
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

            entry.IsPassword = this.InputType == MaterialTextFieldInputType.Password || this.InputType == MaterialTextFieldInputType.NumericPassword;
        }

        private void UpdateCounter()
        {
            if (_counterEnabled)
            {
                var count = entry.Text != null ? entry.Text.Length : 0;
                counter.Text = entry.IsFocused ? $"{count}/{this.MaxLength}" : string.Empty;
            }
        }
    }
}