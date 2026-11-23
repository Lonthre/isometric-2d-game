using UnityEngine;


namespace Isometric2DGame.Player {

    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour {
        [SerializeField] [Min(0f)] private float _speed = 4f;
        [SerializeField] private bool _hasInstantAcceleration = true;
        [SerializeField] [Min(0f)] private float _acceleration = 10f;
        private PlayerInput _input;

        private Rigidbody2D _rigidbody;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
            _input = GetComponent<PlayerInput>();
        }

        private void FixedUpdate() {
            var direction = Vector2.ClampMagnitude(_input.MoveInput, 1f);
            var targetVelocity = direction * _speed;

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