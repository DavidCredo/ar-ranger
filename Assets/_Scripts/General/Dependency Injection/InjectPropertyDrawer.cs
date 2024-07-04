#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ARRanger.DependencyInjection
{
    /// <summary>
    /// Custom property drawer for the InjectAttribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(InjectAttribute))]
    public class InjectPropertyDrawer : PropertyDrawer
    {
        Texture2D injectIcon;
        Texture2D LoadIcon()
        {
            if (injectIcon == null)
            {
                injectIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_Scripts/General/Dependency Injection/Icons/InjectIcon.png");

            }
            return injectIcon;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var icon = LoadIcon();
            var iconRect = new Rect(position.x, position.y, 20, 20);
            position.xMin += 24;

            if (injectIcon != null)

            {
                var savedColor = GUI.color;
                GUI.color = property.objectReferenceValue == null ? savedColor : Color.green;
                GUI.DrawTexture(iconRect, icon);
                GUI.color = savedColor;
            }
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
#endif
