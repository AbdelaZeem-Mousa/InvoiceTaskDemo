using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataAccess
{
    public class InvoiceDetails : BaseClass
    {
        public int InvoiceMasterId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
        public decimal Total { get; set; }


        [ForeignKey("InvoiceMasterId")]
        public virtual InvoiceMaster InvoiceMaster { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }


}
