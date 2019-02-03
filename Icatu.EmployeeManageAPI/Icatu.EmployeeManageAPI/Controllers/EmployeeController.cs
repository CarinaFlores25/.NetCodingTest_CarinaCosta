using System;
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
                _context.Employees.Add(new EmployeeItem { Nome = "FuncIcatu", Email = "teste@icatu.com.br", Departamento = "RH" });
                _context.SaveChanges();
            }
        }

        [HttpGet("{currentPage}/{pageSize}")]
        public async Task<List<EmployeeItem>> GetTodosFunc(int currentPage, int pageSize)
        {
            var data = await _context.Employees.ToListAsync();
            return data.OrderBy(d => d.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeItem>> GetFunc(long id)
        {
            var funcId = await _context.Employees.FindAsync(id);

            if (funcId == null)
            {
                return NotFound();
            }

            return funcId;
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeItem>> PostTodoItem(EmployeeItem func)
        {
            _context.Employees.Add(func);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFunc), new { id = func.ID }, func);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFunc(long id)
        {
            var funcId = await _context.Employees.FindAsync(id);

            if (funcId == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(funcId);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExist(long id)
        {
            return _context.Employees.Count(e => e.ID == id) > 0;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFunc(long id, EmployeeItem func)
        {
            if (id != func.ID)
            {
                return BadRequest();
            }

            _context.Entry(func).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExist(func.ID))
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

    }
}


