using Newtonsoft.Json;
using ToDoApp.Models;

namespace ToDoApp.Repository
{
    
    public class UserRepository
    {
        string filePath = "users.json";
        public List<User> ReadAlldata()
        {
            if (!File.Exists(filePath))
            {
                return new List<User>();
            }
            var vSettings = new JsonSerializerSettings();
            vSettings.TypeNameHandling = TypeNameHandling.Objects;
            var vJsonStr = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<List<User>>(vJsonStr, vSettings);
        }

        public void WriteData(List<User> userList)
        {

            var vSettings = new JsonSerializerSettings();
            vSettings.TypeNameHandling = TypeNameHandling.Objects;
            var vJsonStr = JsonConvert.SerializeObject(userList, Formatting.Indented, vSettings);
            File.WriteAllText(filePath, vJsonStr);
        }
    }
}
