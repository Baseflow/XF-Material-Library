# Release Notes

##### 1.0.6
- Refactored `MaterialDialogs`. A feature enhancement stated in this [issue](https://github.com/contrix09/XF-Material-Library/issues/4).
  - Added `MaterialDialogs.ShowConfirmAsync()`. Use this for user confirmation of action. Returns a `boolean` value based on what the user chose.
  - `MaterialDialogs.ShowAlertAsync` is now only used for user acknowledgement purposes.
  - The overload method of `MaterialDialogs.ShowSnackbarAsync()` that has an action now also returns a `boolean` value.
- Changed the minimum `Xamarin.Android.Support.*` verion to `25.4.0.2` to address this [issue](https://github.com/contrix09/XF-Material-Library/issues/3). This is the same minimum version that the latest `Xamarin.Forms` Nuget package accepts. 
- Changed the assembly and namespace name from `XF.Material` to `XF.Material.Forms`.
- Added static properties `ColorConfiguration` and `FontConfiguration` to `XF.Material.Forms.Material` class.

##### 1.0.5
- Added NuGet Package icon.
- Added `MaterialAlertDialogConfiguration`, `MaterialLoadingConfiguration`, and `MaterialSnackbarConfiguration` for
styling modals created by `MaterialDialogs`.
- Added `MaterialConstants`, a static class to provide constant key values of Material Resources.
- Refactored methods in `MaterialDialogs`.
- Fixed `MaterialButton` throwing exception on devices running Android 4.2.
- Fixed `MaterialTextField` not showing error icon on iOS when the property `HasError` is set to `true`.

##### 1.0.4
- Fixed NuGet Package not including the features from version `1.0.3`.
- Updated to the latest Xamarin.Forms.

##### 1.0.3
- Added `MaterialTextField` and `MaterialIcon`.

##### 1.0.2
- Added type scale fonts in `MaterialFontConfiguration`.

##### 1.0.1
- Fixed some rendering bugs.
- Added a `FontAttribute.Bold` `DynamicResource` value to `MaterialButton`.
- Removed `Padding` values in `MaterialChip`.
- Pages pushed in `MaterialNavigationPage` will have a default `BackgroundColor` value equal to the `MaterialColorConfiguration.Background`, unless the `BackgroundColor` property is set in the page.

##### 1.0.0
- Initial release.