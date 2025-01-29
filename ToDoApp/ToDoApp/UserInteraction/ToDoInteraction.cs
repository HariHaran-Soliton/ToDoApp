using ToDoApp.Models;
using ToDoApp.Service;

namespace ToDoApp.UserInteraction
{
    public class ToDoInteraction
    {
        private ToDoService _toDoService;

        public ToDoInteraction(ToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        public void PrintToDoMenu()
        {
            Console.WriteLine("\n    TODO APP    ");
            Console.WriteLine("================");
            Console.WriteLine("1.Add");
            Console.WriteLine("2.View");
            Console.WriteLine("3.Delete");
            Console.WriteLine("4.Edit");
            Console.WriteLine("5.Calender");
            Console.WriteLine("6.Exit");
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
        public string GetValidString()
        {
            string ValidInput = Console.ReadLine();
            while (ValidInput == null || ValidInput == "")
            {
                Console.WriteLine("Enter Valid input");
                ValidInput = Console.ReadLine();
            }

            return ValidInput;
        }

        public TaskRecurrence AddRecurrenceToNewToDo()
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
        public void AddNewToDo(string userName)
        {
            Console.WriteLine("In Add Section\n");
            Console.WriteLine("Enter New ToDo");
            Console.WriteLine("Enter Title:");
            string title = GetValidString();
            Console.WriteLine("Enter description:");
            string description = GetValidString();
            Console.WriteLine("Enter ToDo Date");
            DateTime date = GetValidToDoDate();
            Console.WriteLine("Would You Like to Add recurrence Of ToDo");
            Console.WriteLine("Y/N");
            TaskRecurrence taskRecurrence = AddRecurrenceToNewToDo();
            _toDoService.AddToListOfToDo(userName, new ToDo(title, description, date, taskRecurrence));
            Console.WriteLine("New To Do Added Successfully!");
            _toDoService.saveToJson();
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

        public void PrintAllToDo(string userName)
        {
            Console.WriteLine("In View Section");
            IEnumerable<ToDo> toDos = _toDoService.GetAll(userName);
            if (toDos != null)
            {
                foreach (var toDo in toDos)
                {

                    Console.WriteLine($"Tile: {toDo.ToDoHeading} | Description: {toDo.ToDoDescription} | Date: {toDo.TargetDate.ToString("dd/MM/yyyy")} | Recurrence: {toDo.ToDoRecurrence}");
                }
            }
            else
            {
                Console.WriteLine("No To-Do Added\n");
            }
        }

        public void DeleteToDo(string userName)
        {
            Console.WriteLine("In Delete Section\n");
            Console.WriteLine("Enter Date");
            DateTime date = GetValidToDoDate();
            foreach (ToDo toDo in _toDoService.ToDoOfUser[userName])
            {
                if (toDo.TargetDate == date)
                {
                    Console.WriteLine($"{toDo.ToDoHeading}| {toDo.ToDoDescription} | {toDo.TargetDate.ToString("dd/MM/yyyy")} | {toDo.ToDoRecurrence} |");
                    Console.WriteLine("----------------");
                    Console.WriteLine("1.Delete ToDo");
                    Console.WriteLine("2.Move to Next");
                    int option = GetOptionOneOrTwo();
                    if (option == 1)
                    {
                        _toDoService.DeleteFromToDO(userName, toDo);
                        Console.WriteLine("Deleted successfully\n");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Moving to next");
                    }
                }
            }
            Console.WriteLine("No ToDo found to delete!\n");
            _toDoService.saveToJson();
        }
        public void EditToDo(string userName)
        {
            Console.WriteLine("In Edit Section\n");
            Console.WriteLine("Enter Date");
            DateTime date = GetValidToDoDate();
            foreach (ToDo toDo in _toDoService.ToDoOfUser[userName])
            {
                if (toDo.TargetDate == date)
                {
                    Console.WriteLine($"{toDo.ToDoHeading}| {toDo.ToDoDescription} | {toDo.TargetDate.ToString("dd/MM/yyyy")} | {toDo.ToDoRecurrence} |");
                    Console.WriteLine("----------------");
                    Console.WriteLine("1.Edit ToDo");
                    Console.WriteLine("2.Move to Next");
                    int option = GetOptionOneOrTwo();
                    if (option == 1)
                    {
                        EditAToDo(toDo);
                        Console.WriteLine("Edited successfully\n");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Moving to next");
                    }
                }
            }
            Console.WriteLine("No ToDo found to Edit!\n");
            _toDoService.saveToJson();
        }

        public void EditAToDo(ToDo toDo)
        {
            Console.WriteLine("What to Edit");
            Console.WriteLine("1.Title");
            Console.WriteLine("2.Description");
            Console.WriteLine("3.Target Date");
            Console.WriteLine("4.Task Recurrence");
            Console.WriteLine("Enter the option");
            int choice = GetUserChoice();
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter new Title");
                    string newTitle = GetValidString();
                    toDo.ToDoHeading = newTitle;
                    break;
                case 2:
                    Console.WriteLine("Enter New description");
                    string newDescription = GetValidString();
                    toDo.ToDoDescription = newDescription;
                    break;
                case 3:
                    Console.WriteLine("Enter New Date");
                    DateTime newDate = GetValidToDoDate();
                    toDo.TargetDate = newDate;
                    break;
                case 4:
                    Console.WriteLine("Enter new Recurrence");
                    Console.WriteLine("Y/N");
                    TaskRecurrence newTaskRecurrence = AddRecurrenceToNewToDo();
                    toDo.ToDoRecurrence = newTaskRecurrence;
                    break;
                default:
                    Console.WriteLine("Not Valid Option");
                    break;
            }

        }
        public int GetOptionOneOrTwo()
        {
            bool IsValidOption = int.TryParse(Console.ReadLine(), out int option);
            while (!IsValidOption)
            {
                Console.WriteLine("Enter 1 or 2");
                IsValidOption = int.TryParse(Console.ReadLine(), out option);
            }
            return option;
        }

        public void PrintDashBoard(string userName)
        {
            var toDo = _toDoService.ToDoOfUser[userName];
            var dashboardList = toDo.OrderBy(x => x.TargetDate).Take(2);

            if (dashboardList.Any())
            {
                Console.WriteLine("\n================");
                Console.WriteLine("    DASHBOARD   ");
                Console.WriteLine("  UpComing Tasks");
                foreach (var item in dashboardList)
                {
                    Console.WriteLine($"Tile: {item.ToDoHeading} | Description: {item.ToDoDescription} | Date: {item.TargetDate.ToString("dd/MM/yyyy")} | Recurrence: {item.ToDoRecurrence}");
                }
            }
            else
            {
                Console.WriteLine("\n================");
                Console.WriteLine("    DASHBOARD   ");
                Console.WriteLine("No upcoming tasks.");
            }
        }

        public void DisplayCalendar(string userName)
        {
            Console.WriteLine("1. Daily");
            Console.WriteLine("2. Weekly");
            Console.WriteLine("3. Monthly");
            Console.WriteLine("4. Yearly");
            Console.WriteLine("Enter the duration:");
            string duration = Console.ReadLine();
            var toDoList = _toDoService.ToDoOfUser[userName];
            IEnumerable<ToDo> filteredToDos = Enumerable.Empty<ToDo>();

            switch (duration)
            {
                case "1":
                    filteredToDos = GetTasksByRecurrence(toDoList, TaskRecurrence.Daily);
                    break;
                case "2":
                    filteredToDos = GetTasksByRecurrence(toDoList, TaskRecurrence.Weekly);
                    break;
                case "3":
                    filteredToDos = GetTasksByRecurrence(toDoList, TaskRecurrence.Monthly);
                    break;
                case "4":
                    filteredToDos = GetTasksByRecurrence(toDoList, TaskRecurrence.Yearly);
                    break;
                default:
                    Console.WriteLine("Invalid duration. Please choose '1', '2', '3', or '4'.");
                    return;
            }

            if (filteredToDos.Any())
            {
                Console.WriteLine("\n================");
                Console.WriteLine("    CALENDAR    ");
                foreach (var toDo in filteredToDos.OrderBy(toDo => toDo.TargetDate))
                {
                    Console.WriteLine($"Title: {toDo.ToDoHeading} | Description: {toDo.ToDoDescription}");
                }
            }
            else
            {
                Console.WriteLine($"No tasks found for the selected duration.");
            }
        }

        private IEnumerable<ToDo> GetTasksByRecurrence(IEnumerable<ToDo> toDoList, TaskRecurrence recurrence)
        {
            DateTime today = DateTime.Today;
            var tasks = new List<ToDo>();

            foreach (var toDo in toDoList)
            {
                if (toDo.ToDoRecurrence == recurrence)
                {
                    switch (recurrence)
                    {
                        case TaskRecurrence.Daily:
                            tasks.Add(new ToDo(toDo.ToDoHeading, toDo.ToDoDescription, today, toDo.ToDoRecurrence));
                            break;
                        case TaskRecurrence.Weekly:
                            tasks.Add(new ToDo(toDo.ToDoHeading, toDo.ToDoDescription, today.AddDays(7), toDo.ToDoRecurrence));
                            break;
                        case TaskRecurrence.Monthly:
                            tasks.Add(new ToDo(toDo.ToDoHeading, toDo.ToDoDescription, today.AddMonths(1), toDo.ToDoRecurrence));
                            break;
                        case TaskRecurrence.Yearly:
                            tasks.Add(new ToDo(toDo.ToDoHeading, toDo.ToDoDescription, today.AddYears(1), toDo.ToDoRecurrence));
                            break;
                    }
                }
            }

            return tasks;
        }
    }
}
