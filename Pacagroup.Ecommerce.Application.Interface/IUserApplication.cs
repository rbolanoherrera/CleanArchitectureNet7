using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.Interface
{
    public interface IUserApplication
    {
        Response<UserDTO> Authenticate(string username, string password);

        Response<IEnumerable<UserDTO>> GetAll();

        Task<Response<IEnumerable<UserDTO>>> GetAllAsync();

        Response<bool> Insert(UserDTO userDTO);

        Task<Response<bool>> InsertASync(UserDTO userDTO);
    }
}