using BankStatementAnalyzer.Repository.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BankStatementAnalyzer.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            User user;
            using (var dbContext = new BankStatementAnalyzerContext())
            {
                user = await dbContext.User.Include("Roles").FirstOrDefaultAsync(u => u.Username == userIdentity.Name);

                // Add custom user claims here
                foreach (Role role in user.Roles)
                {
                    userIdentity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
                }

                userIdentity.AddClaim(new Claim("FullName", user.Name));

                var logins = manager.GetLogins(userIdentity.GetUserId()).ToList();
                if (logins.Count > 0)
                {
                    userIdentity.AddClaim(new Claim("Provider", logins[0].LoginProvider));
                }

                return userIdentity;
            }

        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("BankStatementAnalyzerContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}