using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamAPI.Data;
using ExamModels;
using ExamModels;

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamModels.User_roles>>> GetUser_roles()
        {
            return await _context.User_roles.ToListAsync();
        }

        // GET: api/User_roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamModels.User_roles>> GetUser_roles(int id)
        {
            var user_roles = await _context.User_roles.FindAsync(id);

            if (user_roles == null)
            {
                return NotFound();
            }

            return user_roles;
        }

        // PUT: api/User_roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
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
        [HttpPost]
        public async Task<ActionResult<ExamModels.User_roles>> PostUser_roles(ExamModels.User_roles user_roles)
        {
            _context.User_roles.Add(user_roles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser_roles", new { id = user_roles.Id }, user_roles);
        }

        // DELETE: api/User_roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser_roles(int id)
        {
            var user_roles = await _context.User_roles.FindAsync(id);
            if (user_roles == null)
            {
                return NotFound();
            }

            _context.User_roles.Remove(user_roles);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool User_rolesExists(int id)
        {
            return _context.User_roles.Any(e => e.Id == id);
        }
    }
}
