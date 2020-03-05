using System.ComponentModel;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.Core.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers.Internals;
using XF.Material.Forms.UI.Internals;

[assembly: ExportRenderer(typeof(MaterialEntry), typeof(MaterialEntryRenderer))]
namespace XF.Material.Droid.Renderers.Internals
{
    internal class MaterialEntryRenderer : EntryRenderer
    {
        private MaterialEntry _materialEntry;

        public MaterialEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                ChangeCursorColor();
                _materialEntry = Element as MaterialEntry;
                SetControl();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e?.PropertyName == nameof(MaterialEntry.TintColor))
            {
                ChangeCursorColor();
            }
        }

        private void SetControl()
        {
            if (Control == null)
            {
                return;
            }

            Control.Background = new ColorDrawable(Color.Transparent.ToAndroid());
            Control.SetPadding(0, 0, 0, 0);
            Control.SetIncludeFontPadding(false);
            Control.SetMinimumHeight((int)MaterialHelper.ConvertToDp(20));

            //// DEV HINT: This will be used for the future control `MaterialTextArea`.
            //// This removes the 'Next' button and shows a 'Done' button when the device's orientation is in landscape.
            //// This prevents the crash that is caused by a `java.lang.IllegalStateException'.
            //// Reported here https://github.com/xamarin/Xamarin.Forms/issues/4832.
            //this.Control.ImeOptions = Android.Views.InputMethods.ImeAction.Done;
        }

        private void ChangeCursorColor()
        {
            if (Control == null)
            {
                return;
            }

            try
            {
                var field = Java.Lang.Class.FromType(typeof(Android.Widget.TextView)).GetDeclaredField("mCursorDrawableRes");
                field.Accessible = true;
                var resId = field.GetInt(Control);

                field = Java.Lang.Class.FromType(typeof(Android.Widget.TextView)).GetDeclaredField("mEditor");
                field.Accessible = true;

                var cursorDrawable = ContextCompat.GetDrawable(Context, resId);
                cursorDrawable.SetColorFilter(((MaterialEntry)Element).TintColor.ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcIn);

                var editor = field.Get(Control);
                field = editor.Class.GetDeclaredField("mCursorDrawable");
                field.Accessible = true;
                field.Set(editor, new[] { cursorDrawable, cursorDrawable });
            }
            catch (Java.Lang.NoSuchFieldException)
            {
                // Cannot change textfield's cursor color.
            }
        }

        public bool OnEditorAction(TextView v, [GeneratedEnum] ImeAction actionId, KeyEvent e)
        {
            var currentFocus = (Context as Activity).CurrentFocus;

            if (currentFocus != null)
            {
                var inputMethodManager = (InputMethodManager)(Context as Activity).GetSystemService(Context.InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
            }

            _materialEntry.ReturnCommand?.Execute(_materialEntry.ReturnCommandParameter);

            return false;
        }
    }
}
