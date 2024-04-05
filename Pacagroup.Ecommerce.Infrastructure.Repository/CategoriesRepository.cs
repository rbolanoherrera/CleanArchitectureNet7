using Dapper;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Infrastructure.Data;
using Pacagroup.Ecommerce.Infrastructure.Interface;
using System.Data;

namespace Pacagroup.Ecommerce.Infrastructure.Repository
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
