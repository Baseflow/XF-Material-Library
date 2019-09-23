using System;
using Xamarin.Forms;

namespace XF.Material.Forms.UI.Internals
{
    public class CorneredContentView : ContentView
    {

        public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(float), typeof(CorneredContentView), default(float));

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CorneredContentView), default(Color));

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(CorneredContentView), default(CornerRadius));

        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(CorneredContentView), default(bool));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)this.GetValue(CornerRadiusProperty); }
            set { this.SetValue(CornerRadiusProperty, value); }
        }

        public float BorderThickness
        {
            get { return (float)this.GetValue(BorderThicknessProperty); }
            set { this.SetValue(BorderThicknessProperty, value); }
        }

        public Color BorderColor
        {
            get { return (Color)this.GetValue(BorderColorProperty); }
            set { this.SetValue(BorderColorProperty, value); }
        }

        public bool HasShadow
        {
            get { return (bool)this.GetValue(HasShadowProperty); }
            set { this.SetValue(HasShadowProperty, value); }
        }
    }
}
