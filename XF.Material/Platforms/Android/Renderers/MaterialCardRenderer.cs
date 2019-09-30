using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Forms.UI;
using static Android.Views.View;

[assembly: ExportRenderer(typeof(MaterialCard), typeof(MaterialCardRenderer))]

namespace XF.Material.Droid.Renderers
{
    public class MaterialCardRenderer : Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer, IOnTouchListener
    {
        private MaterialCard _materialCard;

        public MaterialCardRenderer(Context context) : base(context)
        {
        }

        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            if (_materialCard.GestureRecognizers.Count <= 0 || this.Control.Foreground == null) return false;
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    this.Control.Foreground.SetHotspot(e.GetX(), e.GetY());
                    this.Control.Pressed = true;
                    break;
                case MotionEventActions.Up:
                case MotionEventActions.Cancel:
                case MotionEventActions.Outside:
                    this.Control.Pressed = false;
                    break;
            }
            return false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement == null) return;
            _materialCard = this.Element as MaterialCard;

            this.UpdateStrokeColor();
            this.Control.Elevate(_materialCard.Elevation);
            this.SetClickable();
            this.Control.SetOnTouchListener(this);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e?.PropertyName)
            {
                case nameof(MaterialCard.Elevation):
                    this.Control.Elevate(_materialCard.Elevation);
                    break;
                case nameof(MaterialCard.IsClickable):
                    this.SetClickable();
                    break;
                case nameof(Frame.BackgroundColor):
                    this.UpdateStrokeColor();
                    break;
            }
        }

        private void SetClickable()
        {
            var clickable = _materialCard.IsClickable;
            if (clickable && this.Control.Foreground == null)
            {
                var outValue = new TypedValue();
                this.Context.Theme.ResolveAttribute(
                    Android.Resource.Attribute.SelectableItemBackground, outValue, true);
                this.Control.Foreground = this.Context.GetDrawable(outValue.ResourceId);
            }

            this.Control.Focusable = clickable;
            this.Control.Clickable = clickable;
        }

        private void UpdateStrokeColor()
        {
            var borderColor = _materialCard.BorderColor.IsDefault ? _materialCard.BackgroundColor : _materialCard.BorderColor;
            var drawable = (GradientDrawable)this.Control.Background;
            drawable.SetStroke((int)MaterialHelper.ConvertToDp(1), borderColor.ToAndroid());
        }
    }
}
