using UnityEngine;

namespace Isometric2DGame.Player {
    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour {
        [SerializeField, Min(0f)] private float _speed = 4f;
        [SerializeField] private bool _hasInstantAcceleration = true;
        [SerializeField, Min(0f)] private float _acceleration = 10f;

        private Rigidbody2D _rigidbody;
        private PlayerInput _input;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
            _input = GetComponent<PlayerInput>();
        }

        private void FixedUpdate() {
            Vector2 direction = Vector2.ClampMagnitude(_input.MoveInput, 1f);
            Vector2 targetVelocity = direction * _speed;
            
            if (_hasInstantAcceleration) {
                _rigidbody.linearVelocity = targetVelocity;
                return;
            }
            
            _rigidbody.linearVelocity = Vector2.MoveTowards(
                _rigidbody.linearVelocity,
                targetVelocity,
                _acceleration * Time.fixedDeltaTime);
        }
    }
}