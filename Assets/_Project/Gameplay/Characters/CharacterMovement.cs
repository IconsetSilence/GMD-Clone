using GMDClone.Core;
using GMDClone.ScriptableObjects;
using System;
using UnityEngine;

namespace GMDClone.Gameplay.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovement : MonoBehaviour
    {
        public event Action<SpeedMode> ChangeSpeedEvent;

        [Header(InspectorHeader.References)]
        [SerializeField] private SpeedMode _speedMode = SpeedMode.Normal;
        [SerializeField] private CharacterSpeed _characterSpeed;

        private Rigidbody2D _rigidbody;
        private SpeedMode _currentSpeedMode = SpeedMode.Normal;
        private float _currentSpeed;

        //That so it is possible to set and get the speed without using numbers, but only existing speeds
        public SpeedMode Speed
        {
            get => _currentSpeedMode;
            set
            {
                _currentSpeedMode = value;

                //There is setting the speed and not setting in method Update
                _currentSpeed = _characterSpeed.GetSpeed(value);

                ChangeSpeedEvent?.Invoke(value);
            }
        }

        private void OnValidate() => Speed = _speedMode;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _currentSpeed = _characterSpeed.GetSpeed(SpeedMode.Normal);
        }

        //There is using the property "position" from the rigidbody in order for the character`s speed doesn`t depent on gravity
        //If use MovePosition instead of position, gravity will break down (the character won`t be able to fall by gravity
        private void FixedUpdate() => _rigidbody.position += _currentSpeed * Time.fixedDeltaTime * Vector2.right;
    }
}