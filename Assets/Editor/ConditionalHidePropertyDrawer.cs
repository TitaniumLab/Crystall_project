using UnityEditor;
using UnityEngine;

namespace CrystalProject.Internal
{
    [CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
    public class ConditionalHidePropertyDrawer : PropertyDrawer
    {
        private readonly string _warningText = $"Attempting to use a {nameof(ConditionalHideAttribute)} but no matching {nameof(SerializedProperty)} found in object: ";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
            bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

            bool wasEnabled = GUI.enabled;
            GUI.enabled = enabled;
            if (!condHAtt.HideInInspector || enabled)
                EditorGUI.PropertyField(position, property, label, true);

            GUI.enabled = wasEnabled;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
            bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

            if (!condHAtt.HideInInspector || enabled)
                return EditorGUI.GetPropertyHeight(property, label);
            else
                return -EditorGUIUtility.standardVerticalSpacing;
        }

        private bool GetConditionalHideAttributeResult(ConditionalHideAttribute condHAtt, SerializedProperty property)
        {
            bool enabled = true;
            string propertyPath = property.propertyPath; //returns the property path of the property we want to apply the attribute to
            string conditionPath = propertyPath.Replace(property.name, condHAtt.ConditionalSourceField); //changes the path to the conditionalsource property path
            SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

            if (sourcePropertyValue != null)
                enabled = sourcePropertyValue.boolValue;
            else
                Debug.LogWarning(_warningText + condHAtt.ConditionalSourceField);

            return enabled;
        }
    }
}