using AutoMapper;
using Core.Commands;
using Core.Queries;
using Reactivities.Common.Messages.Models.Request;

namespace Core.MappingProfiles;

public class MessagesMappingProfile : Profile
{
    public MessagesMappingProfile()
    {
        this.CreateMap<SendMessageRequestModel, SendMessage.Command>();
        this.CreateMap<GetSenderReceiverConversationRequestModel, GetSenderReceiverConversation.Query>();
    }
}