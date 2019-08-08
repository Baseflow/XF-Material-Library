using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers.Internals;
using XF.Material.Forms.UI.Internals;
using static Android.Text.TextUtils;
using static Android.Widget.TextView;

[assembly: ExportRenderer(typeof(MaterialEditor), typeof(MaterialEditorRenderer))]
namespace XF.Material.Droid.Renderers.Internals
{
    internal class MaterialEditorRenderer : EditorRenderer
    {
        private MaterialEditor _materialEditor;

        public MaterialEditorRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                this.ChangeCursorColor();
                this._materialEditor = this.Element as MaterialEditor;
                this.SetControl();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e?.PropertyName == nameof(MaterialEditor.TintColor))
            {
                this.ChangeCursorColor();
            }
        }

        private void SetControl()
        {
            if (this.Control == null)
            {
                return;
            }

            this.Control.Background = new ColorDrawable(Color.Transparent.ToAndroid());
            this.Control.SetPadding(0, 0, 0, 0);
            this.Control.SetIncludeFontPadding(false);
            this.Control.SetMinimumHeight((int)MaterialHelper.ConvertToDp(20));

            // This removes the 'Next' button and shows a 'Done' button when the device's orientation is in landscape.
            // This prevents the crash that is caused by a `java.lang.IllegalStateException'.
            // Reported here https://github.com/xamarin/Xamarin.Forms/issues/4832.
            this.Control.ImeOptions = Android.Views.InputMethods.ImeAction.Done;
        }

        private void ChangeCursorColor()
        {
            if (this.Control == null)
            {
                return;
            }

            try
            {
                var field = Java.Lang.Class.FromType(typeof(Android.Widget.TextView)).GetDeclaredField("mCursorDrawableRes");
                field.Accessible = true;
                var resId = field.GetInt(this.Control);

                field = Java.Lang.Class.FromType(typeof(Android.Widget.TextView)).GetDeclaredField("mEditor");
                field.Accessible = true;

                var cursorDrawable = ContextCompat.GetDrawable(this.Context, resId);
                cursorDrawable.SetColorFilter(((MaterialEditor)this.Element).TintColor.ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcIn);

                var editor = field.Get(this.Control);
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
            var currentFocus = (this.Context as Activity).CurrentFocus;

            if (currentFocus != null)
            {
                var inputMethodManager = (InputMethodManager)(this.Context as Activity).GetSystemService(Context.InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
            }

            return false;
        }
    }
}