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

namespace ExamAPI.Controllers.Save_results
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]

    public class Save_resultsController : ControllerBase
    {
        private readonly ExamAPIContext _context;

        public Save_resultsController(ExamAPIContext context)
        {
            _context = context;
        }

        // GET: api/Save_results
        [HttpGet("GET")]
        public async Task<ActionResult<IEnumerable<ExamModels.Save_results>>> GetSave_results()
        {
            return await _context.Save_Results.Include(u => u.User_id).Include(u => u.Name_Test).Include(u => u.Questions).Include(u => u.Exam_id).ToListAsync();
        }

        // GET: api/Save_results/5
        [HttpGet("GETId/{id}")]
        public async Task<ActionResult<ExamModels.Save_results>> GetSave_results(int id)
        {
            var save_results = await _context.Save_Results.Include(u => u.User_id).Include(u => u.Name_Test).Include(u => u.Questions).Include(u => u.Exam_id).FirstOrDefaultAsync(u => u.Id == id);

            if (save_results == null)
            {
                return NotFound();
            }

            return save_results;
        }

        // PUT: api/Save_results/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PUTId/{id}")]
        public async Task<IActionResult> PutSave_results(int id, ExamModels.Save_results save_results)
        {
            if (id != save_results.Id)
            {
                return BadRequest();
            }

            _context.Entry(save_results).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Save_resultsExists(id))
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

        // POST: api/Save_results
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("POST")]
        public async Task<ActionResult<ExamModels.Save_results>> PostSave_results(ExamModels.Save_results save_results)
        {
            _context.Save_Results.Add(save_results);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSave_results", new { id = save_results.Id }, save_results);
        }

        // DELETE: api/Save_results/5
        [HttpDelete("DELETE/{id}")]
        public async Task<IActionResult> DeleteSave_results(int id)
        {
            var save_results = await _context.Save_Results.FindAsync(id);
            if (save_results == null)
            {
                return NotFound();
            }

            _context.Save_Results.Remove(save_results);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Save_resultsExists(int id)
        {
            return _context.Save_Results.Any(e => e.Id == id);
        }
    }
}
