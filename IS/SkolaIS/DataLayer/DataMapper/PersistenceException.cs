using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Database
{
    public class PersistenceException : ApplicationException
    {
        public PersistenceException(string message) : base(message)
        {

        }
    }
}
