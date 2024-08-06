using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Application.Interface.UseCases;
using Pacagroup.Ecommerce.Application.Validator;
using Pacagroup.Ecommerce.Domain.Entities;
using Pacagroup.Ecommerce.Transversal.Common;
using Pacagroup.Ecommerce.Transversal.Mapper;

namespace Pacagroup.Ecommerce.Application.UseCases
{
    public class UserApplication : IUserApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserBuilder userBuilder;
        private readonly IAppLogger<UserApplication> logger;
        private readonly UserDtoValidator userValidator;

        public UserApplication(IUnitOfWork unitOfWork, 
            UserBuilder userBuilder, 
            IAppLogger<UserApplication> logger,
            UserDtoValidator userValidator)
        {
            _unitOfWork = unitOfWork;
            this.userBuilder = userBuilder;
            this.logger = logger;
            this.userValidator = userValidator;
        }

        public Response<UserDTO> Authenticate(string username, string password)
        {
            var response = new Response<UserDTO>();

            var validator = userValidator.Validate(new UserDTO() { UserName = username, Password = password });

            if (!validator.IsValid)
            {
                response.Message = "Errores de Validación del objeto";
                response.Errros = validator.Errors;

                return response;
            }

            try
            {
                User user = _unitOfWork.Users.Authenticate(username, password);

                if (user != null)
                {
                    response.IsSuccess = true;
                    response.Data = userBuilder.Convert(user);
                    response.Message = "Autenticación de Usuario exitosa";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Data = null;
                    response.Message = "Usuario o contraseña Invalidos";
                }
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = true;
                response.Message = "Usuario no existe";
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                logger.LogError($"Error en metodo Authenticate. {ex.Message}");
            }

            return response;
        }

        public Response<IEnumerable<UserDTO>> GetAll()
        {
            Response<IEnumerable<UserDTO>> response = new Response<IEnumerable<UserDTO>>();

            try
            {
                var users = _unitOfWork.Users.GetAll();

                if (users != null)
                {
                    response.IsSuccess= true;
                    response.Message = "Usuarios obtenidos correctamente";
                    response.Data = userBuilder.Convert(users.ToList());
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Error al obtener el listado de Usuarios";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                logger.LogError($"Error en metodo User GetAll. {ex.Message}");
            }

            return response;
        }

        public async Task<Response<IEnumerable<UserDTO>>> GetAllAsync()
        {
            Response<IEnumerable<UserDTO>> response = new Response<IEnumerable<UserDTO>>();

            try
            {
                var users = await _unitOfWork.Users.GetAllAsync();

                if (users != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Usuarios obtenidos correctamente";
                    response.Data = userBuilder.Convert(users.ToList());
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Error al obtener el listado de Usuarios";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                logger.LogError($"Error en metodo User GetAllAsync. {ex.Message}");
            }

            return response;
        }

        public Response<bool> Insert(UserDTO userDTO)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                User user = userBuilder.Convert(userDTO);
                response.Data = _unitOfWork.Users.Insert(user);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Usuario Registrado exitosamente";
                }
                else
                    response.Message = "El Usuario no pudo ser registrado";
            }
            catch (Exception ex)
            {
                logger.LogError($"Error en metodo User Insert. {ex.Message}", ex);

                response.Data = false;
                response.IsSuccess = false;
                response.Message = $"Ocurrio un error al insertar al Usuario. {ex.Message}";
            }

            return response;
        }

        public async Task<Response<bool>> InsertASync(UserDTO userDTO)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                User user = userBuilder.Convert(userDTO);
                response.Data = await _unitOfWork.Users.InsertAsync(user);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Usuario Registrado exitosamente";
                }
                else
                    response.Message = "El Usuario no pudo ser registrado";
            }
            catch (Exception ex)
            {
                logger.LogError($"Error en metodo User Insert. {ex.Message}", ex);

                response.Data = false;
                response.IsSuccess = false;
                response.Message = $"Ocurrio un error al insertar al Usuario. {ex.Message}";
            }

            return response;
        }

    }
}
