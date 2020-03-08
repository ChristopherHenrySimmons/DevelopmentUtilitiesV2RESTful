using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevelopmentUtilitiesV2RESTful.Models;

namespace DevelopmentUtilitiesV2RESTful.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class CodeBlocksController : ControllerBase
    {
        private readonly DevelopmentUtilitiesV2Context _context;

        public CodeBlocksController(DevelopmentUtilitiesV2Context context)
        {
            _context = context;
        }

        // GET: api/CodeBlocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodeBlocks>>> GetCodeBlocks()
        {
            return await _context.CodeBlocks.ToListAsync();
        }

        // GET: api/CodeBlocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CodeBlocks>> GetCodeBlocks(int id)
        {
            var codeBlocks = await _context.CodeBlocks.FindAsync(id);

            if (codeBlocks == null)
            {
                return NotFound();
            }

            return codeBlocks;
        }

        // PUT: api/CodeBlocks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCodeBlocks(int id, CodeBlocks codeBlocks)
        {
            if (id != codeBlocks.Id)
            {
                return BadRequest();
            }

            _context.Entry(codeBlocks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CodeBlocksExists(id))
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

        // POST: api/CodeBlocks
        [HttpPost]
        public async Task<ActionResult<CodeBlocks>> PostCodeBlocks(CodeBlocks codeBlocks)
        {
            _context.CodeBlocks.Add(codeBlocks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCodeBlocks", new { id = codeBlocks.Id }, codeBlocks);
        }

        // DELETE: api/CodeBlocks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CodeBlocks>> DeleteCodeBlocks(int id)
        {
            var codeBlocks = await _context.CodeBlocks.FindAsync(id);
            if (codeBlocks == null)
            {
                return NotFound();
            }

            _context.CodeBlocks.Remove(codeBlocks);
            await _context.SaveChangesAsync();

            return codeBlocks;
        }

        private bool CodeBlocksExists(int id)
        {
            return _context.CodeBlocks.Any(e => e.Id == id);
        }
    }
}
