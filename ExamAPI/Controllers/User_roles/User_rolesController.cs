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

namespace ExamAPI.Controllers.User_roles
{
    [Route("api/[controller]")]
    [ApiController]

    public class User_rolesController : ControllerBase
    {
        private readonly ExamAPIContext _context;

        public User_rolesController(ExamAPIContext context)
        {
            _context = context;
        }

        // GET: api/User_roles
        [HttpGet("GET")]
        public async Task<ActionResult<IEnumerable<ExamModels.User_roles>>> GetUser_roles()
        {
            return await _context.User_Roles.Include(u => u.User_id.Email).Include(u => u.Id_roles).ToListAsync();
        }

        // GET: api/User_roles/5
        [HttpGet("GETId/{id}")]
        public async Task<ActionResult<ExamModels.User_roles>> GetUser_roles(int id)
        {
            var user_roles = await _context.User_Roles.Include(u => u.User_id.Email).Include(u => u.Id_roles).FirstOrDefaultAsync(u => u.Id == id);

            if (user_roles == null)
            {
                return NotFound();
            }

            return user_roles;
        }

        // PUT: api/User_roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PUTId/{id}")]
        public async Task<IActionResult> PutUser_roles(int id, ExamModels.User_roles user_roles)
        {
            if (id != user_roles.Id)
            {
                return BadRequest();
            }

            _context.Entry(user_roles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!User_rolesExists(id))
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

        // POST: api/User_roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("POST")]
        public async Task<ActionResult<ExamModels.User_roles>> PostUser_roles(ExamModels.User_roles user_roles)
        {
            _context.User_Roles.Add(user_roles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser_roles", new { id = user_roles.Id }, user_roles);
        }

        // DELETE: api/User_roles/5
        [HttpDelete("DELETE/{id}")]
        public async Task<IActionResult> DeleteUser_roles(int id)
        {
            var user_roles = await _context.User_Roles.FindAsync(id);
            if (user_roles == null)
            {
                return NotFound();
            }

            _context.User_Roles.Remove(user_roles);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool User_rolesExists(int id)
        {
            return _context.User_Roles.Any(e => e.Id == id);
        }
    }
}
