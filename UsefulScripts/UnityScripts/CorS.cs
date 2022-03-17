using System;
using System.Collections;
using Plugins.CoroutinesWithoutMb.Coroutines;

namespace UsefulScripts.UnityScripts
{
    public static class CorS
    {
        public static IEnumerator Wait(float secondTime, Action action)
        {
            yield return CorUtil.WaitForSeconds(secondTime);
            action?.Invoke();
        }

        public static IEnumerator InvokeRepeating(float secondTime, Action action)
        {
            while (true)
            {
                yield return CorUtil.WaitForSeconds(secondTime);
                action?.Invoke();
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}