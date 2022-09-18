using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Compatibility.Platform.Android.AppCompat;
using Microsoft.Maui.Controls.Platform;
using XF.Material.Droid.Renderers;
using XF.Material.Maui.UI;
using static Android.Views.View;

[assembly: ExportRenderer(typeof(MaterialCard), typeof(MaterialCardRenderer))]
namespace XF.Material.Droid.Renderers
{
    public class MaterialCardRenderer : FrameRenderer, IOnTouchListener
    {
        private MaterialCard _materialCard;

        public MaterialCardRenderer(Context context) : base(context)
        {
        }

        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            if (_materialCard.GestureRecognizers.Count <= 0 || Control.Foreground == null)
            {
                return false;
            }

            switch (e.Action)
            {
                case MotionEventActions.Down:
                    Control.Foreground.SetHotspot(e.GetX(), e.GetY());
                    Control.Pressed = true;
                    break;
                case MotionEventActions.Up:
                case MotionEventActions.Cancel:
                case MotionEventActions.Outside:
                    Control.Pressed = false;
                    break;
            }
            return false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement == null)
            {
                return;
            }

            _materialCard = Element as MaterialCard;

            UpdateStrokeColor();
            Control.Elevate(_materialCard.Elevation);
            SetClickable();
            Control.SetOnTouchListener(this);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e?.PropertyName)
            {
                case nameof(MaterialCard.Elevation):
                    Control.Elevate(_materialCard.Elevation);
                    break;
                case nameof(MaterialCard.IsClickable):
                    SetClickable();
                    break;
                case nameof(Frame.BackgroundColor):
                    UpdateStrokeColor();
                    break;
            }
        }

        private void SetClickable()
        {
            var clickable = _materialCard.IsClickable;
            if (clickable && Control.Foreground == null)
            {
                var outValue = new TypedValue();
                Context.Theme.ResolveAttribute(
                    Android.Resource.Attribute.SelectableItemBackground, outValue, true);
                Control.Foreground = Context.GetDrawable(outValue.ResourceId);
            }

            Control.Focusable = clickable;
            Control.Clickable = clickable;
        }

        private void UpdateStrokeColor()
        {
            var borderColor = _materialCard.BorderColor.IsDefault() ? _materialCard.BackgroundColor : _materialCard.BorderColor;
            var drawable = (GradientDrawable)Control.Background;
            drawable.SetStroke((int)MaterialHelper.ConvertDpToPx(1), borderColor.ToAndroid());
        }
    }
}
