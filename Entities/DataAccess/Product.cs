using System.Collections.Generic;

namespace Entities.DataAccess
{
    public class Product : BaseClass
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; }


    }


}
