using UnityEngine;

namespace UsefulScripts.UnityScripts
{
    public static class RotationS
    {
        /// <summary>
        ///     Получение угла между двумя точками
        /// </summary>
        public static float GetAngle(Vector3 fromPos, Vector3 toPos)
        {
            toPos.z = 0;
            var direction = toPos - fromPos;

            return GetAngle(direction);
        }

        /// <summary>
        ///     Преобразование направления в угл в градусах
        /// </summary>
        public static float GetAngle(Vector3 direction)
        {
            return Mathf.RoundToInt(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        }

        /// <summary>
        ///     Преобразование градусов в квартенионы в 2д плоскости
        /// </summary>
        public static Quaternion GetQuaternion(float angle)
        {
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }

        /// <summary>
        ///     Получение угла из квартениона
        /// </summary>
        public static float GetAngle(Quaternion quaternion)
        {
            return quaternion.eulerAngles.z;
        }

        /// <summary>
        ///     Получение направления из угла
        /// </summary>
        public static Vector3 GetDirection(float angle)
        {
            var rad = angle * Mathf.Deg2Rad;
            var direction = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);

            return direction;
        }
    }
}