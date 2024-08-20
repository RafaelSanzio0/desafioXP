using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Management.Domain.Entities
{
    public class PortfolioEntity : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FinancialProductId { get; set; }
        public int Quantity { get; set; }
        public decimal AveragePrice { get; set; }

        [NotMapped]
        public string ProductName { get; set; }

        public PortfolioEntity() : base()
        {
        }
    }
}