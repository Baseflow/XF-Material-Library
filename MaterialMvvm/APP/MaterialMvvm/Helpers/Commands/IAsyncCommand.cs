using System.Threading.Tasks;
using System.Windows.Input;

namespace MaterialMvvm.Helpers.Commands
{
    /// <summary>
    /// An async version of <see cref="ICommand"/>.
    /// </summary>
    public interface IAsyncCommand : ICommand
    {
        /// <summary>
        /// Executes the asynchronous command.
        /// </summary>
        /// <param name="parameter">The parameter for the command.</param>
        Task ExecuteAsync(object parameter);
    }
}
