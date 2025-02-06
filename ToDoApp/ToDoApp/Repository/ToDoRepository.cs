using Newtonsoft.Json;
using ToDoApp.Models;

namespace ToDoApp.Repository
{
    public class ToDoRepository
    {
        string filePath = "ToDoS.json";
        public Dictionary<string, List<ToDo>> ReadAlldata()
        {
            if (!File.Exists(filePath))
            {
                return new Dictionary<string, List<ToDo>>();
            }
            var vSettings = new JsonSerializerSettings();
            vSettings.TypeNameHandling = TypeNameHandling.Objects;
            var vJsonStr = File.ReadAllText(filePath);
            Dictionary<string, List<ToDo>> toDoDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<ToDo>>>(vJsonStr);
           return toDoDictionary;
        }

        public void WriteData(Dictionary<string, List<ToDo>> ToDoList)
        {
            var vSettings = new JsonSerializerSettings();
            vSettings.TypeNameHandling = TypeNameHandling.Objects;
            var vJsonStr = JsonConvert.SerializeObject(ToDoList, Formatting.Indented, vSettings);
            File.WriteAllText(filePath, vJsonStr);
        }
    }
}
