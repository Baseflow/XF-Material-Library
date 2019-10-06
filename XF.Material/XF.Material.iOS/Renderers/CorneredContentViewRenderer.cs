using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI.Internals;

[assembly: ExportRenderer(typeof(XF.Material.Forms.UI.Internals.CorneredContentView), typeof(XF.Material.iOS.Renderers.CorneredContentViewRenderer))]
namespace XF.Material.iOS.Renderers
{
    public class CorneredContentViewRenderer : ViewRenderer<CorneredContentView, UIView>
    {
        private UIView _actualView;
        private UIView _wrapperView;

        private UIColor _colorToRender;
        private CGSize _previousSize;

        /// <summary>
        /// Hint to the linker to keep us around
        /// </summary>
        public static new void Init()
        {
#pragma warning disable 0219
            Type link1 = typeof(CorneredContentViewRenderer);
            Type link2 = typeof(CorneredContentView);
#pragma warning restore 0219
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CorneredContentView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                this.Validate(this.Element as CorneredContentView);

                this._actualView = new UIView();
                this._wrapperView = new UIView();

                for (int i = 0; i < this.NativeView.Subviews.Length; i++)
                {
                    UIView item = this.NativeView.Subviews[i];
                    this._actualView.AddSubview(item);
                }

                if (this.NativeView.GestureRecognizers != null)
                {
                    for (int i = 0; i < this.NativeView.GestureRecognizers.Length; i++)
                    {
                        UIGestureRecognizer gesture = this.NativeView.GestureRecognizers[i];
                        this._actualView.AddGestureRecognizer(gesture);
                    }
                }

                this._wrapperView.AddSubview(this._actualView);

                this.SetNativeControl(this._wrapperView);

                this.SetBackgroundColor(this.Element.BackgroundColor);
                this.SetCornerRadius();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            this.Validate(this.Element as CorneredContentView);

            switch(e.PropertyName)
            {
                case nameof(VisualElement.BackgroundColor):
                    {
                        this.SetBackgroundColor(this.Element.BackgroundColor);
                        break;
                    }
                case nameof(CorneredContentView.CornerRadius):
                    {
                        this.SetCornerRadius();
                        break;
                    }
                case nameof(CorneredContentView.BorderColor):
                case nameof(CorneredContentView.BorderThickness):
                    {
                        this.DrawBorder();
                        break;
                    }
                case nameof(VisualElement.IsVisible):
                    {
                        if (this.Element.IsVisible)
                        {
                            this.SetNeedsDisplay();
                        }
                        break;
                    }
                case nameof(CorneredContentView.HasShadow):
                {
                        this.SetNeedsDisplay();
                        break;
                }
            }
        }

        private void Validate(CorneredContentView corneredContentView)
        {
        }

        public override void LayoutSubviews()
        {
            if (this._previousSize != this.Bounds.Size)
            {
                this.SetNeedsDisplay();
            }

            base.LayoutSubviews();
        }

        public override void Draw(CGRect rect)
        {
            this._actualView.Frame = this.Bounds;
            this._wrapperView.Frame = this.Bounds;

            this.DrawBackground();
            this.DrawShadow();
            this.DrawBorder();

            base.Draw(rect);

            this._previousSize = this.Bounds.Size;
        }

        private void SetCornerRadius()
        {
            if (this.Element == null)
                return;

            this.SetNeedsDisplay();
        }

        protected override void SetBackgroundColor(Xamarin.Forms.Color color)
        {
            if (this.Element == null)
                return;

            Xamarin.Forms.Color elementColor = this.Element.BackgroundColor;

            if (!elementColor.IsDefault)
                this._colorToRender = elementColor.ToUIColor();
            else
                this._colorToRender = color.ToUIColor();

            this.SetNeedsDisplay();
        }

        private void DrawBackground()
        {
            CorneredContentView corneredContentView = this.Element as CorneredContentView;
            string layerName = "backgroundLayer";

            // Remove previous background layer if any
            CALayer prevBackgroundLayer = this._actualView.Layer.Sublayers?.FirstOrDefault(x => x.Name == layerName);
            prevBackgroundLayer?.RemoveFromSuperLayer();

            UIBezierPath cornerPath = null;

            cornerPath = CreateRoundedRectPath(this.Bounds, corneredContentView.CornerRadius);

            // The layer used to mask other layers we draw on the background.
            CAShapeLayer maskLayer = new CAShapeLayer
            {
                Frame = Bounds,
                Path = cornerPath.CGPath
            };

            this._actualView.Layer.Mask = maskLayer;
            this._actualView.Layer.MasksToBounds = true;


            // Create a shape layer that draws our background.
            CAShapeLayer shapeLayer = new CAShapeLayer
            {
                Frame = Bounds,
                Path = cornerPath.CGPath,
                MasksToBounds = true,
                FillColor = this._colorToRender.CGColor,
                Name = layerName
            };

            this.AddLayer(shapeLayer, 0, this._actualView);

        }

