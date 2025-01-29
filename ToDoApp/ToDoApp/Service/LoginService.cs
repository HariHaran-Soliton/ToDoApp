using ToDoApp.Models;
using ToDoApp.Repository;

namespace ToDoApp.Service
{
    public class LoginService
    {
        private UserRepository _userRepository;
        public List<User> UserList { get; set; }
        public LoginService(UserRepository userRepository)
        {
            _userRepository = userRepository;
            this.UserList = new List<User>();
            UserList =userRepository.ReadAlldata();
        }
        public void AddUserToList(User user)
        {
            UserList.Add(user);
        }

        public IEnumerable<User> GetListOfUsers()
        {
            return UserList;
        }

        public bool PassWordValidator(string PassWord)
        {
            if (PassWord.Length >= 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkUserIdPresent(int id)
        {
            IEnumerable<User> usersInList =GetListOfUsers();
            foreach (var user in usersInList)
            {
                if (user.UserId == id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool checkPassWordPresent(string passWord)
        {
            IEnumerable<User> usersInList =GetListOfUsers();
            foreach (var user in usersInList)
            {
                if (user.Password == passWord)
                {
                    return true;
                }
            }
            return false;
        }

        public string? GetUserPassWord(int userId)
        {
            string? userName=null;
            foreach(var user in UserList)
            {
                if(user.UserId == userId)
                {
                    userName= user.UserName;
                }

            }
            return userName;
        }
        public void saveToJson()
        {
            _userRepository.WriteData(UserList);
        }
    }
}
