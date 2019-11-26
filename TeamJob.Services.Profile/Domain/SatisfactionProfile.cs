﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace TeamJob.Services.Profile.Domain
{
    public class SatisfactionProfile
    {
        public int MinHours                { get; private set; }
        public int MaxHours                { get; private set; }
        public int AvgHours                { get; private set; }
        public HourStructure HourStructure { get; private set; }
        public List<WorkShift> WorkShifts  { get; private set; }
        public bool IsAspiringLeader       { get; private set; }
        public bool IsAspiringTrainer      { get; private set; }

        [JsonConstructor]
        public SatisfactionProfile(int             minHours,
                                   int             maxHours,
                                   int             avgHours,
                                   HourStructure   hourStructure,
                                   List<WorkShift> workShifts, 
                                   bool            isAspiringLeader,
                                   bool            isAspiringTrainer)
        {
            MinHours          = minHours;
            MaxHours          = maxHours;
            AvgHours          = avgHours;
            HourStructure     = hourStructure;
            WorkShifts        = workShifts;
            IsAspiringLeader  = isAspiringLeader;
            IsAspiringTrainer = isAspiringTrainer;

        }
    }
}