using SQLite;

namespace MaterialMvvm.Repositories.Database
{
    public interface IPlatformDatabaseConnection
    {
        /// <summary>
        /// Creates and returns an SQLite Database Connection from the respective native platforms.
        /// </summary>
        /// <param name="databaseName">The name of the Database file.</param>
        SQLiteConnection CreateDatabaseConnection(string databaseName);
    }
}
