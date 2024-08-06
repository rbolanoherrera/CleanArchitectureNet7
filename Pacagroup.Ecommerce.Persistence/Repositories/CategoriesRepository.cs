using Dapper;
using Pacagroup.Ecommerce.Persistence.Contexts;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using System.Data;
using Pacagroup.Ecommerce.Persistence.Repository;
using Pacagroup.Ecommerce.Domain.Entities;

namespace Pacagroup.Ecommerce.Persistence.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly DapperContext context;

        public CategoriesRepository(DapperContext dapperContext)
        {
            context = dapperContext;
        }

        public IEnumerable<Categories> GetAll()
        {
            using (var connection = context.CreateConnection())
            {
                string query = DatabaseConst.SelectAllCategories;

                var customers = connection.Query<Categories>(sql: query, param: null, commandType: CommandType.Text);

                return customers;
            }
        }
    }
}
