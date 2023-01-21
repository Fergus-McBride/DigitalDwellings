using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DigitalDwellings.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public override string UserName { get; set; }
        [Display(Name = "User Name")]
        public override string NormalizedUserName { get; set; }
        public int? RetryAttempts { get; set; }
        public bool? IsLocked { get; set; }
        public bool? Enabled { get; set; }
        public bool status
        {
            get
            {
                return (this.Enabled ?? true);
            }
        }
    }
}
