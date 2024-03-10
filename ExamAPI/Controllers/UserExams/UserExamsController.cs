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

namespace ExamAPI.Controllers.UserExams
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserExamsController : ControllerBase
    {
        private readonly ExamAPIContext _context;

        public UserExamsController(ExamAPIContext context)
        {
            _context = context;
        }

        // GET: api/UserExams
        [HttpGet("GET")]
        public async Task<ActionResult<IEnumerable<ExamModels.UserExams>>> GetUserExams()
        {
            return await _context.UserExams.Include(u => u.User).Include(u => u.Exams).ToListAsync();
        }

        // GET: api/UserExams/5
        [HttpGet("GETId/{id}")]
        public async Task<ActionResult<ExamModels.UserExams>> GetUserExams(int id)
        {
            var userExams = await _context.UserExams.Include(u => u.User).Include(u => u.Exams).FirstOrDefaultAsync(u => u.Id == id);

            if (userExams == null)
            {
                return NotFound();
            }

            return userExams;
        }

        // PUT: api/UserExams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PUTId/{id}")]
        public async Task<IActionResult> PutUserExams(int id, ExamModels.UserExams userExams)
        {
            if (id != userExams.Id)
            {
                return BadRequest();
            }

            _context.Entry(userExams).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExamsExists(id))
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

        // POST: api/UserExams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("POST")]
        public async Task<ActionResult<ExamModels.UserExams>> PostUserExams(ExamModels.UserExams userExams)
        {
            _context.UserExams.Add(userExams);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserExams", new { id = userExams.Id }, userExams);
        }

        // DELETE: api/UserExams/5
        [HttpDelete("DELETE/{id}")]
        public async Task<IActionResult> DeleteUserExams(int id)
        {
            var userExams = await _context.UserExams.FindAsync(id);
            if (userExams == null)
            {
                return NotFound();
            }

            _context.UserExams.Remove(userExams);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExamsExists(int id)
        {
            return _context.UserExams.Any(e => e.Id == id);
        }
    }
}
