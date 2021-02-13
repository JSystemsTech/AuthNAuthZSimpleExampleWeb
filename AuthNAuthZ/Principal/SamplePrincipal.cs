using AuthNAuthZ.Identity;
using System.Linq;
using System.Security.Principal;

namespace AuthNAuthZ.Principal
{
    public abstract class PrincipalBase : IPrincipal
    {
        public IIdentity Identity => IdentityBase;
        private IdentityBase IdentityBase { get; set; }
        public bool IsInRole(string role) => IdentityBase.Roles.Contains(role);
        public PrincipalBase(IdentityBase identity) { IdentityBase = identity; }
    }
    public class SamplePrinciapl: PrincipalBase
    {
        public SamplePrinciapl(SampleIdentity identity) : base(identity) { }

    }

}
