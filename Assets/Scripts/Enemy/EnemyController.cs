using UnityEngine;
using Isometric2DGame.Combat;
using Isometric2DGame.Core;


namespace Isometric2DGame.Enemy {

    /// <summary>Manages the enemy AI behaviour through a state machine and an enemy stats data.</summary>
    public class EnemyController : MonoBehaviour {
        [SerializeField] private EnemyData data;
        [SerializeField] private Transform[] _patrolPoints;

        private readonly StateMachine<EnemyController> _stateMachine = new();
        private LayerMask _playerMask;
        private Rigidbody2D _rigidbody;

        public EnemyData Data => data;
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

        private void Start() {
            _stateMachine.ChangeState(Idle);
        }

        private void Update() {
            _stateMachine.Tick();
        }

        private void FixedUpdate() {
            _stateMachine.FixedTick();
        }

        public void ChangeState(State<EnemyController> next) {
            _stateMachine.ChangeState(next);
        }

        public bool CanSeePlayer(float range) {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, range, _playerMask);
            Target = hit != null && hit.attachedRigidbody != null
                ? hit.attachedRigidbody.transform
                : null;

            return Target != null;
        }

        public float DistanceToTarget() {
            return Vector2.Distance(transform.position, Target.position);
        }

        public void MoveToward(Vector2 point, float speed) {
            var direction = (point - _rigidbody.position).normalized;
            _rigidbody.linearVelocity = direction * speed;
        }

        public void EngageTarget() {
            ChangeState(DistanceToTarget() <= data.AttackRange ? Attack : Chase);
        }

        public void PerformAttack() {
            Vector2 direction = (Target.position - transform.position).normalized;

            if (Target.TryGetComponent<IDamageable>(out var damageable)) {
                damageable.TakeDamage(new HitInfo(Data.Damage, direction, gameObject));
            }
        }

        public void Stop() {
            _rigidbody.linearVelocity = Vector2.zero;
        }
    }

}