using AutoMapper;
using FluxoCaixa.Services.Wallet.Application.Dto.Transaction;
using FluxoCaixa.Services.Wallet.Domain.Entities;

namespace FluxoCaixa.Services.Wallet.Application.Mappers
{
    public class EntityXDtoMapper : Profile
    {
        public EntityXDtoMapper()
        {
            CreateMap<Transaction, TransactionDto>();
        }
    }
}
