using System;

namespace UsefulScripts.NetScripts
{
    public static class SystemRandomExt
    {
        public static float NextFloat(this Random random)
        {
            return (float) random.NextDouble();
        }
    }
}