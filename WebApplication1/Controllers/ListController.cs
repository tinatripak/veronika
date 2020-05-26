using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {

        private readonly TodoContext _context;

        public ListController(TodoContext context)
        {
            _context = context;
        }
        // GET: api/list
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }
        //GET: api/list/5
       [HttpGet("{id}")]
            public async Task<ActionResult<Book>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }
            return todoItem;
        }
        // POST: api/list
        [HttpPost]
        public async Task<ActionResult<Book>> PostTodoItem(Book item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }
        // DELETE: api/list/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
