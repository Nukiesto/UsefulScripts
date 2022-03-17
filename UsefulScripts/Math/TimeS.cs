using UnityEngine;

namespace UsefulScripts.Math
{
    public static class TimeS
    {
        public static string GetMinutes(int seconds)
        {
            var minutes = seconds / 60;
            return minutes.ToString();
        }

        public static string GetTime(float secondsIn)
        {
            //mm:ss
            var seconds = (int) secondsIn;
            var minutes = seconds / 60;
            seconds %= 60;

            minutes = Mathf.Clamp(minutes, 0, 99);

            string App(int t)
            {
                if (t == 0)
                    return "00";
                if (t < 10)
                    return "0" + t;
                return t.ToString();
            }

            return App(minutes) + " : " + App(seconds);
        }
    }
}