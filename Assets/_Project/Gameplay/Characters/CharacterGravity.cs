using System;
using UnityEngine;

namespace GMDClone.Gameplay.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterGravity : MonoBehaviour
    {
        public event Action FlipGravityEvent, NormalGravityEvent;

        [Header("Reference")]
        [SerializeField] private CharacterGround _ground;
        [Header("Gameplay Settings")]
        [SerializeField, Min(0)] private float _gravity = 10f;
        [SerializeField, Min(0)] private float _maxDistanceDelta = 0.4f; //In order to make the smooth on change the gravity
        [SerializeField] private bool _flipGravity = false;

        private Rigidbody2D _rigidbody;
        private Vector2 _direction = Vector2.up;
        private bool _currentFlipGravity; //Field indicates which direction to fall. If true, the character falls up and if false, down

        public bool FlipGravity
        {
            get => _currentFlipGravity;
            set
            {
                _currentFlipGravity = value;

                //There is setting the direction of the gravity and not setting in method Update
                _direction = value == true ? Vector2.up : Vector2.down;

                //There is calling the events on set the property and not in the another places
                if (value == true)
                    FlipGravityEvent?.Invoke();
                else
                    NormalGravityEvent?.Invoke();
            }
        }

        public float Value => _gravity;

        private void OnValidate() => FlipGravity = _flipGravity;

        private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

        private void FixedUpdate()
        {
            //The character begins fall when it isn`t on the ground in order to overload the game
            if (_ground.IsOnGround == false)
                _rigidbody.linearVelocity = Vector2.MoveTowards(_rigidbody.linearVelocity, _gravity * _direction, _maxDistanceDelta);
        }
    }
}