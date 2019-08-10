using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WEBTaskList.Models;

using Microsoft.EntityFrameworkCore;

namespace WEBTaskList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]//Этот атрибут указывает, что контроллер отвечает на запросы веб-API.
    public class TaskListController : ControllerBase
    {
        private readonly TaskListContext _context;

        public TaskListController(TaskListContext context)
        {
            _context = context;

            if (_context.TaskListItems.Count() == 0)
            {
                _context.TaskListItems.Add(new TaskListItem { Name = "Task1" });
                _context.SaveChanges();
            }
        }

        //методя для получения API, который извлекает элементы из списка дел:
        //1. GET method for implemnts GET /api/tasklist
        [HttpGet]   //Атрибут [HttpGet] обозначает метод, который отвечает на запрос HTTP GET.
        public async Task<ActionResult<IEnumerable<TaskListItem>>> GetTaskListItems()
        {
            return await _context.TaskListItems.ToListAsync();
        }

        //2. methos for implements GET /api/tasklist{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskListItem>> GetTaskListItem(long id)
        {
            var taskListItem = await _context.TaskListItems.FindAsync(id);

            if (taskListItem == null)
            {
                return NotFound();
            }

            return taskListItem;
        }

        //3. POST method
        [HttpPost]
        public async Task<ActionResult<TaskListItem>> PostTaskListItem(TaskListItem item)
        {
            _context.TaskListItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskListItems), new { id = item.Id }, item);

            //метод CreatedAtAction:
            //в случае успеха возвращает код состояния HTTP 201
            //добавляет заголовок Location в ответ, этот заголовок указывает URI вновь созданной задачи
            //указывает дейсвтия GetTaskLictItem для создания URI заголовка Location.
            //кл. слвово nameof C# используется для пердовращения жесткого программирования имени дейсвтия в вызове CreatedAtAction
        }

        //4. PUT method
        //Согласно спецификации HTTP, запрос PUT требует, чтобы клиент отправлял всю обновленную сущность, а не только изменения
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskListItem(long id, TaskListItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //5. DELETED method
        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskListItem(long id)
        {
            var taskListItem = await _context.TaskListItems.FindAsync(id);

            if(taskListItem == null)
            {
                return NotFound();
            }

            _context.TaskListItems.Remove(taskListItem);

            await _context.SaveChangesAsync();

            return NoContent(); //DeletetasklistItemОтвет — 204 (нет содержимого).
        }
    }
}
