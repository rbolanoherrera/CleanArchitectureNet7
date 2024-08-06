using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Domain.Entities;
using Pacagroup.Ecommerce.Transversal.Mapper.Base;

namespace Pacagroup.Ecommerce.Transversal.Mapper
{
    public class CustomerBuilder : BuilderBase<Customer, CustomerDTO>
    {
        public override Customer Convert(CustomerDTO param)
        {
            return new Customer()
            {
                CustomerId = param.CustomerId,
                CompanyName = param.CompanyName,
                ContactName = param.ContactName,
                ContactTitle = param.ContactTitle,
                Address = param.Address,
                City = param.City,
                Region = param.Region,
                PostalCode = param.PostalCode,
                Country = param.Country,
                Phone = param.Phone,
                Fax = param.Fax
            };
        }

        public override CustomerDTO Convert(Customer param)
        {
            return new CustomerDTO()
            {
                CustomerId = param.CustomerId,
                CompanyName = param.CompanyName,
                ContactName = param.ContactName,
                ContactTitle = param.ContactTitle,
                Address = param.Address,
                City = param.City,
                Region = param.Region,
                PostalCode = param.PostalCode,
                Country = param.Country,
                Phone = param.Phone,
                Fax = param.Fax
            };
        }
    }
}
