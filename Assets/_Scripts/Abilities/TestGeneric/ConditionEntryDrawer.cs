#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ConditionEntry))]
public class ConditionEntryDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var conditionProp = property.FindPropertyRelative("condition");
        var paramsProp = property.FindPropertyRelative("parameters");

        // Рисуем сам condition
        position.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(position, conditionProp);

        // Если condition выбран
        if (conditionProp.objectReferenceValue != null)
        {
            var condition = conditionProp.objectReferenceValue as AbilityCondition;
            if (condition == null)
                return;

            // Узнаем generic параметр
            var type = condition.GetType().BaseType;
            if (type.IsGenericType)
            {
                var paramType = type.GetGenericArguments()[0];

                // если parameters ещё пуст
                if (paramsProp.managedReferenceValue == null ||
                    paramsProp.managedReferenceValue.GetType() != paramType)
                {
                    paramsProp.managedReferenceValue = Activator.CreateInstance(paramType);
                }

                // Рисуем параметры как foldout
                EditorGUI.indentLevel++;
                position.y += EditorGUIUtility.singleLineHeight + 2;
                EditorGUI.PropertyField(position, paramsProp, true);
                EditorGUI.indentLevel--;
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight + 4;
        var conditionProp = property.FindPropertyRelative("condition");
        var paramsProp = property.FindPropertyRelative("parameters");

        if (conditionProp.objectReferenceValue != null && paramsProp.managedReferenceValue != null)
        {
            height += EditorGUI.GetPropertyHeight(paramsProp, true);
        }

        return height;
    }
}
#endif