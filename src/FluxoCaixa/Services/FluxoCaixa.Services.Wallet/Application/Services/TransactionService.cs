using AutoMapper;
using FluxoCaixa.Services.Wallet.Application.Dto.Transaction;
using FluxoCaixa.Services.Wallet.Application.Interfaces.Services;
using FluxoCaixa.Services.Wallet.Domain.Interfaces.Repositories;

namespace FluxoCaixa.Services.Wallet.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllAsync(TransactionFilterDto dto)
        {
            var entities = await _repository.GetAllAsync(dto);
            return _mapper.Map<IEnumerable<TransactionDto>>(entities);
        }
    }
}
