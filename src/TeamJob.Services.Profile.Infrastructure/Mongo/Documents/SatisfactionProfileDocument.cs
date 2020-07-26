using System;
using System.Collections.Generic;
using System.Text;

namespace TeamJob.Services.Profile.Infrastructure.Mongo.Documents
{
    public class SatisfactionProfileDocument
    {
        public int MinHours               { get; set; }
        public int MaxHours               { get; set; }
        public int AvgHours               { get; set; }
        public IEnumerable<string> Skills { get; set; }
        public IEnumerable<string> Limits { get; set; }
        public bool IsAspiringLeader      { get; set; }
        public bool IsAspiringTrainer     { get; set; }
    }
}
