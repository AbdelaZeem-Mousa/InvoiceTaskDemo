namespace Entities.InterfaceRepository
{
    public interface IUnitOfWork<T> where T : class
    {
        IGenricRepository<T> genricRepostitory { get; }
        void Save();
    }


}
