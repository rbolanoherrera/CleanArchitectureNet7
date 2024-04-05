using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Transversal.Mapper.Base;

namespace Pacagroup.Ecommerce.Transversal.Mapper
{
    public class UserBuilder : BuilderBase<User, UserDTO>
    {
        public override User Convert(UserDTO param)
        {
            return new User()
            {
                UserId = param.UserId,
                UserName = param.UserName,
                FirstName = param.FirstName,
                LastName = param.LastName,
                Password = param.Password
            };
        }

        public override UserDTO Convert(User param)
        {
            return new UserDTO()
            {
                UserId = param.UserId,
                UserName = param.UserName,
                FirstName = param.FirstName,
                LastName = param.LastName,
                Password = param.Password
            };
        }
    }
}