using Isometric2DGame.Core;
using UnityEngine;


namespace Isometric2DGame.Enemy {

    public class EnemyAttackState : State<EnemyController> {
        private const float ExitRangeMultiplier = 1.1f;

        private float _cooldownTimer;

        public EnemyAttackState(EnemyController entity) : base(entity) { }

        public override void Enter() {
            Entity.Stop();
            _cooldownTimer = 0f;
        }

        public override void Tick() {
            if (!Entity.CanSeePlayer(Entity.Data.LoseSightRange)) {
                Entity.ChangeState(Entity.Idle);
                return;
            }

            if (Entity.DistanceToTarget() > Entity.Data.AttackRange * ExitRangeMultiplier) {
                Entity.ChangeState(Entity.Chase);
                return;
            }

            _cooldownTimer -= Time.deltaTime;

            if (_cooldownTimer <= 0f) {
                Entity.PerformAttack();
                _cooldownTimer = Entity.Data.AttackCooldown;
            }
        }
    }

}