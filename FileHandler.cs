using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleProject
{
    public class FileHandler
    {
        int width = Console.WindowWidth;
        private const string jsonurl = "C:\\Users\\PranayMhaiske\\source\\repos\\ConsoleProject\\Todo.json";
        public static string CenterText(string text, int width)
        {
            return text;
        }
        
        public void SaveTasks(List<TaskItem> tasks)
        {
            string jsonData = JsonSerializer.Serialize(tasks);
            File.WriteAllText(jsonurl, jsonData);
            Console.WriteLine(CenterText("Tasks Updated to file successfully.", width));
        }
        public List<TaskItem> LoadTasksFromFile()
        {
           

            string jsonData = File.ReadAllText(jsonurl);
            return JsonSerializer.Deserialize<List<TaskItem>>(jsonData);
        }
    }
}
