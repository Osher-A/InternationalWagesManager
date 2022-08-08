using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InternationalWagesManager.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkConditionsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public WorkConditionsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/WorkConditions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkConditions>>> GetWorkConditions()
        {
          if (_context.WorkConditions == null)
          {
              return NotFound();
          }
            return await _context.WorkConditions.ToListAsync();
        }

        // GET: api/WorkConditions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkConditions>> GetWorkConditions(int id)
        {
          if (_context.WorkConditions == null)
          {
              return NotFound();
          }
            var workConditions = await _context.WorkConditions.FindAsync(id);

            if (workConditions == null)
            {
                return NotFound();
            }

            return workConditions;
        }

        // PUT: api/WorkConditions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkConditions(int id, WorkConditions workConditions)
        {
            if (id != workConditions.Id)
            {
                return BadRequest();
            }

            _context.Entry(workConditions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkConditionsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WorkConditions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkConditions>> PostWorkConditions(WorkConditions workConditions)
        {
          if (_context.WorkConditions == null)
          {
              return Problem("Entity set 'MyDbContext.WorkConditions'  is null.");
          }
            _context.WorkConditions.Add(workConditions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkConditions", new { id = workConditions.Id }, workConditions);
        }

        // DELETE: api/WorkConditions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkConditions(int id)
        {
            if (_context.WorkConditions == null)
            {
                return NotFound();
            }
            var workConditions = await _context.WorkConditions.FindAsync(id);
            if (workConditions == null)
            {
                return NotFound();
            }

            _context.WorkConditions.Remove(workConditions);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkConditionsExists(int id)
        {
            return (_context.WorkConditions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
