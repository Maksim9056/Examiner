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

namespace ExamAPI.Controllers.Test
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ExamAPIContext _context;

        public TestsController(ExamAPIContext context)
        {
            _context = context;
        }

        // GET: api/Tests
        [HttpGet("GET")]
        public async Task<ActionResult<IEnumerable<ExamModels.Test>>> GetTest()
        {
            return await _context.Test.ToListAsync();
        }

        // GET: api/Tests/5
        [HttpGet("GETId/{id}")]
        public async Task<ActionResult<ExamModels.Test>> GetTest(int id)
        {
            var test = await _context.Test.FindAsync(id);

            if (test == null)
            {
                return NotFound();
            }

            return test;
        }

        // PUT: api/Tests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PUTId/{id}")]
        public async Task<IActionResult> PutTest(int id, ExamModels.Test test)
        {
            if (id != test.Id)
            {
                return BadRequest();
            }

            _context.Entry(test).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestExists(id))
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

        // POST: api/Tests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("POST")]
        public async Task<ActionResult<ExamModels.Test>> PostTest(ExamModels.Test test)
        {
            _context.Test.Add(test);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTest", new { id = test.Id }, test);
        }

        // DELETE: api/Tests/5
        [HttpDelete("DELETE/{id}")]
        public async Task<IActionResult> DeleteTest(int id)
        {
            var test = await _context.Test.FindAsync(id);
            if (test == null)
            {
                return NotFound();
            }

            _context.Test.Remove(test);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TestExists(int id)
        {
            return _context.Test.Any(e => e.Id == id);
        }
    }
}
