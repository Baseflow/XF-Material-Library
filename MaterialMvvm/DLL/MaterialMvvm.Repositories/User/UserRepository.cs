using MaterialMvvm.DataContracts.User;
using MaterialMvvm.Repositories.Database;

namespace MaterialMvvm.Repositories.User
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IAppDatabase appDatabase) : base(appDatabase) { }

        public void SaveUser(UserDataContract user)
        {
            this.Db.InsertItem(user);
        }
    }
}
