using Pacagroup.Ecommerce.Domain.Entity;

namespace Pacagroup.Ecommerce.Infrastructure.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User Authenticate(string username, string password);
    }
}