#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ARRanger.DependencyInjection
{
    /// <summary>
    /// Custom editor for the Injector component.
    /// </summary>
    [CustomEditor(typeof(Injector))]
    public class InjectorEditor : Editor
    {
        /// <summary>
        /// Displays an icon for injectable classes in the inspector.
        /// </summary>
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Injector injector = (Injector)target;

            if (GUILayout.Button("Validate Dependencies"))
            {
                injector.ValidateDependencies();
            }

            if (GUILayout.Button("Clear All Injectable Fields"))
            {
                injector.ClearDependencies();
                EditorUtility.SetDirty(injector);
            }
        }
    }
}
#endif
