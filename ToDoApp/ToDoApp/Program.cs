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
            ToDoRepository toDoRepository = new ToDoRepository();
            ToDoInteraction toDoInteraction=new ToDoInteraction(toDoRepository);
            ToDoManager toDoManager=new ToDoManager(toDoInteraction);
            LoginManager loginManager = new LoginManager(loginInteraction,toDoRepository,toDoManager);
            loginManager.LoginMenu();
            Console.ReadKey();
        }
    }
}
