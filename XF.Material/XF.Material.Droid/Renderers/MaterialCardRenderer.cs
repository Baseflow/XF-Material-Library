using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using System.ComponentModel;
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

        public MaterialCardRenderer(Context context) : base(context) { }

        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            if (this._materialCard.GestureRecognizers.Count > 0)
            {
                if (this.Control.Foreground != null)
                {
                    if (e.Action == MotionEventActions.Down)
                    {
                        this.Control.Foreground.SetHotspot(e.GetX(), e.GetY());
                        this.Control.Pressed = true;
                    }
                    else if (e.Action == MotionEventActions.Up ||
                        e.Action == MotionEventActions.Cancel ||
                        e.Action == MotionEventActions.Outside)
                    {
                        this.Control.Pressed = false;
                    }
                }
            }
            return false;
        }


        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _materialCard = this.Element as MaterialCard;

                #region SHADOW FIX FOR BELOW API 23

                if (Build.VERSION.SdkInt < BuildVersionCodes.N && _materialCard.Elevation > 0)
                {
                    _materialCard.BorderColor = _materialCard.BackgroundColor;
                }

                #endregion

                this.Control.Elevate(_materialCard.Elevation);

                this.SetClickable();
                this.Control.SetOnTouchListener(this);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e?.PropertyName == nameof(MaterialCard.Elevation))
            {
                this.Control.Elevate(_materialCard.Elevation);
            }

            if (e?.PropertyName == nameof(MaterialCard.IsClickable))
            {
                this.SetClickable();

            }
        }

        protected void SetClickable()
        {
            bool clickable = _materialCard.IsClickable;
            if (clickable && this.Control.Foreground == null)
            {
                TypedValue outValue = new TypedValue();
                this.Context.Theme.ResolveAttribute(
                    Resource.Attribute.selectableItemBackground, outValue, true);
                this.Control.Foreground = this.Context.GetDrawable(outValue.ResourceId);
            }

            this.Control.Focusable = clickable;
            this.Control.Clickable = clickable;
        }
    }
}