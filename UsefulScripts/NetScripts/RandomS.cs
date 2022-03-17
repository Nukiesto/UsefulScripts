using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace UsefulScripts.NetScripts
{
    public static class RandomS
    {
        public static int GetRandomId<T>(this T[] array, int inclusion)
        {
            var list = array.ToList();
            list.RemoveAt(inclusion);
            var c = list.GetRandomId();
            return c;
        }

        public static int GetRandomId<T>(this List<T> list)
        {
            return UnityEngine.Random.Range(0, list.Count);
        }

        public static float GetRandom(this Vector2 vector2)
        {
            return UnityEngine.Random.Range(vector2.x, vector2.y);
        }

        public static float GetRandom(this Vector2 vector2, Random random)
        {
            return random.NextFloat(vector2.x, vector2.y);
        }

        public static int GetRandom(this Vector2Int vector2)
        {
            return UnityEngine.Random.Range(vector2.x, vector2.y);
        }

        public static int GetRandom(this Vector2Int vector2, System.Random random)
        {
            return random.Next(vector2.x, vector2.y);
        }

        public static int GetRandom(this Vector2 vector2, System.Random random)
        {
            return random.Next((int) vector2.x, (int) vector2.y);
        }

        /// <summary>
        ///     Дает случайный элемент массива
        /// </summary>
        public static T GetRandom<T>(this T[] array)
        {
            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        /// <summary>
        ///     Дает случайный элемент листа
        /// </summary>
        public static T GetRandom<T>(this List<T> array)
        {
            return array[UnityEngine.Random.Range(0, array.Count)];
        }

        /// <summary>
        ///     Получить случайный бул в шансе 1 к 2
        /// </summary>
        public static bool RandomBool()
        {
            return UnityEngine.Random.Range(0, 2) == 0;
        }

        /// <summary>
        ///     Получить бул по шансу 1 к n
        /// </summary>
        public static bool RandomBoolChance(int n)
        {
            return UnityEngine.Random.Range(0, n) == 0;
        }

        /// <summary>
        ///     Получить бул по процентному шансу
        /// </summary>
        public static bool RandomBoolProcent(float a)
        {
            return UnityEngine.Random.Range(0f, 101f) < a;
        }

        /// <summary>
        ///     Получить item по в юнитах
        /// </summary>
        public static object Choose(ChanceItem[] vars)
        {
            var list = new List<object>();
            foreach (var item in vars)
                if (RandomBoolChance(item.chance))
                    list.Add(item.item);

            return list.Count > 0 ? list[UnityEngine.Random.Range(0, list.Count)] : vars[0].item;
        }

        [Serializable]
        public struct ChanceItem
        {
            //Юнит шанса, где item - объект, int - шанс в процентах
            public object item;
            public int chance;

            public ChanceItem(object item, int chance)
            {
                this.item = item;
                this.chance = chance;
            }
        }
    }
}