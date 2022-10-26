namespace FluxoCaixa.Services.CashIn.Applications.Dto.Order
{
    public class OrderCreateDto
    {
        public long Number { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
    }
}
