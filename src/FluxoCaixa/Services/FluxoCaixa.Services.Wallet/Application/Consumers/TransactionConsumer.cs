using System.Text.Json;
using FluxoCaixa.Chassis.Logging.Interfaces;
using FluxoCaixa.Chassis.Messaging.Interfaces;
using FluxoCaixa.Services.Wallet.Domain.Entities;
using FluxoCaixa.Services.Wallet.Domain.Interfaces.Repositories;

namespace FluxoCaixa.Services.Wallet.Application.Consumers
{
    public class TransactionConsumer : IConsumer
    {
        private readonly ITransactionRepository _repository;
        private readonly IErrorLogger _logger;

        public TransactionConsumer(ITransactionRepository repository, IErrorLogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnExecuting(string data)
        {
            try
            {
                var transaction = JsonSerializer.Deserialize<Transaction>(data);
                await _repository.AddAsync(transaction);
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "Exception on create a new transaction");
            }
        }

        public string GetKey()
        {
            return nameof(TransactionConsumer);
        }
    }
}
