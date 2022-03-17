using Leopotam.Group.Math;
using UnityEngine;

namespace UsefulScripts.Math
{
    public class MathS
    {
        public static int RaznostNumbers(int x, int y)
        {
            return MathFast.Abs(MathFast.Abs(x) - MathFast.Abs(y));
        }

        /// <summary>
        ///     Пример 1:
        ///     x = 75, max1 = 255, max2 = 1
        ///     return 0.29
        ///     Пример 2:
        ///     x = 127.5, max1 = 255, max2 = 1
        ///     return 0.5
        /// </summary>
        public static float CompressValue(float x, float maxOrigin, float maxTo)
        {
            // Пример 1:
            // x = 75, max1 = 255, max2 = 1
            // return 0.29
            // Пример 2:
            // x = 127.5, max1 = 255, max2 = 1
            // return 0.5
            return x * maxTo / maxOrigin;
        }

        /// <summary>
        ///     Находим стороны четырехугольника с точками pos1 и pos2
        /// </summary>
        /// <param name="pos1">Начальная точка</param>
        /// <param name="pos2">Противоположная точка</param>
        /// <returns></returns>
        public static (float w, float h) GetWidthHeight(Vector3 pos1, Vector3 pos2)
        {
            var w = Get(pos1.x, pos2.x);
            var h = Get(pos1.y, pos2.y);

            return (w, h);

            float Get(float a, float b)
            {
                return a < b ? Mathf.Abs(b - a) : Mathf.Abs(a - b);
            }
        }
    }
}