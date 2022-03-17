using LeopotamGroup.Threading;
using UnityEngine;

namespace LeopotamGroup.Examples.ThreadingTest {
    /// <summary>
    /// Custom background worker.
    /// </summary>
    public class BackgroundWorker : BackgroundWorkerBase<BackgroundWorker, int> {
        // optional.
        protected override void OnWorkerStartInBackground () {
            Debug.Log ("[WORKER] Started");
        }

        protected override int OnWorkerTickInBackground (int item) {
            // we work in background thread here.
            Debug.Log ("[WORKER] Processing " + item);
            return item * 2;
        }

        protected override void OnResultFromWorker (int result) {
            // we work in unity thread thread here.
            Debug.Log ("[MAIN] Result from worker: " + result);
        }

        // optional.
        protected override void OnWorkerStopInBackground () {
            Debug.Log ("[WORKER] Stopped");
        }
    }
}