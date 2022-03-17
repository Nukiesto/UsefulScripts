using UsefulScripts.NetScripts.Data;

namespace UsefulScripts.NetScripts
{
    public static class SizeObjectS
    {
        /// <summary>
        ///     Вычисляет размер объекта в байтах
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns>Размер объекта в байтах</returns>
        public static int GetSizeOfObject<T>(T obj) where T : class
        {
            var serializeObject = ByteConvert.SerializeObject<T>(obj);
            return serializeObject.Length;
        }
    }
}