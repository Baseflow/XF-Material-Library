using Android.Content;
using Android.OS;
using Android.Support.V7.App;

namespace XF.Material.Droid
{
    public static class Material
    {
        public static Context Context;

        public static void Init(Context context, Bundle bundle)
        {
            Context = context;
            AppCompatDelegate.CompatVectorFromResourcesEnabled = true;
            Rg.Plugins.Popup.Popup.Init(context, bundle);
        }
    }
}