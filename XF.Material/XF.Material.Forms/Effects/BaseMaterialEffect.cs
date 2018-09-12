using Xamarin.Forms;

namespace XF.Material.Forms.Effects
{
    public abstract class BaseMaterialEffect : RoutingEffect
    {
        protected BaseMaterialEffect(string effectId) : base(effectId) { }

        public virtual void ApplyEffect() { }
    }
}
