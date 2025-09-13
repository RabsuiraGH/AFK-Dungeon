#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace LA.Gameplay.Abilities.Editor
{
    [CustomPropertyDrawer(typeof(ConditionEntry))]
    public class ConditionEntryDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty conditionProp = property.FindPropertyRelative("_condition");
            SerializedProperty paramsProp = property.FindPropertyRelative("_parameters");

            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, conditionProp);

            if (conditionProp.objectReferenceValue != null)
            {
                AbilityConditionBase condition = conditionProp.objectReferenceValue as AbilityConditionBase;
                if (condition == null)
                    return;

                Type type = condition.GetType().BaseType;

                if (type.IsGenericType)
                {
                    Type paramType = type.GetGenericArguments()[0];

                    if (paramsProp.managedReferenceValue == null ||
                        paramsProp.managedReferenceValue.GetType() != paramType)
                    {
                        paramsProp.managedReferenceValue = Activator.CreateInstance(paramType);
                    }

                    EditorGUI.indentLevel++;
                    position.y += EditorGUIUtility.singleLineHeight;
                    EditorGUI.PropertyField(position, paramsProp, true);
                    EditorGUI.indentLevel--;
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight;
            SerializedProperty conditionProp = property.FindPropertyRelative("_condition");
            SerializedProperty paramsProp = property.FindPropertyRelative("_parameters");

            if (conditionProp.objectReferenceValue != null && paramsProp.managedReferenceValue != null)
            {
                height += EditorGUI.GetPropertyHeight(paramsProp, true);
            }

            return height;
        }
    }
}

#endif