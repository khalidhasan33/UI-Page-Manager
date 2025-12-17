using UnityEngine;
using UnityEditor;
using UIPackage.UI;

namespace UIPackage.Editor
{
    [CustomPropertyDrawer(typeof(Required))]
    public class RequiredPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label);

            if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null)
            {
                GUILayout.Space(30);

                Rect warningRect = new Rect(position.x, position.y + position.height * 1.3f, position.width, position.height * 1.3f);

                EditorGUI.HelpBox(warningRect, "This field is required!", MessageType.Error);
            }
        }
    }
}
