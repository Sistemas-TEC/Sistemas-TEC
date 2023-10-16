using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Sistema_Tec_Web_API.Models;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices.JavaScript;

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
        public async Task<ActionResult<User>> GetUsers()
        {
            if (_context.People == null)
            {
                return NotFound();
            }

            var peopleXStudent = await _context.People
            .Where(p => _context.Students.Any(s => s.email == p.email))
            .Include(p => p.Student.degree)
            .Select(p => new Person
            {
                email = p.email,
                id = p.id,
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
                id = p.id,
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
            List<UserApplicationRole> userApplicationRolesList;
            List<UserDepartment> userDepartmentList;
            List<UserSchool> userSchoolList;
            List<User> userList = new List<User>();
            foreach (var person in peopleXStudent)
            {
                if (person == null)
                {
                    continue;
                }

                userDepartmentList = new List<UserDepartment>();
                userSchoolList = new List<UserSchool>();
                userApplicationRolesList = new List<UserApplicationRole>();
                foreach (var appRole in person.applicationRoles)
                {
                    appRole.emails = null;
                    appRole.application = null;
                    
                    var applicationMatch = _context.Applications.FirstOrDefault(s => s.id == appRole.applicationId);
                    applicationMatch.ApplicationRoles = null;
                    UserApplicationRole userApplicationRole = new UserApplicationRole
                    {
                        id = appRole.id,
                        applicationId = appRole.applicationId,
                        applicationRoleName = appRole.applicationRoleName,
                        applicationName = applicationMatch.applicationName
                    };
                    userApplicationRolesList.Add(userApplicationRole);
                }

                foreach (var department in person.departments)
                {
                    department.emails = null;

                    var campusMatch = _context.Campuses.FirstOrDefault(s => s.id == department.campusId);
                    campusMatch.Departments = null;
                    campusMatch.Schools = null;
                    UserDepartment userDepartment = new UserDepartment
                    {
                        id = department.id,
                        departmentName = department.departmentName,
                        campusId = department.campusId,
                        campusName = campusMatch.campusName
                    };
                    userDepartmentList.Add(userDepartment);
                }

                foreach (var school in person.schools)
                {
                    school.emails = null;

                    var campusMatch = _context.Campuses.FirstOrDefault(s => s.id == school.campusId);
                    campusMatch.Departments = null;
                    campusMatch.Schools = null;
                    UserSchool userSchool = new UserSchool
                    {
                        id = school.id,
                        schoolName = school.schoolName,
                        campusId = school.campusId,
                        campusName = campusMatch.campusName
                    };
                    userSchoolList.Add(userSchool);
                }

                UserEmployee userEmployee = null;
                if (person.Employee != null)
                {
                    userEmployee = new UserEmployee
                    {
                        id = person.Employee.id,
                        isProfessor = person.Employee.isProfessor
                    };
                }
                UserStudent userStudent = null;
                if (person.Student != null)
                {
                    var degree = _context.Degrees.FirstOrDefault(d => d.id == person.Student.degreeId);
                    userStudent = new UserStudent
                    {
                        id = person.Student.id,
                        degreeName = degree.degreeName,
                        degreeId = person.Student.degreeId,
                        isExemptFromPrintingCosts = person.Student.isExemptFromPrintingCosts
                    };
                }


                User user = new User{ 
                                    email = person.email,
                                    id = person.id,
                                    personName = person.personName,
                                    firstLastName = person.firstLastName,
                                    secondLastName = person.secondLastName,
                                    debt = person.debt,
                                    departments = userDepartmentList,
                                    schools = userSchoolList,
                                    applicationRoles = userApplicationRolesList,
                                    employee = userEmployee,
                                    student = userStudent
                };
                userList.Add(user);
            }
             
            return Ok(userList);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetPerson(string id)
        {
            if (_context.People == null)
            {
                return NotFound();
            }

            var peopleXStudent = await _context.People.Where(p => p.email == id)
            .Where(p => _context.Students.Any(s => s.email == id))
            .Include(p => p.Student.degree)
            .Select(p => new Person
            {
                email = p.email,
                id = p.id,
                personPassword = p.personPassword,
                personName = p.personName,
                firstLastName = p.firstLastName,
                secondLastName = p.secondLastName,
                debt = p.debt,
                applicationRoles = p.applicationRoles,
                departments = p.departments,
                schools = p.schools,
                Student = _context.Students.FirstOrDefault(s => s.email == id)
            })
            .ToListAsync();

            var peopleXEmployee = await _context.People.Where(p => p.email == id)
            .Where(p => _context.Employees.Any(s => s.email == id))
            .Include(p => p.applicationRoles)
            .Include(p => p.departments)
            .Include(p => p.schools)
            .Select(p => new Person
            {
                email = p.email,
                id = p.id,
                personPassword = p.personPassword,
                personName = p.personName,
                firstLastName = p.firstLastName,
                secondLastName = p.secondLastName,
                debt = p.debt,
                applicationRoles = p.applicationRoles,
                departments = p.departments,
                schools = p.schools,
                Employee = _context.Employees.FirstOrDefault(s => s.email == id)
            })

            .ToListAsync();

            peopleXStudent.AddRange(peopleXEmployee);
            List<UserApplicationRole> userApplicationRolesList;
            List<UserDepartment> userDepartmentList;
            List<UserSchool> userSchoolList;
            List<User> userList = new List<User>();
            foreach (var person in peopleXStudent)
            {
                if (person == null)
                {
                    continue;
                }

                userDepartmentList = new List<UserDepartment>();
                userSchoolList = new List<UserSchool>();
                userApplicationRolesList = new List<UserApplicationRole>();
                foreach (var appRole in person.applicationRoles)
                {
                    appRole.emails = null;
                    appRole.application = null;

                    var applicationMatch = _context.Applications.FirstOrDefault(s => s.id == appRole.applicationId);
                    applicationMatch.ApplicationRoles = null;
                    UserApplicationRole userApplicationRole = new UserApplicationRole
                    {
                        id = appRole.id,
                        applicationId = appRole.applicationId,
                        applicationRoleName = appRole.applicationRoleName,
                        applicationName = applicationMatch.applicationName
                    };
                    userApplicationRolesList.Add(userApplicationRole);
                }

                foreach (var department in person.departments)
                {
                    department.emails = null;

                    var campusMatch = _context.Campuses.FirstOrDefault(s => s.id == department.campusId);
                    campusMatch.Departments = null;
                    campusMatch.Schools = null;
                    UserDepartment userDepartment = new UserDepartment
                    {
                        id = department.id,
                        departmentName = department.departmentName,
                        campusId = department.campusId,
                        campusName = campusMatch.campusName
                    };
                    userDepartmentList.Add(userDepartment);
                }

                foreach (var school in person.schools)
                {
                    school.emails = null;

                    var campusMatch = _context.Campuses.FirstOrDefault(s => s.id == school.campusId);
                    campusMatch.Departments = null;
                    campusMatch.Schools = null;
                    UserSchool userSchool = new UserSchool
                    {
                        id = school.id,
                        schoolName = school.schoolName,
                        campusId = school.campusId,
                        campusName = campusMatch.campusName
                    };
                    userSchoolList.Add(userSchool);
                }

                UserEmployee userEmployee = null;
                if (person.Employee != null)
                {
                    userEmployee = new UserEmployee
                    {
                        id = person.Employee.id,
                        isProfessor = person.Employee.isProfessor
                    };
                }
                UserStudent userStudent = null;
                if (person.Student != null)
                {
                    var degree = _context.Degrees.FirstOrDefault(d => d.id == person.Student.degreeId);
                    userStudent = new UserStudent
                    {
                        id = person.Student.id,
                        degreeName = degree.degreeName,
                        degreeId = person.Student.degreeId,
                        isExemptFromPrintingCosts = person.Student.isExemptFromPrintingCosts
                    };
                }


                User user = new User
                {
                    email = person.email,
                    id = person.id,
                    personName = person.personName,
                    firstLastName = person.firstLastName,
                    secondLastName = person.secondLastName,
                    debt = person.debt,
                    departments = userDepartmentList,
                    schools = userSchoolList,
                    applicationRoles = userApplicationRolesList,
                    employee = userEmployee,
                    student = userStudent
                };
                userList.Add(user);
            }
            if (userList.Count == 0)
            {
                return Ok(null);
            }

            return Ok(userList[0]);
        }

        // GET: api/Users/5
        [HttpPost]
        public async Task<ActionResult<User>> Login(LoginBody data)
        {
            if (_context.People == null)
            {
                return NotFound();
            }
            
            string id = data.email;
            string password = data.password;

            var peopleXStudent = await _context.People.Where(p => p.email == id)
            .Where(p => _context.Students.Any(s => s.email == id))
            .Include(p => p.Student.degree)
            .Select(p => new Person
            {
                email = p.email,
                id = p.id,
                personPassword = p.personPassword,
                personName = p.personName,
                firstLastName = p.firstLastName,
                secondLastName = p.secondLastName,
                debt = p.debt,
                applicationRoles = p.applicationRoles,
                departments = p.departments,
                schools = p.schools,
                Student = _context.Students.FirstOrDefault(s => s.email == id)
            })
            .ToListAsync();

            var peopleXEmployee = await _context.People.Where(p => p.email == id)
            .Where(p => _context.Employees.Any(s => s.email == id))
            .Include(p => p.applicationRoles)
            .Include(p => p.departments)
            .Include(p => p.schools)
            .Select(p => new Person
            {
                email = p.email,
                id = p.id,
                personPassword = p.personPassword,
                personName = p.personName,
                firstLastName = p.firstLastName,
                secondLastName = p.secondLastName,
                debt = p.debt,
                applicationRoles = p.applicationRoles,
                departments = p.departments,
                schools = p.schools,
                Employee = _context.Employees.FirstOrDefault(s => s.email == id)
            })

            .ToListAsync();

            
            peopleXStudent.AddRange(peopleXEmployee);
            List<UserApplicationRole> userApplicationRolesList;
            List<UserDepartment> userDepartmentList;
            List<UserSchool> userSchoolList;
            List<User> userList = new List<User>();
            foreach (var person in peopleXStudent)
            {
                if (person == null)
                {
                    continue;
                }
                if (person.email == id && person.personPassword == password)
                {
                    userDepartmentList = new List<UserDepartment>();
                    userSchoolList = new List<UserSchool>();
                    userApplicationRolesList = new List<UserApplicationRole>();
                    foreach (var appRole in person.applicationRoles)
                    {
                        appRole.emails = null;
                        appRole.application = null;

                        var applicationMatch = _context.Applications.FirstOrDefault(s => s.id == appRole.applicationId);
                        applicationMatch.ApplicationRoles = null;
                        UserApplicationRole userApplicationRole = new UserApplicationRole
                        {
                            id = appRole.id,
                            applicationId = appRole.applicationId,
                            applicationRoleName = appRole.applicationRoleName,
                            applicationName = applicationMatch.applicationName
                        };
                        userApplicationRolesList.Add(userApplicationRole);
                    }

                    foreach (var department in person.departments)
                    {
                        department.emails = null;

                        var campusMatch = _context.Campuses.FirstOrDefault(s => s.id == department.campusId);
                        campusMatch.Departments = null;
                        campusMatch.Schools = null;
                        UserDepartment userDepartment = new UserDepartment
                        {
                            id = department.id,
                            departmentName = department.departmentName,
                            campusId = department.campusId,
                            campusName = campusMatch.campusName
                        };
                        userDepartmentList.Add(userDepartment);
                    }

                    foreach (var school in person.schools)
                    {
                        school.emails = null;

                        var campusMatch = _context.Campuses.FirstOrDefault(s => s.id == school.campusId);
                        campusMatch.Departments = null;
                        campusMatch.Schools = null;
                        UserSchool userSchool = new UserSchool
                        {
                            id = school.id,
                            schoolName = school.schoolName,
                            campusId = school.campusId,
                            campusName = campusMatch.campusName
                        };
                        userSchoolList.Add(userSchool);
                    }

                    UserEmployee userEmployee = null;
                    if (person.Employee != null)
                    {
                        userEmployee = new UserEmployee
                        {
                            id = person.Employee.id,
                            isProfessor = person.Employee.isProfessor
                        };
                    }
                    UserStudent userStudent = null;
                    if (person.Student != null)
                    {
                        var degree = _context.Degrees.FirstOrDefault(d => d.id == person.Student.degreeId);
                        userStudent = new UserStudent
                        {
                            id = person.Student.id,
                            degreeName = degree.degreeName,
                            degreeId = person.Student.degreeId,
                            isExemptFromPrintingCosts = person.Student.isExemptFromPrintingCosts
                        };
                    }


                    User user = new User
                    {
                        email = person.email,
                        id = person.id,
                        personName = person.personName,
                        firstLastName = person.firstLastName,
                        secondLastName = person.secondLastName,
                        debt = person.debt,
                        departments = userDepartmentList,
                        schools = userSchoolList,
                        applicationRoles = userApplicationRolesList,
                        employee = userEmployee,
                        student = userStudent
                    };
                    return Ok(user);
                }
            }
            return Ok(null);
        }

        // GET: api/Users
        [HttpPut]
        public async Task<ActionResult<bool>> Change_Password(LoginBody data)
        {
            if (_context.People == null)
            {
                return NotFound();
            }

            var id = data.email;
            var oldPassword = data.oldPassword;
            var newPassword = data.newPassword;

            var peopleXStudent = await _context.People.Where(p => p.email == id)
            .Where(p => _context.Students.Any(s => s.email == id))
            .Include(p => p.Student.degree)
            .Select(p => new Person
            {
                email = p.email,
                id = p.id,
                personPassword = p.personPassword,
                personName = p.personName,
                firstLastName = p.firstLastName,
                secondLastName = p.secondLastName,
                debt = p.debt,
                applicationRoles = p.applicationRoles,
                departments = p.departments,
                schools = p.schools,
                Student = _context.Students.FirstOrDefault(s => s.email == id)
            })
            .ToListAsync();

            var peopleXEmployee = await _context.People.Where(p => p.email == id)
            .Where(p => _context.Employees.Any(s => s.email == id))
            .Include(p => p.applicationRoles)
            .Include(p => p.departments)
            .Include(p => p.schools)
            .Select(p => new Person
            {
                email = p.email,
                id = p.id,
                personPassword = p.personPassword,
                personName = p.personName,
                firstLastName = p.firstLastName,
                secondLastName = p.secondLastName,
                debt = p.debt,
                applicationRoles = p.applicationRoles,
                departments = p.departments,
                schools = p.schools,
                Employee = _context.Employees.FirstOrDefault(s => s.email == id)
            })

            .ToListAsync();


            peopleXStudent.AddRange(peopleXEmployee);
            foreach (var person in peopleXStudent)
            {
                if (person == null)
                {
                    continue;
                }
                if (person.email == id && person.personPassword == oldPassword)
                {
                    _context.People.Where(p => p.email == id && p.personPassword == oldPassword).ExecuteUpdate(setters => setters.SetProperty(p => p.personPassword, newPassword));

                    return Ok(true);
                }
            }
            return Ok(false);
        }

    }
}
