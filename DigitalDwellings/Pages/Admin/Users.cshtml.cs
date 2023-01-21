using DigitalDwellings.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitalDwellings.Pages.Admin
{
    //[Authorize(Policy = "Admin")]
    public class UsersModel : PageModel
    {
        public ApplicationDbContext _context { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }
                        = Enumerable.Empty<ApplicationUser>();

        public UsersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Users = _context.Users.ToList();
        }
    }
}
