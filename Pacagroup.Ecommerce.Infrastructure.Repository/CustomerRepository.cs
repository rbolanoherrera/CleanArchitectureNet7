using Dapper;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Infrastructure.Data;
using Pacagroup.Ecommerce.Infrastructure.Interface;
using System.Data;

namespace Pacagroup.Ecommerce.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DapperContext context;

        public CustomerRepository(DapperContext context)
        {
            this.context = context;
        }

        public bool Delete(string customerId)
        {
            using (var con = context.CreateConnection())
            {
                var query = "CustomersDelete";
                var parameters = new DynamicParameters();

                parameters.Add("CustomerID", customerId);

                var result = con.Execute(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<bool> DeleteAsync(string customerId)
        {
            using (var con = context.CreateConnection())
            {
                var query = "CustomersDelete";
                var parameters = new DynamicParameters();

                parameters.Add("CustomerID", customerId);

                var result = await con.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

                return result > 0;
            }
        }

        public Customer Get(string customerId)
        {
            using (var connection = context.CreateConnection())
            {
                string query = "CustomersGetById";
                var parameters = new DynamicParameters();
                parameters.Add("customerId", customerId);

                var customer = connection.QuerySingle<Customer>(sql: query, param: parameters, commandType: CommandType.StoredProcedure);

                return customer;
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            using (var connection = context.CreateConnection())
            {
                string query = "CustomersList";

                var customers = connection.Query<Customer>(sql: query, param: null, commandType: CommandType.StoredProcedure);

                return customers;
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            using (var connection = context.CreateConnection())
            {
                string query = "CustomersList";

                var customers = await connection.QueryAsync<Customer>(sql: query, param: null, commandType: CommandType.StoredProcedure);

                return customers;
            }
        }

        public async Task<Customer> GetAsync(string customerId)
        {
            using (var connection = context.CreateConnection())
            {
                string query = "CustomersGetById";
                var parameters = new DynamicParameters();
                parameters.Add("customerId", customerId);

                var customer = await connection.QuerySingleAsync<Customer>(sql: query, param: parameters, commandType: CommandType.StoredProcedure);

                return customer;
            }
        }

        public bool Insert(Customer customer)
        {
            using(var connection = context.CreateConnection())
            {
                var query = "CustomersInsert";
                var parameters = new DynamicParameters();

                parameters.Add("CustomerId", customer.CustomerId);
                parameters.Add("CompanyName", customer.CompanyName);
                parameters.Add("ContactName", customer.ContactName);
                parameters.Add("ContactTitle", customer.ContactTitle);
                parameters.Add("Address", customer.Address);
                parameters.Add("City", customer.City);
                parameters.Add("Region", customer.Region);
                parameters.Add("PostalCode", customer.PostalCode);
                parameters.Add("Country", customer.Country);
                parameters.Add("Phone", customer.Fax);
                parameters.Add("Fax", customer.Fax);

                var result = connection.Execute(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<bool> InsertAsync(Customer customer)
        {
            using (var con = context.CreateConnection())
            {
                var query = "CustomersInsert";
                var parameters = new DynamicParameters();

                parameters.Add("CustomerID", customer.CustomerId);
                parameters.Add("CompanyName", customer.CompanyName);
                parameters.Add("ContactName", customer.ContactName);
                parameters.Add("ContactTitle", customer.ContactTitle);
                parameters.Add("Address", customer.Address);
                parameters.Add("City", customer.City);
                parameters.Add("Region", customer.Region);
                parameters.Add("PostalCode", customer.PostalCode);
                parameters.Add("Country", customer.Country);
                parameters.Add("Phone", customer.Fax);
                parameters.Add("Fax", customer.Fax);

                var result = await con.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

                return result > 0;
            }
        }

        public bool Update(Customer customer)
        {
            using (var con = context.CreateConnection())
            {
                var query = "CustomersUpdate";
                var parameters = new DynamicParameters();

                parameters.Add("CustomerID", customer.CustomerId);
                parameters.Add("CompanyName", customer.CompanyName);
                parameters.Add("ContactName", customer.ContactName);
                parameters.Add("ContactTitle", customer.ContactTitle);
                parameters.Add("Address", customer.Address);
                parameters.Add("City", customer.City);
                parameters.Add("Region", customer.Region);
                parameters.Add("PostalCode", customer.PostalCode);
                parameters.Add("Country", customer.Country);
                parameters.Add("Phone", customer.Fax);
                parameters.Add("Fax", customer.Fax);

                var result = con.Execute(query, param: parameters, commandType: CommandType.StoredProcedure);

                return result > 0;
            }
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            using (var con = context.CreateConnection())
            {
                var query = "CustomersUpdate";
                var parameters = new DynamicParameters();

                parameters.Add("CustomerID", customer.CustomerId);
                parameters.Add("CompanyName", customer.CompanyName);
                parameters.Add("ContactName", customer.ContactName);
                parameters.Add("ContactTitle", customer.ContactTitle);
                parameters.Add("Address", customer.Address);
                parameters.Add("City", customer.City);
                parameters.Add("Region", customer.Region);
                parameters.Add("PostalCode", customer.PostalCode);
                parameters.Add("Country", customer.Country);
                parameters.Add("Phone", customer.Fax);
                parameters.Add("Fax", customer.Fax);

                var result = await con.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

                return result > 0;
            }
        }
    }
}
