using LayoutTemplateWebApp.Models.QuickType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace LayoutTemplateWebApp.Pages
{
    public class PasswordChangeModel : PageModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public void OnGet()
        {

        }
        public async Task OnPost(string OldPassword, string NewPassword) {
            string email = Request.Cookies["email"];
            if (email == null)
            {
                return;
            }
            var json = JsonConvert.SerializeObject(new { email = email, oldPassword = OldPassword, newPassword = NewPassword });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://www.sistema-tec.somee.com/api/users/";
            using var client = new HttpClient();
            var response = await client.PutAsync(url, data);
            var result = await response.Content.ReadAsStringAsync();

            if (result == null)
            {
                return;
            }
            Debug.WriteLine(OldPassword);
            Debug.WriteLine(result);
            Debug.WriteLine(email);
            return;
        }
        
    }
}
