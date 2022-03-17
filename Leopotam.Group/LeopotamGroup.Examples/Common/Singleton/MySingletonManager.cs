using Leopotam.Group.Common;
using UnityEngine;

namespace Leopotam.Group.LeopotamGroup.Examples.Common.Singleton {
    // This singleton can be used at scene "Singleton 1 - From code".
    [MonoBehaviourServiceFilter ("Singleton 1 - From code")]

    // This singleton can be used at any scene with name started from "Singleton 2 - Fro.*".
    // Name of scene can be regular expression.
    [MonoBehaviourServiceFilter ("Singleton 2 - Fro.*")]

    // This singleton cant be used at other scenes due to UnitySingletonAllowedScene attributes were used,
    // exception will be raised on all attempts.
    // validation - editor only and not affect execution flow / performance of standalone builds!
    public class MySingletonManager : MonoBehaviourService<MySingletonManager> {
        [SerializeField]
        string _stringParameter = "String param value";

        protected override void OnCreateService () {
            // Use this overrided method instead of Awake, it will guarantee correct initialization for one instance.
            Debug.Log ("MySingletonManager instance created");
            Debug.Log ("MySingletonManager.StringParameter on start: " + _stringParameter);
        }

        protected override void OnDestroyService () {
            // Use this overrided method instead of OnDestroy, it will guarantee correct dispose for one instance.

            // Dont forget to check UnitySingleton<T>.IsInstanceCreated () at any OnDestroy method (it can be
            // already killed before), otherwise new instance will be created and unity throw exception.

            Debug.Log ("MySingletonManager instance destroyed");
        }

        public void Test () {
            Debug.Log ("MySingletonManager.Test method called");
        }

        public string GetStringParameter () {
            return _stringParameter;
        }
    }
}