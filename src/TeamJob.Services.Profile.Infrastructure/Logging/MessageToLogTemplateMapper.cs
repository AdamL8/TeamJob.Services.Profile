using System;
using System.Collections.Generic;
using Convey.Logging.CQRS;
using TeamJob.Services.Profile.Application.Commands;
using TeamJob.Services.Profile.Application.Events.External;

namespace TeamJob.Services.Profile.Infrastructure.Logging
{
    public class MessageToLogTemplateMapper : IMessageToLogTemplateMapper
    {
        private static IReadOnlyDictionary<Type, HandlerLogTemplate> MessageTemplates
            => new Dictionary<Type, HandlerLogTemplate>
            {
                {
                    typeof(CompleteUserProfileRegistration), new HandlerLogTemplate
                    {
                        After = "Completed a registration for the user profile with id: {Id}."
                    }
                },
                {
                    typeof(Registered), new HandlerLogTemplate
                    {
                        After = "Created a new user profile with id: {Id}."
                    }
                },
                {
                    typeof(DeleteProfile), new HandlerLogTemplate
                    {
                        After = "Deleted user profile with id: {Id}."
                    }
                },
                {
                    typeof(UpdateProfile), new HandlerLogTemplate
                    {
                        After = "Updated user profile with id: {Id}."
                    }
                },
                {
                    typeof(AcceptCandidate), new HandlerLogTemplate
                    {
                        After = "Profile with id {ProfileId} in Team with id {TeamId} as been promoted to Member status"
                    }
                },
                {
                    typeof(AddCandidate), new HandlerLogTemplate
                    {
                        After = "Profile with id {ProfileId} was added to the Team {TeamName} with id {TeamId} as a Candidate"
                    }
                },
                {
                    typeof(RemoveCandidate), new HandlerLogTemplate
                    {
                        After = "Profile with id {ProfileId} was removed from the Team with id {TeamId} as a Candidate"
                    }
                },
                {
                    typeof(RemoveMember), new HandlerLogTemplate
                    {
                        After = "Profile with id {ProfileId} was removed from the Team with id {TeamId} as a Member"
                    }
                },
                {
                    typeof(TeamCreated), new HandlerLogTemplate
                    {
                        After = "Profile with id OwnerId} was added to the Team {Name} with id {Id} as an Owner"
                    }
                }
            };

        public HandlerLogTemplate Map<TMessage>(TMessage message) where TMessage : class
        {
            var key = message.GetType();
            return MessageTemplates.TryGetValue(key, out var template)
                ? template
                : null;
        }
    }
}