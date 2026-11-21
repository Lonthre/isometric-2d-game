using UnityEngine;

namespace Isometric2DGame.Enemy {
    /// <summary>Configurable stats for an enemy type.</summary>
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Scriptable Objects/EnemyConfig")]
    public class EnemyConfig : ScriptableObject {
        [Header("Detection")] [Tooltip("Distance at which the enemy notices the player.")] [SerializeField, Min(0f)]
        private float _detectRange = 5f;

        [Tooltip(
            "Distance at which the enemy loses the player. Keep larger than Detect Range for smooth state transitions.")]
        [SerializeField, Min(0f)]
        private float _loseSightRange = 7f;

        [Header("Combat")] [SerializeField, Min(0)]
        private int _damage = 10;

        [SerializeField, Min(0f)] private float _attackRange = 1f;

        [SerializeField, Min(0f)] private float _attackCooldown = 1f;

        [Header("Movement")] [SerializeField, Min(0f)]
        private float _patrolSpeed = 1.5f;

        [SerializeField, Min(0f)] private float _chaseSpeed = 3f;

        [Header("Behavior")] [Tooltip("Time(seconds) spent idle before patrolling.")] [SerializeField, Min(0f)]
        private float _idleTime = 2f;

        [Tooltip("Distance to a patrol point that already counts as target reached.")] [SerializeField, Min(0.01f)]
        private float _patrolPointTolerance = 0.1f;

        public float DetectRange => _detectRange;
        public float LoseSightRange => _loseSightRange;
        public int Damage => _damage;
        public float AttackRange => _attackRange;
        public float AttackCooldown => _attackCooldown;
        public float PatrolSpeed => _patrolSpeed;
        public float ChaseSpeed => _chaseSpeed;
        public float IdleTime => _idleTime;
        public float PatrolPointTolerance => _patrolPointTolerance;
    }
}