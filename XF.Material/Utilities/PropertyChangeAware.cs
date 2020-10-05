using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XF.Material.Forms.Utilities
{
    /// <summary>
    /// An abstract class that implements the <see cref="INotifyPropertyChanged"/> interface.
    ///</summary>
    internal abstract class PropertyChangeAware : INotifyPropertyChanged
    {
        /// <inheritdoc />
        /// <summary>
        /// Raised when any properties on this instance have changed by using the <see cref="M:XF.Material.Forms.Utilities.PropertyChangeAware.Set``1(``0@,``0,System.String)" /> method.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method to change a property's value.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="field">The field containing the current property's value.</param>
        /// <param name="newValue">The new value to be assigned.</param>
        /// <param name="propertyName">The name of the property.</param>
        protected void Set<T>(ref T field, T newValue = default(T), [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, default(T)))
            {
                if (field.Equals(newValue))
                {
                    return;
                }

                field = newValue;
                OnPropertyChanged(propertyName);
            }
            else if (!EqualityComparer<T>.Default.Equals(newValue, default(T)))
            {
                field = newValue;
                OnPropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// Method called to raise the <see cref="INotifyPropertyChanged.PropertyChanged" /> event if there was a change in any property.
        /// </summary>
        /// <param name="propertyName">The name of the property who's value has changed.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
