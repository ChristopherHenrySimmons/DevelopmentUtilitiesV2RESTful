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
    public class ResourcesController : ControllerBase
    {
        private readonly DevelopmentUtilitiesV2Context _context;

        public ResourcesController(DevelopmentUtilitiesV2Context context)
        {
            _context = context;
        }

        // GET: api/Resources
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resources>>> GetResources()
        {
            return await _context.Resources.ToListAsync();
        }

        // GET: api/Resources/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Resources>> GetResources(int id)
        {
            var resources = await _context.Resources.FindAsync(id);

            if (resources == null)
            {
                return NotFound();
            }

            return resources;
        }

        // PUT: api/Resources/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResources(int id, Resources resources)
        {
            if (id != resources.Id)
            {
                return BadRequest();
            }

            _context.Entry(resources).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourcesExists(id))
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

        // POST: api/Resources
        [HttpPost]
        public async Task<ActionResult<Resources>> PostResources(Resources resources)
        {
            _context.Resources.Add(resources);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResources", new { id = resources.Id }, resources);
        }

        // DELETE: api/Resources/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Resources>> DeleteResources(int id)
        {
            var resources = await _context.Resources.FindAsync(id);
            if (resources == null)
            {
                return NotFound();
            }

            _context.Resources.Remove(resources);
            await _context.SaveChangesAsync();

            return resources;
        }

        private bool ResourcesExists(int id)
        {
            return _context.Resources.Any(e => e.Id == id);
        }
    }
}
