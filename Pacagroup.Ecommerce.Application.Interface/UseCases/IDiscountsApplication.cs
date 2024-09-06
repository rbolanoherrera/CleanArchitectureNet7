using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Transversal.Common;
using System.Threading;

namespace Pacagroup.Ecommerce.Application.Interface.UseCases
{
    public interface IDiscountsApplication
    {

        Task<Response<bool>> Create(DiscountDTO discountDTO, CancellationToken cancellationToken = default);
        Task<Response<bool>> Update(DiscountDTO discountDTO, CancellationToken cancellationToken = default);
        Task<Response<bool>> Delete(int id, CancellationToken cancellationToken = default);
        Task<Response<DiscountDTO>> Get(int id, CancellationToken cancellationToken = default);
        Task<Response<List<DiscountDTO>>> GetAll(CancellationToken cancellationToken = default);


        //#region "Metodos Sincronos"

        //Response<DiscountDTO> Get(string id);
        //Response<bool> Delete(string id);
        //Response<IEnumerable<DiscountDTO>> GetAll();
        //Response<bool> Insert(DiscountDTO entity);
        //Response<bool> Update(DiscountDTO entity);


        //#endregion

        //#region "Metodos Asincronos"


        //Task<Response<bool>> DeleteAsync(string id);

        //Task<Response<List<DiscountDTO>>> GetAllAsync(CancellationToken cancellationToken);

        //Task<Response<IEnumerable<DiscountDTO>>> GetAllAsync();

        //Task<Response<DiscountDTO>> GetAsync(int id, CancellationToken cancellationToken);

        //Task<Response<DiscountDTO>> GetAsync(string id);

        //Task<Response<bool>> InsertAsync(DiscountDTO entity);

        //Task<Response<bool>> UpdateAsync(DiscountDTO discount);

        //#endregion
    }
}