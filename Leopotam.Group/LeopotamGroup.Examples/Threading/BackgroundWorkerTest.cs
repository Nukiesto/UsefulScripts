using System;
using LeopotamGroup.Common;
using UnityEngine;

namespace LeopotamGroup.Examples.ThreadingTest {
    public class BackgroundWorkerTest : MonoBehaviour {
        [NonSerialized]
        int _iterator;

        void Update () {
            // try to spam worker with test data.
            var worker = Service<BackgroundWorker>.Get ();
            if (worker.IsWorkerStarted) {
                worker.EnqueueItem (_iterator++);
            }
        }
    }
}