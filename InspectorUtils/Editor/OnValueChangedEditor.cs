using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InspectorUtils
{
    [CustomPropertyDrawer(typeof(OnValueChangedAttribute))]
    internal sealed class OnValueChangedEditor : PropertyDrawer
    {
        private OnValueChangedCatcherRenderer _renderer;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
	        _renderer ??= new OnValueChangedCatcherRenderer(this, property.serializedObject.targetObject);
            _renderer.OnGUI(position, property, label);
        }
    }

    internal sealed class OnValueChangedCatcherRenderer
    {
	    private readonly MethodInfo[] _methodTargets;
	    private readonly bool _can;
	    private readonly Object _obj;
	    
	    public OnValueChangedCatcherRenderer(PropertyDrawer drawer, Object obj)
        {
	        var targetName = "";
	        if (drawer.attribute is OnValueChangedAttribute valueChanged)
		        targetName = valueChanged.MethodName;
	        if (string.IsNullOrEmpty(targetName))
		        return;
	        _obj = obj;
	        _can = true;
	        var type = obj.GetType();
	        _methodTargets = type.GetMethods(BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public)
			        .Where(s => s.Name == targetName && s.GetParameters().Length==0)
			        .ToArray();
        }

	    public void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
	        EditorGUI.BeginChangeCheck();
	        EditorGUI.PropertyField(position, property, label, property.isExpanded);
	        if (EditorGUI.EndChangeCheck())
	        {
		        if (_can)
			        foreach (var methodTarget in _methodTargets)
				        methodTarget.Invoke(_obj, Array.Empty<object>());
	        }
        }
    }
}