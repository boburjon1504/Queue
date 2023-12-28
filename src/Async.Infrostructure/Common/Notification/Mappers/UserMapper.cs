using Async.Application.Common.Identity.Model;
using Async.Domain.Entities;
using AutoMapper;
namespace Async.Infrostructure.Common.Notification.Mappers;
public class UserMapper:Profile
{
    public UserMapper()
    {
        CreateMap<SignUpDetails, User>();
    }
}
