using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Text;

namespace LayoutTemplateWebApp.Pages
{
    public class CreateEmployeeModel : PageModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string firstLastName { get; set; }
        public string secondLastName { get; set; }
        public string id { get; set; }
        public string departmentId { get; set; }
        public string employeeId { get; set; }


        public bool showErrorMsg = false;
        public bool showIncorrect = false;
        public bool showIdError = false;
        public bool emailError = false;
        public void OnGet()
        {
            Debug.WriteLine("get create professor");
        }

        public async Task<IActionResult> OnPostEmployee(string email, string password, string name, string firstLastName, string secondLastName, string id, string employeeId, string departmentId)
        {
            Debug.WriteLine("on post call debuga " + email + " // " + password + " // " + name + " // " + firstLastName + " // " + secondLastName + " // " + id
                            + " // " + employeeId + " // " + departmentId);
            if (email == null || password == null || name == null || firstLastName == null || secondLastName == null || id == null || employeeId == null || departmentId == null)
            {
                showErrorMsg = true;
                Debug.WriteLine("campo nulo");
                return null;
            }
            else if (!email.Contains("@itcr.ac.cr"))
            {
                Debug.WriteLine("correo no es itcr");
                emailError = true;
                return null;
            }

            else if (!Regex.IsMatch(id, @"^[0-9]") || !Regex.IsMatch(employeeId, @"^[0-9]"))
            {
                Debug.WriteLine("No eran números");
                showIdError = true;
                return null;
            }
            else if (int.Parse(id) >= 2147483647 || int.Parse(employeeId) >= 2147483647)
            {
                Debug.WriteLine("era mayor");
                showIdError = true;
                return null;
            }

            var json = JsonConvert.SerializeObject(new
            {
                email = email,
                password = password,
                name = name,
                firstLastName = firstLastName,
                secondLastName = secondLastName,
                id = id,
                departmentId = departmentId,
                employeeId = employeeId,
                isExemptFromPrintingCosts = "no",
                isProfessor = true
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://www.sistema-tec.somee.com/api/users/insert";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);
            var result = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(result);

            return RedirectToPage("Login");

        }
    }
}
