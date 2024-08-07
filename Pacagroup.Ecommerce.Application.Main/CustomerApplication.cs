﻿using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Application.Interface.UseCases;
using Pacagroup.Ecommerce.Transversal.Common;
using Pacagroup.Ecommerce.Transversal.Mapper;

namespace Pacagroup.Ecommerce.Application.UseCases
{
    public class CustomerApplication : ICustomerApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CustomerBuilder customerBuilder;
        private readonly IAppLogger<CustomerApplication> logger;

        public CustomerApplication(IUnitOfWork unitOfWork, 
            CustomerBuilder customerBuilder,
            IAppLogger<CustomerApplication> logger)
        {
            _unitOfWork = unitOfWork;
            this.customerBuilder = customerBuilder;
            this.logger = logger;
        }

        #region "Metodos Sincronos"
        public Response<bool> Insert(CustomerDTO customerDTO)
        {
            var response = new Response<bool>();

            try
            {
                var customer = customerBuilder.Convert(customerDTO);
                response.Data = _unitOfWork.Customers.Insert(customer);
                
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Cliente Registrado exitosamente";
                }
                else
                    response.Message = "El Cliente no pudo ser registrado";
            }
            catch(Exception ex)
            {
                logger.LogError($"Error en metodo Insert. {ex.Message}", ex);

                response.Data = false;
                response.IsSuccess = false;
                response.Message = $"Ocurrio un error al insertar al Cliente. {ex.Message}";
            }

