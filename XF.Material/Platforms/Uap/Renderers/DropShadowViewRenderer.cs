using System;
using System.ComponentModel;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using XF.Material.Forms.UI;
using XF.Material.UWP.Renderers;
using Rectangle = Windows.UI.Xaml.Shapes.Rectangle;

[assembly: ExportRenderer(typeof(DropShadowView), typeof(DropShadowViewRenderer))]
namespace XF.Material.UWP.Renderers
{
    public class DropShadowViewRenderer : ViewRenderer<DropShadowView, DropShadowPanel>
    {
        private Rectangle _rectangle;

        protected override void OnElementChanged(ElementChangedEventArgs<DropShadowView> e)
        {
            base.OnElementChanged(e);

            if (e?.OldElement != null)
            {
                ParentView.SizeChanged -= Element_SizeChanged;
            }
            if (e?.NewElement != null)
            {
                SetNativeControl(new DropShadowPanel()
                {
                    Color = Colors.Black,
                    ShadowOpacity = 0.34f,
                });

                _rectangle = new Rectangle();

                Control.Content = _rectangle;

                UpdateSurfaceColor();
                UpdateCornerRadius();
                UpdateElevation();

                ParentView.SizeChanged += Element_SizeChanged;
            }
        }

        private View ParentView => Element.Parent as View;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e?.PropertyName == nameof(DropShadowView.SurfaceColor))
            {
                UpdateSurfaceColor();
            }
        }

        private void Element_SizeChanged(object sender, EventArgs e)
        {
            if (ParentView.Width * ParentView.Height > 0)
            {
                _rectangle.Width = ParentView.Width;
                _rectangle.Height = ParentView.Height;
            }
        }

        private void UpdateCornerRadius()
        {
            _rectangle.RadiusX = Element.CornerRadius;
            _rectangle.RadiusY = Element.CornerRadius;
        }

        private void UpdateSurfaceColor()
        {
            _rectangle.Fill = new SolidColorBrush(Element.SurfaceColor.ToWindowsColor());
        }

        private void UpdateElevation()
        {
            Control.OffsetY = Element.OffsetY;
            Control.OffsetX = Element.OffsetX;
            Control.BlurRadius = Element.BlurRadius;
        }
    }
}
