using GMDClone.Core;
using UnityEditor;
using UnityEngine;
using static UnityEditor.EditorGUILayout;

namespace GMDClone.Gameplay
{
    [CustomEditor(typeof(CameraTarget))]
    public class CameraTargetEditor : Editor
    {
        private SerializedProperty _character, _offset;

        private void OnEnable()
        {
            _character = serializedObject.FindProperty(nameof(_character));
            _offset = serializedObject.FindProperty(nameof(_offset));
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Space(8);
            LabelField(InspectorHeader.EditorSettings, EditorStyles.boldLabel);

            if (GUILayout.Button("Show Target"))
            {
                CameraTarget cameraTarget = (CameraTarget)target;
                Transform characterTransform = (Transform)_character.objectReferenceValue;

                if (characterTransform != null)
                    cameraTarget.transform.position = characterTransform.position + _offset.vector3Value;
                else
                    Debug.LogWarning("You need any character");
            }
        }
    }
}