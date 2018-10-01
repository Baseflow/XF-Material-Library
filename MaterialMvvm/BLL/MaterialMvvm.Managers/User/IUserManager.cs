using MaterialMvvm.Entities.User;
using System.Threading.Tasks;

namespace MaterialMvvm.Managers.User
{
    public interface IUserManager
    {
        Task<string> LoginUser(UserEntity user);
    }
}
