using System.Collections.Generic;
using System.Security.Principal;

namespace AuthNAuthZ.Identity
{
    public abstract class IdentityBase : IIdentity
    {
        public string Name { get; private set; }

        public string AuthenticationType { get; private set; }

        public bool IsAuthenticated { get; private set; }
        public IEnumerable<string> Roles { get; private set; }

        public IdentityBase() { Roles = new string[0]; }
        public IdentityBase(string name, IEnumerable<string> roles) { Name = name; Roles = roles; IsAuthenticated = true; }
    }
    public class SampleIdentity : IdentityBase {
        public SampleIdentity() : base() { }
        public SampleIdentity(string name, IEnumerable<string> roles) : base(name, roles) { }
    }

}
