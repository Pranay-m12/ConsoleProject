using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{

    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public class TaskItem
    {
         public int TaskID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public bool CompletionStatus { get; set; }
            public DateTime DueDate { get; set; }
            public Priority Priority { get; set; }

    }

}
