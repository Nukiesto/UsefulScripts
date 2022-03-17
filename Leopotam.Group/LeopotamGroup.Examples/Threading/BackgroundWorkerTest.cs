using System;
using Leopotam.Group.Common;
using UnityEngine;

namespace Leopotam.Group.LeopotamGroup.Examples.Threading {
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