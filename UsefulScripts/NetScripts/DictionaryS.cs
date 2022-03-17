using System.Collections;
using System.Collections.Generic;

namespace UsefulScripts.NetScripts
{
    public static class DictionaryS
    {
        /// <summary>
        ///     Поиск ключа по значению
        /// </summary>
        public static bool TryGetKey(this IDictionary dictionary, object value, out object key)
        {
            foreach (var keyFinded in dictionary.Keys)
                if (dictionary[keyFinded] == value)
                {
                    key = keyFinded;
                    return true;
                }

            key = default;
            return false;
        }

        /// <summary>
        ///     Поиск ключа по значению
        /// </summary>
        public static bool TryGetKey<T>(this Dictionary<T, object> dictionary, object value, out T key)
        {
            foreach (var keyPair in dictionary)
                if (keyPair.Value == value)
                {
                    key = keyPair.Key;
                    return true;
                }

            key = default;
            return false;
        }
    }
}