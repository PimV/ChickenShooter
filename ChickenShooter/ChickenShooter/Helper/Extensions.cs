using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.Helper
{
    public static class Extensions
    {
        public static T GetAttribute<T>(this Enum enumValue)
            where T : Attribute
        {
            FieldInfo field = enumValue.GetType().GetField(enumValue.ToString());
            object[] attribs = field.GetCustomAttributes(typeof(T), false);
            T result = default(T);

            if (attribs.Length > 0)
            {
                result = attribs[0] as T;
            }

            return result;
        }
    }
}
