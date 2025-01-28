using ToDoApp.Repository;
using ToDoApp.UserInteraction;

namespace ToDoApp.Manager
{
    public class LoginManager
    {
        public LoginInteraction _loginInteraction;
        public ToDoRepository ToDoRepository;
        public ToDoManager ToDoManager;
        public LoginManager(LoginInteraction loginInteraction, ToDoRepository toDoRepository, ToDoManager toDoManager)
        {
            _loginInteraction = loginInteraction;
            ToDoRepository = toDoRepository;
            ToDoManager = toDoManager;
        }

        public void LoginMenu()
        {
            bool IsNeedToExit=true;
            while (IsNeedToExit)
            {
                _loginInteraction.PrintLoginMenu();
                int choice=GetUserChoice();
                switch (choice)
                {
                    case 1:
                        _loginInteraction.AddNewUser();
                        break;
                    case 2:
                        int userId=_loginInteraction.ExistingUser();
                        ToDoManager.ToDoMenu(userId);
                        break;
                    case 3:
                        IsNeedToExit = false;
                        break;
                    default:
                        Console.WriteLine("Enter Valid Option");
                        break;

                }
            }
            Console.WriteLine("Exiting the App....");
        }

        public int GetUserChoice()
        {
            int userChoice;
            bool IsValidChoice=int.TryParse(Console.ReadLine(), out userChoice);
            while((userChoice<=0 || userChoice >=4) || !IsValidChoice)
            {
                Console.WriteLine("Enter Valid Option");
                IsValidChoice = int.TryParse(Console.ReadLine(), out userChoice);
            }
            return userChoice;
        }
    }
}
