using LayoutTemplateWebApp.Models.QuickType;
using LayoutTemplateWebApp.Models.QuickTypeApplication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace LayoutTemplateWebApp.Pages.AppCards
{
    public class RolesModel : PageModel
    {
        public Application application { get; set; }
        public async Task OnGet()
        {
            var url = "http://www.sistema-tec.somee.com/api/applications/";
            using var client = new HttpClient();
            Debug.WriteLine(url);
            var response = await client.GetAsync(url + "10");
            var result = await response.Content.ReadAsStringAsync();
            
            if (result == null)
            {
                return;
            }

            application = Models.QuickTypeApplication.Application.FromJson(result);
        }
        public ActionResult OnPostNavigateToPage()
        {
            return RedirectToPage("/RoleAssignment");
        }
    }
}