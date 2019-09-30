using System;
using Xamarin.Forms;

namespace XF.Material.Forms.UI.Internals
{
    /// <inheritdoc />
    /// <summary>
    /// Used in MaterialSlider as a a tap container.
    /// </summary>
    public class MaterialBoxView : BoxView
    {
        internal event EventHandler<TappedEventArgs> Tapped;

        internal MaterialBoxView() { }

        public void OnTapped(double x, double y)
        {
            Tapped?.Invoke(this, new TappedEventArgs(x, y));
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// Used in <see cref="E:XF.Material.Forms.UI.Internals.MaterialBoxView.Tapped" /> as an event argument.
    /// </summary>
    internal class TappedEventArgs : EventArgs
    {
        internal double X { get; }

        internal double Y { get; }

        internal TappedEventArgs(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
