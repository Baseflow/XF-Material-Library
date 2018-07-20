
# XF.Material Library
A Xamarin.Forms library for Xamarin.Android and Xamarin.iOS to implement [Google's Material Design](https://material.io/design).

#### Getting Started
1. Download it through [NuGet](https://www.nuget.org/packages/XF.Material) and install it to your Xamarin.Forms projects.
2. Call the `Material.Init()` method in each project:

```c#
//Xamarin.Forms

public App()
{
    InitializeComponent();
    XF.Material.Material.Init(this);
}

//Xamarin.Android

protected override void OnCreate(Bundle savedInstanceState)
{
    TabLayoutResource = Resource.Layout.Tabbar;
    ToolbarResource = Resource.Layout.Toolbar;

    base.OnCreate(savedInstanceState);

    Xamarin.Forms.Forms.Init(this, savedInstanceState);
    XF.Material.Droid.Material.Init(this, savedInstanceState);

    LoadApplication(new App());
}

//Xamarin.iOS

public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
    Xamarin.Forms.Forms.Init();
    XF.Material.iOS.Material.Init();
    LoadApplication(new App());

    return base.FinishedLaunching(app, options);
}

```

## Features

### Material Views


#### Cards

Use this for content emphasis. Cards are surfaces that display content and actions on a single topic.
They should be easy to scan for relevant and actionable information. Elements, like text and images, should be placed on them in a way that clearly indicates hierarchy.

| Code | Android  | iOS |
| ------------- | ------------- | ------------- |
| ` <material:MaterialCard CornerRadius="2" Elevation="1" HeightRequest="80" HorizontalOptions="FillAndExpand" /> ` |<img src="https://imgur.com/ewedLPj.jpg" alt="Android card" width="500" />|<img src="https://imgur.com/C2dAeI7.jpg" alt="iOS card" width="550"/> |

<b>Properties:</b>

`MaterialCard` inherits the `Frame` class.
- `Elevation` - By default, this property has a value of 1. As you can see in the above images, as you increase the value, the more visible the shadow becomes.

#### Buttons

Buttons allow users to take actions, and make choices, with a single tap.

| Code | Android  | iOS |
| ------------- | ------------- | ------------- |
| `<material:MaterialButton BackgroundColor="#EAEAEA" HorizontalOptions="Center" Text="Elevated Button" TextColor="Black" VerticalOptions="Center" /> ` |<img src="https://imgur.com/o3ubmaD.jpg" alt="Android button" width="500" />|<img src="https://imgur.com/kjy9rAl.jpg" alt="iOS button" width="550"/> |

<b>Properties:</b>

`MaterialButton` inherits the `Button` class
1. `ButtonType` - The type of the button.

    - `Elevated` - This button will cast a shadow. The shadow will react to click and focus events on the button.
    - `Flat` - This button will have no shadow.
    - `Outlined` - This button will have no shadow, has a transparent background, and has a border.
    - `Text` - This button will only show its label. It will not have a shadow, has a transparent background, and no border. *Text buttons has a smaller inner padding as compared to the other button types*

2. `BackgroundColor` - The color of the button's background. *Outlined and Text button types will always have a transparent background color.*

3. `AllCaps` - Whether the letters in the label of the button should be in upper case or not. By default, this is set to `true`.

<b>Differences compared to the default Xamarin.Forms.Button</b>

As you can see in the image below, `MaterialButton` has an additional touch padding of 6 on all sides. This makes them a little taller/wider than the default button.

<img src="https://imgur.com/5X6uVFp.jpg" width="500" />
