using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamAPI.Data;
using ExamModels;
using Microsoft.AspNetCore.Cors;

namespace ExamAPI.Controllers.Filles
{
    [Route("api/[controller]")]
    [ApiController]

    public class FillesController : ControllerBase
    {
        private readonly ExamAPIContext _context;

        public FillesController(ExamAPIContext context)
        {
            _context = context;
        }

        // GET: api/Filles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamModels.Filles>>> GetFilles()
        {
            return await _context.Filles.ToListAsync();
        }

        // GET: api/Filles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamModels.Filles>> GetFilles(int id)
        {
            var filles = await _context.Filles.FindAsync(id);

            if (filles == null)
            {
                return NotFound();
            }

            return filles;
        }

        // PUT: api/Filles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilles(int id, ExamModels.Filles filles)
        {
            if (id != filles.Id)
            {
                return BadRequest();
            }

            _context.Entry(filles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FillesExists(id))
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

        // POST: api/Filles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExamModels.Filles>> PostFilles(ExamModels.Filles filles)
        {
            _context.Filles.Add(filles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilles", new { id = filles.Id }, filles);
        }

        // DELETE: api/Filles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilles(int id)
        {
            var filles = await _context.Filles.FindAsync(id);
            if (filles == null)
            {
                return NotFound();
            }

            _context.Filles.Remove(filles);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FillesExists(int id)
        {
            return _context.Filles.Any(e => e.Id == id);
        }
    }
}
