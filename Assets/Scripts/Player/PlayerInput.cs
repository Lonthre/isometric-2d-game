using UnityEngine;


namespace Isometric2DGame.Player {

    /// <summary>Wraps the InputActions class and exposes player input.</summary>
    public class PlayerInput : MonoBehaviour {
        private InputActions _actions;

        public Vector2 MoveInput => _actions.Player.Move.ReadValue<Vector2>();
        public Vector2 CursorPosition => _actions.Player.Point.ReadValue<Vector2>();
        public bool AttackPressed => _actions.Player.Attack.WasPressedThisFrame();

        public bool HasMoveInput => MoveInput.sqrMagnitude > 0.01f;

        private void Awake() {
            _actions = new InputActions();
        }

        private void OnEnable() {
            _actions.Player.Enable();
        }

        private void OnDisable() {
            _actions.Player.Disable();
        }

        private void OnDestroy() {
            _actions.Dispose();
        }
    }

}