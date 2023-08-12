using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ConsoleProject
{
    internal class Program
    {
        public static string CenterText(string text, int width)
        {
            return text;
        }
        static void Main(string[] args)
        {
            TodoManeger todoManager = new TodoManeger();
            FileHandler fileHandler = new FileHandler();
            List<TaskItem> tasks = fileHandler.LoadTasksFromFile();
            int width = Console.WindowWidth;
            todoManager = new TodoManeger();

            string appTitle = @"
___________              __    
\__    ___/____    _____|  | __
  |    |  \__  \  /  ___/  |/ /
  |    |   / __ \_\___ \|    < 
  |____|  (____  /____  >__|_ \
               \/     \/     \/
                          
";
            string[] titleLines = appTitle.Split("\n");
            foreach (string line in titleLines)
            {
                Console.WriteLine(CenterText(line, width));
            }
            int flag = 0;
            bool isRunning = true;
            while (isRunning)
            {
                
               
                Console.WriteLine("\n\n");
                string r;
                Console.ForegroundColor = ConsoleColor.Green;
              
                Console.WriteLine(CenterText("Main Menu",width));
               
                Console.WriteLine(CenterText("a. Add a task", width));
                Console.WriteLine(CenterText("b. View all tasks", width));
                Console.WriteLine(CenterText("c. View a specific task", width));
                Console.WriteLine(CenterText("d. Mark a task as completed", width));
                Console.WriteLine(CenterText("e. Update a task", width));
                Console.WriteLine(CenterText("f. Delete a task", width));
                Console.WriteLine(CenterText("g. Save tasks to a file", width));
                Console.WriteLine(CenterText("h. Load tasks from a file", width));
                Console.WriteLine(CenterText("i. Exit", width));

                Console.Write(CenterText("Enter your choice: ", width));
                Console.ResetColor();
                string choice = Console.ReadLine().ToLower();

                switch (choice)
                {
                    case "a":
                      
                        todoManager.AddTask();
                        flag = 1;
                        Console.WriteLine(CenterText("Do you want to update  File:(Yes/No)", width));
                        
                        r = Console.ReadLine().ToLower();
                        while (r != "yes" || r != "no")
                        {
                            Console.WriteLine("Enter Again");
                            r = Console.ReadLine().ToLower();
                            if (r == "yes")
                            {
                                goto case "g";
                                break;
                            }
                            else if (r == "no")
                            {
                                Console.WriteLine("Not Updated File");
                                break;
                            }
                        }
                        break;
                    case "b":
                        todoManager.ViewTasks();
                       
                        break;
                    case "c":
                        Console.Write("Enter the task ID: ");
                        try
                        {
                            
                            int taskId=int.Parse(Console.ReadLine());
                                var task = todoManager.GetTaskById(taskId);
                                if (task != null)
                                {
                                if (task.CompletionStatus)
                                {
                                    Console.WriteLine($"ID: {task.TaskID}, Title: {task.Title}, Description: {task.Description}, Completion Status: {(task.CompletionStatus ? "Completed" : "Not Completed")}");
                                }
                                else
                                {
                                    Console.WriteLine(CenterText($"ID: {task.TaskID}, Title: {task.Title}, Description: {task.Description}, Completion Status: {(task.CompletionStatus ? "Completed" : "Not Completed")}, Due Date: {task.DueDate}, Priority: {task.Priority}", width));

                                }
                            }
                                else
                                {
                                    Console.WriteLine(CenterText("Task not found.", width));
                                }
                            
                        }
                        catch(FormatException)
                        {
                            Console.WriteLine(CenterText("Invalid input. Please enter a valid task ID.", width));
                        }
                        break;
                    case "d":
                        Console.WriteLine(CenterText("Enter the task ID to mark as completed: ", width));
                        flag = 1;
                        try
                        {
                            
                                int completeTaskId = int.Parse(Console.ReadLine());
                                todoManager.MarkTaskAsCompleted(completeTaskId);
                            
                        }
                        catch(FormatException)
                        {
                            Console.WriteLine(CenterText("Invalid input. Please enter a valid task ID.", width));
                        }
                        Console.WriteLine(CenterText("Do you want to update  File:(Yes/No)", width));

                        r =null;
                        while (r != "yes" || r != "no")
                        {
                            Console.WriteLine("Enter :");
                            r = Console.ReadLine().ToLower();
                            if (r == "yes")
                            {
                                goto case "g";
                                break;
                            }
                            else if (r == "no")
                            {
                                Console.WriteLine("Not Updated File");
                                break;
                            }
                        }
                        break;
                    case "e":
                        Console.WriteLine(CenterText("Enter the task ID to update: ", width));
                        flag = 1;
                        DateTime newdate;
                        try
                        {
                            
                                int updateTaskId = int.Parse(Console.ReadLine());
                                var taskToUpdate = todoManager.GetTaskById(updateTaskId);
                                if (taskToUpdate != null)
                                {
                                Console.WriteLine(CenterText("Enter new title: ", width));
                                string newTitle = Console.ReadLine();
                                Console.WriteLine(CenterText("Enter new description: ", width));
                                string newDescription = Console.ReadLine();
                               

                                    Console.WriteLine(CenterText("Is the task completed? (true/false): ", width));
                                try
                                    {
                                    
                                        bool newCompletionStatus = bool.Parse(Console.ReadLine());
                                        todoManager.UpdateTask(updateTaskId, newTitle, newDescription, newCompletionStatus);
                                    Console.WriteLine("Task Updated Sucessfully.");

                                }
                                    catch(FormatException)
                                    {
                                        Console.WriteLine(CenterText("Invalid input for completion status. Please enter true or false.", width));
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(CenterText("Task not found.", width));
                                }
                            
                        }
                        catch(FormatException)
                        {
                            Console.WriteLine(CenterText("Invalid input. Please enter a valid task ID.", width));
                        }
                        Console.WriteLine(CenterText("Do you want to update  File:(Yes/No)", width));

                        r = null;
                        while (r != "yes" || r != "no")
                        {
                            Console.WriteLine("Enter:");
                            r = Console.ReadLine().ToLower();
                            if (r == "yes")
                            {
                                goto case "g";
                                break;
                            }
                            else if (r == "no")
                            {
                                Console.WriteLine("Not Updated File");
                                break;
                            }
                        }

                        break;
                    case "f":
                        Console.WriteLine(CenterText("Enter the task ID to delete: ", width));
                         flag = 1;
                        if (int.TryParse(Console.ReadLine(), out int deleteTaskId))
                        {
                            todoManager.DeleteTask(deleteTaskId);
                            Console.WriteLine("Task Deleted Sucessfully.");
                        }
                        else
                        {
                            Console.WriteLine(CenterText("Invalid input. Please enter a valid task ID.", width));
                        }
                        Console.WriteLine(CenterText("Do you want to update  File:(Yes/No)", width));

                        r = null;
                        while (r != "yes" || r != "no")
                        {
                            Console.WriteLine("Enter:");
                            r = Console.ReadLine().ToLower();
                            if (r == "yes")
                            {
                                goto case "g";
                                break;
                            }
                            else if (r == "no")
                            {
                                Console.WriteLine("Not Updated File");
                                break;
                            }
                        }
                        break;
                    case "g":
                        fileHandler.SaveTasks(todoManager.GetTasks());
                        break;
                    case "h":
                        tasks = fileHandler.LoadTasksFromFile();
                        todoManager = new TodoManeger(tasks);
                        Console.WriteLine(CenterText("Tasks loaded from file successfully.", width));
                        break;
                    case "i":
                        if (flag == 1)
                        {
                            Console.WriteLine(CenterText("If Want to Save Task to File:(Yes/No):", width));
                            r = Console.ReadLine();
                            if (r == "Yes")
                            {
                                goto case "g";
                            }
                        }
                        isRunning = false ;
                        Console.WriteLine("Bye Bye ");
                        break;
                    default:
                        Console.WriteLine(CenterText("Enter Valid Option", width));
                        break;
                }
                
            }
        }
    }
}