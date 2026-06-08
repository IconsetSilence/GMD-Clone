using UnityEngine;
using UnityEditor;

namespace GMDClone.Gameplay.Character
{
    [CustomEditor(typeof(CharacterMovement))]
    public class CharacterMovementEditor : Editor
    {
        private Vector2 _teleportPosition = Vector2.zero;
        private const string KeyTeleportAxisX = "TeleportAxisX", KeyTeleportAxisY = "TeleportAxisY";

        private void OnEnable()
        {
            _teleportPosition.x = EditorPrefs.GetFloat(KeyTeleportAxisX, Vector2.zero.x);
            _teleportPosition.y = EditorPrefs.GetFloat(KeyTeleportAxisY, Vector2.zero.y);
        }

        private void OnDisable()
        {
            EditorPrefs.SetFloat(KeyTeleportAxisX, _teleportPosition.x);
            EditorPrefs.SetFloat(KeyTeleportAxisY, _teleportPosition.y);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space(8);
            EditorGUILayout.LabelField("Editor Settings", EditorStyles.boldLabel);

            _teleportPosition = EditorGUILayout.Vector2Field("Teleport Position", _teleportPosition);

            EditorGUILayout.Space(4);

            if (GUILayout.Button("Teleport Character") == true)
            {
                CharacterMovement characterMovement = (CharacterMovement)target;
                characterMovement.transform.position = _teleportPosition;
            }
        }
    }
}