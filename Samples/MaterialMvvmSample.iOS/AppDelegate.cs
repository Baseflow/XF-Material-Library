using Foundation;
using MaterialMvvmSample.iOS.Core;
using Microsoft.Maui;
using UIKit;

namespace MaterialMvvmSample.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MauiUIApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            XF.Material.iOS.Material.Init();

            var appContainer = new PlatformContainer();
            appContainer.Setup();

            return base.FinishedLaunching(uiApplication, launchOptions);
        }

        protected override MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
            ;

            return builder.Build();
        }
    }
}
