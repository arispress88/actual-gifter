using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Gifter.Repositories
{
    // Abstract classes are classes that cannot be instantiated. They exist to be inherited by other classes.
    public abstract class BaseRepository
    {
        private readonly string _connectionString;

        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // We mark the Connection property as protected to make it available to child classes, but inaccessible to any other code.
        protected SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }
    }
}