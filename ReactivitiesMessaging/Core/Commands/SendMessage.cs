using Core.Interfaces.DataServices;
using Domain.Models;
using MediatR;
using Reactivities.Common.Result.Models;

namespace Core.Commands;

public class SendMessage
{
    public class Command : IRequest<Result<bool>>
    {
        public Command(string senderUsername, string receiverUsername, string content, DateTime dateSent)
        {
            this.SenderUsername = senderUsername;
            this.ReceiverUsername = receiverUsername;
            this.Content = content;
            this.DateSent = dateSent;
        }

        public string SenderUsername { get; private set; }
        public string ReceiverUsername { get; private set; }
        public string Content { get; private set; }
        public DateTime DateSent { get; private set; }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly IMessagesDataService _messagesDataService;
        private readonly IProfilesDataService _profilesDataService;

        public Handler(
            IMessagesDataService messagesDataService,
            IProfilesDataService profilesDataService)
        {
            this._messagesDataService = messagesDataService;
            this._profilesDataService = profilesDataService;
        }

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var sender = await this._profilesDataService
                    .GetByUsernameAsync(request.SenderUsername);

                var receiver = await this._profilesDataService
                    .GetByUsernameAsync(request.ReceiverUsername);

                var message = Message.New(sender, receiver, request.Content, request.DateSent);

                await this._messagesDataService.SaveMessageAsync(message);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }

            return Result<bool>.Success(true);
        }
    }
}