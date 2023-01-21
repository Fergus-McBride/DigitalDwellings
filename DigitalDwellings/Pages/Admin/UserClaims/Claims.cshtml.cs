using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using RuneBox.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using DigitalDwellings.Data;

namespace DigitalDwellings.Pages.UserClaims
{

    public class ClaimsModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ClaimsModel> _logger;
        private readonly IEmailSender _emailSender;
        private ApplicationDbContext _context;
        public IEnumerable<ApplicationUser> Users { get; set; }

        public ClaimsModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ClaimsModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }


        public UserManager<ApplicationUser> UserManager { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public IEnumerable<Claim> Claims { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(Id))
            {
                //Redirect to NotFound
                return RedirectToPage("/");
            }
            ApplicationUser user = await _userManager.FindByIdAsync(Id);
            Claims = await _userManager.GetClaimsAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAddClaimAsync([Required] string type,
                                                             [Required] string value)
        {

            ApplicationUser user = await _userManager.FindByIdAsync(Id);

            if (ModelState.IsValid)
            {
                var claim = new Claim(type, value);
                var result = await _userManager.AddClaimAsync(user, claim);
                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err.Description);
                    }
                }
            }
            Claims = await _userManager.GetClaimsAsync(user);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditClaimAsync([Required] string type,
                                                              [Required] string value,
                                                              [Required] string oldValue)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(Id);
            if (ModelState.IsValid)
            {
                var claimNew = new Claim(type, value);
                var claimOld = new Claim(type, oldValue);
                var result = await _userManager.ReplaceClaimAsync(user, claimOld, claimNew);
            }
            Claims = await _userManager.GetClaimsAsync(user);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteClaimAsync([Required] string type,
                                                                [Required] string value)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(Id);
            if (ModelState.IsValid)
            {
                var claim = new Claim(type, value);
                var result = await _userManager.RemoveClaimAsync(user, claim);
            }
            Claims = await _userManager.GetClaimsAsync(user);
            return RedirectToPage();

        }
    }
}