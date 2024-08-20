using Portfolio.Management.Domain.Enums;

namespace Portfolio.Management.Domain.Entities
{
    public class TransactionEntity : BaseEntity
    {
        public int Id { get; set; }
        public int FinancialProductId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public TransactionType Type { get; set; }

        public TransactionEntity() : base()
        {
        }
    }
}