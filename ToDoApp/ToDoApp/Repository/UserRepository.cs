using ToDoApp.Models;

namespace ToDoApp.Repository
{
    public class UserRepository
    {
        public List<User> UserList { get; set; }
        public UserRepository()
        {
            this.UserList = new List<User>();
        }
        public void AddUserToList(User user)
        {
            UserList.Add(user);
        }

        public IEnumerable<User> GetListOfUsers()
        {
            return UserList;
        }
    }
}
