using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UsefulScripts.UnityScripts
{
    public static class ColorS
    {
        public static Color GetMediumColorForSprite(Sprite sprite)
        {
            if (sprite == null)
                return default;
            var texture = sprite.texture;
            var colors = texture.GetPixels();
            var colorDic = new Dictionary<Color, int>();
            foreach (var color in colors)
                if (colorDic.ContainsKey(color))
                    colorDic[color] += 1;
                else
                    colorDic.Add(color, 1);
            var x = colorDic.OrderBy(c => c.Value);
            foreach (var a in x)
                return a.Key;
            return default;
        }

        public static Color GetAverageColorForSprite(Sprite sprite)
        {
            if (sprite == null)
                return default;
            var texture = sprite.texture;
            var colors = texture.GetPixels();
            var color = new Color();
            var colorCount = colors.Length;
            color.r = colors.Select(x => x.r).Sum() / colorCount;
            color.g = colors.Select(x => x.g).Sum() / colorCount;
            color.b = colors.Select(x => x.b).Sum() / colorCount;
            color.a = 1;
            return color;
        }
    }
}