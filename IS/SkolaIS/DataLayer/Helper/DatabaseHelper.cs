using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Helper
{
    public static class DatabaseHelper
    {
        public static T GetColumnValue<T>(this SqlDataReader reader, string name)
        {
            if (!reader.HasRows)
            {
                return default(T);
            }

            int index = reader.GetOrdinal(name);

            if (reader.IsDBNull(index))
            {
                return default(T);
            }
            else return reader.GetFieldValue<T>(index);
        }
    }
}
