﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamAPI.Data;
using ExamModels;
using Microsoft.AspNetCore.Cors;

namespace ExamAPI.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly ExamAPIContext _context;

        public UsersController(ExamAPIContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet("GET")]
        public async Task<ActionResult<IEnumerable<ExamModels.User>>> GetUser()
        {
            return await _context.Users.Include(u => u.Email).ToListAsync();
            //return await _context.Users.ToListAsync();
        }

        // GET: api/Users/email,password
        [HttpGet("GETCheckPassword/{email,password}")]
        public async Task<ActionResult<ExamModels.User>> GetUserCheckPassword(string email,string password)
        {
            var user = await _context.Users.Include(u => u.Email).FirstOrDefaultAsync(u => u.Employee_Mail == email && u.Password == password);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/5
        [HttpGet("GETId/{id}")]
        public async Task<ActionResult<ExamModels.User>> GetUser(int id)
        {/*.Include(u => u.Email).ToListAsync()*/
            //var user = await _context.Users.FindAsync(id);
            var user = await _context.Users.Include(u => u.Email).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PUTId/{id}")]
        public async Task<IActionResult> PutUser(int id, ExamModels.User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/POST/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("POST")]
        public async Task<ActionResult<ExamModels.User>> PostUser(ExamModels.User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/DELETE/5
        [HttpDelete("DELETE/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
