using Dapper;
using Pacagroup.Ecommerce.Persistence.Contexts;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using System.Data;
using Pacagroup.Ecommerce.Domain.Entities;

namespace Pacagroup.Ecommerce.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext context;

        public UserRepository(DapperContext context)
        {
            this.context = context;
        }

        public User Authenticate(string username, string password)
        {
            using (var connection = context.CreateConnection())
            {
                string query = "UsersGetByUserAndPassword";
                var parameters = new DynamicParameters();
                parameters.Add("username", username);
                parameters.Add("password", password);

                User user = connection.QuerySingle<User>(query, param: parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public User Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            using (var connection = context.CreateConnection())
            {
                string query = "select UserId, FirstName, LastName, UserName, Password from Users order by firstName";

                var customers = connection.Query<User>(sql: query, param: null, commandType: CommandType.Text);

                return customers;
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (var connection = context.CreateConnection())
            {
                string query = "select UserId, FirstName, LastName, UserName, Password from Users order by firstName";

                var customers = await connection.QueryAsync<User>(sql: query, param: null, commandType: CommandType.Text);

                return customers;
            }
        }

        public Task<User> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(User entity)
        {
            using (var connection = context.CreateConnection())
            {
                var query = "CreateUsers";
                var parameters = new DynamicParameters();

                parameters.Add("@FirstName", entity.FirstName);
                parameters.Add("@LastName", entity.LastName);
                parameters.Add("@UserName", entity.UserName);
                parameters.Add("@Password", entity.Password);

                var result = connection.Execute(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<bool> InsertAsync(User entity)
        {
            using (var connection = context.CreateConnection())
            {
                var query = "CreateUsers";
                var parameters = new DynamicParameters();

                parameters.Add("@FirstName", entity.FirstName);
                parameters.Add("@LastName", entity.LastName);
                parameters.Add("@UserName", entity.UserName);
                parameters.Add("@Password", entity.Password);

                var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}