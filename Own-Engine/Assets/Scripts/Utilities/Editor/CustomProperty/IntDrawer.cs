using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace CustomProperty
{
    [CustomPropertyDrawer(typeof(MyInt))]
    public class IntDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position,label,property);
            
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var inputRect = new Rect(position.x,position.y,position.width,position.height);
            var valueRect = new Rect(position.x,position.y,position.width,position.height);

            EditorGUI.PropertyField(inputRect,property.FindPropertyRelative("intValue"),GUIContent.none);
            // EditorGUI.PropertyField(inputRect,property.FindPropertyRelative("stringValue"),GUIContent.none);

            EditorGUI.LabelField(valueRect,property.FindPropertyRelative("intValue").intValue.ToString());

            EditorGUI.EndProperty();
        }
    }
}
