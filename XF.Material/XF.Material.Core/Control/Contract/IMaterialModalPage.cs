using System;
using System.Threading.Tasks;

namespace XF.Material.Core.Control.Contract
{
    public interface IMaterialModalPage : IDisposable
    {
        /// <summary>
        /// Dismisses this modal dialog asynchronously.
        /// </summary>
        Task DismissAsync();
    }
}
