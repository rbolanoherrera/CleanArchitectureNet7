using Pacagroup.Ecommerce.Application.DTO;
using FluentValidation;

namespace Pacagroup.Ecommerce.Application.Validator
{
    public class UserDtoValidator : AbstractValidator<UserDTO>
    {
        public UserDtoValidator()
        {
            RuleFor(u => u.UserName).NotNull().NotEmpty().WithMessage("El Usuario no puede ser null o vacio");
            RuleFor(u => u.Password).NotNull().NotEmpty();
        }
    }
}
