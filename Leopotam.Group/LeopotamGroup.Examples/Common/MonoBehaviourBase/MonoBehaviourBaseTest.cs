using System.Collections;
using UnityEngine;

namespace Leopotam.Group.LeopotamGroup.Examples.Common.MonoBehaviourBase {
    public class MonoBehaviourBaseTest : Group.Common.MonoBehaviourBase {
        IEnumerator Start () {
            yield return new WaitForSeconds (1f);

            var sw = new System.Diagnostics.Stopwatch ();
            var T = 1000000;
#pragma warning disable 219
            Transform t;

            sw.Reset ();
            sw.Start ();
            for (int i = 0; i < T; i++) {
                t = transform;
            }
            sw.Stop ();
            Debug.Log (sw.ElapsedTicks + " - patched transform, access from local component");

            sw.Reset ();
            sw.Start ();
            for (int i = 0; i < T; i++) {
                t = CachedTransform;
            }
            sw.Stop ();
            Debug.Log (sw.ElapsedTicks + " - cached to internal field transform, access from local component");

            var c = gameObject.AddComponent<StandardMonoBehaviour> ();
            sw.Reset ();
            sw.Start ();
            for (int i = 0; i < T; i++) {
                t = c.transform;
            }
            sw.Stop ();
            Debug.Log (sw.ElapsedTicks + " - standard transform, access from external component");
#pragma warning restore 219
        }
    }
}