using GMDClone.Input;
using UnityEngine.InputSystem;

namespace GMDClone.Core
{
    public class InputReader : Singleton<InputReader>
    {
        private InputActionSystem _inputActionSystem;

        public InputAction ClickAction { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _inputActionSystem = new InputActionSystem();
            ClickAction = _inputActionSystem.Player.Click;
        }

        private void OnEnable() => _inputActionSystem.Enable();

        private void OnDisable() => _inputActionSystem.Disable();
    }
}