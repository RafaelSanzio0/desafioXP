using Portfolio.Management.Domain.Enums;

namespace Portfolio.Management.Domain.Entities
{
    public class FinancialProductEntity : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FinancialProductType Type { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpirationDate { get; set; }

        public FinancialProductEntity() : base()
        {
        }
    }
}