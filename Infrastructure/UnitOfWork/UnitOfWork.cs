using Entities.InterfaceRepository;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
   public class UnitOfWork<T> :IUnitOfWork<T> where T:class
    {
        private readonly AfakyContext context;
        private IGenricRepository<T> _genricRepository;
        public UnitOfWork(AfakyContext _context)
        {
            context = _context;
        }

        public IGenricRepository<T> genricRepostitory
        {
            get
            {
                return _genricRepository ?? (_genricRepository = new GenricRepository<T>(context));
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
