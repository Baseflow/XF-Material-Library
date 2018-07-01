using Android.Animation;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Views;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Views;

[assembly: ExportRenderer(typeof(MaterialButton), typeof(MaterialButtonRenderer))]
namespace XF.Material.Droid.Renderers
{
    public class MaterialButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        private MaterialButton _materialButton;

        public MaterialButtonRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _materialButton = this.Element as MaterialButton;

                this.Control.SetAllCaps(_materialButton.AllCaps);

                if (Material.IsLollipop)
                {
                    _materialButton.MinimumHeightRequest = _materialButton.HeightRequest += 12;
                    this.Control.Background = this.CreateRippleDrawable();
                    this.Control.StateListAnimator = AnimatorInflater.LoadStateListAnimator(this.Context, Resource.Animator.material_button_state_list_anim);
                }

                if (_materialButton.Image != null)
                {
                    string fileName = _materialButton.Image.File.Split('.')[0];
                    int id = this.Resources.GetIdentifier(fileName, "drawable", Material.Context.PackageName);
                    var width = (int)MaterialExtensions.ConvertDpToPx(18);
                    var height = (int)MaterialExtensions.ConvertDpToPx(18);
                    var drawable = ContextCompat.GetDrawable(Material.Context, id);
                    drawable.SetBounds(0, 0, width, height);
                    this.Control.SetCompoundDrawables(drawable, null, null, null);
                }
            }
        }

        private RippleDrawable CreateRippleDrawable()
        {
            var normalColor = _materialButton.BackgroundColor.ToAndroid();
            var cornerRadius = _materialButton.CornerRadius.ConvertDpToPx();
            var borderWidth = (int)MaterialExtensions.ConvertDpToPx((int)_materialButton.BorderWidth);
            var borderColor = _materialButton.BorderColor.ToAndroid();
            var rippleDrawable = (normalColor.IsColorDark() ? ContextCompat.GetDrawable(this.Context, Resource.Drawable.drawable_ripple_dark) as RippleDrawable : ContextCompat.GetDrawable(this.Context, Resource.Drawable.drawable_ripple_light) as RippleDrawable).GetConstantState().NewDrawable().Mutate() as RippleDrawable;
            var insetDrawable = rippleDrawable.FindDrawableByLayerId(Resource.Id.inset_drawable) as InsetDrawable;

            var gradientDrawable = insetDrawable.Drawable as GradientDrawable;
            gradientDrawable.SetCornerRadius(cornerRadius);
            gradientDrawable.SetColor(normalColor);
            gradientDrawable.SetStroke(borderWidth, borderColor);

            return rippleDrawable;
        }
    }
}