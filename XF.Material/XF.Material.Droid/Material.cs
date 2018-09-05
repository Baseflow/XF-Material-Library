using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using XF.Material.Dialogs;

namespace XF.Material.Droid
{
    public static class Material
    {
        public static Context Context { get; private set; }

        public static bool IsLollipop { get; private set; }

        public static bool IsJellyBean { get; private set; }

        public static void Init(Context context, Bundle bundle)
        {
            Context = context;
            IsLollipop = Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop;
            IsJellyBean = Build.VERSION.SdkInt < BuildVersionCodes.Kitkat;

            AppCompatDelegate.CompatVectorFromResourcesEnabled = true;
            Popup.Init(context, bundle);
        }

        public static void HandleBackButton(Action backAction)
        {
            var materialDialog = PopupNavigation.Instance.PopupStack.FirstOrDefault(p => p is MaterialAlertDialog);
            var loadingDialog = PopupNavigation.Instance.PopupStack.FirstOrDefault(p => p is MaterialLoadingDialog);

            if (Popup.SendBackPressed(backAction) && materialDialog != null && loadingDialog == null)
            {
                PopupNavigation.Instance.RemovePageAsync(materialDialog);
            }

            else if (Popup.SendBackPressed(backAction) && materialDialog == null && loadingDialog == null)
            {
                backAction?.Invoke();
            }
        }
    }
}