using InternationalWagesManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryComponentsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public SalaryComponentsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/SalaryComponents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaryComponents>>> GetSalariesComponents()
        {
            if (_context.SalariesComponents == null)
            {
                return NotFound();
            }
            return await _context.SalariesComponents.ToListAsync();
        }

        // GET: api/SalaryComponents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalaryComponents>> GetSalaryComponents(int id)
        {
            if (_context.SalariesComponents == null)
            {
                return NotFound();
            }
            var salaryComponents = await _context.SalariesComponents.FindAsync(id);

            if (salaryComponents == null)
            {
                return NotFound();
            }

            return salaryComponents;
        }

        // PUT: api/SalaryComponents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalaryComponents(int id, SalaryComponents salaryComponents)
        {
            if (id != salaryComponents.Id)
            {
                return BadRequest();
            }

            _context.Entry(salaryComponents).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaryComponentsExists(id))
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

        // POST: api/SalaryComponents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SalaryComponents>> PostSalaryComponents(SalaryComponents salaryComponents)
        {
            if (_context.SalariesComponents == null)
            {
                return Problem("Entity set 'MyDbContext.SalariesComponents'  is null.");
            }
            _context.SalariesComponents.Add(salaryComponents);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalaryComponents", new { id = salaryComponents.Id }, salaryComponents);
        }

        // DELETE: api/SalaryComponents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalaryComponents(int id)
        {
            if (_context.SalariesComponents == null)
            {
                return NotFound();
            }
            var salaryComponents = await _context.SalariesComponents.FindAsync(id);
            if (salaryComponents == null)
            {
                return NotFound();
            }

            _context.SalariesComponents.Remove(salaryComponents);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalaryComponentsExists(int id)
        {
            return (_context.SalariesComponents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
