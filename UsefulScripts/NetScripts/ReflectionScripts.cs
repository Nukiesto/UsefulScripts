using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UsefulScripts.NetScripts
{
    public static class ReflectionScripts
    {
        public static IEnumerable<FieldInfo> GetFieldsHasAttribute<T>(Type type) where T : Attribute, new()
        {
            return type.GetFields().Where(p =>
                p.GetCustomAttribute<T>() != null &&
                p.GetCustomAttribute<T>().Match(new T()));
        }
    }
}