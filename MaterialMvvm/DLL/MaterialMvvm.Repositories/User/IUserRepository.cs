using MaterialMvvm.DataContracts.User;

namespace MaterialMvvm.Repositories.User
{
    public interface IUserRepository
    {
        void SaveUser(UserDataContract user);
    }
}
