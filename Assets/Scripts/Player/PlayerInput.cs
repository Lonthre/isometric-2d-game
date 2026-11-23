using UnityEngine;


namespace Isometric2DGame.Player {

    /// <summary>Wraps the InputActions class and exposes player input.</summary>
    public class PlayerInput : MonoBehaviour {
        private InputActions _controls;

        public Vector2 MoveInput => _controls.Player.Move.ReadValue<Vector2>();

        private void Awake() {
            _controls = new InputActions();
        }

        private void OnEnable() {
            _controls.Player.Enable();
        }

        private void OnDisable() {
            _controls.Player.Disable();
        }

        private void OnDestroy() {
            _controls.Dispose();
        }
    }

}