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
    public class ApplicationRolesController : ControllerBase
    {
        private readonly Sistema_TecContext _context;

        public ApplicationRolesController(Sistema_TecContext context)
        {
            _context = context;
        }

        // GET: api/ApplicationRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationRole>>> GetApplicationRoles()
        {
          if (_context.ApplicationRoles == null)
          {
              return NotFound();
          }
            return await _context.ApplicationRoles.ToListAsync();
        }

        // GET: api/ApplicationRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationRole>> GetApplicationRole(int id)
        {
          if (_context.ApplicationRoles == null)
          {
              return NotFound();
          }
            var applicationRole = await _context.ApplicationRoles.FindAsync(id);

            if (applicationRole == null)
            {
                return NotFound();
            }

            return applicationRole;
        }

        // PUT: api/ApplicationRoles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationRole(int id, ApplicationRole applicationRole)
        {
            if (id != applicationRole.id)
            {
                return BadRequest();
            }

            _context.Entry(applicationRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationRoleExists(id))
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

        // POST: api/ApplicationRoles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApplicationRole>> PostApplicationRole(ApplicationRole applicationRole)
        {
          if (_context.ApplicationRoles == null)
          {
              return Problem("Entity set 'Sistema_TecContext.ApplicationRoles'  is null.");
          }
            _context.ApplicationRoles.Add(applicationRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicationRole", new { id = applicationRole.id }, applicationRole);
        }

        // DELETE: api/ApplicationRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationRole(int id)
        {
            if (_context.ApplicationRoles == null)
            {
                return NotFound();
            }
            var applicationRole = await _context.ApplicationRoles.FindAsync(id);
            if (applicationRole == null)
            {
                return NotFound();
            }

            _context.ApplicationRoles.Remove(applicationRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicationRoleExists(int id)
        {
            return (_context.ApplicationRoles?.Any(e => e.id == id)).GetValueOrDefault();
        }

        [HttpPost("{roleId}/{email}")]
        public async Task<ActionResult<ApplicationRole>> AssignApplicationRole(int roleId, string email)
        {
            if (_context.ApplicationRoles == null)
            {
                return NotFound();
            }
            if (_context.People == null)
            {
                return NotFound();
            }
            var peopleList = await _context.People.Where(p => p.email == email).ToListAsync();
            var roleList = await _context.People.Where(p => p.email == email).ToListAsync();

            if (peopleList.Count() > 0 && roleList.Count() > 0)
            {
                await _context.Database.ExecuteSqlRawAsync("INSERT INTO PersonXApplicationRole(email, applicationRoleId) VALUES (@p0, @p1)", email, roleId);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

    }
}
