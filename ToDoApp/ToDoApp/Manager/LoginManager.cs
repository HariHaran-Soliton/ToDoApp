using ToDoApp.UserInteraction;

namespace ToDoApp.Manager
{
    public class LoginManager
    {
        public LoginInteraction _loginInteraction;

        public LoginManager(LoginInteraction loginInteraction)
        {
            _loginInteraction = loginInteraction;
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
                        _loginInteraction.ExistingUser();
                        break;
                    case 3:
                        IsNeedToExit = false;
                        break;
                    default:
                        Console.WriteLine("Enter Valid Option");
                        break;

                }
            }
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
