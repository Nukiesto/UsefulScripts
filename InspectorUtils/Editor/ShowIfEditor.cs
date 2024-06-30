using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InspectorUtils
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    internal sealed class ShowIfEditor : PropertyDrawer
    {
        private ConditionalRenderer	_renderer;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
	        _renderer ??= new ConditionalRenderer(this, property.serializedObject.targetObject);
            return _renderer.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _renderer.OnGUI(position, property, label);
        }
    }

    internal sealed class ConditionalRenderer
    {
	    private const float	EmptyHeight = -2F;
	    
	    private readonly Object _obj;
	    private readonly Type _objType;
	    private readonly PropertyDrawer _drawer;

	    private string _targetName;

	    public	ConditionalRenderer(PropertyDrawer drawer, Object obj)
        {
	        _drawer = drawer;
	        _obj = obj;
            _objType = obj.GetType();
        }
        
        public float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
	        if (_drawer.attribute is ShowIfAttribute showIfAttr)
		        _targetName = showIfAttr.Name;
	        
	        return ToShow(_targetName, _objType, _obj) ? EditorGUI.GetPropertyHeight(property, label, property.isExpanded) : EmptyHeight;
        }

        public void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
	        if (ToShow(_targetName, _objType, _obj))
	        {
		        EditorGUI.BeginChangeCheck();
		        EditorGUI.PropertyField(position, property, label, property.isExpanded);
		        EditorGUI.EndChangeCheck();
	        }
        }

        public static bool ToShow(string name, Type type, Object target)
        {
	        if (string.IsNullOrEmpty(name))
		        return true;
	        var props = type.GetProperties(BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Public);
	        foreach (var propertyInfo in props)
	        {
		        if (propertyInfo.Name == name)
		        {
			        var propValue = propertyInfo.GetValue(target);
			        if (propValue is null or false)
				        return false;
		        }
	        }
	        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Public);
	        foreach (var fieldInfo in fields)
	        {
		        if (fieldInfo.Name == name)
		        {
			        var fieldValue = fieldInfo.GetValue(target);
			        if (fieldValue is null or false)
				        return false;
		        }
	        }

	        var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public);
	        foreach (var methodInfo in methods)
	        {
		        if (methodInfo.Name == name && 
		            methodInfo.GetParameters().Length == 0 &&
		            methodInfo.ReturnType == typeof(bool))
		        {
			        var methodValue = methodInfo.Invoke(target, Array.Empty<object>());
			        if (methodValue is null or false)
				        return false;
		        }
	        }

	        return true;
        }
    }
}