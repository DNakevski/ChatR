using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatR.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        //custom fields
        public string Avatar { get; set; }

        public ClaimsIdentity GenerateUserIdentity(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity<ApplicationUser, int>(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            return Task.FromResult(GenerateUserIdentity(manager));
        }
    }


    /// <summary>
    /// ApplicationRole in IdentityConfig.cs
    /// </summary>
    public partial class ApplicationRole : IdentityRole<int, ApplicationUserRole>
    {
        public ApplicationRole() { }
        public ApplicationRole(string name) { Name = name; }
    }

    public class ApplicationUserRole : IdentityUserRole<int> { }

    public class ApplicationUserClaim : IdentityUserClaim<int> { }

    public class ApplicationUserLogin : IdentityUserLogin<int> { }
}