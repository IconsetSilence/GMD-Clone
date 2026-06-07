using System;
using UnityEngine;

namespace GMDClone.Gameplay.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovement : MonoBehaviour
    {
        public event Action<SpeedMode> SetSpeedEvent;

        [SerializeField] private SpeedMode _speedMode = SpeedMode.Normal;
        [Header("Speed")]
        [SerializeField, Min(0)] private float _low;
        [SerializeField, Min(0)] private float _normal, _medium, _high;

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
                _currentSpeed = value switch
                {
                    SpeedMode.Low => _low,
                    SpeedMode.Normal => _normal,
                    SpeedMode.Medium => _medium,
                    SpeedMode.High => _high,
                    _ => default
                };

                SetSpeedEvent?.Invoke(value);
            }
        }

        public enum SpeedMode { Low, Normal, Medium, High }

        private void OnValidate() => Speed = _speedMode;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _currentSpeed = _normal;
        }

        //There is using the property "position" from the rigidbody in order for the character`s speed doesn`t depent on gravity
        //If use MovePosition instead of position, gravity will break down (the character won`t be able to fall by gravity
        private void FixedUpdate() => _rigidbody.position += _currentSpeed * Time.fixedDeltaTime * Vector2.right;
    }
}