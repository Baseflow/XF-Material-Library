using Android.App;
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
        internal static bool IsLollipop { get; private set; }

        /// <summary>
        /// Gets whether the current Android version is running on 4.2 (Jellybean) or lower.
        /// </summary>
        internal static bool IsJellyBean { get; private set; }

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
            var backPressedResult = Popup.SendBackPressed(backAction);
            var alertDialog = PopupNavigation.Instance.PopupStack.FirstOrDefault(p => p is MaterialAlertDialog);
            var loadingDialog = PopupNavigation.Instance.PopupStack.FirstOrDefault(p => p is MaterialLoadingDialog);
            var simpleDialog = PopupNavigation.Instance.PopupStack.FirstOrDefault(p => p is MaterialSimpleDialog);
            var confirmationDialog = PopupNavigation.Instance.PopupStack.FirstOrDefault(p => p is MaterialConfirmationDialog);
            var fragmentDialog = PopupNavigation.Instance.PopupStack.FirstOrDefault(p => p is MaterialDialogFragment);

            if (backPressedResult && alertDialog != null && loadingDialog == null)
            {
                ((Activity)Context).RunOnUiThread(async() => await PopupNavigation.Instance.RemovePageAsync(alertDialog));
            }

            else if (backPressedResult && simpleDialog != null && loadingDialog == null)
            {
                ((Activity)Context).RunOnUiThread(async () => await PopupNavigation.Instance.RemovePageAsync(simpleDialog));
            }

            else if(backPressedResult && confirmationDialog != null && loadingDialog == null)
            {
                ((Activity)Context).RunOnUiThread(async () => await PopupNavigation.Instance.RemovePageAsync(confirmationDialog));
            }

            else if (backPressedResult && fragmentDialog != null && loadingDialog == null)
            {
                ((Activity)Context).RunOnUiThread(async () => await PopupNavigation.Instance.RemovePageAsync(fragmentDialog));
            }

            else if (backPressedResult && alertDialog == null && loadingDialog == null)
            {
                backAction?.Invoke();
            }
        }
    }
}