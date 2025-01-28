using ToDoApp.Models;
using ToDoApp.Repository;

namespace ToDoApp.UserInteraction
{
    public class LoginInteraction
    {
        private readonly UserRepository _userRepository;

        public LoginInteraction(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void PrintLoginMenu()
        {
            Console.WriteLine("================");
            Console.WriteLine("    ToDo App");
            Console.WriteLine("================");
            Console.WriteLine("1.New User");
            Console.WriteLine("2.Existing User");
            Console.WriteLine("3.Exit");
            Console.WriteLine("Enter the Option");
        }

        public int GetUserId()
        {
            int userId;
            bool IsUserId = int.TryParse(Console.ReadLine(), out userId);
            IsUserId = checkUserIdPresent(userId);
            while (IsUserId)
            {
                Console.WriteLine("Enter Unique Id");
                IsUserId = int.TryParse(Console.ReadLine(), out userId);
                IsUserId = checkUserIdPresent(userId);
            }
            return userId;
        }

        public string GetUserPassWord()
        {
            string? passWord = Console.ReadLine();
            bool isPass = PassWordValidator(passWord);
            while (!isPass)
            {
                Console.WriteLine("Enter Valid PassWord");
                passWord = Console.ReadLine();
                isPass = PassWordValidator(passWord);
            }
            return passWord;
        }
        public void AddNewUser()
        {
            Console.WriteLine("Enter Your Name");
            string? name=Console.ReadLine();
            Console.WriteLine("Enter new UserId");
            int userId = GetUserId();
            Console.WriteLine("Enter The Password (six characters)");
            string passWord=GetUserPassWord();
            string userName=name+userId;
            _userRepository.AddUserToList(new User(userId, name, userName, passWord));
            Console.WriteLine("User Added Successfully!");
        }

        public bool PassWordValidator(string PassWord)
        {
           if(PassWord.Length >=6)
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
            IEnumerable<User> usersInList = _userRepository.GetListOfUsers();
            foreach (var user in usersInList)
            {
                if(user.UserId == id)
                {
                    return true;
                }
            }
            return false;
        }
        public bool checkPassWordPresent(string passWord)
        {
            IEnumerable<User> usersInList = _userRepository.GetListOfUsers();
            foreach (var user in usersInList)
            {
                if (user.Password == passWord)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetExistingUserId()
        {
            int userId;
            bool IsUserId = int.TryParse(Console.ReadLine(), out userId);
            IsUserId = checkUserIdPresent(userId);
            while (!IsUserId)
            {
                Console.WriteLine("User ID not present! Enter again");
                IsUserId = int.TryParse(Console.ReadLine(), out userId);
                IsUserId = checkUserIdPresent(userId);
            }
            return userId;
        }

        public string GetExistingUserPassWord()
        {
            string? passWord = Console.ReadLine();
            bool isPass = checkPassWordPresent(passWord);
            while (!isPass)
            {
                Console.WriteLine("Enter Valid PassWord");
                passWord = Console.ReadLine();
                isPass = PassWordValidator(passWord);
            }
            return passWord;
        }
        public int ExistingUser()
        {
            Console.WriteLine("Enter UserId");
            int userId=GetExistingUserId();
            Console.WriteLine("Enter The Password");
            string passWord =GetExistingUserPassWord();
            Console.WriteLine("Login successful");
            return userId;
        }
    }
}
