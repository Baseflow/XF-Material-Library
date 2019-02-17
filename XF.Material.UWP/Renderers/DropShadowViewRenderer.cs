using System;
using System.ComponentModel;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using Rectangle = System.Drawing.Rectangle;

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
                this.ParentView.SizeChanged -= this.Element_SizeChanged;
            }
            if (e?.NewElement != null)
            {
                SetNativeControl(new DropShadowPanel()
                {
                    Color = Colors.Black,
                    ShadowOpacity = 0.34f,
                });

                _rectangle = new Rectangle();

                this.Control.Content = _rectangle;

                UpdateSurfaceColor();
                UpdateCornerRadius();
                UpdateElevation();

                this.ParentView.SizeChanged += this.Element_SizeChanged;
            }
        }

        private View ParentView => this.Element.Parent as View;

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
            if (this.ParentView.Width * this.ParentView.Height > 0)
            {
                _rectangle.Width = this.ParentView.Width;
                _rectangle.Height = this.ParentView.Height;
            }
        }

        private void UpdateCornerRadius()
        {
            _rectangle.RadiusX = this.Element.CornerRadius;
            _rectangle.RadiusY = this.Element.CornerRadius;
        }

        private void UpdateSurfaceColor()
        {
            _rectangle.Fill = new SolidColorBrush(this.Element.SurfaceColor.ToWindows());
        }

        private void UpdateElevation()
        {
            this.Control.OffsetY = this.Element.OffsetY;
            this.Control.OffsetX = this.Element.OffsetX;
            this.Control.BlurRadius = this.Element.BlurRadius;
        }
    }
}