using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_Tec_Web_API.Models;

namespace Sistema_Tec_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationTypesController : ControllerBase
    {
        private readonly Sistema_TecContext _context;

        public OrganizationTypesController(Sistema_TecContext context)
        {
            _context = context;
        }

        // GET: api/OrganizationTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationType>>> GetOrganizationTypes()
        {
          if (_context.OrganizationTypes == null)
          {
              return NotFound();
          }
            return await _context.OrganizationTypes.ToListAsync();
        }

        // GET: api/OrganizationTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationType>> GetOrganizationType(int id)
        {
          if (_context.OrganizationTypes == null)
          {
              return NotFound();
          }
            var organizationType = await _context.OrganizationTypes.FindAsync(id);

            if (organizationType == null)
            {
                return NotFound();
            }

            return organizationType;
        }

        // PUT: api/OrganizationTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizationType(int id, OrganizationType organizationType)
        {
            if (id != organizationType.id)
            {
                return BadRequest();
            }

            _context.Entry(organizationType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationTypeExists(id))
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

        // POST: api/OrganizationTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrganizationType>> PostOrganizationType(OrganizationType organizationType)
        {
          if (_context.OrganizationTypes == null)
          {
              return Problem("Entity set 'Sistema_TecContext.OrganizationTypes'  is null.");
          }
            _context.OrganizationTypes.Add(organizationType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganizationType", new { id = organizationType.id }, organizationType);
        }

        // DELETE: api/OrganizationTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizationType(int id)
        {
            if (_context.OrganizationTypes == null)
            {
                return NotFound();
            }
            var organizationType = await _context.OrganizationTypes.FindAsync(id);
            if (organizationType == null)
            {
                return NotFound();
            }

            _context.OrganizationTypes.Remove(organizationType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganizationTypeExists(int id)
        {
            return (_context.OrganizationTypes?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
