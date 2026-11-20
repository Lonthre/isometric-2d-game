using UnityEngine;

namespace Isometric2DGame.Player {
    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour {
        [SerializeField, Min(0f)] private float _speed = 4f;
        [SerializeField] private bool _hasInstantAcceleration = true;
        [SerializeField, Min(0f)] private float _accelaration = 2f;

        private Rigidbody2D _rigidbody;
        private PlayerInput _input;
        // private Transform _spritesTransform;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
            _input = GetComponent<PlayerInput>();
            // _spritesTransform = GetComponentInChildren<Transform>();
        }

        private void FixedUpdate() {
            Vector2 direction = Vector2.ClampMagnitude(_input.MoveInput, 1f);

            _rigidbody.linearVelocity = direction * _speed;
        }
    }
}