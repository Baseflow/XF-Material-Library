using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
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

        public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(nameof(ErrorText), typeof(string), typeof(MaterialTextField), "Error");

        public static readonly BindableProperty FloatingPlaceholderEnabledProperty = BindableProperty.Create(nameof(FloatingPlaceholderEnabled), typeof(bool), typeof(MaterialTextField), true);

        public static readonly BindableProperty FocusCommandProperty = BindableProperty.Create(nameof(FocusCommand), typeof(Command<bool>), typeof(MaterialTextField));

        public static readonly BindableProperty HasErrorProperty = BindableProperty.Create(nameof(HasError), typeof(bool), typeof(MaterialTextField), false);

        public static readonly BindableProperty HasHorizontalPaddingProperty = BindableProperty.Create(nameof(HasHorizontalPadding), typeof(bool), typeof(MaterialTextField), true);

        public static readonly BindableProperty HelperTextColorProperty = BindableProperty.Create(nameof(HelperTextColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#99000000"));

        public static readonly BindableProperty HelperTextFontFamilyProperty = BindableProperty.Create(nameof(HelperTextFontFamily), typeof(string), typeof(MaterialTextField));

        public static readonly BindableProperty HelperTextProperty = BindableProperty.Create(nameof(HelperText), typeof(string), typeof(MaterialTextField), string.Empty);

        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(MaterialTextField));

        public static readonly BindableProperty IconTintColorProperty = BindableProperty.Create(nameof(IconTintColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#99000000"));

        public static readonly BindableProperty InputTypeProperty = BindableProperty.Create(nameof(InputType), typeof(MaterialTextFieldInputType), typeof(MaterialTextField), MaterialTextFieldInputType.Default);

        public static readonly BindableProperty IsSpellCheckEnabledProperty = BindableProperty.Create(nameof(IsSpellCheckEnabled), typeof(bool), typeof(MaterialTextField), true);

        public static readonly BindableProperty IsTextPredictionEnabledProperty = BindableProperty.Create(nameof(IsTextPredictionEnabled), typeof(bool), typeof(MaterialTextField), true);

        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(MaterialTextField), 0);

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#99000000"));

        public static readonly BindableProperty PlaceholderFontFamilyProperty = BindableProperty.Create(nameof(PlaceholderFontFamily), typeof(string), typeof(MaterialTextField));

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialTextField), string.Empty);

        public static readonly BindableProperty ReturnCommandParameterProperty = BindableProperty.Create(nameof(ReturnCommandParameter), typeof(object), typeof(MaterialTextField));

        public static readonly BindableProperty ReturnCommandProperty = BindableProperty.Create(nameof(ReturnCommand), typeof(ICommand), typeof(MaterialTextField));

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(MaterialTextField), ReturnType.Default);

        public static readonly BindableProperty TextChangeCommandProperty = BindableProperty.Create(nameof(TextChangeCommand), typeof(Command<string>), typeof(MaterialTextField));

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#D0000000"));

        public static readonly BindableProperty TextFontFamilyProperty = BindableProperty.Create(nameof(TextFontFamily), typeof(string), typeof(MaterialTextField));

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialTextField), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialTextField), Material.Color.Secondary);

        public static readonly BindableProperty UnderlineColorProperty = BindableProperty.Create(nameof(UnderlineColor), typeof(Color), typeof(MaterialTextField), Color.FromHex("#99000000"));

        private const double AnimationDuration = 0.35;
        private readonly Dictionary<string, Action> _propertyChangeActions;
        private readonly Easing _animationCurve = Easing.SinOut;
        private IList<string> _choices;
        private bool _counterEnabled;
        private bool _wasFocused;

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialTextField"/>.
        /// </summary>
        public MaterialTextField()
        {
            this.InitializeComponent();
            this.SetPropertyChangeHandler(ref _propertyChangeActions);
            this.SetControl();
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

        /// <summary>
        /// Gets or sets whether the placeholder label will float at top of the text field when focused or when it has text.
        /// </summary>
        public bool FloatingPlaceholderEnabled
        {
            get => (bool)this.GetValue(FloatingPlaceholderEnabledProperty);
            set => this.SetValue(FloatingPlaceholderEnabledProperty, value);
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
        /// Gets or sets whether this text field has a padded bounds horizontally.
        /// </summary>
        public bool HasHorizontalPadding
        {
            get => (bool)this.GetValue(HasHorizontalPaddingProperty);
            set => this.SetValue(HasHorizontalPaddingProperty, value);
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

        /// <inheritdoc />
        /// <summary>
        /// For internal use only.
        /// </summary>
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

        /// <summary>
        /// Requests to focus this text field.
        /// </summary>
        public new void Focus() => entry.Focus();

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);

            if (!this.HasHorizontalPadding)
            {
                if (icon.IsVisible)
                {
                    icon.Margin = new Thickness(0, icon.Margin.Top, 0, icon.Margin.Bottom);
                }
                else
                {
                    entry.Margin = new Thickness(0, entry.Margin.Top, 0, entry.Margin.Bottom);
                    placeholder.Margin = 0;
                }

                helper.Margin = new Thickness(0, helper.Margin.Top, 12, 0);
                counter.Margin = new Thickness(0, counter.Margin.Top, 0, 0);
                trailingIcon.Margin = 0;
            }

            if (this.HasError)
            {
                underline.Color = this.ErrorColor;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            this.AnimatePlaceHolderOnStart(this.BindingContext);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            this.AnimatePlaceHolderOnStart(this.Parent);
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

        private void AnimatePlaceHolder()
        {
            Color tintColor;
            double startFont = entry.IsFocused ? 16 : 12;
            double endFOnt = entry.IsFocused ? 12 : 16;
            var startY = placeholder.TranslationY;
            double endY = entry.IsFocused ? -12 : 0;

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
                anim.Add(0.0, AnimationDuration, new Animation(v => underline.WidthRequest = v, 0, this.Width, _animationCurve, () =>
                {
                    underline.HorizontalOptions = LayoutOptions.FillAndExpand;
                    underline.WidthRequest = -1;
                }));
            }
            else
            {
                anim.Add(0.0, AnimationDuration, new Animation(v => underline.HeightRequest = v, underline.HeightRequest, 0, _animationCurve, () =>
                {
                    underline.WidthRequest = 0;
                    underline.HeightRequest = 2;
                    underline.HorizontalOptions = LayoutOptions.Center;
                }));
            }

            anim.Commit(this, "FocusAnimation", rate: 2, length: (uint)(Device.RuntimePlatform == Device.iOS ? 500 : AnimationDuration * 1000), easing: _animationCurve);
        }

        private void AnimatePlaceHolderOnStart(object startObject)
        {
            if (startObject != null && !string.IsNullOrEmpty(this.Text) && !_wasFocused)
            {
                if (placeholder.TranslationY == -12)
                {
                    return;
                }
                entry.Opacity = 0;

                Device.BeginInvokeOnMainThread(() =>
                {
                    var anim = new Animation();

                    if (this.FloatingPlaceholderEnabled)
                    {
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.FontSize = v, 16, 12, _animationCurve));
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.TranslationY = v, placeholder.TranslationY, -12, _animationCurve, () =>
                        {
                            placeholder.TextColor = this.HasError ? this.ErrorColor : this.TintColor;
                            entry.Opacity = 1;
                        }));
                    }

                    anim.Add(0.0, AnimationDuration, new Animation(v => underline.WidthRequest = v, 0, this.Width, _animationCurve, () => underline.HorizontalOptions = LayoutOptions.FillAndExpand));
                    anim.Commit(this, "Anim2", rate: 2, length: (uint)(AnimationDuration * 1000), easing: _animationCurve);
                });

                entry.Opacity = 1;

                return;
            }

            if ((startObject != null && string.IsNullOrEmpty(this.Text) && placeholder.TranslationY == -12) || placeholder.TranslationY == -20)
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
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.FontSize = v, 12, 16, _animationCurve));
                        anim.Add(0.0, AnimationDuration, new Animation(v => placeholder.TranslationY = v, placeholder.TranslationY, 0, _animationCurve, () =>
                        {
                            placeholder.TextColor = this.PlaceholderColor;
                            entry.Opacity = 1;
                        }));
                    }

                    anim.Add(0.0, AnimationDuration, new Animation(v => underline.WidthRequest = v, this.Width, 0, _animationCurve, () => underline.HorizontalOptions = LayoutOptions.Center));
                    anim.Commit(this, "Anim2", rate: 2, length: (uint)(AnimationDuration * 1000), easing: _animationCurve);
                });
            }
        }

        private async Task ChangeToErrorState()
        {
            const int animDuration = 250;
            placeholder.TextColor = (this.FloatingPlaceholderEnabled && entry.IsFocused) || (this.FloatingPlaceholderEnabled && !string.IsNullOrEmpty(this.Text)) ? this.ErrorColor : this.PlaceholderColor;
            counter.TextColor = this.ErrorColor;
            underline.Color = this.ErrorColor;
            persistentUnderline.Color = this.ErrorColor;
            trailingIcon.IsVisible = true;
            trailingIcon.Source = "xf_error";
            trailingIcon.TintColor = this.ErrorColor;

            await Task.WhenAll
            (
                helper.FadeTo(0, animDuration / 2, _animationCurve).ContinueWith(delegate
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        helper.TranslationY = -4;
                        helper.TextColor = this.ErrorColor;
                        helper.Text = this.ErrorText;
                        await Task.WhenAll(helper.FadeTo(1, animDuration / 2, _animationCurve), helper.TranslateTo(0, 0, animDuration / 2, _animationCurve));
                    });
                })
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
                await helper.FadeTo(0, 150, _animationCurve);
                helper.TranslationY = -4;
                helper.TextColor = this.HelperTextColor;
                helper.Text = this.HelperText;
                await Task.WhenAll(helper.FadeTo(1, 150, _animationCurve), helper.TranslateTo(0, 0, 150, _animationCurve));
            });
        }

        private void Entry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.IsFocused))
            {
                _wasFocused = true;
                this.FocusCommand?.Execute(entry.IsFocused);
                this.Focused?.Invoke(this, new FocusEventArgs(entry, entry.IsFocused));
                this.UpdateCounter();
            }

            switch (e.PropertyName)
            {
                case nameof(this.IsFocused) when string.IsNullOrEmpty(entry.Text):
                    this.AnimatePlaceHolder();
                    break;
                case nameof(Entry.Text):
                    this.Text = entry.Text;
                    this.UpdateCounter();
                    break;
            }
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

        private async Task OnErrorTextChanged()
        {
            if (this.HasError)
            {
                await this.ChangeToErrorState();
            }
        }

        private void OnFloatingPlaceholderEnabledChanged(bool isEnabled)
        {
            if (isEnabled) return;
            placeholder.HeightRequest = 20;
            placeholder.VerticalOptions = LayoutOptions.Center;
            placeholder.VerticalTextAlignment = TextAlignment.Center;
            _gridContainer.RowDefinitions[0].Height = 40;
            entry.Margin = new Thickness(entry.Margin.Left, 2, entry.Margin.Right, 0);
            entry.VerticalOptions = LayoutOptions.Center;
        }

        private async Task OnHasErrorChanged()
        {
            if (this.HasError)
            {
                await this.ChangeToErrorState();
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

        private void OnIconChanged(string icon)
        {
            this.icon.Source = icon;
            this.OnIconTintColorChanged(this.IconTintColor);
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

                case MaterialTextFieldInputType.Choice:

                    break;
            }

            _gridContainer.InputTransparent = inputType == MaterialTextFieldInputType.Choice;
            trailingIcon.IsVisible = inputType == MaterialTextFieldInputType.Choice;

            //entry.IsPassword = inputType == MaterialTextFieldInputType.Password || inputType == MaterialTextFieldInputType.NumericPassword;
        }

        private void OnIsSpellCheckEnabledChanged(bool isSpellCheckEnabled)
        {
            entry.IsSpellCheckEnabled = isSpellCheckEnabled;
        }

        private void OnIsTextPredictionEnabledChanged(bool isTextPredictionEnabled)
        {
            entry.IsTextPredictionEnabled = isTextPredictionEnabled;
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
            //entry.ReturnCommand = returnCommand;
        }

        private void OnReturnCommandParameterChanged(object parameter)
        {
            //entry.ReturnCommandParameter = parameter;
        }

        private void OnReturnTypeChangedd(ReturnType returnType)
        {
            //entry.ReturnType = returnType;
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
            this.AnimatePlaceHolderOnStart(this);
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

        private void OnTintColorChanged(Color tintColor)
        {
            entry.TintColor = underline.Color = persistentUnderline.Color = tintColor;
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
                { nameof(this.ErrorText), async () => await this.OnErrorTextChanged() },
                { nameof(this.HasError), async () => await this.OnHasErrorChanged() },
                { nameof(this.FloatingPlaceholderEnabled), () => this.OnFloatingPlaceholderEnabledChanged(this.FloatingPlaceholderEnabled) },
                { nameof(this.Choices), () => this.OnChoicesChanged(this.Choices) },
                { nameof(this.Icon), () => this.OnIconChanged(this.Icon) },
                { nameof(this.IconTintColor), () => this.OnIconTintColorChanged(this.IconTintColor) },
                { nameof(this.IsSpellCheckEnabled), () => this.OnIsSpellCheckEnabledChanged(this.IsSpellCheckEnabled) },
                { nameof(this.IsTextPredictionEnabled), () => this.OnIsTextPredictionEnabledChanged(this.IsTextPredictionEnabled) }
            };
        }

        private void UpdateCounter()
        {
            if (!_counterEnabled) return;
            var count = entry.Text?.Length ?? 0;
            counter.Text = entry.IsFocused ? $"{count}/{this.MaxLength}" : string.Empty;
        }

        private void Entry_SizeChanged(object sender, EventArgs e)
        {
            var diff = entry.Height - 20;

            _autoSizingRow.Height = new GridLength(56 + diff);
        }
    }
}