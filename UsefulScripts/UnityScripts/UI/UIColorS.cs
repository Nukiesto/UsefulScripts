using UnityEngine;
using UnityEngine.UI;

namespace UsefulScripts.UnityScripts.UI
{
    public static class UIColorS
    {
        /// <summary>
        ///     Установка альфа
        /// </summary>
        public static void SetAlpha(this TextMesh graphic, float value)
        {
            var color = graphic.color;
            color.a = value;
            graphic.color = color;
        }

        /// <summary>
        ///     Установка альфа
        /// </summary>
        public static void SetAlpha(this MaskableGraphic graphic, float value)
        {
            var color = graphic.color;
            color.a = value;
            graphic.color = color;
        }
    }
}