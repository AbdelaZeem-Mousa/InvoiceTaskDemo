using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataAccess;

namespace Infrastructure
{
   public class AfakyContext :DbContext
    {
        public AfakyContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Store> Stores { get; set; }

        public virtual DbSet<InvoiceMaster> InvoiceMasters { get; set; }
        public virtual DbSet<vInvoiceMaster> vInvoiceMaster { get; set; }
        public virtual DbSet<InvoiceDetails> InvoiceDetails { get; set; }


    }
}
