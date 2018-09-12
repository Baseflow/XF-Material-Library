using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using XF.Material.Forms.Dialogs;

namespace XF.Material.Droid
{
    public static class Material
    {
        /// <summary>
        /// Gets the context in which the current application is running.
        /// </summary>
        public static Context Context { get; private set; }

        /// <summary>
        /// Gets whether the current Android version is running on 5.0 (Lollipop) or higher.
        /// </summary>
        public static bool IsLollipop { get; private set; }

        /// <summary>
        /// Gets whether the current Android version is running on 4.2 (Jellybean) or lower.
        /// </summary>
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
            var simpleDialog = PopupNavigation.Instance.PopupStack.FirstOrDefault(p => p is MaterialSimpleDialog);

            if (Popup.SendBackPressed(backAction) && materialDialog != null && loadingDialog == null)
            {
                PopupNavigation.Instance.RemovePageAsync(materialDialog);
            }

            else if (Popup.SendBackPressed(backAction) && simpleDialog != null && loadingDialog == null)
            {
                PopupNavigation.Instance.RemovePageAsync(simpleDialog);
            }

            else if (Popup.SendBackPressed(backAction) && materialDialog == null && loadingDialog == null && simpleDialog == null)
            {
                backAction?.Invoke();
            }
        }
    }
}