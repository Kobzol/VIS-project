using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Helper
{
    public static class StringHelper
    {
        public static string FormatWith(this string str, object obj0)
        {
            return String.Format(str, obj0);
        }
        public static string FormatWith(this string str, object obj0, object obj1)
        {
            return String.Format(str, obj0, obj1);
        }
        public static string FormatWith(this string str, object obj0, object obj1, object obj2)
        {
            return String.Format(str, obj0, obj1, obj2);
        }
        public static string FormatWith(this string str, params object[] arguments)
        {
            return String.Format(str, arguments);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return String.IsNullOrEmpty(str);
        }
    }
}
