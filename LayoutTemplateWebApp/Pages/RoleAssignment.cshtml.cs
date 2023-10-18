using LayoutTemplateWebApp.Models.QuickType;
using LayoutTemplateWebApp.Models.QuickTypeApplicationRole;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace LayoutTemplateWebApp.Pages
{
    public class RoleManagementModel : PageModel
    {
        //private readonly ILogger<LoginModel> _logger;
        public string email { get; set; }
        public string roleId { get; set; }

        public bool showErrorMsg = false;

        public bool showCorrect = false;

        public SelectList dropDownList;

        public Collection<Models.QuickTypeApplicationRole.ApplicationRole> applicationRoles { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var url = "http://www.sistema-tec.somee.com/api/applicationroles/";
            using var client = new HttpClient();

            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var getJson = JsonConvert.DeserializeObject(result);
            Debug.WriteLine(getJson.GetType());
            return null;
        }

        public async Task<IActionResult> OnPostAssignRole(string email, string roleId)
        {
            
            Debug.WriteLine("on post call debug " + email + " // " + roleId);
            if (email == null || roleId == null)
            {
                showErrorMsg = true;
                return null;
            }
            var json = JsonConvert.SerializeObject(new { email = email, roleId = roleId });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://www.sistema-tec.somee.com/api/applicationroles/" + roleId + "/" + email;
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);
            var result = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(result);
            
            return RedirectToPage("RoleAssignment");
        }
    }
}
