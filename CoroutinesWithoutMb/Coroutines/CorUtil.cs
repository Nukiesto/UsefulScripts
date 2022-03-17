using System.Collections;
using UnityEngine;

namespace Plugins.CoroutinesWithoutMb.Coroutines
{
    public static class CorUtil
    {
        public static IEnumerator WaitForSecondsRealtime(float time)
        {
            var start = Time.realtimeSinceStartup;

            while (Time.realtimeSinceStartup - start < time)
                yield return null;

            yield return null;
        }

        public static IEnumerator WaitForSeconds(float time)
        {
            var start = Time.timeSinceLevelLoad;

            while (Time.timeSinceLevelLoad - start < time)
                yield return null;

            yield return null;
        }
    }
}