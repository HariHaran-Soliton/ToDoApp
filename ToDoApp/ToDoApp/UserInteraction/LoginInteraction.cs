using ToDoApp.Models;
using ToDoApp.Service;

namespace ToDoApp.UserInteraction
{
    public class LoginInteraction
    {
        private LoginService _loginService;
        private ToDoService _toDoService;

        public LoginInteraction(LoginService loginService, ToDoService toDoService)
        {
            _loginService = loginService;
            _toDoService = toDoService;
        }

        public void PrintLoginMenu()
        {
            Console.WriteLine("\n================");
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
            IsUserId = _loginService.CheckUserIdPresent(userId);
            while (userId==0 || IsUserId)
            {
                Console.WriteLine("Enter Valid Id");
                IsUserId = int.TryParse(Console.ReadLine(), out userId);
                IsUserId = _loginService.CheckUserIdPresent(userId);
            }
            return userId;
        }

        public string GetUserPassWord()
        {
            string? passWord = Console.ReadLine();
            bool isPass = _loginService.CheckPasswordLengthIsGreaterThan6(passWord);
            while (!isPass)
            {
                Console.WriteLine("Enter Valid PassWord");
                passWord = Console.ReadLine();
                isPass = _loginService.CheckPasswordLengthIsGreaterThan6(passWord);
            }
            return passWord;
        }
        public string AddNewUser()
        {
            Console.WriteLine("Enter Your Name");
            string? name=Console.ReadLine();
            Console.WriteLine("Enter new UserId");
            int userId = GetUserId();
            Console.WriteLine("Enter The Password (six characters)");
            string passWord=GetUserPassWord();
            string userName=name+userId;
            _loginService.AddUserToList(new User(userId, name, userName, passWord));
            Console.WriteLine("User Added Successfully!");
            _toDoService.InitializeUser(userName);
            _loginService.saveToJson();
            return userName;
        }
        public int GetExistingUserId()
        {
            int userId;
            bool IsUserId = int.TryParse(Console.ReadLine(), out userId);
            IsUserId = _loginService.CheckUserIdPresent(userId);
            while (!IsUserId)
            {
                Console.WriteLine("User ID not present! Enter again");
                IsUserId = int.TryParse(Console.ReadLine(), out userId);
                IsUserId = _loginService.CheckUserIdPresent(userId);
            }
            return userId;
        }
        public void GetExistingUserPassWord()
        {
            string? passWord = Console.ReadLine();
            bool isPass = _loginService.checkPassWordPresent(passWord);
            while (!isPass)
            {
                Console.WriteLine("Enter Valid PassWord");
                passWord = Console.ReadLine();
                isPass = _loginService.CheckPasswordLengthIsGreaterThan6(passWord);
            }
        }
        public string? ExistingUser()
        {
            Console.WriteLine("Enter UserId");
            int userId=GetExistingUserId();
            Console.WriteLine("Enter The Password");
            GetExistingUserPassWord();
            Console.WriteLine("\n****Login successful****\n");
            string? userName=_loginService.GetUserName(userId);
            return userName;
        }

        public void SaveDataToJson()
        {
            _loginService.saveToJson();
        }
    }
}
