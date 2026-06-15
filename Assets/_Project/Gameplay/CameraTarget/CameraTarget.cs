using GMDClone.Core;
using UnityEngine;

namespace GMDClone.Gameplay
{
    public class CameraTarget : MonoBehaviour
    {
        [Header(InspectorHeader.References)]
        [SerializeField] private Transform _character;
        [Header(InspectorHeader.GameplaySettings)]
        [SerializeField] private Vector3 _offset = Vector3.one;
        [SerializeField, Min(0)] private float _smoothMovement = 0.5f, _maxSpeed = 5f;

        private Transform _transform;
        private float _velocityAxisY, _heightInStaticState;

        public bool StaticState { get; private set; }

        private void Awake() => _transform = transform;

        private void LateUpdate()
        {
            if (ReferenceEquals(_character, null) == false)
            {
                Vector3 target = _character.position + _offset;
                float targetAxisY = StaticState == true ? _heightInStaticState : (_character.position + _offset).y;

                target.y = Mathf.SmoothDamp(_transform.position.y, targetAxisY, ref _velocityAxisY, _smoothMovement, _maxSpeed);
                _transform.position = target;
            }
        }

        public void BecomeStatic(float height)
        {
            StaticState = true;
            _heightInStaticState = height;
        }

        public void QuitStaticState() => StaticState = false;
    }
}