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
            Console.WriteLine("\n    TODO APP    ");
            Console.WriteLine("================");
            Console.WriteLine("1. Add");
            Console.WriteLine("2.View");
            Console.WriteLine("3.Delete");
            Console.WriteLine("4.Edit");
            Console.WriteLine("5.Exit");
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
            string title = ValidateStringinput();
            Console.WriteLine("Enter description:");
            string description = ValidateStringinput();
            Console.WriteLine("Enter ToDo Date");
            DateTime date = GetValidToDoDate();
            Console.WriteLine("Would You Like to Add recurrence Of ToDo");
            Console.WriteLine("Y/N");
            TaskRecurrence taskRecurrence = AddRecurrenceToInput();
            ToDoRepository.AddToListOfToDo(new ToDo(userId, title, description, date, taskRecurrence));
        }

        public TaskRecurrence GetOneRecurrence()
        {
            Console.WriteLine("1.Daily");
            Console.WriteLine("2.Weekly");
            Console.WriteLine("3.Monthy");
            Console.WriteLine("4.Yearly");
            Console.WriteLine("Enter the option:");
            int choice = GetUserChoice();
            if (choice == 1)
            {
                return TaskRecurrence.Daily;
            }
            else if (choice == 2)
            {
                return TaskRecurrence.Weekly;
            }
            else if (choice == 3)
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
            IEnumerable<ToDo> toDos = ToDoRepository.GetAll();
            if (toDos != null)
            {
                foreach (var toDo in toDos)
                {
                    if (toDo.UserId == userID)
                    {
                        Console.WriteLine($"{toDo.ToDoHeading}| {toDo.ToDoDescription} | {toDo.TargetDate.ToString("dd/MM/yyyy")} | {toDo.ToDoRecurrence} |");
                    }
                }
            }
            else
            {
                Console.WriteLine("No To-Do Added\n");
            }
        }

        public void DeleteToDo(int userID)
        {
            Console.WriteLine("Enter Date");
            DateTime date = GetValidToDoDate();
            for (int i = 0; i < ToDoRepository.ToDoOfUser.Count; i++)
            {
                var toDo = ToDoRepository.ToDoOfUser[i];
                if (toDo.UserId == userID)
                {
                    if (toDo.TargetDate == date)
                    {
                        Console.WriteLine($"{toDo.ToDoHeading}| {toDo.ToDoDescription} | {toDo.TargetDate.ToString("dd/MM/yyyy")} | {toDo.ToDoRecurrence} |");
                        Console.WriteLine("----------------");
                        Console.WriteLine("1.Delete ToDo");
                        Console.WriteLine("2.Move to Next");
                        int option = int.Parse(Console.ReadLine());
                        if ( option== 1)
                        {
                            ToDoRepository.ToDoOfUser.Remove(toDo);
                            Console.WriteLine("Deleted successfully\n");
                            i--;
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Moving to next");
                        }
                    }
                }
            }
            Console.WriteLine("No ToDo found deleted!\n");
        }

        public void EditToDo(int userID)
        {
            Console.WriteLine("Enter Date");
            DateTime date = GetValidToDoDate();
            for (int i = 0; i < ToDoRepository.ToDoOfUser.Count; i++)
            {
                var toDo = ToDoRepository.ToDoOfUser[i];
                if (toDo.UserId == userID)
                {
                    if (toDo.TargetDate == date)
                    {
                        Console.WriteLine($"{toDo.ToDoHeading}| {toDo.ToDoDescription} | {toDo.TargetDate.ToString("dd/MM/yyyy")} | {toDo.ToDoRecurrence} |");
                        Console.WriteLine("----------------");
                        Console.WriteLine("1.Edit ToDo");
                        Console.WriteLine("2.Move To next");
                        int option = int.Parse(Console.ReadLine());
                        if ( option== 1)
                        {
                            EditAToDo(toDo);
                            Console.WriteLine("Edited successfully\n");
                            i--;
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Moving to next ToDo");
                        }
                    }
                }
            }
            Console.WriteLine("No ToDo found edited!..\n");
        }

        public void EditAToDo(ToDo toDo)
        {
            Console.WriteLine("What to Edit");
            Console.WriteLine("1.Title");
            Console.WriteLine("2.Description");
            Console.WriteLine("3.Target Date");
            Console.WriteLine("4.Task Recurrence");
            Console.WriteLine("Enter the option");
            int choice =GetUserChoice();
            switch(choice)
            {
                case 1:
                    Console.WriteLine("Enter new Title");
                    string newTitle = ValidateStringinput();
                    toDo.ToDoHeading= newTitle;
                    break;
                case 2:
                    Console.WriteLine("Enter New descripition");
                    string newDescription=ValidateStringinput();
                    toDo.ToDoDescription= newDescription;
                    break;
                case 3:
                    Console.WriteLine("Enter New Date");
                    DateTime newDate=GetValidToDoDate();
                    toDo.TargetDate= newDate;
                    break;
                case 4:
                    Console.WriteLine("Enter new Recurrence");
                    Console.WriteLine("Y/N");
                    TaskRecurrence newTaskRecurrence = AddRecurrenceToInput();
                    toDo.ToDoRecurrence= newTaskRecurrence;
                    break;
                default:
                    Console.WriteLine("Not Valid Option");
                    break;
            }

        }
        
    }
}
