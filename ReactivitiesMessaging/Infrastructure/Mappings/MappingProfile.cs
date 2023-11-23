using AutoMapper;
using Core.Commands;
using Core.Queries;
using Infrastructure.Models;

namespace Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<SendMessageModel, SendMessage.Command>();
        
        this.CreateMap<GetConversationMessageModel, GetConversation.Query>();
    }
}