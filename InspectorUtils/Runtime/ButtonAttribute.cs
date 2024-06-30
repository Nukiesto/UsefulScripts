using System;
using UnityEngine;

namespace InspectorUtils
{
	[AttributeUsage(AttributeTargets.Method)]
	public class ButtonAttribute : PropertyAttribute
	{
		public bool HideInPlayMode { get; private set; }
		public bool HideInEditMode { get; private set; }
		
		public ButtonAttribute(bool hideInPlayMode = false, bool hideInEditMode = false)
		{
			HideInPlayMode = hideInPlayMode;
			HideInEditMode = hideInEditMode;
		}
	}
}