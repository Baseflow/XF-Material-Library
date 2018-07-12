using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Views;
using XF.Material.iOS.Renderers;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(MaterialCard), typeof(MaterialCardRenderer))]
namespace XF.Material.iOS.Renderers
{
    public class MaterialCardRenderer : FrameRenderer
    {
        private MaterialCard _materialCard;

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _materialCard = this.Element as MaterialCard;
                this.Elevate(_materialCard.Elevation);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e?.PropertyName == nameof(MaterialCard.Elevation))
            {
                this.Elevate(_materialCard.Elevation);
            }
        }
    }
}