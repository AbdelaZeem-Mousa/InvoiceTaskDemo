using System.Collections.Generic;

namespace Entities.DataAccess
{
    public class Store:BaseClass
    {
        public string Name { get; set; }
        public virtual ICollection<InvoiceMaster> InvoiceMasters { get; set; }
    }


}
