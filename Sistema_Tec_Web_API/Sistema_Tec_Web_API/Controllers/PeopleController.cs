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
    public class PeopleController : ControllerBase
    {
        private readonly Sistema_TecContext _context;

        public PeopleController(Sistema_TecContext context)
        {
            _context = context;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {
          if (_context.People == null)
          {
              return NotFound();
          }
            var personList = await _context.People
                .Include(p => p.applicationRoles)
                .Include(p => p.departments)
                .Include(p => p.schools)
                .Select(p => new Person
                {
                    email = p.email,
                    personPassword = p.personPassword,
                    personName = p.personName,
                    firstLastName = p.firstLastName,
                    secondLastName = p.secondLastName,
                    debt = p.debt,
                    applicationRoles = p.applicationRoles,
                    departments = p.departments,
                    schools = p.schools,
                    //Employee = _context.Employees.FirstOrDefault(s => s.email == p.email)
                })
                .ToListAsync();

            foreach (var person in personList) 
            {
                foreach (var appRole in person.applicationRoles)
                {
                    appRole.emails = null;
                }
            }
            /*foreach (var person in personList)
            {
                foreach (var department in person.departments)
                {
                    department.emails = null;
                }
            }
            foreach (var person in personList)
            {
                foreach (var school in person.schools)
                {
                    school.emails = null;
                }
            }
            */
            return personList;
            //return await _context.People.ToListAsync();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(string id)
        {
          if (_context.People == null)
          {
              return NotFound();
          }
            var person = await _context.People.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(string id, Person person)
        {
            if (id != person.email)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
          if (_context.People == null)
          {
              return Problem("Entity set 'Sistema_TecContext.People'  is null.");
          }
            _context.People.Add(person);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PersonExists(person.email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPerson", new { id = person.email }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(string id)
        {
            if (_context.People == null)
            {
                return NotFound();
            }
            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(string id)
        {
            return (_context.People?.Any(e => e.email == id)).GetValueOrDefault();
        }
    }
}
