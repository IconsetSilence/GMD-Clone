using UnityEngine;

namespace GMDClone.Gameplay.Character
{
    public class CharacterGround : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private CharacterGravity _gravity;
        [SerializeField] private LayerMask _groundMask;
        [Header("Gameplay Settings")]
        [SerializeField] private Vector2 _pointOnNormalGravity;
        [SerializeField] private Vector2 _pointOnFlipGravity, _checkSize;
        [Header("Editor Settings")]
        [SerializeField] private bool _showFlipGravity = false;
        [SerializeField] private Color _gizmosColor = new(1f, 0f, 0f, 0.25f);

        private bool _isGameplay;
        private const float DefaultAngle = 90f;

        public bool IsOnGround => Physics2D.OverlapBox(transform.position, _checkSize, DefaultAngle, _groundMask);

        private void OnValidate()
        {
            if (_isGameplay == false) //In order for it works only before lauching the game because it`s for visible from editor only
            {
                if (_showFlipGravity == true)
                    OnFlipGravity();
                else
                    OnNormalGravity();
            }
        }

        private void Awake()
        {
            if (_gravity.FlipGravity == true)
                OnFlipGravity();
            else
                OnNormalGravity();

            _isGameplay = true;
        }

        private void OnEnable()
        {
            _gravity.NormalGravityEvent += OnNormalGravity;
            _gravity.FlipGravityEvent += OnFlipGravity;
        }

        private void OnDisable()
        {
            _gravity.NormalGravityEvent -= OnNormalGravity;
            _gravity.FlipGravityEvent -= OnFlipGravity;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawCube(transform.position, _checkSize);
        }

        //There is using local position and not world position because it needs to be under the character
        private void OnNormalGravity() => transform.localPosition = _pointOnNormalGravity;

        //There is too but it needs to be over the character
        private void OnFlipGravity() => transform.localPosition = _pointOnFlipGravity;
    }
}