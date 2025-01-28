using ToDoApp.Manager;
using ToDoApp.Repository;
using ToDoApp.UserInteraction;

namespace ToDoApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserRepository userRepository = new UserRepository();
            LoginInteraction loginInteraction = new LoginInteraction(userRepository);
            LoginManager loginManager = new LoginManager(loginInteraction);
            loginManager.LoginMenu();
            Console.ReadKey();
        }
    }
}
