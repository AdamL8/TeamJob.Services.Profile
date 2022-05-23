using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamJob.Services.Profile.Core.Events;
using TeamJob.Services.Profile.Core.Exceptions;

namespace TeamJob.Services.Profile.Core.Entities
{
    public class UserProfile : AggregateRoot
    {
        private ISet<Team> _teams = new HashSet<Team>();

        public string Email                            { get; private set; }
        public PersonalInformation PersonalInformation { get; private set; }
        public SatisfactionProfile SatisfactionProfile { get; private set; }
        public Role Role                               { get; private set; }
        public State State                             { get; private set; }
        public long CreatedAt                          { get; private set; }

        public IEnumerable<Team> Teams
        {
            get => _teams;
            set => _teams = new HashSet<Team>(value);
        }

        public UserProfile(string id, string email, long createdAt)
            : this(id, email, null, null, Role.Undefined, State.Incomplete, Enumerable.Empty<Team>(), createdAt)
        {
        }

        public UserProfile(string                id,
                           string              email,
                           PersonalInformation personalInformation,
                           SatisfactionProfile satisfactionProfile,
                           Role                role,
                           State               state,
                           IEnumerable<Team>   teams,
                           long                createdAt)
        {
            Id                  = id;
            Email               = email;
            PersonalInformation = personalInformation;
            SatisfactionProfile = satisfactionProfile;
            Role                = role;
            State               = state;
            Teams               = teams;
            CreatedAt           = createdAt;
        }

        public void CompleteRegistration(PersonalInformation personalInformation,
                                         SatisfactionProfile satisfactionProfile,
                                         Role                role)
        {
            if (personalInformation is null)
            {
                throw new InvalidUserProfilePersonalInformationException(Id);
            }

            if (satisfactionProfile is null)
            {
                throw new InvalidUserProfileSatisfactionProfileException(Id);
            }

            if (State != State.Incomplete)
            {
                throw new CannotChangeUserProfileStateException(Id, State);
            }

            PersonalInformation = personalInformation;
            SatisfactionProfile = satisfactionProfile;
            Role                = role;
            SetValid();

            AddEvent(new UserProfileRegistrationCompleted(this));
        }

        public void SetValid()         => SetState(State.Valid);

        public void SetIncomplete()    => SetState(State.Incomplete);

        public void Lock()             => SetState(State.Locked);

        public void MarkAsSuspicious() => SetState(State.Suspicious);

        private void SetState(State state)
        {
            var previousState = State;
            State             = state;

            AddEvent(new UserProfileStateChanged(this, previousState));
        }

        public bool AddTeam(Team team)
        {
            if (team.Id == string.Empty)
            { return false; }

            _teams.Add(team);
            return true;
        }

        public bool RemoveTeam(Team team)
        {
            if (team.Id == string.Empty)
            { return false; }

            _teams.Remove(team);
            return true;
        }

        public bool RemoveTeamById(string teamId)
        {
            if (teamId == string.Empty)
            { return false; }

            var foundTeam = _teams.SingleOrDefault(x => x.Id == teamId);

            if (foundTeam is null)
            { return false; }

            _teams.Remove(foundTeam);
            return true;
        }
    }
}
