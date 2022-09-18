namespace XF.Material.Maui.Models
{
    /// <summary>
    /// Contains data of a <see cref="UI.MaterialMenuButton"/> selection.
    /// </summary>
    public class MaterialMenuResult
    {
        /// <summary>
        /// Gets the index of the selected menu item.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Gets the parameter passed during the selection.
        /// </summary>
        public object Parameter { get; }

        internal MaterialMenuResult(int index, object parameter)
        {
            Index = index;
            Parameter = parameter;
        }
    }
}
