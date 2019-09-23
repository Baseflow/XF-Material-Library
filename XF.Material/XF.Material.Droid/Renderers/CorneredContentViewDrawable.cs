using Android.Graphics;
using Android.Graphics.Drawables;
using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Forms.UI.Internals;
using ACanvas = Android.Graphics.Canvas;

namespace XF.Material.Droid.Renderers
{
    public class CorneredContentViewDrawable : Drawable
    {
        readonly CorneredContentView _corneredContentView;
        readonly Func<double, float> _convertToPixels;
        Bitmap _normalBitmap;
        bool _isDisposed;

        public override int Opacity
        {
            get { return 0; }
        }

        public CorneredContentViewDrawable(CorneredContentView corneredContentView, Func<double, float> convertToPixels)
        {
            this._corneredContentView = corneredContentView;
            this._convertToPixels = convertToPixels;
            this._corneredContentView.PropertyChanged += this.CorneredContentViewOnPropertyChanged;
        }

        public override void Draw(ACanvas canvas)
        {
            int width = this.Bounds.Width();
            int height = this.Bounds.Height();

            if (width <= 0 || height <= 0)
            {
                this.DisposeBitmap();

                return;
            }

            try
            {
                if (this._normalBitmap == null || this._normalBitmap.Height != height || this._normalBitmap.Width != width)
                {
                    // If the user changes the orientation of the screen, make sure to destroy reference before
                    // reassigning a new bitmap reference.
                    this.DisposeBitmap();

                    this._normalBitmap = this.CreateBitmap(false, width, height);
                }
            }
            catch (ObjectDisposedException)
            {
                // This bitmap will sometimes be disposed as ListView/CollectionView scrolling or refreshing happens,
                // so we re-create the bitmap again.
                this._normalBitmap = this.CreateBitmap(false, width, height);
            }

            using (Paint paint = new Paint())
            {
                canvas.DrawBitmap(this._normalBitmap, 0, 0, paint);
            }
        }

        private void DisposeBitmap()
        {
            if (this._normalBitmap != null)
            {
                this._normalBitmap.Dispose();
                this._normalBitmap = null;
            }
        }

        public override void SetAlpha(int alpha)
        {
        }

        public override void SetColorFilter(ColorFilter colorFilter)
        {
        }

        protected override bool OnStateChange(int[] state)
        {
            return false;
        }

        Bitmap CreateBitmap(bool pressed, int width, int height)
        {
            Bitmap bitmap;

            using (Bitmap.Config config = Bitmap.Config.Argb8888)
            {
                bitmap = Bitmap.CreateBitmap(width, height, config);
            }

            using (ACanvas canvas = new ACanvas(bitmap))
            {
                this.DrawCanvas(canvas, width, height, pressed);
            }

            return bitmap;
        }

        void DrawBackground(ACanvas canvas, int width, int height, CornerRadius cornerRadius, bool pressed)
        {
            using (Paint paint = new Paint { AntiAlias = true })
            using (Path.Direction direction = Path.Direction.Cw)
            using (Paint.Style style = Paint.Style.Fill)
            {
                Path path = new Path();

                using (RectF rect = new RectF(0, 0, width, height))
                {
                    float topLeft = this._convertToPixels(cornerRadius.TopLeft);
                    float topRight = this._convertToPixels(cornerRadius.TopRight);
                    float bottomRight = this._convertToPixels(cornerRadius.BottomRight);
                    float bottomLeft = this._convertToPixels(cornerRadius.BottomLeft);

                    if (!this._corneredContentView.HasShadow)
                        path.AddRoundRect(rect, new float[] { topLeft, topLeft, topRight, topRight, bottomRight, bottomRight, bottomLeft, bottomLeft }, direction);
                    else
                        path.AddRoundRect(rect, new float[] { topLeft, topLeft, topLeft, topLeft, topLeft, topLeft, topLeft, topLeft }, direction);
                }

                global::Android.Graphics.Color color = this._corneredContentView.BackgroundColor.ToAndroid();
                paint.SetStyle(style);
                paint.Color = color;

                canvas.DrawPath(path, paint);
            }
        }

        void CorneredContentViewOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName ||
                e.PropertyName == CorneredContentView.CornerRadiusProperty.PropertyName)
            {
                if (this._normalBitmap == null)
                    return;

                using (ACanvas canvas = new ACanvas(this._normalBitmap))
                {
                    int width = this.Bounds.Width();
                    int height = this.Bounds.Height();
                    canvas.DrawColor(global::Android.Graphics.Color.Black, PorterDuff.Mode.Clear);
                    this.DrawCanvas(canvas, width, height, false);
                }

                this.InvalidateSelf();
            }
        }

        void DrawCanvas(ACanvas canvas, int width, int height, bool pressed)
        {
            this.DrawBackground(canvas, width, height, this._corneredContentView.CornerRadius, pressed);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this._isDisposed)
            {
                this.DisposeBitmap();

                if (this._corneredContentView != null)
                {
                    this._corneredContentView.PropertyChanged -= this.CorneredContentViewOnPropertyChanged;
                }

                this._isDisposed = true;
            }

            base.Dispose(disposing);
        }
    }
}