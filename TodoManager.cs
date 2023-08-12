using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleProject
{
    public class TodoManeger
    {
        public List<TaskItem> tasks;
        int width = Console.WindowWidth;
        public static string CenterText(string text, int width)
        {
            return text;
        }


        public TodoManeger()
        {
            tasks = new List<TaskItem>();
        }
        public TodoManeger(List<TaskItem> initialTasks)
        {
            tasks = initialTasks ?? new List<TaskItem>();
        }
        public void AddTask()
        {
            int taskID = tasks.Count > 0 ? tasks.Max(t => t.TaskID) + 1 : 1;


             Console.WriteLine(CenterText("Enter Title of TaskID:" + taskID + "->", width));

            string title = Console.ReadLine();
            while (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Title cannot be null or empty. Please try again.");
                Console.WriteLine(CenterText("Enter Title of TaskID:" + taskID + "->", width));
                title = Console.ReadLine();
            }

            Console.WriteLine(CenterText("Enter Description of TaskID:" + taskID + "->", width));
            string description = Console.ReadLine();
            while (string.IsNullOrEmpty(description))
            {
                Console.WriteLine("Description cannot be null or empty. Please try again.");
                Console.WriteLine(CenterText("Enter Description of TaskID:" + taskID + "->", width));
                description = Console.ReadLine();
            }
            while (true)
            {
                Console.WriteLine(CenterText("Enter Due Date(YYYY-MM-DD) of TaskID:" + taskID + "->", width));
                try
                {
                    DateTime duedate = DateTime.Parse(Console.ReadLine());
                    while(duedate <= DateTime.Now)
                    {
                        Console.WriteLine("Date is Not Past Date.");
                        Console.WriteLine(CenterText("Enter Due Date(YYYY-MM-DD) of TaskID:" + taskID + "->", width));
                         duedate = DateTime.Parse(Console.ReadLine());
                    }
                    while (true)
                    {
                        Console.WriteLine(CenterText("Enter priority (Low/Medium/High) of TaskID:" + taskID + "->", width));
                        try
                        {
                            Priority priority = (Priority)Enum.Parse(typeof(Priority), Console.ReadLine(), true);
                            var newTask = new TaskItem
                            {
                                TaskID = taskID,
                                Title = title,
                                Description = description,
                                CompletionStatus = false,
                                DueDate = duedate,
                                Priority = priority
                            };
                            tasks.Add(newTask);
                            Console.WriteLine("Task Added Sucessfully.");
                            break;
                        }
                        catch
                        {
                            Console.WriteLine(CenterText("Invalid Input Of Priority.Try Using Low,Medium And High Only", width));
                        }
                    }
                    break;
                    
                }
                catch (FormatException)
                {
                    Console.WriteLine(CenterText("Invalid Input Of DueDate.Try Using YYYY-MM-DD Format Only", width));
                }
            }
           
            
        }

        public void ViewTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine(CenterText("No tasks found.", width));
                return;
            }

            var sortedTasks = tasks.OrderByDescending(t => t.Priority).ThenBy(t => t.CompletionStatus == false).ThenBy(t => t.DueDate).ToList();

            Console.WriteLine(CenterText("Tasks:", width));
          
            foreach (var task in sortedTasks)
            {
                Console.WriteLine(CenterText($"ID: {task.TaskID}, Title: {task.Title}, Description: {task.Description}, Completion Status: {(task.CompletionStatus ? "Completed" : "Not Completed")}, Due Date: {task.DueDate}, Priority: {task.Priority}", width));
                Console.WriteLine("\n");
            }
        }

        public List<TaskItem> GetTasks()
        {
            return tasks;
        }

        public TaskItem GetTaskById(int taskId)
        {
            return tasks.FirstOrDefault(t => t.TaskID == taskId);
        }

        public void MarkTaskAsCompleted(int taskId)
        {
            var taskToUpdate = GetTaskById(taskId);
            if (taskToUpdate != null)
            {
                if(taskToUpdate.CompletionStatus == true)
                {
                    Console.WriteLine("Task Is Already Completed");
                }
                else
                {
                    taskToUpdate.CompletionStatus = true;
                    Console.WriteLine("Task Is Completed");
                }
            }
            else
            {
                Console.WriteLine(CenterText("Task not found.", width));
            }
        }

        public void UpdateTask(int taskId, string newTitle, string newDescription, bool newCompletionStatus
            
            
            
            )
        {
            var taskToUpdate = GetTaskById(taskId);
            if (taskToUpdate != null)
            {
                taskToUpdate.Title = newTitle;
                taskToUpdate.Description = newDescription;
                taskToUpdate.CompletionStatus = newCompletionStatus;
              
            }
            else
            {
                Console.WriteLine(CenterText("Task not found.", width));
            }
        }

        public void DeleteTask(int taskId)
        {
            var taskToDelete = GetTaskById(taskId);
            if (taskToDelete != null)
            {
                tasks.Remove(taskToDelete);
            }
            else
            {
                Console.WriteLine(CenterText("Task not found", width));
            }
        }
    }

    [Serializable]
    internal class TitleIsNullException : Exception
    {
        public TitleIsNullException()
        {
        }

        public TitleIsNullException(string? message) : base(message)
        {
        }

        public TitleIsNullException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TitleIsNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
