using Async.Api.Model.Dto;
using Async.Domain.Entities;
using AutoMapper;

namespace Async.Api.Mapper;

public class AccessTokenMapper:Profile
{
    public AccessTokenMapper()
    {
        CreateMap<AccessToken, AccessTokenDto>().ReverseMap();
    }
}
