using System.ComponentModel;
using System.Drawing;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI.Internals;
using XF.Material.iOS.Renderers.Internals;

[assembly: ExportRenderer(typeof(MaterialEntry), typeof(MaterialEntryRenderer))]
namespace XF.Material.iOS.Renderers.Internals
{
    internal class MaterialEntryRenderer : EntryRenderer
    {
        private bool _returnButtonAdded;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                SetControl();
            }
        }

        private void SetControl()
        {
            if (Control == null)
            {
                return;
            }

            var heightConstraint = NSLayoutConstraint.Create(Control, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 20f);
            Control.AddConstraint(heightConstraint);
            Control.BackgroundColor = UIColor.Clear;
            Control.TintColor = (Element as MaterialEntry)?.TintColor.ToUIColor();
            Control.BorderStyle = UITextBorderStyle.None;
            Control.TranslatesAutoresizingMaskIntoConstraints = false;
            AddRemoveReturnKeyToNumericInput((Element as MaterialEntry).IsNumericKeyboard);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
            {
                return;
            }

            if (e?.PropertyName == nameof(MaterialEntry.TintColor))
            {
                Control.TintColor = (Element as MaterialEntry)?.TintColor.ToUIColor();
            }

            if (e?.PropertyName == nameof(MaterialEntry.IsNumericKeyboard))
            {
                AddRemoveReturnKeyToNumericInput((Element as MaterialEntry).IsNumericKeyboard);
            }
        }

        private void AddRemoveReturnKeyToNumericInput(bool isNumeric)
        {
            if (isNumeric)
            {
                UIToolbar toolbar = null;

                if (!_returnButtonAdded)
                {
                    toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 50.0f, 44.0f));

                    var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
                    {
                        Control.ResignFirstResponder();
                        Element.SendCompleted();
                    });

                    toolbar.Items = new[] { new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace), doneButton };

                    _returnButtonAdded = true;
                }

                Control.InputAccessoryView = toolbar;
            }
            else
            {
                Control.InputAccessoryView = null;
            }
        }
    }
}