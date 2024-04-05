using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.Interface
{
    public interface ICustomerApplication
    {
        #region "Metodos Sincronos"
        Response<bool> Insert(CustomerDTO customer);
        Response<bool> Update(CustomerDTO customer);
        Response<bool> Delete(string customerId);
        Response<CustomerDTO> Get(string customerId);
        Response<IEnumerable<CustomerDTO>> GetAll();

        #endregion

        #region "Metodos Asincronos"

        Task<Response<bool>> InsertAsync(CustomerDTO customer);
        Task<Response<bool>> UpdateAsync(CustomerDTO customer);
        Task<Response<bool>> DeleteAsync(string customerId);
        Task<Response<CustomerDTO>> GetAsync(string customerId);
        Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync();

        #endregion "Fin Metodos Asincronos"
    }
}