namespace UsefulScripts.Math
{
    public class StringS
    {
        /// <summary>
        ///     Уменьшает по регистру первую букву в массиве строк
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FirstLower(string str)
        {
            var s = str.Split(' ');

            for (var i = 0; i < s.Length; i++)
                if (s[i].Length > 1)
                    s[i] = s[i].Substring(0, 1).ToLower() + s[i].Substring(1, s[i].Length - 1).ToLower();
                else s[i] = s[i].ToLower();
            return string.Join(" ", s);
        }
    }
}