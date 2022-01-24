using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataAccess
{
    public class InvoiceMaster :BaseClass
    {
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public DateTime DateInvoice { get; set; }
        public string Notes { get; set; }
        public decimal SumPrice { get; set; }
        public decimal DiscountPrecent { get; set; }
        public decimal DiscountVal { get; set; }
        public decimal Net { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customers Customers { get; set; }
        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }

        public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; }
    }


}
