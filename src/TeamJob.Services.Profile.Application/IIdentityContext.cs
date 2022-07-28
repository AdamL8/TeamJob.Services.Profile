using System;
using System.Collections.Generic;

namespace TeamJob.Services.Profile.Application
{
    public interface IIdentityContext
    {
        string Id                            { get; }
        string Role                        { get; }
        bool IsAuthenticated               { get; }
        bool IsAdmin                       { get; }
        IDictionary<string, string> Claims { get; }
    }
}