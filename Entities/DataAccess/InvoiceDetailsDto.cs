using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataAccess
{
    public class InvoiceDetailsDto : BaseClass
    {
        public int InvoiceMasterId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
        public decimal Total { get; set; }



    }


}
