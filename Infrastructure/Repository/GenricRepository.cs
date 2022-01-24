using Entities.DataAccess;
using Entities.InterfaceRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class GenricRepository<T> : IGenricRepository<T> where T : class
    {
        private readonly AfakyContext context;
        private DbSet<T> table = null;

        public GenricRepository(AfakyContext _context)
        {
            context = _context;
            table = context.Set<T>();
        }
        public void Add(T entity)
        {
            table.Add(entity);
        }

        public void Delet(object id)
        {
            T Deleted = GetById(id);
            table.Remove(Deleted);
        }

        public void DeletByMasterId(object id)
        {
            int PkId = Convert.ToInt32(id);
            var details = context.InvoiceDetails.Where(q => q.InvoiceMasterId == PkId);


            context.RemoveRange(details);
        }

        public void Edit(T entity)
        {
            table.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;

        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public IEnumerable<InvoiceDetailsDto> GetByMasterId(object id)
        {
            int PkId = Convert.ToInt32(id);
            var details = context.InvoiceDetails.Where(q => q.InvoiceMasterId == PkId)
                                .Join(context.Products, i => i.ProductId, p => p.Id, (i, p) => new
                                {
                                    Id = i.Id,
                                    InvoiceMasterId = i.InvoiceMasterId,
                                    ProductId = i.ProductId,
                                    Price = i.Price,
                                    ProductName = p.Name,
                                    Qty = i.Qty,
                                    Total = i.Total
                                });
            List<InvoiceDetailsDto> list = new List<InvoiceDetailsDto>();
            foreach (var item in details)
            {
                list.Add(new InvoiceDetailsDto
                {
                    Id = item.Id,
                    InvoiceMasterId = item.InvoiceMasterId,
                    ProductId = item.ProductId,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    Qty = item.Qty,
                    Total = item.Total
                });
            }
            return list;

        }
    }

    
}
