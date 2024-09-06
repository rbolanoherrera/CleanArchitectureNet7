using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Application.Interface.UseCases;
using Pacagroup.Ecommerce.Application.Validator;
using Pacagroup.Ecommerce.Transversal.Common;
using Pacagroup.Ecommerce.Transversal.Mapper;

namespace Pacagroup.Ecommerce.Application.UseCases.Discounts
{
    public class DiscountApplication : IDiscountsApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DiscountBuilder builder;
        private readonly DiscountDtoValidator validations;
        //private readonly IMapper mapper;
        private readonly IAppLogger<DiscountApplication> logger;

        public DiscountApplication(IUnitOfWork unitOfWork,
            DiscountBuilder builder,
            DiscountDtoValidator validations,
            //IMapper mapper,
            IAppLogger<DiscountApplication> logger)
        {
            this._unitOfWork = unitOfWork;
            this.builder = builder;
            this.validations = validations;
            //this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<Response<bool>> Create(DiscountDTO discountDTO, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var validation = await validations.ValidateAsync(discountDTO, cancellationToken);

                if (!validation.IsValid)
                {
                    response.Message = "Errores de validación";
                    response.Errros = validation.Errors;
                    response.IsSuccess = false;
                    return response;
                }

                //var discount = mapper.Map<Discount>(discountDTO);
                var discount = builder.Convert(discountDTO);
                await _unitOfWork.Discounts.InsertAsync(discount);

                response.Data = await _unitOfWork.Save(cancellationToken) > 0;

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Descuento Registrado exitosamente";
                }
                else
                    response.Message = "El Descuento no pudo ser registrado";
            }
            catch (Exception ex)
            {
                logger.LogError($"Error en metodo Insert descuento. {ex.Message}", ex);

                response.Data = false;
                response.IsSuccess = false;
                response.Message = $"Ocurrio un error al insertar el Descuento. {ex.Message}";
            }

            return response;
        }

        public async Task<Response<bool>> Update(DiscountDTO discountDTO, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var validation = await validations.ValidateAsync(discountDTO, cancellationToken);

                if (!validation.IsValid)
                {
                    response.Message = "Errores de validación";
                    response.Errros = validation.Errors;
                    response.IsSuccess = false;
                    return response;
                }

                //var discount = mapper.Map<Discount>(discountDTO);
                var discount = builder.Convert(discountDTO);
                await _unitOfWork.Discounts.UpdateAsync(discount);

                response.Data = await _unitOfWork.Save(cancellationToken) > 0;

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Descuento Actualizado exitosamente";
                }
                else
                    response.Message = "El Descuento no pudo ser actualizado";
            }
            catch (Exception ex)
            {
                logger.LogError($"Error en metodo Update descuento. {ex.Message}", ex);

                response.Data = false;
                response.IsSuccess = false;
                response.Message = $"Ocurrio un error al actualizar el Descuento. {ex.Message}";
            }

            return response;
        }

        public async Task<Response<bool>> Delete(int id, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            try
            {
                await _unitOfWork.Discounts.DeleteAsync(id.ToString());
                response.Data = await _unitOfWork.Save(cancellationToken) > 0;

                if(response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Descuento eliminado exitosamente!";
                }
                else
                    response.Message = "El Descuento no pudo ser eliminado!";
            }
            catch(Exception ex)
            {
                logger.LogError($"Error en metodo Eliminar descuento. {ex.Message}", ex);

                response.Data = false;
                response.IsSuccess = false;
                response.Message = $"Ocurrio un error al eliminar el Descuento. {ex.Message}";
            }

            return response;
        }

        public async Task<Response<DiscountDTO>> Get(int id, CancellationToken cancellationToken = default)
        {
            Response<DiscountDTO> response = new Response<DiscountDTO>();
            try
            {
                var discount = await _unitOfWork.Discounts.GetAsync(id, cancellationToken);

                if (discount != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Descuentos Obtenidos exitosamente";
                    response.Data = builder.Convert(discount);
                    //response.Data = mapper.Map<DiscountDTO>(discount);
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No se obtuvieron los Descuentos";
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error en metodo Get descuentos. {ex.Message}", ex);

                response.IsSuccess = false;
                response.Message = $"Ocurrio un error al obtener los descuentos. {ex.Message}";
            }

            return response;
        }

        public async Task<Response<List<DiscountDTO>>> GetAll(CancellationToken cancellationToken = default)
        {
            Response<List<DiscountDTO>> response = new Response<List<DiscountDTO>>();
            try
            {
                var discounts = await _unitOfWork.Discounts.GetAllAsync(cancellationToken);

                if (discounts != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Descuentos Obtenidos exitosamente";
                    response.Data = builder.Convert(discounts);
                    //response.Data = mapper.Map<List<DiscountDTO>>(discounts);
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No se obtuvo el listado de los Descuentos";
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error en metodo GetAll descuentos. {ex.Message}", ex);

                response.IsSuccess = false;
                response.Message = $"Ocurrio un error al obtener el listado de los descuentos. {ex.Message}";
            }

            return response;
        }



        //public Response<bool> Delete(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Response<bool>> DeleteAsync(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Response<DiscountDTO> Get(string id)
        //{
        //    Response<DiscountDTO> response = new Response<DiscountDTO>();
        //    try
        //    {
        //        var discount = _unitOfWork.Discounts.Get(id);

        //        if (discount != null)
        //        {
        //            response.IsSuccess = true;
        //            response.Message = "Descuentos Obtenidos exitosamente";
        //            response.Data = builder.Convert(discount);
        //        }
        //        else
        //        {
        //            response.IsSuccess = false;
        //            response.Message = "No se obtuvieron los Descuentos";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError($"Error en metodo Getl. {ex.Message}", ex);

        //        response.IsSuccess = false;
        //        response.Message = $"Ocurrio un error al obtener los descuentos. {ex.Message}";
        //    }

        //    return response;
        //}

        //public Response<IEnumerable<DiscountDTO>> GetAll()
        //{
        //    Response<IEnumerable<DiscountDTO>> response = new Response<IEnumerable<DiscountDTO>>();
        //    try
        //    {
        //        var discounts = _unitOfWork.Discounts.GetAll();

        //        if (discounts != null && discounts.Count() > 0)
        //        {
        //            response.IsSuccess = true;
        //            response.Message = "Descuentos Obtenidos exitosamente";
        //            response.Data = builder.Convert(discounts.ToList());
        //        }
        //        else
        //        {
        //            response.IsSuccess = false;
        //            response.Message = "No se obtuvieron los Descuentos";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError($"Error en metodo GetAll. {ex.Message}", ex);

        //        response.IsSuccess = false;
        //        response.Message = $"Ocurrio un error al obtener los descuentos. {ex.Message}";
        //    }

        //    return response;
        //}

        //public async Task<Response<IEnumerable<DiscountDTO>>> GetAllAsync(CancellationToken cancellationToken)
        //{
        //    Response<IEnumerable<DiscountDTO>> response = new Response<IEnumerable<DiscountDTO>>();
        //    try
        //    {
        //        var discounts = await _unitOfWork.Discounts.GetAllAsync(cancellationToken);

        //        if (discounts != null && discounts.Count() > 0)
        //        {
        //            response.IsSuccess = true;
        //            response.Message = "Descuentos Obtenidos exitosamente";
        //            response.Data = builder.Convert(discounts.ToList());
        //        }
        //        else
        //        {
        //            response.IsSuccess = false;
        //            response.Message = "No se obtuvieron los Descuentos";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError($"Error en metodo GetAll. {ex.Message}", ex);

        //        response.IsSuccess = false;
        //        response.Message = $"Ocurrio un error al obtener los descuentos. {ex.Message}";
        //    }

        //    return response;
        //}

        //public async Task<Response<IEnumerable<DiscountDTO>>> GetAllAsync()
        //{
        //    Response<IEnumerable<DiscountDTO>> response = new Response<IEnumerable<DiscountDTO>>();
        //    try
        //    {
        //        var discounts = await _unitOfWork.Discounts.GetAllAsync();

        //        if (discounts != null && discounts.Count() > 0)
        //        {
        //            response.IsSuccess = true;
        //            response.Message = "Descuentos Obtenidos exitosamente";
        //            response.Data = builder.Convert(discounts.ToList());
        //        }
        //        else
        //        {
        //            response.IsSuccess = false;
        //            response.Message = "No se obtuvieron los Descuentos";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError($"Error en metodo GetAll. {ex.Message}", ex);

        //        response.IsSuccess = false;
        //        response.Message = $"Ocurrio un error al obtener los descuentos. {ex.Message}";
        //    }

        //    return response;
        //}

        //public async Task<Response<DiscountDTO>> GetAsync(int id, CancellationToken cancellationToken)
        //{
        //    Response<DiscountDTO> response = new Response<DiscountDTO>();
        //    try
        //    {
        //        var discount = await _unitOfWork.Discounts.GetAsync(id, cancellationToken);

        //        if (discount != null)
        //        {
        //            response.IsSuccess = true;
        //            response.Message = "Descuentos Obtenidos exitosamente";
        //            response.Data = builder.Convert(discount);
        //        }
        //        else
        //        {
        //            response.IsSuccess = false;
        //            response.Message = "No se obtuvieron los Descuentos";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError($"Error en metodo Getl. {ex.Message}", ex);

        //        response.IsSuccess = false;
        //        response.Message = $"Ocurrio un error al obtener los descuentos. {ex.Message}";
        //    }

        //    return response;
        //}

        //public async Task<Response<DiscountDTO>> GetAsync(string id)
        //{
        //    Response<DiscountDTO> response = new Response<DiscountDTO>();
        //    try
        //    {
        //        var discount = await _unitOfWork.Discounts.GetAsync(id);

        //        if (discount != null)
        //        {
        //            response.IsSuccess = true;
        //            response.Message = "Descuentos Obtenidos exitosamente";
        //            response.Data = builder.Convert(discount);
        //        }
        //        else
        //        {
        //            response.IsSuccess = false;
        //            response.Message = "No se obtuvieron los Descuentos";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError($"Error en metodo Getl. {ex.Message}", ex);

        //        response.IsSuccess = false;
        //        response.Message = $"Ocurrio un error al obtener los descuentos. {ex.Message}";
        //    }

        //    return response;
        //}

        //public Response<bool> Insert(DiscountDTO entity)
        //{
        //    var response = new Response<bool>();

        //    try
        //    {
        //        var discount = builder.Convert(entity);
        //        response.Data = _unitOfWork.Discounts.Insert(discount);

        //        if (response.Data)
        //        {
        //            response.IsSuccess = true;
        //            response.Message = "Descuento Registrado exitosamente";
        //        }
        //        else
        //            response.Message = "El Descuento no pudo ser registrado";
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError($"Error en metodo Insert. {ex.Message}", ex);

        //        response.Data = false;
        //        response.IsSuccess = false;
        //        response.Message = $"Ocurrio un error al insertar el Descuento. {ex.Message}";
        //    }

        //    return response;
        //}

        //public async Task<Response<bool>> InsertAsync(DiscountDTO entity)
        //{
        //    var response = new Response<bool>();

        //    try
        //    {
        //        var discount = builder.Convert(entity);
        //        response.Data = await _unitOfWork.Discounts.InsertAsync(discount);

        //        if (response.Data)
        //        {
        //            response.IsSuccess = true;
        //            response.Message = "Descuento Registrado exitosamente";
        //        }
        //        else
        //            response.Message = "El Descuento no pudo ser registrado";
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError($"Error en metodo Insert. {ex.Message}", ex);

        //        response.Data = false;
        //        response.IsSuccess = false;
        //        response.Message = $"Ocurrio un error al insertar el Descuento. {ex.Message}";
        //    }

        //    return response;
        //}

        //public Response<bool> Update(DiscountDTO entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Response<bool>> UpdateAsync(DiscountDTO discount)
        //{
        //    throw new NotImplementedException();
        //}
    }
}