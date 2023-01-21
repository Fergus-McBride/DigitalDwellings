using System.ComponentModel.DataAnnotations;
using DigitalDwellings.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitalDwellings.Pages.UserRoles
{
    public class RolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RolesModel> _logger;
        private readonly IEmailSender _emailSender;
        private ApplicationDbContext _context;

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public IEnumerable<string> Roles {get; set;}
        public RolesModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RolesModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                    if (string.IsNullOrEmpty(Id))
                    {
                        //Redirect to NotFound
                        return RedirectToPage("/");
                    }
                    ApplicationUser user = await _userManager.FindByIdAsync(Id);
                    var userRoles = _userManager.GetRolesAsync(user);
                    Roles = await _userManager.GetRolesAsync(user);

                    return Page();               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error-" + ex);
                return Page();
            }
            
        }

        public async Task<IActionResult> OnPostAddRoleAsync([Required] IEnumerable<string> roleName)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(Id);
            
            await _userManager.AddToRolesAsync(user, roleName);
            return RedirectToPage();    
        }

        //public async Task<IActionResult> OnPostEditRoleAsync([Required] string type,
        //                                                      [Required] string value,
        //                                                      [Required] string oldValue)
        //{
        //}

        public async Task<IActionResult> OnPostCreateRoleAsync([Required] string roleName)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(Id);
            if (ModelState.IsValid)
            {
                var role = new IdentityRole();
                role.Name = roleName;
                var result = await _roleManager.CreateAsync(role);
            }
            Roles = await _userManager.GetRolesAsync(user);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteRoleAsync([Required] IEnumerable<string> roleName)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(Id);
            if (ModelState.IsValid)
            {
                var result = await _userManager.RemoveFromRolesAsync(user, roleName);
            }
            Roles = await _userManager.GetRolesAsync(user);
            return RedirectToPage();
        }    
    }
}
