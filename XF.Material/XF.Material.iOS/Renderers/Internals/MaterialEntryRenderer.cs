using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.iOS.Renderers.Internals;
using XF.Material.Forms.UI.Internals;
using System.ComponentModel;
using System.Drawing;

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
                this.SetControl();
            }
        }

        private void SetControl()
        {
            if(this.Control == null)
            {
                return;
            }

            var heightConstraint = NSLayoutConstraint.Create(this.Control, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 20f);
            this.Control.AddConstraint(heightConstraint);
            this.Control.BackgroundColor = UIColor.Clear;
            this.Control.TintColor = (this.Element as MaterialEntry)?.TintColor.ToUIColor();
            this.Control.BorderStyle = UITextBorderStyle.None;
            this.Control.TranslatesAutoresizingMaskIntoConstraints = false;
            this.AddRemoveReturnKeyToNumericInput();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (this.Control == null) return;

            if (e?.PropertyName == nameof(MaterialEntry.TintColor))
            {
                this.Control.TintColor = (this.Element as MaterialEntry)?.TintColor.ToUIColor();
            }

            if(e?.PropertyName == nameof(MaterialEntry.Keyboard))
            {
                this.AddRemoveReturnKeyToNumericInput();
            }
        }

        private void AddRemoveReturnKeyToNumericInput()
        {
            if(this.Element.Keyboard == Keyboard.Numeric)
            {
                UIToolbar toolbar = null;

                if(!_returnButtonAdded)
                {
                    toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 50.0f, 44.0f));

                    var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
                    {
                        Control.ResignFirstResponder();
                        this.Element.SendCompleted();
                    });

                    toolbar.Items = new[] { new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace), doneButton };

                    _returnButtonAdded = true;
                }

                this.Control.InputAccessoryView = toolbar;
            }

            else if(this.Element.Keyboard != Keyboard.Numeric)
            {
                this.Control.InputAccessoryView = null;
            }

        }
    }
}