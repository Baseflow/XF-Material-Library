# Release Notes

##### 1.0.5
- Added `MaterialDialogConfiguration`, `MaterialLoadingConfiguration`, and `MaterialSnackbarConfiguration` for
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