            return response;
        }

        public Response<bool> Update(CustomerDTO customerDTO)
        {
            var response = new Response<bool>();

            try
            {
                var customer = customerBuilder.Convert(customerDTO);
                response.Data = _unitOfWork.Customers.Update(customer);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Cliente Actualizado exitosamente";
                }
                else
                    response.Message = "El Cliente no pudo actualizado";
            }
            catch (Exception ex)
            {
                logger.LogError($"Error en metodo Update. {ex.Message}", ex);

                response.Data = false;
                response.IsSuccess = false;
                response.Message = $"Ocurrio un error al actualizar un Cliente. {ex.Message}";
            }

            return response;
        }

        public Response<bool> Delete(string customerId)
        {
            var response = new Response<bool>();

            try
            {
                response.Data = _unitOfWork.Customers.Delete(customerId);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Cliente Eliminado exitosamente";
                }
                else
                    response.Message = "El Cliente no pudo eliminado";
            }
            catch (Exception ex)
            {
                logger.LogError($"Error en metodo Delete. {ex.Message}", ex);

                response.Data = false;
                response.IsSuccess = false;
                response.Message = $"Ocurrio un error al eliminar un Cliente. {ex.Message}";
            }

            return response;
        }

        public Response<CustomerDTO> Get(string customerId)
        {
            var response = new Response<CustomerDTO>();

            try
            {
                var customer = _unitOfWork.Customers.Get(customerId);

                if (customer != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Cliente Obtenido exitosamente";
                    response.Data = customerBuilder.Convert(customer);
                }
                else
                    response.Message = "El Cliente no pudo Obtenido";
            }
            catch (Exception ex)
            {
                logger.LogError($"Error en metodo Get. {ex.Message}", ex);

                response.IsSuccess = false;
                response.Message = $"Ocurrio un error al obtener el Cliente. {ex.Message}";
            }

            return response;
        }

        public Response<IEnumerable<CustomerDTO>> GetAll()
        {
            var response = new Response<IEnumerable<CustomerDTO>>();

            logger.LogInformation("entro a metodo GetAll");

            try
            {
                var customer = _unitOfWork.Customers.GetAll();

                if (customer != null && customer.Count() > 0)
                {
                    response.IsSuccess = true;
                    response.Message = "Cliente Obtenido exitosamente";
                    response.Data = customerBuilder.Convert(customer.ToList());
                }
                else
                    response.Message = "El Cliente no pudo Obtenido";
            }
            catch (Exception ex)
            {
                logger.LogError($"Error en metodo GetAll. {ex.Message}", ex);

                response.IsSuccess = false;
                response.Message = $"Ocurrio un error al obtener el Cliente. {ex.Message}";
            }

            return response;
        }

        #endregion

        #region "Metodos Asincronos"

        public async Task<Response<bool>> InsertAsync(CustomerDTO customerDTO)
        {
            var response = new Response<bool>();

            try
            {
                var customer = customerBuilder.Convert(customerDTO);
                response.Data = await _unitOfWork.Customers.InsertAsync(customer);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Cliente Registrado exitosamente";
                }
                else
                    response.Message = "El Cliente no pudo ser registrado";
            }
            catch (Exception ex)
            {
                logger.LogError($"Error en metodo InsertAsync. {ex.Message}", ex);

                response.Data = false;
                response.IsSuccess = false;
                response.Message = $"Ocurrio un error al insertar al Cliente. {ex.Message}";
            }

            return response;
        }

        public async Task<Response<bool>> UpdateAsync(CustomerDTO customerDTO)
        {
            var response = new Response<bool>();

            try
            {
                var customer = customerBuilder.Convert(customerDTO);
                response.Data = await _unitOfWork.Customers.UpdateAsync(customer);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Cliente Actualizado exitosamente";
                }
                else
                    response.Message = "El Cliente no pudo actualizado";
            }
            catch (Exception ex)
            {
                logger.LogError($"Error en metodo UpdateAsync. {ex.Message}", ex);

                response.Data = false;
                response.IsSuccess = false;
                response.Message = $"Ocurrio un error al actualizar un Cliente. {ex.Message}";
            }

            return response;
        }

        public async Task<Response<bool>> DeleteAsync(string customerId)
        {
            var response = new Response<bool>();

            try
            {
                response.Data = await _unitOfWork.Customers.DeleteAsync(customerId);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Cliente Eliminado exitosamente";
                }
                else
                    response.Message = "El Cliente no pudo eliminado";
            }
            catch (Exception ex)
            {
                logger.LogError($"Error en metodo DeleteAsync. {ex.Message}", ex);

                response.Data = false;
                response.IsSuccess = false;
                response.Message = $"Ocurrio un error al eliminar un Cliente. {ex.Message}";
            }

            return response;
        }

        public async Task<Response<CustomerDTO>> GetAsync(string customerId)
        {
            var response = new Response<CustomerDTO>();

            try
            {
                var customer = await _unitOfWork.Customers.GetAsync(customerId);

                if (customer != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Cliente Obtenido exitosamente";
                    response.Data = customerBuilder.Convert(customer);
                }
                else
                    response.Message = "El Cliente no pudo Obtenido";
            }
            catch (Exception ex)
            {
                logger.LogError($"Error en metodo GetAsync. {ex.Message}", ex);

                response.IsSuccess = false;
                response.Message = $"Ocurrio un error al obtener el Cliente. {ex.Message}";
            }

            return response;
        }

        public async Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<CustomerDTO>>();
            logger.LogInformation("entro a metodo GetAllAsync");

            try
            {
                var customer = await _unitOfWork.Customers.GetAllAsync();

                if (customer != null && customer.Count() > 0)
                {
                    response.IsSuccess = true;
                    response.Message = "Cliente Obtenido exitosamente";
                    response.Data = customerBuilder.Convert(customer.ToList());
                }
                else
                    response.Message = "El Cliente no pudo Obtenido";
            }
            catch (Exception ex)
            {
                logger.LogError($"Error en metodo GetAllAsync. {ex.Message}", ex);

                response.IsSuccess = false;
                response.Message = $"Ocurrio un error al obtener el Cliente. {ex.Message}";
            }

            return response;
        }

        #endregion "Fin Metodos Asincronos"
    }
}
