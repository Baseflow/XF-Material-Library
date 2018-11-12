using Xamarin.Forms;

namespace XF.Material.Forms.Effects
{
    /// <summary>
    /// Base class of Material effect classes.
    /// </summary>
    public abstract class BaseMaterialEffect : RoutingEffect
    {
        protected BaseMaterialEffect(string effectId) : base(effectId) { }
    }
}
