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
        public async Task OnPost() {
            /*string email = Request.Cookies["email"];
            if (email == null)
            {
                return;
            }
            var json = JsonConvert.SerializeObject(new { email = email });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://www.sistema-tec.somee.com/api/users/";
            using var client = new HttpClient();

            //var response = await client.PostAsync(url, data);
            url += email;
            Debug.WriteLine(url);
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            if (result == null)
            {
                return;
            }

            user = Models.QuickType.User.FromJson(result);
            Debug.WriteLine(user.ApplicationRoles[0].ApplicationName);
            Debug.WriteLine(email);
            return;*/
        }
        
    }
}