using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBTaskList.Models
{
    public class TaskListItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
