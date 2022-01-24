using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Afaky.Models
{
    public class InvoiceDetailsViewModel
    {
        public int Id { get; set; }
        public int InvoiceMasterId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
        public decimal Total { get; set; }

        public string ProductName { get; set; }
    }
}
