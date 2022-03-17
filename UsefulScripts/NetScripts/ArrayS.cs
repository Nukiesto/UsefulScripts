namespace UsefulScripts.NetScripts
{
    public static class ArrayS
    {
        /// <summary>
        ///     Вставка элемента в начало массива
        /// </summary>
        public static T[] AppendToStart<T>(T[] array, T value)
        {
            var result = new T[array.Length + 1];
            result[0] = value;
            for (var i = 0; i < array.Length; i++)
                result[i + 1] = array[i];

            return result;
        }
    }
}