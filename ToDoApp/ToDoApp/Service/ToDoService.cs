using ToDoApp.Models;
using ToDoApp.Repository;

namespace ToDoApp.Service
{
    public class ToDoService
    {
        private ToDoRepository _toDORepository;
        public Dictionary<string,List<ToDo>> ToDoOfUser { get; set; }

        public ToDoService(ToDoRepository toDORepository)
        {
            _toDORepository = toDORepository;
            ToDoOfUser = new Dictionary<string, List<ToDo>>();
            ToDoOfUser=_toDORepository.ReadAlldata();
        }

        public void InitializeUser(string userName)
        {
            if (!ToDoOfUser.ContainsKey(userName))
            {
                ToDoOfUser[userName] = new List<ToDo>();
                saveToJson();
            }
        }
        public void AddToListOfToDo(string userName,ToDo toDo)
        {
            ToDoOfUser[userName].Add(toDo);
            saveToJson();
        }

        public void DeleteFromToDO(string userName,ToDo toDo)
        {
            ToDoOfUser[userName].Remove(toDo);
            saveToJson();
        }
        public IEnumerable<ToDo> GetAll(string userName)
        {
            if (ToDoOfUser[userName].Count==0)
            {
                return null;
            }
            return ToDoOfUser[userName];
        }
        public void saveToJson()
        {
            _toDORepository.WriteData(ToDoOfUser);
        }
    }
}
