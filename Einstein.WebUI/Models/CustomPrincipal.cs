using Einstein.Domain.Services;
using System.Linq;
using System.Security.Principal;

namespace Einstein.WebUI.Models
{
    public class CustomPrincipal : IPrincipal
    {
        IIdentity identity;
        private IRepository repos;
       

        public CustomPrincipal(IIdentity identity, IRepository _repos)
        {
            this.identity = identity;
            repos = _repos;
        }

        public IIdentity Identity
        {
            get { return this.identity; }
        }

        public bool IsInRole(string role)
        {
            var user = repos.Users.FirstOrDefault(u => u.LOGIN == identity.Name);

            return user != null && user.ROLEID.ToString() == role;
        }

    }
}