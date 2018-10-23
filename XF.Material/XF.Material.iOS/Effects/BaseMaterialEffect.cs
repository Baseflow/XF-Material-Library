using System.Linq;
using Xamarin.Forms.Platform.iOS;

namespace XF.Material.iOS.Effects
{
    public class BaseMaterialEffect<TEffect> : PlatformEffect where TEffect : Forms.Effects.BaseMaterialEffect
    {
        public TEffect MaterialEffect { get; private set; }

        protected override void OnAttached()
        {
            this.MaterialEffect = this.Element.Effects.FirstOrDefault(e => e is TEffect) as TEffect;
            this.MaterialEffect.ApplyEffect();
        }

        protected override void OnDetached() { }
    }
}