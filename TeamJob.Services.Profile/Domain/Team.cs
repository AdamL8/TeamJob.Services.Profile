using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamJob.Services.Profile.Domain
{
    public class Team
    {
        public Guid Id { get; private set; }

        public Team(Guid InId)
        {
            Id = InId;
        }
    }
}
