using System.Collections.Generic;
using System.Linq;

namespace UsefulScripts.NetScripts
{
    public static class EnumerablesS
    {
        /// <summary>
        ///     Удаляет пустые элементы из List<
        /// </summary>
        /// <param name="list">Лист</param>
        /// <typeparam name="T">Тип</typeparam>
        public static void RemoveNull<T>(this List<T> list)
        {
            var listDouble = list.ToList();
            var list2 = new List<int>();
            list.RemoveNull();

            for (var i = 0; i < listDouble.Count; i++)
                if (listDouble[i] == null)
                    list.RemoveAt(i);
        }

        public static void RemoveNull<T>(this IEnumerable<T> enumerable, out IEnumerable<T> outEnum)
        {
            var arr = enumerable.ToArray();
            var listDouble = arr.ToList();
            var outEnumList = arr.ToList();
            for (var i = 0; i < listDouble.Count; i++)
                if (listDouble[i] == null)
                    outEnumList.RemoveAt(i);

            outEnum = outEnumList;
        }
    }
}