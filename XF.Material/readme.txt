---------------------------------
XF.Material
---------------------------------

1. Download the current version through NuGet and install it in your Maui projects.

2. Call the Material.Init() method in each project:

// Maui
public App()
{
    this.InitializeComponent();
    XF.Material.Maui.Material.Init(this);
}

// Maui.Android
protected override void OnCreate(Bundle savedInstanceState)
{
    TabLayoutResource = Resource.Layout.Tabbar;
    ToolbarResource = Resource.Layout.Toolbar;

    base.OnCreate(savedInstanceState);

    Xamarin.Forms.Forms.Init(this, savedInstanceState);
    XF.Material.Droid.Material.Init(this, savedInstanceState);

    this.LoadApplication(new App());
}

// Maui.iOS
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
    Xamarin.Forms.Forms.Init();
    XF.Material.iOS.Material.Init();
    this.LoadApplication(new App());

    return base.FinishedLaunching(app, options);
}

3. Configure your application's color and font resources.

- Additional configuration for iOS
In order to be able to change the status bar's colors using this or by setting your app colors here, add this to your info.plist file:

<key>UIViewControllerBasedStatusBarAppearance</key>
<false/>


---------------------------------
Star on Github if this project helps you: https://github.com/BaseflowIT/XF-Material-Library

Commercial support is available. Integration with your app or services, samples, feature request, etc. Email: hello@baseflow.com
Powered by: https://baseflow.com
---------------------------------

---------------------------------
XF.Material Info.plist file
---------------------------------
Because of a bug in Assets management for Xamarin.iOS projects, it is required to include a very basic Info.plist file
into the XF.Material project, to avoid XF.Material library's assets to be overriden by the final app's assets.
DO NOT MODIFY the Info.plist file