using System;
using UnityEngine;

namespace InspectorUtils
{
    [AttributeUsage(AttributeTargets.Field)]
    public class OnValueChangedAttribute : PropertyAttribute
    {
        public string MethodName { get; private set; }

        public OnValueChangedAttribute(string methodName)
        {
            MethodName = methodName;
        }
    }
}