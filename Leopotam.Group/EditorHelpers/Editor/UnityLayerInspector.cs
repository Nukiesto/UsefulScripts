using UnityEditor;
using UnityEngine;

namespace EditorHelpers.Editor {
    /// <summary>
    /// Helper for single layer selector.
    /// </summary>
    [CustomPropertyDrawer (typeof (UnityLayerAttribute))]
    sealed class UnityLayerAttributeInspector : PropertyDrawer {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
            property.intValue = EditorGUI.LayerField (position, label, property.intValue);
        }
    }
}