using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

// Wrapper class for IEnumerator objects
// This class is nice because it allows IEnumerator's to return other IEnumerator's just like Unity
// We call it CoRoutine instead of Coroutine to differentiate it from UnityEngine.CoRoutine
namespace SimpleAsync.CoroutinesWithoutMb.SimpleAsync
{
    public class CoRoutine
    {
        private readonly List<IEnumerator> _finished = new List<IEnumerator>();
        private readonly Stack<IEnumerator> _processStack;

        private object _returnValue;

        public CoRoutine(IEnumerator enumerator)
        {
            _processStack = new Stack<IEnumerator>();
            _processStack.Push(enumerator);
        }

        public object ReturnValue
        {
            get
            {
                Assert(!_processStack.Any());
                return _returnValue;
            }
        }

        public bool IsDone => !_processStack.Any();

        // Returns true if it needs to be called again
        public bool Pump()
        {
            Assert(_processStack.Any());
            Assert(_returnValue == null);

            var topWorker = _processStack.Peek();

            bool isFinished;

            try
            {
                isFinished = !topWorker.MoveNext();
            }
            catch (CoRoutineException e)
            {
                var objectTrace = GenerateObjectTrace(_finished.Concat(_processStack));

                if (!objectTrace.Any()) throw e;

                throw new CoRoutineException(objectTrace.Concat(e.ObjectTrace).ToList(), e.InnerException);
            }
            catch (Exception e)
            {
                var objectTrace = GenerateObjectTrace(_finished.Concat(_processStack));

                if (!objectTrace.Any()) throw e;

                throw new CoRoutineException(objectTrace, e);
            }

            if (isFinished) _finished.Add(_processStack.Pop());

            if (topWorker.Current != null && typeof(IEnumerator).IsAssignableFrom(topWorker.Current.GetType()))
                _processStack.Push((IEnumerator) topWorker.Current);

            if (!_processStack.Any()) _returnValue = topWorker.Current;

            return _processStack.Any();
        }

        private static List<Type> GenerateObjectTrace(IEnumerable<IEnumerator> enumerators)
        {
            var objTrace = new List<Type>();

            foreach (var enumerator in enumerators)
            {
                var field = enumerator.GetType().GetField("<>4__this",
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                if (field == null)
                {
                    // Mono seems to use a different name
                    field = enumerator.GetType().GetField("<>f__this",
                        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                    if (field == null) continue;
                }

                var obj = field.GetValue(enumerator);

                if (obj == null) continue;

                var objType = obj.GetType();

                if (!objTrace.Any() || objType != objTrace.Last()) objTrace.Add(objType);
            }

            objTrace.Reverse();
            return objTrace;
        }

        private static void Assert(bool condition)
        {
            if (!condition) throw new AssertException("Assert hit in CoRoutine!");
        }

        private static void Assert(bool condition, string message)
        {
            if (!condition)
                throw new AssertException(
                    "Assert hit in CoRoutine! " + message);
        }

        public class TimeoutException : Exception
        {
        }

        public class AssertException : Exception
        {
            public AssertException(string message)
                : base(message)
            {
            }
        }

        public class CoRoutineException : Exception
        {
            public CoRoutineException(List<Type> objTrace, Exception innerException)
                : base(CreateMessage(objTrace), innerException)
            {
                ObjectTrace = objTrace;
            }

            public List<Type> ObjectTrace { get; }

            private static string CreateMessage(List<Type> objTrace)
            {
                var result = new StringBuilder();

                foreach (var objType in objTrace)
                {
                    if (result.Length != 0) result.Append(" -> ");

                    result.Append(objType.Name);
                }

                result.AppendLine();
                return "Coroutine Object Trace: " + result;
            }
        }
    }
}