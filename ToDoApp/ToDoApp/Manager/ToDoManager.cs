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

        public void ToDoMenu(int userID)
        {
            bool IsNeedToExit = true;
            while (IsNeedToExit)
            {
                ToDoInteraction.PrintToDoMenu();
                int choice = GetUserChoice();

                switch (choice)
                {
                    case 1:
                        ToDoInteraction.AddNewToDo(userID);
                        break;
                    case 2:
                        ToDoInteraction.PrintAllToDo(userID);
                        break;
                    case 3:
                        ToDoInteraction.DeleteToDo(userID);
                        break;
                    case 4:
                        ToDoInteraction.EditToDo(userID);
                        break;
                    case 5:
                        IsNeedToExit = false;
                        break;
                    default:
                        Console.WriteLine("Enter valid choice");
                        break;
                }
            }
        }
        public int GetUserChoice()
        {
            int userChoice;
            bool IsValidChoice = int.TryParse(Console.ReadLine(), out userChoice);
            while ((userChoice <= 0 || userChoice >= 6) || !IsValidChoice)
            {
                Console.WriteLine("Enter Valid Option");
                IsValidChoice = int.TryParse(Console.ReadLine(), out userChoice);
            }
            return userChoice;
        }
    }
}
