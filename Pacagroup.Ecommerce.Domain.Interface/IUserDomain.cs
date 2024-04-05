using Pacagroup.Ecommerce.Domain.Entity;

namespace Pacagroup.Ecommerce.Domain.Interface
{
    public interface IUserDomain
    {
        User Authenticate(string username, string password);

        IEnumerable<User> GetAll();

        Task<IEnumerable<User>> GetAllAsync();

        bool Insert(User entity);

        Task<bool> InsertASync(User entity);
    }
}