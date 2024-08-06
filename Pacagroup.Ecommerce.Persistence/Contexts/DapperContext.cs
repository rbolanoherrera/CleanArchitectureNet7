using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Pacagroup.Ecommerce.Persistence.Contexts
{
    public class DapperContext
    {
        private readonly IConfiguration configuration;
        private readonly string _connectionString;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public DapperContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            _connectionString = configuration.GetConnectionString("NorthwindConnection")!;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    }
}