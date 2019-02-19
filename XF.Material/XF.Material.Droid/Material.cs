using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using XF.Material.Forms.UI.Dialogs;

namespace XF.Material.Droid
{
    public static class Material
    {
        /// <summary>
        /// Gets the context in which the current application is running.
        /// </summary>
        internal static Context Context { get; private set; }

        /// <summary>
        /// Gets whether the current Android version is running on 4.2 (Jellybean) or lower.
        /// </summary>
        internal static bool IsJellyBean { get; private set; }

        /// <summary>
        /// Gets whether the current Android version is running on 5.0 (Lollipop) or higher.
        /// </summary>
        internal static bool IsLollipop { get; private set; }

        /// <summary>
        /// Handles the physical back button event to dismiss specific dialogs shown by <see cref="XF.Material.Forms.UI.Dialogs.MaterialDialog.Instance"/>.
        /// </summary>
        /// <param name="backAction">The base <see cref="Activity.OnBackPressed"/> method.</param>
        public static async void HandleBackButton(Action backAction)
        {
            var popupStack = PopupNavigation.Instance.PopupStack;
            var dismissableDialog = popupStack.FirstOrDefault(p => p is BaseMaterialModalPage modalPage && modalPage.Dismissable) as BaseMaterialModalPage;
            var snackBar = popupStack.FirstOrDefault(p => p is BaseMaterialModalPage modalPage && !modalPage.Dismissable) as MaterialSnackbar;

            if (popupStack.FirstOrDefault(p => p is BaseMaterialModalPage modalPage && !modalPage.Dismissable) is MaterialLoadingDialog)
            {
                return;
            }

            if (dismissableDialog != null)
            {
                await dismissableDialog.DismissAsync();
                dismissableDialog.OnBackButtonDismissed();
            }
            else if (snackBar != null)
            {
                backAction.Invoke();
            }
            else
            {
                Popup.SendBackPressed(backAction);
            }
        }

        /// <summary>
        /// Initializes the core Material components for the Android platform.
        /// </summary>
        /// <param name="context">The context in which the current App is running.</param>
        /// <param name="bundle">The string values parameter passed to the <see cref="Activity.OnCreate(Bundle)"/> method.</param>
        public static void Init(Context context, Bundle bundle)
        {
            Context = context;
            IsLollipop = Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop;
            IsJellyBean = Build.VERSION.SdkInt < BuildVersionCodes.Kitkat;

            AppCompatDelegate.CompatVectorFromResourcesEnabled = true;
            Popup.Init(context, bundle);
        }
    }
}