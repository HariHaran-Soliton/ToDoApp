using ToDoApp.Manager;
using ToDoApp.Repository;
using ToDoApp.Service;
using ToDoApp.UserInteraction;

namespace ToDoApp
{
    public class ToDoApp
    {
        public void RunApp()
        {
            ToDoRepository repository = new ToDoRepository();
            UserRepository userRepository = new UserRepository();
            LoginService loginService = new LoginService(userRepository);
            ToDoService toDoService = new ToDoService(repository);

            LoginInteraction loginInteraction = new LoginInteraction(loginService, toDoService);
            LoginManager loginManager = new LoginManager(loginInteraction);

            ToDoInteraction toDoInteraction = new ToDoInteraction(toDoService);
            ToDoManager toDoManager = new ToDoManager(toDoInteraction);

            bool exitApp = false;
            while (!exitApp)
            {
                string? userName = loginManager.LoginMenu();
                if (userName != null)
                {
                    Console.Clear();
                    Console.WriteLine($"*****Welcome*****");
                    exitApp = toDoManager.ToDoMenu(userName);
                    if (!exitApp)
                    {
                        Console.WriteLine("Returning to Login Menu...!!!");
                    }
                }
                else
                {
                    exitApp = true;
                }
            }
            Console.WriteLine("Exiting the App....");
        }
    }
}
