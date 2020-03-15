using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevelopmentUtilitiesV2RESTful.Models;
using Microsoft.AspNetCore.Authorization;

namespace DevelopmentUtilitiesV2RESTful.Controllers
{
     [Authorize]
     [Route("api/v2/[controller]")]
    [ApiController]
    public class ProblemsController : ControllerBase
    {
        private readonly DevelopmentUtilitiesV2Context _context;

        public ProblemsController(DevelopmentUtilitiesV2Context context)
        {
            _context = context;
        }

        // GET: api/Problems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Problems>>> GetProblems()
        {
            return await _context.Problems.ToListAsync();
        }

        // GET: api/Problems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Problems>> GetProblems(int id)
        {
            var problems = await _context.Problems.FindAsync(id);

            if (problems == null)
            {
                return NotFound();
            }

            return problems;
        }

        // PUT: api/Problems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProblems(int id, Problems problems)
        {
            if (id != problems.Id)
            {
                return BadRequest();
            }

            _context.Entry(problems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProblemsExists(id))
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

        // POST: api/Problems
        [HttpPost]
        public async Task<ActionResult<Problems>> PostProblems(Problems problems)
        {
            _context.Problems.Add(problems);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProblems", new { id = problems.Id }, problems);
        }

        // DELETE: api/Problems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Problems>> DeleteProblems(int id)
        {
            var problems = await _context.Problems.FindAsync(id);
            if (problems == null)
            {
                return NotFound();
            }

            _context.Problems.Remove(problems);
            await _context.SaveChangesAsync();

            return problems;
        }

        private bool ProblemsExists(int id)
        {
            return _context.Problems.Any(e => e.Id == id);
        }
    }
}
