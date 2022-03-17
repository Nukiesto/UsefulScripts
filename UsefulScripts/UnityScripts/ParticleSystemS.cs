using UnityEngine;

namespace UsefulScripts.UnityScripts
{
    public class ParticleSystemS
    {
        public static void SetStartColor(Color color, ParticleSystem particleSystem)
        {
            var main = particleSystem.main;
            var startColor = main.startColor;
            startColor.color = color;
            main.startColor = startColor;
        }
    }
}