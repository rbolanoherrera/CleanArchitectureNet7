using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.Interface.UseCases
{
    public interface ICategoriesApplication
    {
        Response<IEnumerable<CategoriesDTO>> GetAll();
        Task<Response<IEnumerable<CategoriesListDTO>>> GetForList();
    }
}