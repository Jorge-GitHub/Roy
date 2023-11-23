using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Roy.Mvc.UT.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            FileStream file = new FileStream("filedoesnotexist.json", FileMode.Open);
        }
    }
}