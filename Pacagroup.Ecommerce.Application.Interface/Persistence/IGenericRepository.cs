namespace Pacagroup.Ecommerce.Application.Interface.Persistence
{
    public interface IGenericRepository<T> where T: class
    {
        #region "Metodos Sincronos"
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(string id);
        T Get(string id);
        IEnumerable<T> GetAll();

        #endregion

        #region "Metodos Asincronos"

        Task<bool> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(string id);
        Task<T> GetAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();

        #endregion "Fin Metodos Asincronos"
    }
}