using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Icatu.EmployeeManageAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Icatu.EmployeeManageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeeController(EmployeeContext context)
        {
            _context = context;

            if (_context.Employees.Count() == 0)
            {
               _context.Employees.Add(new EmployeeItem { Nome = "Item1", Email = "teste", Departamento = "RH" });
               _context.SaveChanges();
            }
        }

        [HttpGet("{currentPage}/{pageSize}")]
        public async Task<List<EmployeeItem>> GetTodoItems(int currentPage, int pageSize)
        {
            var data = await _context.Employees.ToListAsync();
            return data.OrderBy(d => d.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.Employees.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeItem>> PostTodoItem(EmployeeItem item)
        {
            _context.Employees.Add(item);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetTodoItem), new { id = item.ID }, item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.Employees.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, EmployeeItem item)
        { 
            if (id != item.ID)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}  


