using System.Collections.Generic;

namespace MaterialMvvm.Repositories.Database
{
    public interface IAppDatabase
    {
        /// <summary>
        /// Deletes all item in a table from the database.
        /// </summary>
        /// <typeparam name="T">The type of the items to be deleted.</typeparam>
        void DeleteAllItems<T>() where T : class, new();

        /// <summary>
        /// Deletes an item in a table from the database.
        /// </summary>
        /// <typeparam name="T">The type of the item to be deleted.</typeparam>
        /// <param name="id">The primary key of the item to be deleted.</param>
        void DeleteItem<T>(int id) where T : class, new();

        /// <summary>
        /// Inserts a list of items of the same type in a table from the database.
        /// </summary>
        /// <typeparam name="T">The type of the items to be inserted.</typeparam>
        /// <param name="items">The list of items to be inserted.</param>
        void InsertAllItems<T>(IEnumerable<T> items);

        /// <summary>
        /// Inserts an item in a table from the database.
        /// </summary>
        /// <param name="item">The item to be inserted.</param>
        void InsertItem(object item);

        /// <summary>
        /// Retrieves all items in a table from the database.
        /// </summary>
        /// <typeparam name="T">The type of the items to be retrieved.</typeparam>
        IEnumerable<T> SelectAllItems<T>() where T : class, new();

        /// <summary>
        /// Retrieves the first item in a table from the database.
        /// </summary>
        /// <typeparam name="T">The type of the item to be retrieved.</typeparam>
        T SelectFirstItem<T>() where T : class, new();

        /// <summary>
        /// Retrieves an item in a table from the database using a primary key.
        /// </summary>
        /// <typeparam name="T">The type of the item to be retrieved.</typeparam>
        /// <param name="id">The primary key of the item.</param>
        T SelectItem<T>(int id) where T : class, new();

        /// <summary>
        /// Updates an item in a table from the database. The item should have a primary key.
        /// </summary>
        /// <param name="item">The item to be updated.</param>
        void UpdateItem(object item);

        /// <summary>
        /// Updates a specified list of items in a table from the database. Each item should have a primary key.
        /// </summary>
        /// <typeparam name="T">The type of the items to be updated.</typeparam>
        /// <param name="items">The items to be updated.</param>
        void UpdateItems<T>(IEnumerable<T> items);
    }
}
