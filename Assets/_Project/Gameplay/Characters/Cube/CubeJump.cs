using GMDClone.Core;
using System;
using UnityEngine;

namespace GMDClone.Gameplay.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CubeJump : MonoBehaviour
    {
        public event Action JumpEvent;

        [Header(InspectorHeader.References)]
        [SerializeField] private CharacterGround _ground;
        [SerializeField] private CharacterGravity _gravity;
        [Header(InspectorHeader.GameplaySettings)]
        [SerializeField, Min(0)] private float _jumpForce = 1.5f;

        private Rigidbody2D _rigidbody;

        private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

        private void FixedUpdate()
        {
            if (InputReader.Instance.ClickAction.IsPressed() == true && _ground.IsOnGround == true)
            {
                _rigidbody.linearVelocity = Mathf.Sqrt(_jumpForce * 2f * _gravity.Value) * Vector2.up;
                JumpEvent?.Invoke(); //There is calling the event once only because there is fine
            }
        }
    }
}