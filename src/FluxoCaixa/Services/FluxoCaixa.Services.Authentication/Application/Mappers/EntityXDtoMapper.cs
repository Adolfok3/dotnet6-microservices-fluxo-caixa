using AutoMapper;
using FluxoCaixa.Services.Authentication.Application.Dto.Users;
using FluxoCaixa.Services.Authentication.Domain.Entities;

namespace FluxoCaixa.Services.Authentication.Application.Mappers
{
    public class EntityXDtoMapper : Profile
    {
        public EntityXDtoMapper()
        {
            CreateMap<User, UserAuthenticationResponseDto>();
        }
    }
}
