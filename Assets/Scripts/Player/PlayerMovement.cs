using UnityEngine;


namespace Isometric2DGame.Player {

    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour {
        [SerializeField] [Min(0f)] private float _speed = 4f;
        [SerializeField] private bool _hasInstantAcceleration = true;
        [SerializeField] [Min(0f)] private float _acceleration = 10f;

        private Rigidbody2D _rigidbody;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        /// <summary>Moves toward the target velocity for the given input. Call from FixedUpdate.</summary>
        public void Move(Vector2 input) {
            var direction = Vector2.ClampMagnitude(input, 1f);
            var targetVelocity = direction * _speed;

            _rigidbody.linearVelocity = _hasInstantAcceleration
                ? targetVelocity
                : Vector2.MoveTowards(
                    _rigidbody.linearVelocity,
                    targetVelocity,
                    _acceleration * Time.fixedDeltaTime
                );
        }

        public void Stop() {
            _rigidbody.linearVelocity = Vector2.zero;
        }
    }

}