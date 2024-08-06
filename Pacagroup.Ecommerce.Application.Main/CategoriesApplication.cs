using Microsoft.Extensions.Caching.Distributed;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Application.Interface.UseCases;
using Pacagroup.Ecommerce.Transversal.Common;
using Pacagroup.Ecommerce.Transversal.Mapper;
using System.Text;
using System.Text.Json;

namespace Pacagroup.Ecommerce.Application.UseCases
{
    public class CategoriesApplication : ICategoriesApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CategoriesBuilder categoriesBuilder;
        private readonly CategoriesForListBuilder categoriesForListBuilder;
        private readonly IDistributedCache distributedCache;
        private readonly IAppLogger<CategoriesApplication> logger;
        private readonly string cacheKey = "categoriesKey";

        public CategoriesApplication(IUnitOfWork unitOfWork, 
            CategoriesBuilder categoriesBuilder,
            CategoriesForListBuilder categoriesForListBuilder,
            IDistributedCache distributedCache,
            IAppLogger<CategoriesApplication> logger)
        {
            this._unitOfWork = unitOfWork;
            this.categoriesBuilder = categoriesBuilder;
            this.categoriesForListBuilder = categoriesForListBuilder;
            this.distributedCache = distributedCache;
            this.logger = logger;
        }

        public Response<IEnumerable<CategoriesDTO>> GetAll()
        {
            Response<IEnumerable<CategoriesDTO>> response = new Response<IEnumerable<CategoriesDTO>>();
            try
            {
                var categories = _unitOfWork.Categories.GetAll();


                if (categories != null && categories.Count() > 0)
                {
                    response.IsSuccess = true;
                    response.Message = "Categorias Obtenidas exitosamente";
                    response.Data = categoriesBuilder.Convert(categories.ToList());
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No se obtuvieron las Categorias";
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error en metodo GetAll. {ex.Message}", ex);

                response.IsSuccess = false;
                response.Message = $"Ocurrio un error al obtener las Categorias. {ex.Message}";
            }

            return response;
        }

        /// <summary>
        /// Listado de las categorias pero sin el campo imagen para listar en un select/bombox box
        /// </summary>
        /// <returns></returns>
        public async Task<Response<IEnumerable<CategoriesListDTO>>> GetForList()
        {
            Response<IEnumerable<CategoriesListDTO>> response = new Response<IEnumerable<CategoriesListDTO>>();

            try
            {
                var categoriesCache = await distributedCache.GetAsync(cacheKey);

                if (categoriesCache != null)
                {
                    response.Data = JsonSerializer.Deserialize<IEnumerable<CategoriesListDTO>>(categoriesCache);

                    response.IsSuccess = true;
                    response.Message = "Categorias Obtenidas exitosamente";
                }
                else
                {
                    var categories = _unitOfWork.Categories.GetAll();

                    if (categories != null && categories.Count() > 0)
                    {
                        var categoriesListDTO = categoriesForListBuilder.Convert(categories.ToList());

                        response.Data = categoriesListDTO;

                        var options = new DistributedCacheEntryOptions()
                            .SetAbsoluteExpiration(DateTime.Now.AddHours(8))
                            .SetSlidingExpiration(TimeSpan.FromMinutes(20));

                        await distributedCache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(categoriesListDTO)), options);

                        response.IsSuccess = true;
                        response.Message = "Categorias Obtenidas exitosamente";

                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "No se obtuvieron las Categorias";
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error en metodo GetAll. {ex.Message}", ex);

                response.IsSuccess = false;
                response.Message = $"Ocurrio un error al obtener las Categorias. {ex.Message}";
            }

            return response;
        }
    }
}