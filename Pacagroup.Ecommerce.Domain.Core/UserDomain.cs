using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Infrastructure.Interface;

namespace Pacagroup.Ecommerce.Domain.Core
{
    public class UserDomain : IUserDomain
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User Authenticate(string username, string password)
        {
            return _unitOfWork.Users.Authenticate(username, password);
        }

        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.Users.GetAll();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _unitOfWork.Users.GetAllAsync();
        }

        public bool Insert(User entity)
        {
            return _unitOfWork.Users.Insert(entity);
        }

        public async Task<bool> InsertASync(User entity)
        {
            return await _unitOfWork.Users.InsertAsync(entity);
        }
    }
}