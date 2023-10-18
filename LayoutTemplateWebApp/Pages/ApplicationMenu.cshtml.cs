using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Text;

namespace LayoutTemplateWebApp.Pages
{
    public class ApplicationMenuModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPostCancherks() { Debug.WriteLine(""); }
    }
}
