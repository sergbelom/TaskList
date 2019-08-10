using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEBTaskListTelerik.Data
{
    public class TaskViewModel
    {
        [Key]
        public int TaskID
        {
            get;
            set;
        }

        [Required]
        public decimal? Duration
        {
            get;
            set;
        }

        public DateTime? Date
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string TaskName
        {
            get;
            set;
        }
    }
}