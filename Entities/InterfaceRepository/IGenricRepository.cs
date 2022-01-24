using Entities.DataAccess;
using System.Collections.Generic;

namespace Entities.InterfaceRepository
{
    public  interface IGenricRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<InvoiceDetailsDto> GetByMasterId(object id);
        T GetById(object id);
        void Add(T entity);
        void Edit(T entity);
        void Delet(object id);
        void DeletByMasterId(object id);

    }


}
