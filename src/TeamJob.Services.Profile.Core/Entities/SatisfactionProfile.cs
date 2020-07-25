using System.Collections.Generic;

namespace TeamJob.Services.Profile.Core.Entities
{
    public class SatisfactionProfile
    {
        public int MinHours               { get; private set; }
        public int MaxHours               { get; private set; }
        public int AvgHours               { get; private set; }
        public IEnumerable<string> Skills { get; private set; }
        public IEnumerable<string> Limits { get; private set; }
        public bool IsAspiringLeader      { get; private set; }
        public bool IsAspiringTrainer     { get; private set; }

        public SatisfactionProfile(int                 minHours,
                                   int                 maxHours,
                                   int                 avgHours,
                                   IEnumerable<string> skills,
                                   IEnumerable<string> limits,
                                   bool                isAspiringLeader,
                                   bool                isAspiringTrainer)
        {
            MinHours          = minHours;
            MaxHours          = maxHours;
            AvgHours          = avgHours;
            Skills            = skills;
            Limits            = limits;
            IsAspiringLeader  = isAspiringLeader;
            IsAspiringTrainer = isAspiringTrainer;
        }
    }
}