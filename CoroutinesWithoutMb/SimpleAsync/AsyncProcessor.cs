using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SimpleAsync
{
    public class AsyncProcessor
    {
        private readonly List<CoroutineInfo> _newWorkers = new List<CoroutineInfo>();
        private readonly LinkedList<CoroutineInfo> _workers = new LinkedList<CoroutineInfo>();
        
        public bool IsRunning => _workers.Any() || _newWorkers.Any();

        public void Tick()
        {
            AddNewWorkers();

            if (!_workers.Any())
                return;

            AdvanceFrameAll();
            AddNewWorkers();
        }

        public void Clear()
        {
            _workers.Clear();
        }

        public IEnumerator Process(IEnumerator process, out string guid)
        {
            return ProcessInternal(process, out guid);
        }

        public IEnumerator Process(IEnumerator process)
        {
            return ProcessInternal(process, out var guid);
        }

        private void AdvanceFrameAll()
        {
            var currentNode = _workers.First;

            while (currentNode != null)
            {
                var next = currentNode.Next;
                var worker = currentNode.Value;

                try
                {
                    worker.CoRoutine.Pump();
                    worker.IsFinished = worker.CoRoutine.IsDone;
                }
                catch (Exception e)
                {
                    worker.IsFinished = true;
                    Debug.LogException(e);
                }

                if (worker.IsFinished) _workers.Remove(currentNode);

                currentNode = next;
            }
        }

        private IEnumerator ProcessInternal(IEnumerator process, out string guid)
        {
            guid = Guid.NewGuid().ToString();
            var data = new CoroutineInfo
            {
                CoRoutine = new CoRoutine(process),
                Guid = guid
            };

            _newWorkers.Add(data);

            return WaitUntilFinished(data);
        }

        private IEnumerator WaitUntilFinished(CoroutineInfo workerData)
        {
            while (!workerData.IsFinished) yield return null;
        }

        private void AddNewWorkers()
        {
            foreach (var worker in _newWorkers) _workers.AddLast(worker);
            _newWorkers.Clear();
        }

        public void StopCoroutine(string guid)
        {
            var cor = _workers.ToList().Find(c => c.Guid == guid);
            if (_workers.Contains(cor))
                _workers.Remove(cor);
        }

        private class CoroutineInfo
        {
            public CoRoutine CoRoutine;
            public string Guid;
            public bool IsFinished;
        }
    }
}