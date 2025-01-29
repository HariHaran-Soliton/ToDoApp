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

        public string? LoginMenu()
        {
            string? userName = null;
            bool IsNeedToExit = true;
            while (IsNeedToExit)
            {
                _loginInteraction.PrintLoginMenu();
                int choice = GetUserChoice();
                switch (choice)
                {
                    case 1:
                        userName = _loginInteraction.AddNewUser();
                        return userName;
                    case 2:
                        userName = _loginInteraction.ExistingUser();
                        return userName;
                    case 3:
                        IsNeedToExit = false;
                        _loginInteraction.SaveDataToJson();
                        break;
                    default:
                        Console.WriteLine("Enter Valid Option");
                        break;
                }
            }

            return userName;
        }

        public int GetUserChoice()
        {
            int userChoice;
            bool IsValidChoice = int.TryParse(Console.ReadLine(), out userChoice);
            while ((userChoice <= 0 || userChoice >= 4) || !IsValidChoice)
            {
                Console.WriteLine("Enter Valid Option");
                IsValidChoice = int.TryParse(Console.ReadLine(), out userChoice);
            }
            return userChoice;
        }
    }
}
