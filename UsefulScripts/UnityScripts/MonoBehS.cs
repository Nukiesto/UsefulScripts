using UnityEngine;

namespace UsefulScripts.UnityScripts
{
    public static class MonoBehS
    {
        // ReSharper disable once RedundantAssignment
        public static T Get<T>(this MonoBehaviour mono, ref T obj)
        {
            return obj = mono.gameObject.GetComponent<T>();
        }
    }
}