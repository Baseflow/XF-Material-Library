using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.Resources;
using static XF.Material.Forms.UI.MaterialButton;

namespace XF.Material.Forms.UI
{
    public class MaterialIconButton : View, IMaterialButton
    {
        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(MaterialColor), typeof(MaterialIconButton), MaterialColor.Default);

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialIconButton), default(Color));

        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(MaterialIconButton), 0.0);

        public static readonly BindableProperty ButtonTypeProperty = BindableProperty.Create(nameof(ButtonType), typeof(MaterialButtonType), typeof(MaterialIconButton), MaterialButtonType.Elevated, propertyChanged: ButtonTypeChanged);

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MaterialIconButton));

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialIconButton));

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(MaterialIconButton), 4);

        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(string), typeof(MaterialIconButton));

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialIconButton), default(Color));

        public MaterialIconButton()
        {
            this.VerticalOptions = LayoutOptions.Start;
            this.HorizontalOptions = LayoutOptions.Start;
            this.SetDynamicResource(HeightRequestProperty, MaterialConstants.MATERIAL_BUTTON_HEIGHT);
            this.SetDynamicResource(WidthRequestProperty, MaterialConstants.MATERIAL_BUTTON_HEIGHT);
        }

        public event EventHandler Clicked;

        public new MaterialColor BackgroundColor
        {
            get => (MaterialColor)this.GetValue(BackgroundColorProperty);
            set => this.SetValue(BackgroundColorProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)this.GetValue(BorderColorProperty);
            set => this.SetValue(BorderColorProperty, value);
        }

        public double BorderWidth
        {
            get => (double)this.GetValue(BorderWidthProperty);
            set => this.SetValue(BorderWidthProperty, value);
        }

        public virtual MaterialButtonType ButtonType
        {
            get => (MaterialButtonType)this.GetValue(ButtonTypeProperty);
            set => this.SetValue(ButtonTypeProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => this.GetValue(CommandParameterProperty);
            set => this.SetValue(CommandParameterProperty, value);
        }

        public int CornerRadius
        {
            get => (int)this.GetValue(CornerRadiusProperty);
            set => this.SetValue(CornerRadiusProperty, value);
        }

        public FileImageSource Source
        {
            get => (string)this.GetValue(SourceProperty);
            set => this.SetValue(SourceProperty, value);
        }

        public Color TintColor
        {
            get => (Color)this.GetValue(TintColorProperty);
            set => this.SetValue(TintColorProperty, value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void OnClick()
        {
            this.Command?.Execute(this.CommandParameter);
            this.Clicked?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(this.BackgroundColor))
            {
                propertyName = MaterialButtonColorChanged;
            }

            base.OnPropertyChanged(propertyName);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void ButtonTypeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MaterialIconButton materialIconButton)
            {
                switch (materialIconButton.ButtonType)
                {
                    case MaterialButtonType.Text:
                        materialIconButton.SetDynamicResource(TintColorProperty, MaterialConstants.Color.SECONDARY);
                        break;
                    case MaterialButtonType.Outlined:

                        if (materialIconButton.BorderColor == (Color)BorderColorProperty.DefaultValue)
                        {
                            materialIconButton.SetDynamicResource(BorderColorProperty, MaterialConstants.MATERIAL_BUTTON_OUTLINED_BORDERCOLOR);
                        }

                        if (materialIconButton.BorderWidth == (double)BorderWidthProperty.DefaultValue)
                        {
                            materialIconButton.SetDynamicResource(BorderWidthProperty, MaterialConstants.MATERIAL_BUTTON_OUTLINED_BORDERWIDTH);
                        }

                        break;
                }
            }
        }
    }
}