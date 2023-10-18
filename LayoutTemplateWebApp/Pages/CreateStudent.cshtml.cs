using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace LayoutTemplateWebApp.Pages
{
    public class CreateStudentModel : PageModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string firstLastName { get; set; }
        public string secondLastName { get; set; }
        public string id { get; set; }
        public string degreeId { get; set; }
        public string studentId { get; set; }
        public string isExemptFromPrintingCosts { get; set; }


        public bool showErrorMsg = false;
        public bool showIncorrect = false;
        public bool showIdError = false;
        public void OnGet()
        {
            Debug.WriteLine("get create student");
        }

        public async Task<IActionResult> OnPostStudent(string email, string password, string name, string firstLastName, string secondLastName, string id, string isExemptFromPrintingCosts, string studentId, string degreeId)
        {
            Debug.WriteLine("on post call debuga " + email + " // " + password + " // " + name + " // " + firstLastName + " // " + secondLastName + " // " + id 
                            + " // " + studentId + " // " + degreeId + " // " + isExemptFromPrintingCosts);
            if (email == null || password == null || name == null || firstLastName == null || secondLastName == null || id == null || studentId == null || degreeId == null || isExemptFromPrintingCosts == null)
            {
                showErrorMsg = true;
                return null;
            }
            else if (!Regex.IsMatch(id, @"^[0-9]") || !Regex.IsMatch(studentId, @"^[0-9]"))
            {
                Debug.WriteLine("No eran números");
                showIdError = true;
                return null;
            }
            else if (int.Parse(id) >= 2147483647 || int.Parse(studentId) >= 2147483647)
            {
                Debug.WriteLine("era mayor");
                showIdError = true;
                return null;
            }
            var json = JsonConvert.SerializeObject(new { email = email, password = password, name = name, firstLastName = firstLastName, secondLastName = secondLastName, 
                                                    id = id, degreeId = degreeId, studentId = studentId, isExemptFromPrintingCosts = isExemptFromPrintingCosts
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://www.sistema-tec.somee.com/api/users/insert";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);
            var result = await response.Content.ReadAsStringAsync();

            //Debug.WriteLine(result);

            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(5) // Sets cookie expiration
            };

            Response.Cookies.Append("email", email, options);
            //return Redirect("https://www.youtube.com");
            /*if (result == "")
            {
                showIncorrect = true;
                return null;
            }
            showErrorMsg = false;
            showIncorrect = false;*/
            
            return RedirectToPage("CreateStudent");

        }
    }
}