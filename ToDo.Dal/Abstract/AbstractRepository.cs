using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Dal.Abstract
{
    public abstract class AbstractRepository
    {
        private readonly string _connectionString;

        public AbstractRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected DbContext CreateDbContext()
        {
            return new DbContext(_connectionString);
        }
    }
}
