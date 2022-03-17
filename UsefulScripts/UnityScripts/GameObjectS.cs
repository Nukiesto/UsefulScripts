using UnityEngine;

namespace UsefulScripts.UnityScripts
{
    public static class GameObjectS
    {
        /// <summary>
        ///     Переключает активность объекта
        /// </summary>
        public static void ToggleActive(this GameObject gameObject)
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }
    }
}