using ToDoApp.Models;

namespace ToDoApp.Repository
{
    public class ToDoRepository
    {
        public List<ToDo> ToDoOfUser { get; set; }

        public ToDoRepository()
        {
            this.ToDoOfUser = new List<ToDo>();
        }

        public void AddToListOfToDo(ToDo toDo)
        {
            ToDoOfUser.Add(toDo);
        }

        public IEnumerable<ToDo> GetAll()
        {
            if(ToDoOfUser.Count == 0)
            {
                return null;
            }
            return ToDoOfUser;
        }
    }
}
