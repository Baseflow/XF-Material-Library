using CommonServiceLocator;
using MaterialMvvm.Common.Runtime;
using MaterialMvvm.Helpers;
using MaterialMvvm.Utilities.Navigation;

namespace MaterialMvvm.ViewModels
{
    /// <summary>
    /// Base class of view models.
    /// </summary>
    public abstract class BaseViewModel : PropertyChangeAware, ICleanUp
    {
        /// <summary>
        /// The current navigation context.
        /// </summary>
        protected INavigationService Navigation { get; }

        protected BaseViewModel()
        {
            this.Navigation = ServiceLocator.Current.GetInstance<INavigationService>();
        }

        /// <summary>
        /// Method for view model initialization.
        /// </summary>
        /// <param name="parameter">The parameter, if any, that will be passed during this view model's initialization.</param>
        public virtual void Init(object parameter = null) { }

        /// <summary>
        /// Method to clean up objects in this view model.
        /// </summary>
        public virtual void CleanUp() { }
    }
}
