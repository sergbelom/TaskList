using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace WEBTaskList.Models
{
    public class TaskListContext : DbContext
    {

        public DbSet<TaskListItem> TaskListItems { get; set; }

        public TaskListContext(DbContextOptions<TaskListContext> options)
            : base(options)
        {

        }
    }
}
