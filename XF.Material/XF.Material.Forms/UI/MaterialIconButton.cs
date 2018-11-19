using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.Resources;

namespace XF.Material.Forms.UI
{
    public class MaterialIconButton : View, IMaterialTintableElement, IMaterialTouchableElement
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialIconButton));

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MaterialIconButton));

        public MaterialIconButton()
        {
            this.VerticalOptions = LayoutOptions.Start;
            this.HorizontalOptions = LayoutOptions.Start;
            this.SetDynamicResource(HeightRequestProperty, MaterialConstants.MATERIAL_BUTTON_HEIGHT);
            this.SetDynamicResource(WidthRequestProperty, MaterialConstants.MATERIAL_BUTTON_HEIGHT);
        }

        public event EventHandler Clicked;

        public FileImageSource Source { get; set; }

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

        public Color TintColor { get; set; }

        public Color RippleColor { get; set; } = Color.FromHex("#52000000");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void OnClick()
        {
            this.Command?.Execute(this.CommandParameter);
            this.Clicked?.Invoke(this, EventArgs.Empty);
        }
    }

    public interface IMaterialTintableElement
    {
        Color TintColor { get; set; }
    }

    public interface IMaterialTouchableElement
    {
        Color RippleColor { get; set; }
    }
}
