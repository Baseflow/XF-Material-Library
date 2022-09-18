using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.AppCompat.App;
using Microsoft.Maui.LifecycleEvents;
using Mopups.Hosting;
using Mopups.Services;
using XF.Material.Maui.UI.Dialogs;

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
        /// Handles the physical back button event to dismiss specific dialogs shown by <see cref="XF.Material.Maui.UI.Dialogs.MaterialDialog.Instance"/>.
        /// </summary>
        /// <param name="backAction">The base <see cref="Activity.OnBackPressed"/> method.</param>
        public static void HandleBackButton(Action backAction)
        {
            var popupStack = MopupService.Instance.PopupStack;
            var dismissableDialog = popupStack.FirstOrDefault(p => p is BaseMaterialModalPage modalPage && modalPage.Dismissable) as BaseMaterialModalPage;
            var snackBar = popupStack.FirstOrDefault(p => p is BaseMaterialModalPage modalPage && !modalPage.Dismissable) as MaterialSnackbar;

            if (popupStack.FirstOrDefault(p => p is BaseMaterialModalPage modalPage && !modalPage.Dismissable) is MaterialLoadingDialog)
            {
                return;
            }

            if (dismissableDialog != null)
            {
                ((Activity)Context).RunOnUiThread(async () => await dismissableDialog.DismissAsync());
                dismissableDialog.RaiseOnBackButtonDismissed();
            }
            else if (snackBar != null)
            {
                backAction.Invoke();
            }
            else
            {
                Mopups.Droid.Implementation.AndroidMopups.SendBackPressed(backAction);
            }
        }

        /// <summary>
        /// Initializes the core Material components for the Android platform.
        /// </summary>
        /// <param name="context">The context in which the current App is running.</param>
        /// <param name="bundle">The string values parameter passed to the <see cref="Activity.OnCreate(Bundle)"/> method.</param>
        public static void Init(Context context, Bundle bundle)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            IsLollipop = Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop;
            IsJellyBean = Build.VERSION.SdkInt < BuildVersionCodes.Kitkat;

            AppCompatDelegate.CompatVectorFromResourcesEnabled = true;
            // TODO: Init Popups has changed
        }
    }
}
