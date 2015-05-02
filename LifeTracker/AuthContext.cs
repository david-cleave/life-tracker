using Microsoft.AspNet.Identity.EntityFramework;

namespace LifeTracker
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("DefaultConnection")
        {

        }
    }
}