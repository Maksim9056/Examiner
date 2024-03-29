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

namespace ExamAPI.Controllers.QuestionAnswer
{
    [Route("api/[controller]")]
    [ApiController]

    public class QuestionAnswersController : ControllerBase
    {
        private readonly ExamAPIContext _context;

        public QuestionAnswersController(ExamAPIContext context)
        {
            _context = context;
        }

        // GET: api/QuestionAnswers
        [HttpGet("GET")]
        public async Task<ActionResult<IEnumerable<ExamModels.QuestionAnswer>>> GetQuestionAnswer()
        {
            return await _context.QuestionAnswer.Include(u => u.Answer).Include(u => u.Questions).ToListAsync();
        }

        // GET: api/QuestionAnswers/5
        [HttpGet("GETId/{id}")]
        public async Task<ActionResult<ExamModels.QuestionAnswer>> GetQuestionAnswer(int id)
        {
            var questionAnswer = await _context.QuestionAnswer.Include(u => u.Answer).Include(u => u.Questions).FirstOrDefaultAsync(u => u.Id == id);

            if (questionAnswer == null)
            {
                return NotFound();
            }

            return questionAnswer;
        }

        // PUT: api/QuestionAnswers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PUTId/{id}")]
        public async Task<IActionResult> PutQuestionAnswer(int id, ExamModels.QuestionAnswer questionAnswer)
        {
            if (id != questionAnswer.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionAnswer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionAnswerExists(id))
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

        // POST: api/QuestionAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("POST")]
        public async Task<ActionResult<ExamModels.QuestionAnswer>> PostQuestionAnswer(ExamModels.QuestionAnswer questionAnswer)
        {
            _context.QuestionAnswer.Add(questionAnswer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionAnswer", new { id = questionAnswer.Id }, questionAnswer);
        }

        // DELETE: api/QuestionAnswers/5
        [HttpDelete("DELETE/{id}")]
        public async Task<IActionResult> DeleteQuestionAnswer(int id)
        {
            var questionAnswer = await _context.QuestionAnswer.FindAsync(id);
            if (questionAnswer == null)
            {
                return NotFound();
            }

            _context.QuestionAnswer.Remove(questionAnswer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionAnswerExists(int id)
        {
            return _context.QuestionAnswer.Any(e => e.Id == id);
        }
    }
}
