namespace FluxoCaixa.Chassis.Persistence.Common
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
