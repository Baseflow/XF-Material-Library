using Nito.Mvvm;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MaterialMvvm.Helpers.Commands
{
    /// <summary>
    /// A basic asynchronous command, which (by default) is disabled while the command is executing.
    /// </summary>
    public sealed class AsyncCommand : AsyncCommandBase, INotifyPropertyChanged
    {
        /// <summary>
        /// The implementation of <see cref="IAsyncCommand.ExecuteAsync(object)"/>.
        /// </summary>
        private readonly Func<object, Task> _executeAsync;

        /// <summary>
        /// Creates a new asynchronous command, with the specified asynchronous delegate as its implementation.
        /// </summary>
        /// <param name="executeAsync">The implementation of <see cref="IAsyncCommand.ExecuteAsync(object)"/>.</param>
        /// <param name="canExecuteChangedFactory">The factory for the implementation of <see cref="ICommand.CanExecuteChanged"/>.</param>
        public AsyncCommand(Func<object, Task> executeAsync, Func<object, ICanExecuteChanged> canExecuteChangedFactory)
            : base(canExecuteChangedFactory)
        {
            this._executeAsync = executeAsync;
        }

        /// <summary>
        /// Creates a new asynchronous command, with the specified asynchronous delegate as its implementation.
        /// </summary>
        /// <param name="executeAsync">The implementation of <see cref="IAsyncCommand.ExecuteAsync(object)"/>.</param>
        public AsyncCommand(Func<object, Task> executeAsync)
            : this(executeAsync, CanExecuteChangedFactories.DefaultCanExecuteChangedFactory)
        {
        }

        /// <summary>
        /// Creates a new asynchronous command, with the specified asynchronous delegate as its implementation.
        /// </summary>
        /// <param name="executeAsync">The implementation of <see cref="IAsyncCommand.ExecuteAsync(object)"/>.</param>
        /// <param name="canExecuteChangedFactory">The factory for the implementation of <see cref="ICommand.CanExecuteChanged"/>.</param>
        public AsyncCommand(Func<Task> executeAsync, Func<object, ICanExecuteChanged> canExecuteChangedFactory)
            : this(_ => executeAsync(), canExecuteChangedFactory)
        {
        }

        /// <summary>
        /// Creates a new asynchronous command, with the specified asynchronous delegate as its implementation.
        /// </summary>
        /// <param name="executeAsync">The implementation of <see cref="IAsyncCommand.ExecuteAsync(object)"/>.</param>
        public AsyncCommand(Func<Task> executeAsync)
            : this(_ => executeAsync(), CanExecuteChangedFactories.DefaultCanExecuteChangedFactory)
        {
        }

        /// <summary>
        /// Represents the most recent execution of the asynchronous command. Returns <c>null</c> until the first execution of this command.
        /// </summary>
        public NotifyTask Execution { get; private set; }

        /// <summary>
        /// Whether the asynchronous command is currently executing.
        /// </summary>
        public bool IsExecuting
        {
            get
            {
                if (this.Execution == null)
                    return false;
                return this.Execution.IsNotCompleted;
            }
        }

        /// <summary>
        /// Executes the asynchronous command.
        /// </summary>
        /// <param name="parameter">The parameter for the command.</param>
        public override async Task ExecuteAsync(object parameter)
        {
            var tcs = new TaskCompletionSource<object>();
            this.Execution = NotifyTask.Create(DoExecuteAsync(tcs.Task, this._executeAsync, parameter));
            OnCanExecuteChanged();
            var propertyChanged = PropertyChanged;
            propertyChanged?.Invoke(this, PropertyChangedEventArgsCache.Instance.Get("Execution"));
            propertyChanged?.Invoke(this, PropertyChangedEventArgsCache.Instance.Get("IsExecuting"));
            tcs.SetResult(null);
            await this.Execution.TaskCompleted;
            OnCanExecuteChanged();
            PropertyChanged?.Invoke(this, PropertyChangedEventArgsCache.Instance.Get("IsExecuting"));
            await this.Execution.Task;
        }

        /// <summary>
        /// Raised when any properties on this instance have changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The implementation of <see cref="ICommand.CanExecute(object)"/>. Returns <c>false</c> whenever the async command is in progress.
        /// </summary>
        /// <param name="parameter">The parameter for the command.</param>
        protected override bool CanExecute(object parameter) => !this.IsExecuting;

        private static async Task DoExecuteAsync(Task precondition, Func<object, Task> executeAsync, object parameter)
        {
            await precondition;
            await executeAsync(parameter);
        }
    }
}
