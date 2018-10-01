using MaterialMvvm.DataContracts.User;
using System.Threading.Tasks;

namespace MaterialMvvm.WebServices.User
{
    public interface IUserWebService
    {
        Task LoginUser(UserDataContract user);
    }
}
