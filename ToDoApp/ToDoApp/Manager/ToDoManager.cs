using ToDoApp.UserInteraction;

namespace ToDoApp.Manager
{
    public class ToDoManager
    {
        public ToDoInteraction ToDoInteraction;

        public ToDoManager(ToDoInteraction toDoInteraction)
        {
            ToDoInteraction = toDoInteraction;
        }

        public bool ToDoMenu(string userName)
        {
            bool IsNeedToExit = true;
            while (IsNeedToExit)
            {
                ToDoInteraction.PrintDashBoard(userName);
                ToDoInteraction.PrintToDoMenu();
                int choice = GetUserChoice();

                switch (choice)
                {
                    case 1:
                        ToDoInteraction.AddNewToDo(userName);
                        break;
                    case 2:
                        ToDoInteraction.PrintAllToDo(userName);
                        break;
                    case 3:
                        ToDoInteraction.DeleteToDo(userName);
                        break;
                    case 4:
                        ToDoInteraction.EditToDo(userName);
                        break;
                    case 5:
                        ToDoInteraction.DisplayCalendar(userName);
                        break;
                    case 6:
                        IsNeedToExit = false;
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Enter valid choice");
                        break;
                }
            }
            return IsNeedToExit;
        }
        public int GetUserChoice()
        {
            int userChoice;
            bool IsValidChoice = int.TryParse(Console.ReadLine(), out userChoice);
            while ((userChoice <= 0 || userChoice >= 7) || !IsValidChoice)
            {
                Console.WriteLine("Enter Valid Option");
                IsValidChoice = int.TryParse(Console.ReadLine(), out userChoice);
            }
            return userChoice;
        }
    }
}
