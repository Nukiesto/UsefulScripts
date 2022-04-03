#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace UsefulScripts.UnityScripts
{
    public static class EditorS
    {
        public static void AddAssetToAsset(this Object origin, Object toAdd)
        {
#if UNITY_EDITOR
            AssetDatabase.AddObjectToAsset(toAdd, origin);

            origin.SaveChanges();
#endif
        }

        public static void RemoveAssetFromAsset(this Object origin, Object toRemove)
        {
#if UNITY_EDITOR
            AssetDatabase.RemoveObjectFromAsset(toRemove);
            
            EditorUtility.SetDirty(origin);
            AssetDatabase.SaveAssets();
#endif
        }

        public static void SaveChanges(this Object origin)
        {
#if UNITY_EDITOR     
            EditorUtility.SetDirty(origin);
            AssetDatabase.SaveAssets(origin);
#endif
        }
    }
}
