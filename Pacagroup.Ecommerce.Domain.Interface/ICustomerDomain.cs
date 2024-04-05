using Pacagroup.Ecommerce.Domain.Entity;

namespace Pacagroup.Ecommerce.Domain.Interface
{
    public interface ICustomerDomain
    {
        #region "Metodos Sincronos"
        bool Insert(Customer customer);
        bool Update(Customer customer);
        bool Delete(string customerId);
        Customer Get(string customerId);
        IEnumerable<Customer> GetAll();
        //IEnumerable<Customer> GetAllWithPAgination(int pageNumber, int pageSize);
        //int Count();

        #endregion

        #region "Metodos Asincronos"

        Task<bool> InsertAsync(Customer customer);
        Task<bool> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(string customerId);
        Task<Customer> GetAsync(string customerId);
        Task<IEnumerable<Customer>> GetAllAsync();

        //Task<IEnumerable<Customer>> GetAllWithPAginationAsync(int pageNumber, int pageSize);
        //Task<int> CountAsync();

        #endregion "Fin Metodos Asincronos"
    }
}