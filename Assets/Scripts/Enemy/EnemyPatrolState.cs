using UnityEngine;
using Isometric2DGame.Core;

namespace Isometric2DGame.Enemy {
    public class EnemyPatrolState : State<EnemyController> {
        private int _index;

        public EnemyPatrolState(EnemyController entity) : base(entity) { }

        private Vector2 CurrentPoint => Entity.PatrolPoints[_index].position;

        public override void Tick() {
            if (Entity.CanSeePlayer(Entity.Config.DetectRange)) {
                Entity.EngageTarget();
                return;
            }

            float distance = Vector2.Distance(Entity.transform.position, CurrentPoint);

            if (distance <= Entity.Config.PatrolPointTolerance) {
                _index = (_index + 1) % Entity.PatrolPoints.Length;
                Entity.ChangeState(Entity.Idle);
            }
        }

        public override void FixedTick() {
            Entity.MoveToward(CurrentPoint, Entity.Config.PatrolSpeed);
        }

        public override void Exit() {
            Entity.Stop();
        }
    }
}