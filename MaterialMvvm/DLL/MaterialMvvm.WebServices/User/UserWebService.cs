using MaterialMvvm.Common;
using MaterialMvvm.DataContracts.User;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MaterialMvvm.WebServices.User
{
    public class UserWebService : BaseWebService, IUserWebService
    {
        public async Task LoginUser(UserDataContract user)
        {
            string json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await this.PostAsync(WebServiceEndpoints.LOGIN, content).ConfigureAwait(false);
        }
    }
}