        private void DrawBorder()
        {
            CorneredContentView corneredContentView = this.Element as CorneredContentView;
            string layerName = "borderLayer";

            // remove previous background layer if any
            CALayer prevBorderLayer = this._wrapperView.Layer.Sublayers?.FirstOrDefault(x => x.Name == layerName);
            prevBorderLayer?.RemoveFromSuperLayer();

            if (corneredContentView.BorderThickness > 0)
            {
                CAShapeLayer borderLayer = new CAShapeLayer
                {
                    StrokeColor = corneredContentView.BorderColor == Xamarin.Forms.Color.Default ? UIColor.Clear.CGColor : corneredContentView.BorderColor.ToCGColor(),
                    FillColor = null,
                    LineWidth = corneredContentView.BorderThickness,
                    Name = layerName
                };


                borderLayer.Path = CreateRoundedRectPath(this.Bounds, corneredContentView.CornerRadius).CGPath;

                CGPoint layerPosition = new CGPoint(borderLayer.Path.BoundingBox.Width / 2, borderLayer.Path.BoundingBox.Height / 2);

                borderLayer.Frame = borderLayer.Path.BoundingBox;
                borderLayer.Position = layerPosition;

                this.AddLayer(borderLayer, -1, this._wrapperView);
            }
        }

        private void DrawShadow()
        {
            CorneredContentView corneredContent = this.Element;

            nfloat cornerRadius = (nfloat)corneredContent.CornerRadius.TopLeft;

            if (corneredContent.HasShadow)
            {
                this.DrawDefaultShadow(this._wrapperView.Layer, this.Bounds, cornerRadius);
                this._actualView.Layer.CornerRadius = (nfloat)corneredContent.CornerRadius.TopLeft;
                this._actualView.ClipsToBounds = true;
            }
            else
            {
                this._wrapperView.Layer.ShadowOpacity = 0;
            }

            // Set the rasterization for performance optimization.
            this._wrapperView.Layer.RasterizationScale = UIScreen.MainScreen.Scale;
            this._wrapperView.Layer.ShouldRasterize = true;

            this._actualView.Layer.RasterizationScale = UIScreen.MainScreen.Scale;
            this._actualView.Layer.ShouldRasterize = true;
        }

        private void DrawDefaultShadow(CALayer layer, CGRect bounds, nfloat cornerRadius)
        {
            CorneredContentView corneredContentView = this.Element as CorneredContentView;

            // Ideally we want to be able to have individual corner radii + shadows
            // However, on iOS we can only do one radius + shadow.
            layer.CornerRadius = cornerRadius;
            layer.ShadowRadius = 10;
            layer.ShadowColor = UIColor.Black.CGColor;
            layer.ShadowOpacity = 0.4f;
            layer.ShadowOffset = new SizeF();

            layer.ShadowPath = CreateRoundedRectPath(bounds, corneredContentView.CornerRadius).CGPath;
        }

        public void AddLayer(CALayer layer, int position, UIView viewToAddTo)
        {
            // If there is already a layer with the given name, remove it before inserting.
            if (layer != null)
            {
                // There's no background layer yet, insert it.
                if (position > -1)
                    viewToAddTo.Layer.InsertSublayer(layer, position);
                else
                    viewToAddTo.Layer.AddSublayer(layer);
            }
        }

        public static UIBezierPath CreateRoundedRectPath(CGRect rect, CornerRadius cornerRadius)
        {
            UIBezierPath path = new UIBezierPath();

            path.MoveTo(new CGPoint(rect.Width - cornerRadius.TopRight, rect.Y));
            path.AddArc(new CGPoint((float)rect.X + rect.Width - cornerRadius.TopRight, (float)rect.Y + cornerRadius.TopRight), (nfloat)cornerRadius.TopRight, (float)(Math.PI * 1.5), (float)Math.PI * 2, true);
            path.AddLineTo(new CGPoint(rect.Width, rect.Height - cornerRadius.BottomRight));
            path.AddArc(new CGPoint((float)rect.X + rect.Width - cornerRadius.BottomRight, (float)rect.Y + rect.Height - cornerRadius.BottomRight), (nfloat)cornerRadius.BottomRight, 0, (float)(Math.PI * .5), true);
            path.AddLineTo(new CGPoint(cornerRadius.BottomLeft, rect.Height));
            path.AddArc(new CGPoint((float)rect.X + cornerRadius.BottomLeft, (float)rect.Y + rect.Height - cornerRadius.BottomLeft), (nfloat)cornerRadius.BottomLeft, (float)(Math.PI * .5), (float)Math.PI, true);
            path.AddLineTo(new CGPoint(rect.X, cornerRadius.TopLeft));
            path.AddArc(new CGPoint((float)rect.X + cornerRadius.TopLeft, (float)rect.Y + cornerRadius.TopLeft), (nfloat)cornerRadius.TopLeft, (float)Math.PI, (float)(Math.PI * 1.5), true);

            path.ClosePath();

            return path;
        }
    }
}