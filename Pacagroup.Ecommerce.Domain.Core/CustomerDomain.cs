using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Infrastructure.Interface;

namespace Pacagroup.Ecommerce.Domain.Core
{
    public class CustomerDomain : ICustomerDomain
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Delete(string customerId)
        {
            return _unitOfWork.Customers.Delete(customerId);
        }

        public async Task<bool> DeleteAsync(string customerId)
        {
            return await _unitOfWork.Customers.DeleteAsync(customerId);
        }

        public Customer Get(string customerId)
        {
            return _unitOfWork.Customers.Get(customerId);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _unitOfWork.Customers.GetAll();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _unitOfWork.Customers.GetAllAsync();
        }

        public async Task<Customer> GetAsync(string customerId)
        {
            return await _unitOfWork.Customers.GetAsync(customerId);
        }

        public bool Insert(Customer customer)
        {
            return _unitOfWork.Customers.Insert(customer);
        }

        public async Task<bool> InsertAsync(Customer customer)
        {
            return await _unitOfWork.Customers.InsertAsync(customer);
        }

        public bool Update(Customer customer)
        {
            return _unitOfWork.Customers.Update(customer);
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            return await _unitOfWork.Customers.UpdateAsync(customer);
        }
    }
}