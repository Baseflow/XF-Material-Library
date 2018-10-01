using System;
using System.Collections.Generic;

namespace MaterialMvvm.Repositories.Database
{
    public class AppDatabase : IAppDatabase
    {
        #region Fields

        private const string DB_NAME = "App.db";
        private readonly IPlatformDatabaseConnection _dbConnection;

        #endregion

        #region Constructor

        public AppDatabase(IPlatformDatabaseConnection dbConnection)
        {
            this._dbConnection = dbConnection ?? throw new ArgumentNullException();

            using (var db = this._dbConnection.CreateDatabaseConnection(DB_NAME))
            {
                //Create your database tables here
                //db.CreateTable<UserDataContract>();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes all item in a table from the database.
        /// </summary>
        /// <typeparam name="T">The type of the items to be deleted.</typeparam>
        public void DeleteAllItems<T>() where T : class, new()
        {
            using (var db = this._dbConnection.CreateDatabaseConnection(DB_NAME))
            {
                db.DeleteAll<T>();
            }
        }

        /// <summary>
        /// Deletes an item in a table from the database.
        /// </summary>
        /// <typeparam name="T">The type of the item to be deleted.</typeparam>
        /// <param name="id">The primary key of the item to be deleted.</param>
        public void DeleteItem<T>(int id) where T : class, new()
        {
            using (var db = this._dbConnection.CreateDatabaseConnection(DB_NAME))
            {
                db.Delete<T>(id);
            }
        }

        /// <summary>
        /// Inserts a list of items of the same type in a table from the database.
        /// </summary>
        /// <typeparam name="T">The type of the items to be inserted.</typeparam>
        /// <param name="items">The list of items to be inserted.</param>
        public void InsertAllItems<T>(IEnumerable<T> items)
        {
            using (var db = this._dbConnection.CreateDatabaseConnection(DB_NAME))
            {
                db.InsertAll(items, typeof(T));
            }
        }

        /// <summary>
        /// Inserts an item in a table from the database.
        /// </summary>
        /// <param name="item">The item to be inserted.</param>
        public void InsertItem(object item)
        {
            using (var db = this._dbConnection.CreateDatabaseConnection(DB_NAME))
            {
                db.InsertOrReplace(item, item.GetType());
            }
        }

        /// <summary>
        /// Retrieves all items in a table from the database.
        /// </summary>
        /// <typeparam name="T">The type of the items to be retrieved.</typeparam>
        public IEnumerable<T> SelectAllItems<T>() where T : class, new()
        {
            using (var db = this._dbConnection.CreateDatabaseConnection(DB_NAME))
            {
                return db.Table<T>();
            }
        }

        /// <summary>
        /// Retrieves the first item in a table from the database.
        /// </summary>
        /// <typeparam name="T">The type of the item to be retrieved.</typeparam>
        public T SelectFirstItem<T>() where T : class, new()
        {
            using (var db = this._dbConnection.CreateDatabaseConnection(DB_NAME))
            {
                return db.Table<T>().FirstOrDefault();
            }
        }

        /// <summary>
        /// Retrieves an item in a table from the database using a primary key.
        /// </summary>
        /// <typeparam name="T">The type of the item to be retrieved.</typeparam>
        /// <param name="id">The primary key of the item.</param>
        public T SelectItem<T>(int id) where T : class, new()
        {
            using (var db = this._dbConnection.CreateDatabaseConnection(DB_NAME))
            {
                return db.Find<T>(id);
            }
        }

        /// <summary>
        /// Updates an item in a table from the database. The item should have a primary key.
        /// </summary>
        /// <param name="item">The item to be updated.</param>
        public void UpdateItem(object item)
        {
            using (var db = this._dbConnection.CreateDatabaseConnection(DB_NAME))
            {
                db.Update(item, item.GetType());
            }
        }

        /// <summary>
        /// Updates a specified list of items in a table from the database. Each item should have a primary key.
        /// </summary>
        /// <typeparam name="T">The type of the items to be updated.</typeparam>
        /// <param name="items">The items to be updated.</param>
        public void UpdateItems<T>(IEnumerable<T> items)
        {
            using (var db = this._dbConnection.CreateDatabaseConnection(DB_NAME))
            {
                db.UpdateAll(items);
            }
        }

        #endregion
    }
}