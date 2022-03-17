using UnityEngine;

namespace UsefulScripts.UnityScripts
{
    public static class TransformS
    {
        public static void FlipX(this Transform transform, bool isLeft)
        {
            //Отражает объект
            var scale = transform.localScale;
            scale.x = isLeft ? -1 : 1;
            transform.localScale = scale;
        }

        public static void SetRotation(this Transform transform, float angle)
        {
            //Установка вращения трансформа через градусы в 2д плоскости
            transform.rotation = RotationS.GetQuaternion(angle);
        }

        public static void SetScale(this Transform transform, float scale)
        {
            transform.localScale = new Vector3(scale, scale);
        }
    }
}