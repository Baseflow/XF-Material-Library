using System.Threading.Tasks;

namespace XF.Material.Forms.Dialogs
{
    /// <summary>
    /// Interface that provides a <see cref="TaskCompletionSource{TResult}"/> property for dialogs awaiting user input.
    /// </summary>
    /// <typeparam name="T">The return type of the <see cref="TaskCompletionSource{TResult}"/></typeparam>
    internal interface IMaterialAwaitableDialog<T>
    {
        /// <summary>
        /// Gets or sets the task completion source.
        /// </summary>
        TaskCompletionSource<T> InputTaskCompletionSource { get; set; }
    }
}
