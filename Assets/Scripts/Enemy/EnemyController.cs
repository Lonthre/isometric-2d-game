using Isometric2DGame.Combat;
using UnityEngine;
using Isometric2DGame.Core;

namespace Isometric2DGame.Enemy {
    /// <summary>Manages the enemy AI behaviour through a state machine and an enemy stats config.</summary>
    public class EnemyController : MonoBehaviour {
        [SerializeField] private EnemyConfig _config;
        [SerializeField] private Transform[] _patrolPoints;

        private readonly StateMachine<EnemyController> _stateMachine = new StateMachine<EnemyController>();
        private Rigidbody2D _rigidbody;
        private LayerMask _playerMask;

        public EnemyConfig Config => _config;
        public Transform[] PatrolPoints => _patrolPoints;
        public Transform Target { get; private set; }

        public EnemyIdleState Idle { get; private set; }
        public EnemyPatrolState Patrol { get; private set; }
        public EnemyChaseState Chase { get; private set; }
        public EnemyAttackState Attack { get; private set; }

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
            _playerMask = LayerMask.GetMask("Player");
            Idle = new EnemyIdleState(this);
            Patrol = new EnemyPatrolState(this);
            Chase = new EnemyChaseState(this);
            Attack = new EnemyAttackState(this);
        }

        private void Start() => _stateMachine.ChangeState(Idle);
        private void Update() => _stateMachine.Tick();
        private void FixedUpdate() => _stateMachine.FixedTick();

        public void ChangeState(State<EnemyController> next) => _stateMachine.ChangeState(next);

        public bool CanSeePlayer(float range) {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, range, _playerMask);
            Target = collider != null ? collider.transform : null;
            return Target != null;
        }

        public float DistanceToTarget() => Vector2.Distance(transform.position, Target.position);

        public void MoveToward(Vector2 point, float speed) {
            Vector2 direction = (point - _rigidbody.position).normalized;
            _rigidbody.linearVelocity = direction * speed;
        }

        public void EngageTarget() {
            ChangeState(DistanceToTarget() <= _config.AttackRange ? Attack : Chase);
        }

        public void PerformAttack() {
            Debug.Log("Attacking");

            if (Target.TryGetComponent<IDamageable>(out var damageable)) {
                damageable.TakeDamage(Config.Damage);
            }
        }

        public void Stop() => _rigidbody.linearVelocity = Vector2.zero;
    }
}