using System.Collections;
using UnityEngine;

namespace Leopotam.Group.LeopotamGroup.Examples.Common.Yields {
    public class YieldsTest : MonoBehaviour {
        IEnumerator Start () {
            // warming up. new instance with 1 second delay will be allocated here.
            yield return Group.Common.Yields.WaitForSeconds (1f);
            var startTime = Time.time;

            // no new allocations - reuse pooled instance.
            yield return Group.Common.Yields.WaitForSeconds (1f);
            // should be around 1.
            Debug.Log (Time.time - startTime);

            // no new allocations - reuse pooled instance.
            yield return Group.Common.Yields.WaitForSeconds (1f);
            // should be around 2.
            Debug.Log (Time.time - startTime);

            // new instance with 2 seconds delay will be allocated here.
            yield return Group.Common.Yields.WaitForSeconds (2f);
            // should be around 4.
            Debug.Log (Time.time - startTime);

            // case for checking that it works without GC allocations - you can open profiler and check it.
            StartCoroutine (OnTest1 ());
            while (true) {
                // Debug.Log("at start");

                // no new allocations - reuse pooled instance.
                yield return Group.Common.Yields.WaitForSeconds (2f);
            }

        }

        IEnumerator OnTest1 () {
            while (true) {
                // Debug.Log("at ontest1");

                // no new allocations - reuse pooled instance.
                yield return Group.Common.Yields.WaitForSeconds (1f);
            }
        }
    }
}