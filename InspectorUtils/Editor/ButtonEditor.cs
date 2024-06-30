using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InspectorUtils
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(Object), true, isFallback = true)]
	internal sealed class ButtonEditor : Editor
	{
		private InternalData _internalData;

		private void OnEnable()
		{
			_internalData ??= new InternalData(target);
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			EditorGUILayout.Space();
		
			for (var i = 0; i < _internalData.ValidMethods.Count; i++)
			{
				var currentButton = _internalData.Buttons[i];
				var method = _internalData.ValidMethods[i];
			
				if (Application.isPlaying && currentButton.HideInPlayMode || !Application.isPlaying && currentButton.HideInEditMode)
					return;

				if (GUILayout.Button(method.Name.SplitPascalCase()))
				{
					method.Invoke(target, null);
					EditorUtility.SetDirty(target);
				}
			}
		}

		private class InternalData
		{
			public List<MethodInfo> ValidMethods;
			public List<ButtonAttribute> Buttons;
		
			public InternalData(Object target)
			{
				Initialize(target, target.GetType());
			}

			private void Initialize(Object target, Type objType)
			{
				ValidMethods = new List<MethodInfo>();
				Buttons = new List<ButtonAttribute>();

				foreach (var method in Utils.GetMethodsWithAttribute(objType, typeof(ButtonAttribute)))
				{
					if (method.ReturnType == typeof(void) && method.GetParameters().Length == 0)
					{
						var attr = Utils.GetAttributeOfMethod<ButtonAttribute>(method);
					
						if (Show())
						{
							ValidMethods.Add(method);
							Buttons.Add(attr);
						}

						bool Show()
						{
							var showAttr = Utils.GetAttributeOfMethod<ShowIfAttribute>(method);

							if (showAttr != null && !string.IsNullOrEmpty(showAttr.Name))
								return ConditionalRenderer.ToShow(showAttr.Name, objType, target);
							return true;
						}
					}
				}
			}
		}
	}

	internal static class Utils
	{
		public static IEnumerable<MethodInfo> GetMethodsWithAttribute(Type classType, Type attributeType)
		{
			return classType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
				.Where(methodInfo => methodInfo.GetCustomAttributes(attributeType, true).Length > 0);
		}

		public static T GetAttributeOfMethod<T>(MethodInfo methodInfo) where T : Attribute
		{
			return methodInfo.GetCustomAttributes(typeof(T), true).Cast<T>().FirstOrDefault();
		}
	}

	internal static class Extensions
	{
		public static string SplitPascalCase(this string input)
		{
			var stringBuilder = new StringBuilder(input.Length);
			stringBuilder.Append(char.ToUpper(input[0]));

			for (var i = 1; i < input.Length; i++)
			{
				var c = input[i];
				if (char.IsUpper(c) && !char.IsUpper(input[i - 1]))
					stringBuilder.Append(' ');
				stringBuilder.Append(c);
			}

			return stringBuilder.ToString();
		}
	}
}