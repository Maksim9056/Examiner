using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamAPI.Data;
using ExamModels;

namespace ExamAPI.Controllers.Exams
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly ExamAPIContext _context;

        public ExamsController(ExamAPIContext context)
        {
            _context = context;
        }

        // GET: api/Exams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamModels.Exams>>> GetExams()
        {
            return await _context.Exams.ToListAsync();
        }

        // GET: api/Exams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamModels.Exams>> GetExams(int id)
        {
            var exams = await _context.Exams.FindAsync(id);

            if (exams == null)
            {
                return NotFound();
            }

            return exams;
        }

        // PUT: api/Exams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExams(int id, ExamModels.Exams exams)
        {
            if (id != exams.Id)
            {
                return BadRequest();
            }

            _context.Entry(exams).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamsExists(id))
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

        // POST: api/Exams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExamModels.Exams>> PostExams(ExamModels.Exams exams)
        {
            _context.Exams.Add(exams);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExams", new { id = exams.Id }, exams);
        }

        // DELETE: api/Exams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExams(int id)
        {
            var exams = await _context.Exams.FindAsync(id);
            if (exams == null)
            {
                return NotFound();
            }

            _context.Exams.Remove(exams);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExamsExists(int id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }
    }
}
