using System;

namespace UsefulScripts.Math
{
    public static class EnumS
    {
        public static T[] GetEnums<T>() where T : Enum
        {
            return (T[]) Enum.GetValues(typeof(T));
        }
    }
}