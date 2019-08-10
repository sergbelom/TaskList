using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEBTaskListTelerik.Data;
using Kendo.Mvc.Extensions;

namespace WEBTaskListTelerik.Pages
{
    public class IndexModel : PageModel
    {
        public static IList<TaskViewModel> tasks;

        public void OnGet()
        {
            if (tasks == null)
            {
                tasks = new List<TaskViewModel>();

                Enumerable.Range(0, 50).ToList().ForEach(i => tasks.Add(new TaskViewModel
                {
                    TaskID = i + 1,
                    Duration = i * 10,
                    Date = new DateTime(2016, 9, 15).AddDays(i % 7),
                    TaskName = "TaskName " + i,
                    Description = "Description " + i
                }));

            }
        }

        public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
        {
            return new JsonResult(tasks.ToDataSourceResult(request));
        }

        public JsonResult OnPostCreate([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            task.TaskID = tasks.Count + 2;
            tasks.Add(task);

            return new JsonResult(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostUpdate([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            tasks.Where(x => x.TaskID == task.TaskID).Select(x => task);

            return new JsonResult(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostDestroy([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            tasks.Remove(tasks.First(x => x.TaskID == task.TaskID));

            return new JsonResult(new[] { task }.ToDataSourceResult(request, ModelState));
        }
    }
}
