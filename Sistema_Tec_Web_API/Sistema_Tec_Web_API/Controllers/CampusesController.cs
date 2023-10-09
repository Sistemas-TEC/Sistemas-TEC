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
    public class CampusesController : ControllerBase
    {
        private readonly Sistema_TecContext _context;

        public CampusesController(Sistema_TecContext context)
        {
            _context = context;
        }

        // GET: api/Campuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campus>>> GetCampuses()
        {
          if (_context.Campuses == null)
          {
              return NotFound();
          }
            return await _context.Campuses.ToListAsync();
        }

        // GET: api/Campuses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Campus>> GetCampus(int id)
        {
          if (_context.Campuses == null)
          {
              return NotFound();
          }
            var campus = await _context.Campuses.FindAsync(id);

            if (campus == null)
            {
                return NotFound();
            }

            return campus;
        }

        // PUT: api/Campuses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCampus(int id, Campus campus)
        {
            if (id != campus.id)
            {
                return BadRequest();
            }

            _context.Entry(campus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampusExists(id))
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

        // POST: api/Campuses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Campus>> PostCampus(Campus campus)
        {
          if (_context.Campuses == null)
          {
              return Problem("Entity set 'Sistema_TecContext.Campuses'  is null.");
          }
            _context.Campuses.Add(campus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCampus", new { id = campus.id }, campus);
        }

        // DELETE: api/Campuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampus(int id)
        {
            if (_context.Campuses == null)
            {
                return NotFound();
            }
            var campus = await _context.Campuses.FindAsync(id);
            if (campus == null)
            {
                return NotFound();
            }

            _context.Campuses.Remove(campus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CampusExists(int id)
        {
            return (_context.Campuses?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
