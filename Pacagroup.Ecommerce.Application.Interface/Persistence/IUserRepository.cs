using Pacagroup.Ecommerce.Domain.Entities;

namespace Pacagroup.Ecommerce.Application.Interface.Persistence
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User Authenticate(string username, string password);
    }
}