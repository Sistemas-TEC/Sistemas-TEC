using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sistema_Tec_Web_API.Models;

namespace Sistema_Tec_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Sistema_TecContext _context;

        public UsersController(Sistema_TecContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetUsers()
        {
            if (_context.People == null)
            {
                return NotFound();
            }

            var peopleXStudent = await _context.People
            .Where(p => _context.Students.Any(s => s.email == p.email))
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
                Student = _context.Students.FirstOrDefault(s => s.email == p.email)
            })
            .ToListAsync();

            var peopleXEmployee = await _context.People
            .Where(p => _context.Employees.Any(s => s.email == p.email))
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
                Employee = _context.Employees.FirstOrDefault(s => s.email == p.email)
            })
            
            .ToListAsync();
            
            peopleXStudent.AddRange(peopleXEmployee);
            foreach (var person in peopleXStudent)
            {
                foreach (var appRole in person.applicationRoles)
                {
                    appRole.emails = null;
                }
            }
            return peopleXStudent;
        }

        // GET: api/Users/5
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

    }
}
