using Leopotam.Group.Common;
using UnityEngine;

namespace Leopotam.Group.LeopotamGroup.Examples.Common.Singleton {
    public class SingletonTest : MonoBehaviour {
        void Start () {
            Service<MySingletonManager>.Get ().Test ();
            Debug.Log ("MySingletonManager.GetStringParameter: " + Service<MySingletonManager>.Get ().GetStringParameter ());
        }

        void OnDestroy () {
            // Dont forget to check Service<T>.IsRegistered at any OnDestroy method (it can be
            // already killed before, execution order not defined), otherwise new instance of singleton class
            // will be created and unity throw exception about it.
            if (Service<MySingletonManager>.IsRegistered) {
                Debug.Log ("MySingletonManager still alive!");
            } else {
                Debug.Log ("MySingletonManager already killed!");
            }
        }
    }
}