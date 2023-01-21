using DigitalDwellings.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Security.Claims;


namespace RuneBox.Pages.Home
{
    //[Authorize(Policy = "Admin")]
    public class DashboardModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<DashboardModel> _logger;
        private readonly IEmailSender _emailSender;
        private ApplicationDbContext _context;
        public IEnumerable<ApplicationUser> Users { get; set; }

        public DashboardModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<DashboardModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
        public async void OnGetAsync()
        {
            //Return the users ID into a variable
            //ClaimsPrincipal currentUser = this.User;
            //var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            //ApplicationUser user = (from a in _context.ApplicationUsers where a.Id == currentUserID select a).FirstOrDefault();
            //string currentUserName = user.FirstName + " " + user.LastName;
            //Guid currentUserIDGuid = Guid.Parse(currentUserID);
        }
    }
}
