using MaterialMvvm.Repositories.Database;
using System;

namespace MaterialMvvm.Repositories
{
    public class BaseRepository
    {
        protected IAppDatabase Db { get; }

        public BaseRepository(IAppDatabase appDatabase)
        {
            this.Db = appDatabase ?? throw new ArgumentNullException();
        }
    }
}
