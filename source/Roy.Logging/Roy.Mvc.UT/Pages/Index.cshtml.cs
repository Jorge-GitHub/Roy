using Microsoft.AspNetCore.Mvc.RazorPages;
using Roy.Logging;

namespace Roy.Mvc.UT.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            //new Author { Description = "Great singer", Name = "Roy" }.LogAsync();
            FileStream file = new FileStream("filedoesnotexist.json", FileMode.Open);
        }
    }

    /// <summary>
    /// Test class. 
    /// </summary>
    public class Author
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}