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

namespace ExamAPI.Controllers.Options
{
    [Route("api/[controller]")]
    [ApiController]

    public class OptionsController : ControllerBase
    {
        private readonly ExamAPIContext _context;

        public OptionsController(ExamAPIContext context)
        {
            _context = context;
        }

        // GET: api/Options
        [HttpGet("GET")]
        public async Task<ActionResult<IEnumerable<ExamModels.Options>>> GetOptions()
        {
            return await _context.Options.ToListAsync();
        }

        // GET: api/Options/5
        [HttpGet("GETId/{id}")]
        public async Task<ActionResult<ExamModels.Options>> GetOptions(int id)
        {
            var options = await _context.Options.FindAsync(id);

            if (options == null)
            {
                return NotFound();
            }

            return options;
        }

        // PUT: api/Options/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PUTId/{id}")]
        public async Task<IActionResult> PutOptions(int id, ExamModels.Options options)
        {
            if (id != options.Id)
            {
                return BadRequest();
            }

            _context.Entry(options).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OptionsExists(id))
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

        // POST: api/Options
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("POST")]
        public async Task<ActionResult<ExamModels.Options>> PostOptions(ExamModels.Options options)
        {
            _context.Options.Add(options);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOptions", new { id = options.Id }, options);
        }

        // DELETE: api/Options/5
        [HttpDelete("DELETE/{id}")]
        public async Task<IActionResult> DeleteOptions(int id)
        {
            var options = await _context.Options.FindAsync(id);
            if (options == null)
            {
                return NotFound();
            }

            _context.Options.Remove(options);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OptionsExists(int id)
        {
            return _context.Options.Any(e => e.Id == id);
        }
    }
}
