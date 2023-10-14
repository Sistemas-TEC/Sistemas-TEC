using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace LayoutTemplateWebApp.Pages
{
    public class LoginModel : PageModel
    {
        //private readonly ILogger<LoginModel> _logger;
        public string email { get; set; }
        public string password { get; set; }


        public LoginModel()
        {
        }

        public void OnGet()
        {
            Debug.WriteLine("on get call debug");
            System.Console.Write("onget call");
        }

        public async Task<IActionResult> OnPost(string email, string password)
        {
            
            Debug.WriteLine("on post call debug" + email + " // " + password);

            var json = JsonConvert.SerializeObject(new { email = email, password = password });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://www.sistema-tec.somee.com/api/users";
            using var client = new HttpClient();

            //var response = await client.PostAsync(url, data);
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(result);

            //record Person(string Name, string Occupation);

            return RedirectToPage("Login");
        }
    }
}