using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Material")]
namespace XF.Material.Droid.Effects
{
    public class BaseMaterialEffect<TEffect> : PlatformEffect where TEffect : Forms.Effects.BaseMaterialEffect
    {
        public TEffect MaterialEffect { get; private set; }

        protected override void OnAttached()
        {
            this.MaterialEffect = this.Element.Effects.FirstOrDefault(e => e is TEffect) as TEffect;
        }

        protected override void OnDetached() { }
    }
}