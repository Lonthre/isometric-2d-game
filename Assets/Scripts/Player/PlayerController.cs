using Isometric2DGame.Combat;
using Isometric2DGame.Core;
using UnityEngine;


namespace Isometric2DGame.Player {

    /// <summary>Owns the player's state machine and shared data such as facing direction and aiming point.</summary>
    [RequireComponent(typeof(PlayerInput), typeof(PlayerMovement))]
    public class PlayerController : MonoBehaviour {
        [SerializeField] private Weapon _weapon;

        [Tooltip("Aim is measured from here, e.g. a weapon pivot.")] [SerializeField]
        private Transform _aimOrigin;

        public float MinAimDistanceSqr = 0.0001f;
        private readonly StateMachine<PlayerController> _machine = new();
        private Camera _camera;

        public PlayerInput Input { get; private set; }
        public PlayerMovement Movement { get; private set; }
        public Weapon Weapon => _weapon;
        public Vector2 FacingDirection { get; private set; } = Vector2.down;
        public int FacingIndex => Direction8.ToIndex(FacingDirection);

        public PlayerIdleState Idle { get; private set; }
        public PlayerMoveState Move { get; private set; }
        public PlayerAttackState Attack { get; private set; }

        private void Awake() {
            Input = GetComponent<PlayerInput>();
            Movement = GetComponent<PlayerMovement>();
            _camera = Camera.main;

            Idle = new PlayerIdleState(this);
            Move = new PlayerMoveState(this);
            Attack = new PlayerAttackState(this);
        }

        private void Start() {
            _machine.ChangeState(Move);
        }

        private void Update() {
            _machine.Tick();
        }

        private void FixedUpdate() {
            _machine.FixedTick();
        }

        public void ChangeState(State<PlayerController> next) {
            _machine.ChangeState(next);
        }

        public void SetFacingDirection(Vector2 direction) {
            FacingDirection = Direction8.Snap(direction);
        }

        /// <summary>Snapped vector to an 8-direction from the aim origin to the cursor.</summary>
        public Vector2 GetAimDirection() {
            var world = _camera.ScreenToWorldPoint(Input.CursorPosition);
            var raw = (Vector2)world - (Vector2)_aimOrigin.position;

            return raw.sqrMagnitude > MinAimDistanceSqr ? Direction8.Snap(raw) : FacingDirection;
        }
    }

}