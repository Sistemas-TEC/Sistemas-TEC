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

        [HttpPost("/api/Users/insert")]
        public async Task<ActionResult<User>> Insert(LoginBody data)
        {
            if (_context.People == null)
            {
                return Ok(null);
            }

            string email = data.email;
            string password = data.password;
            string name = data.name;
            string firstLastName = data.firstLastName;
            string secondLastName = data.secondLastName;
            string id = data.id;
            string degreeId = data.degreeId;
            string studentId = data.studentId;
            string employeeId = data.employeeId;
            string isExemptFromPrintingCosts = data.isExemptFromPrintingCosts;
            string departmentId = data.departmentId;
            string schoolId = data.schoolId;
            bool isProfessor = data.isProfessor;

            List<Department> departmentList = new List<Department>();
            List<School> schoolList = new List<School>();
            List<Student> studentList = new List<Student>();
            List<Employee> employeeList = new List<Employee>();
            List<Degree> degreeList = new List<Degree>();
            List<Person> personList = new List<Person>();

            if (email != null)
            {
                personList = await _context.People.Where(p => p.email == email || p.id == int.Parse(id)).ToListAsync();
                if (personList.Count > 0)
                {
                    return Ok("idExists");
                }
            }

            if (employeeId != null)
            {
                employeeList = await _context.Employees.Where(s => s.id == int.Parse(employeeId)).ToListAsync();
                if (employeeList.Count > 0)
                {
                    return Ok("employeeIdExists");
                }

                if (departmentId != null)
                {
                    departmentList = await _context.Departments.Where(s => s.id == int.Parse(departmentId)).ToListAsync(); ;
                    if (departmentList.Count == 0)
                    {
                        return Ok("noDepartment");
                    }
                }

                if (schoolId != null)
                {
                    schoolList = await _context.Schools.Where(s => s.id == int.Parse(schoolId)).ToListAsync(); ;
                    if (schoolList.Count == 0)
                    {
                        return Ok("noSchool");
                    }
                }
            }

            if (studentId != null)
            {
                studentList = await _context.Students.Where(s => s.id == int.Parse(studentId)).ToListAsync();
                if (studentList.Count > 0)
                {
                    return Ok("studentIdExists");
                }
                if (degreeId != null)
                {
                    degreeList = await _context.Degrees.Where(s => s.id == int.Parse(degreeId)).ToListAsync(); ;
                    if (degreeList.Count == 0)
                    {
                        return Ok("noDegree");
                    }
                }
                
            }

            _context.People.Add(new Person
            {
                email = email,
                id = int.Parse(id),
                personPassword = password,
                personName = name,
                firstLastName = firstLastName,
                secondLastName = secondLastName,
                debt = 0
            });
            _context.SaveChanges();
            if (studentId != null)
            {

                _context.Students.Add(new Student
                {
                    email = email,
                    id = int.Parse(studentId),
                    isExemptFromPrintingCosts = false,
                    degreeId = int.Parse(degreeId)
                });
                _context.SaveChanges();
            }
            if (employeeId != null)
            {
                _context.Employees.Add(new Employee
                {
                    email = email,
                    id = int.Parse(employeeId),
                    isProfessor = isProfessor
                });
                _context.SaveChanges();
            }
            return Ok("ok");
        }

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
