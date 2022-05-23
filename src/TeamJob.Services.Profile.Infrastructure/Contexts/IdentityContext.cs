using System;
using System.Collections.Generic;
using TeamJob.Services.Profile.Application;

namespace TeamJob.Services.Profile.Infrastructure.Contexts
{
    internal class IdentityContext : IIdentityContext
                                                  {
        public string Id                            { get; }
        public string Role                        { get; } = string.Empty;
        public bool IsAuthenticated               { get; }
        public bool IsAdmin                       { get; }
        public IDictionary<string, string> Claims { get; } = new Dictionary<string, string>();

        internal IdentityContext()
        {
        }

        internal IdentityContext(CorrelationContext.UserContext context)
            : this(context.Id, context.Role, context.IsAuthenticated, context.Claims)
        {
        }

        internal IdentityContext(string id, string role, bool isAuthenticated, IDictionary<string, string> claims)
        {
            Id              = id ?? string.Empty;
            Role            = role ?? string.Empty;
            IsAuthenticated = isAuthenticated;
            IsAdmin         = Role.Equals("admin", StringComparison.InvariantCultureIgnoreCase);
            Claims          = claims ?? new Dictionary<string, string>();
        }
        
        internal static IIdentityContext Empty => new IdentityContext();
    }
}