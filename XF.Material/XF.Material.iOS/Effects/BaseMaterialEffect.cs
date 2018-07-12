using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace XF.Material.iOS.Effects
{
    public class BaseMaterialEffect<T> : PlatformEffect where T : XF.Material.Effects.BaseMaterialEffect
    {
        public T MaterialEffect { get; private set; }

        protected override void OnAttached()
        {
            this.MaterialEffect = this.Element.Effects.FirstOrDefault(e => e is T) as T;
            this.MaterialEffect.MaterialElement = this.Element as VisualElement;
            this.MaterialEffect.ApplyEffect();
        }

        protected override void OnDetached() { }
    }
}