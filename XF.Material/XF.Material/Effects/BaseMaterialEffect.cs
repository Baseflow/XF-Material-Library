using Xamarin.Forms;

namespace XF.Material.Effects
{
    public class BaseMaterialEffect : RoutingEffect
    {
        public VisualElement MaterialElement { get; set; }

        public BaseMaterialEffect(string effectId) : base(effectId) { }

        public virtual void ApplyEffect() { }
    }
}
