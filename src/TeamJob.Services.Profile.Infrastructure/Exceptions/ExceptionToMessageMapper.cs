using System;
using Convey.MessageBrokers.RabbitMQ;
using TeamJob.Services.Profile.Application.Commands;
using TeamJob.Services.Profile.Application.Events.Rejected;
using TeamJob.Services.Profile.Application.Exceptions;
using TeamJob.Services.Profile.Core.Exceptions;

namespace TeamJob.Services.Profile.Infrastructure.Exceptions
{
    public class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
            => exception switch
            {
                CannotChangeUserProfileStateException ex => message switch
                {
                    CompleteUserProfileRegistration _ => new CompleteUserProfileRegistrationRejected(ex.Id, ex.Message, ex.Code),
                    _ => null
                },
                UserProfileNotFoundException ex => message switch
                {
                    CompleteUserProfileRegistration _ => new CompleteUserProfileRegistrationRejected(ex.Id, ex.Message, ex.Code),
                    DeleteProfile _                   => new DeleteProfileRejected(ex.Id, ex.Message, ex.Code),
                    UpdateProfile _                   => new UpdateProfileRejected(ex.Id, ex.Message, ex.Code),
                    _ => null
                },

                UserProfileAlreadyCreatedException ex             => new CompleteUserProfileRegistrationRejected(ex.Id, ex.Message, ex.Code),
                CannotSetUserProfileRoleException ex              => new CompleteUserProfileRegistrationRejected(ex.Id, ex.Message, ex.Code),
                InvalidUserProfilePersonalInformationException ex => new CompleteUserProfileRegistrationRejected(ex.Id, ex.Message, ex.Code),
                InvalidUserProfileSatisfactionProfileException ex => new CompleteUserProfileRegistrationRejected(ex.Id, ex.Message, ex.Code),
                _ => null
            };
    }
}