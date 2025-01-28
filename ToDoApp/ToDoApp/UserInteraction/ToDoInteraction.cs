using ToDoApp.Models;
using ToDoApp.Repository;

namespace ToDoApp.UserInteraction
{
    public class ToDoInteraction
    {
        public ToDoRepository ToDoRepository { get; set; }

        public ToDoInteraction(ToDoRepository toDoRepository)
        {
            ToDoRepository = toDoRepository;
        }

        public void PrintToDoMenu()
        {
            Console.WriteLine("1. Add");
            Console.WriteLine("2.View");
            Console.WriteLine("3.Exit");
            Console.WriteLine("Enter Option:");
        }

        public DateTime GetValidToDoDate()
        {
            DateTime date;
            bool IsDate = DateTime.TryParse(Console.ReadLine(), out date);
            while (date < DateTime.Now || !IsDate)
            {
                Console.WriteLine("Enter Valid Date!");
                IsDate = DateTime.TryParse(Console.ReadLine(), out date);
            }
            return date;
        }
        public string ValidateStringinput()
        {
            string ValidInput = Console.ReadLine();
            while (ValidInput == null || ValidInput == "")
            {
                Console.WriteLine("Enter Valid input");
                ValidInput = Console.ReadLine();
            }

            return ValidInput;
        }

        public TaskRecurrence AddRecurrenceToInput()
        {
            string enterToRecurrence = Console.ReadLine();
            while ((enterToRecurrence == "Y" && enterToRecurrence == "N") || enterToRecurrence == null || enterToRecurrence == "")
            {
                Console.WriteLine("Enter Y/N");
                enterToRecurrence = Console.ReadLine();
            }
            TaskRecurrence taskRecurrence = TaskRecurrence.None;
            switch (enterToRecurrence)
            {
                case "Y":
                case "y":
                    taskRecurrence = GetOneRecurrence();
                    break;
                case "N":
                case "n":
                    break;
            }

            return taskRecurrence;
        }
        public void AddNewToDo(int userId)
        {
            Console.WriteLine("Enter New ToDo");
            Console.WriteLine("Enter Title:");
            string title=ValidateStringinput();
            Console.WriteLine("Enter description:");
            string description =ValidateStringinput();
            Console.WriteLine("Enter ToDo Date");
            DateTime date=GetValidToDoDate();
            Console.WriteLine("Would You Like to Add recurrence Of ToDo");
            Console.WriteLine("Y/N");
            TaskRecurrence taskRecurrence=AddRecurrenceToInput();
            ToDoRepository.AddToListOfToDo(new ToDo(userId,title,description,date,taskRecurrence));
        }

        public TaskRecurrence GetOneRecurrence()
        {
            Console.WriteLine("1.Daily");
            Console.WriteLine("2.Weekly");
            Console.WriteLine("3.Monthy");
            Console.WriteLine("4.Yearly");
            Console.WriteLine("Enter the option:");
            int choice=GetUserChoice();
            if(choice == 1)
            {
                return TaskRecurrence.Daily;
            }
            else if(choice == 2)
            {
                return TaskRecurrence.Weekly;
            }
            else if(choice == 3)
            {
                return TaskRecurrence.Monthly;
            }
            else if (choice == 4)
            {
                return TaskRecurrence.Yearly;
            }
            else
            {
                return TaskRecurrence.None;
            }
        }
        public int GetUserChoice()
        {
            int userChoice;
            bool IsValidChoice = int.TryParse(Console.ReadLine(), out userChoice);
            while ((userChoice <= 0 || userChoice >= 5) || !IsValidChoice)
            {
                Console.WriteLine("Enter Valid Option");
                IsValidChoice = int.TryParse(Console.ReadLine(), out userChoice);
            }
            return userChoice;
        }

        public void PrintAllToDo(int userID)
        {
            IEnumerable<ToDo> toDos= ToDoRepository.GetAll();
            if (toDos != null)
            {
                foreach (var toDo in toDos)
                {
                    Console.WriteLine($"{toDo.ToDoHeading}| {toDo.ToDoDescription} | {toDo.TargetDate.ToString("dd/MM/yyyy")} | {toDo.ToDoRecurrence} |");
                }
            }
            else
            {
                Console.WriteLine("No To-Do Added");
            }
        }
    }
}
