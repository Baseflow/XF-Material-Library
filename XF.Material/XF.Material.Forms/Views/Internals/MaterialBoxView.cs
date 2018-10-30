using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace XF.Material.Forms.Views.Internals
{
    /// <summary>
    /// Used in <see cref="MaterialSlider"/>
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class MaterialBoxView : BoxView
    {
        internal event EventHandler<TappedEventArgs> Tapped;

        internal MaterialBoxView() { }

        public void OnTapped(double x, double y)
        {
            this.Tapped?.Invoke(this, new TappedEventArgs(x, y));
        }
    }

    internal class TappedEventArgs : EventArgs
    {
        internal double X { get; }

        internal double Y { get; }

        internal TappedEventArgs(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
