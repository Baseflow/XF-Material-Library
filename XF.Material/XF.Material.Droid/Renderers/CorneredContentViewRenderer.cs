using System;
using System.ComponentModel;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.View;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Forms.UI.Internals;
using ACanvas = Android.Graphics.Canvas;

[assembly: ExportRenderer(typeof(XF.Material.Forms.UI.Internals.CorneredContentView), typeof(XF.Material.Droid.Renderers.CorneredContentViewRenderer))]
namespace XF.Material.Droid.Renderers
{
    public class CorneredContentViewRenderer : VisualElementRenderer<ContentView>
    {
        bool _disposed;
        private CorneredContentViewDrawable _drawable;

        public CorneredContentViewRenderer(Context context) : base(context)
        {
        }

        /// <summary>
        /// This method ensures that we don't get stripped out by the linker.
        /// </summary>
        public static void Init()
        {
#pragma warning disable 0219
            Type link1 = typeof(CorneredContentViewRenderer);
            Type link2 = typeof(CorneredContentView);
#pragma warning restore 0219
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ContentView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && e.OldElement == null)
            {
                CorneredContentView corneredContentView = (this.Element as CorneredContentView);

                if (corneredContentView.Content == null)
                {
                    corneredContentView.Content = new Grid();
                }

                this.Validate(corneredContentView);

                this.SetBackground(this._drawable = new CorneredContentViewDrawable(corneredContentView, this.Context.ToPixels));

                this.SetupShadow(corneredContentView);
            }
        }

        private void Validate(CorneredContentView corneredContentView)
        {
        }

        private void SetupShadow(CorneredContentView corneredContentView)
        {
            // clear previous shadow/elevation
            this.Elevation = 0;
            this.TranslationZ = 0;


            // If it has a shadow, give it a default Droid looking shadow.
            if (corneredContentView.HasShadow)
            {
                this.Elevation = 10;
                this.TranslationZ = 10;
                this.OutlineProvider = null;
                this.ClipToOutline = false;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CorneredContentView corneredContentView = this.Element as CorneredContentView;
            this.Validate(corneredContentView);

            base.OnElementPropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case nameof(VisualElement.BackgroundColor):
                    {
                        this._drawable.Dispose();
                        this.SetBackground(this._drawable = new CorneredContentViewDrawable(corneredContentView, this.Context.ToPixels));
                        break;
                    }
                case nameof(CorneredContentView.CornerRadius):
                    {
                        this.Invalidate();
                        this.SetupShadow(corneredContentView);
                        break;
                    }
                case nameof(CorneredContentView.BorderColor):
                case nameof(CorneredContentView.BorderThickness):
                    {
                        this.Invalidate();
                        break;
                    }
                case nameof(CorneredContentView.HasShadow):
                    {
                        this.SetupShadow(corneredContentView);
                        break;
                    }
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing && !this._disposed)
            {
                this._drawable?.Dispose();
                this._disposed = true;
            }
        }

        protected override void OnDraw(ACanvas canvas)
        {
            if (this.Element == null) return;

            CorneredContentView control = (CorneredContentView)this.Element;

            this.SetClipChildren(true);

            //Create path to clip the child
            using (Path path = new Path())
            {
                path.AddRoundRect(new RectF(0, 0, this.Width, this.Height), this.GetRadii(control), Path.Direction.Ccw);

                canvas.Save();
                canvas.ClipPath(path);
            }

            this.DrawBorder(canvas, control);
        }

        protected override bool DrawChild(ACanvas canvas, global::Android.Views.View child, long drawingTime)
        {
            if (this.Element == null) return false;

            CorneredContentView control = (CorneredContentView)this.Element;

            this.SetClipChildren(true);

            //Create path to clip the child
            using (Path path = new Path())
            {
                path.AddRoundRect(new RectF(0, 0, this.Width, this.Height), this.GetRadii(control), Path.Direction.Ccw);

                canvas.Save();
                canvas.ClipPath(path);
            }

            // Draw the child first so that the border shows up above it.        
            bool result = base.DrawChild(canvas, child, drawingTime);
            canvas.Restore();

            this.DrawBorder(canvas, control);

            return result;
        }

        private float[] GetRadii(CorneredContentView control)
        {
            float topLeft = this.Context.ToPixels(control.CornerRadius.TopLeft);
            float topRight = this.Context.ToPixels(control.CornerRadius.TopRight);
            float bottomRight = this.Context.ToPixels(control.CornerRadius.BottomRight);
            float bottomLeft = this.Context.ToPixels(control.CornerRadius.BottomLeft);

            float[] radii = new[] { topLeft, topLeft, topRight, topRight, bottomRight, bottomRight, bottomLeft, bottomLeft };

            if (control.HasShadow)
            {
                radii = new[] { topLeft, topLeft, topLeft, topLeft, topLeft, topLeft, topLeft, topLeft };
            }

            return radii;
        }

        private void DrawBorder(ACanvas canvas, CorneredContentView control)
        {
            if (control.BorderThickness > 0)
            {
                float borderThickness = this.Context.ToPixels(control.BorderThickness);
                float halfBorderThickness = borderThickness / 2;

                using (Paint paint = new Paint { AntiAlias = true })
                using (Path.Direction direction = Path.Direction.Cw)
                using (Paint.Style style = Paint.Style.Stroke)
                using (RectF rect = new RectF(!control.HasShadow ? -halfBorderThickness : halfBorderThickness,
                                            !control.HasShadow ? -halfBorderThickness : halfBorderThickness,
                                            !control.HasShadow ? canvas.Width + halfBorderThickness : canvas.Width - halfBorderThickness,
                                            !control.HasShadow ? canvas.Height + halfBorderThickness : canvas.Height - halfBorderThickness))
                {
                    Path path = new Path();
                    path.AddRoundRect(rect, this.GetRadii(control), direction);

                    paint.Color = control.BorderColor.ToAndroid();

                    paint.StrokeCap = Paint.Cap.Square;
                    paint.StrokeWidth = borderThickness;

                    paint.SetStyle(style);

                    canvas.DrawPath(path, paint);
                }
            }
        }
    }
}
