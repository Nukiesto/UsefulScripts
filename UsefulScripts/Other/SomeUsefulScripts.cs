using UnityEngine;
using UnityEngine.Events;

namespace UsefulScripts.Other
{
    public class SimpleAsyncOperation
    {
        public bool IsRun;
        public event UnityAction OnStart;
        public event UnityAction OnEnd;
        public event UnityAction<float> OnRun;

        public void StartInvoke()
        {
            IsRun = true;
            OnStart?.Invoke();
        }

        public void EndInvoke()
        {
            IsRun = false;
            OnEnd?.Invoke();
        }

        public void ClearEvents()
        {
            OnStart = null;
            OnEnd = null;
        }

        public void RunInvoke(float progress)
        {
            OnRun?.Invoke(progress);
        }
    }

    public static class SomeUsefulScripts
    {
        /// <summary>
        ///     Переключает бул из одного значения в другое
        /// </summary>
        public static bool Toggle(this ref bool value)
        {
            value = !value;
            return value;
        }
    }

    public static class OtherScripts
    {
        /// <summary>
        ///     Проверка на Null для монобехов
        /// </summary>
        public static bool NotNull(this MonoBehaviour monoBehaviour)
        {
            return !ReferenceEquals(monoBehaviour, null);
            //(object) monoBehaviour != (object) null;
        }

        public static bool IsNull(object value)
        {
            //Проверка на Null
            return value == null; //ReferenceEquals(value, null);
        }
    }
}