using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using XF.Material.Resources;

namespace XF.Material.Droid
{
    public static class Material
    {
        public static bool IsLollipop;
        public static Context Context;

        public static void Init(Context context, Bundle bundle)
        {
            Context = context;
            IsLollipop = Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop;

            AppCompatDelegate.CompatVectorFromResourcesEnabled = true;
            Rg.Plugins.Popup.Popup.Init(context, bundle);
        }
    }
}