using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Database
{
    class DatabaseException : ApplicationException
    {
        public DatabaseException(string message) : base(message)
        {

        }
    }
}
