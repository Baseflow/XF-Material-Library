using Newtonsoft.Json;
using SQLite;

namespace MaterialMvvm.DataContracts.User
{
    [Table("Tbl_User")]
    public class UserDataContract
    {
        [PrimaryKey, AutoIncrement]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
