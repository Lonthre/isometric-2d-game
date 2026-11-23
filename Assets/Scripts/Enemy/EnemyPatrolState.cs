using Isometric2DGame.Core;
using UnityEngine;


namespace Isometric2DGame.Enemy {

    public class EnemyPatrolState : State<EnemyController> {
        private int _index;

        public EnemyPatrolState(EnemyController entity) : base(entity) { }

        private Vector2 CurrentPoint => Entity.PatrolPoints[_index].position;

        public override void Tick() {
            if (Entity.CanSeePlayer(Entity.Data.DetectRange)) {
                Entity.EngageTarget();
                return;
            }

            var distance = Vector2.Distance(Entity.transform.position, CurrentPoint);

            if (distance <= Entity.Data.PatrolPointTolerance) {
                _index = (_index + 1) % Entity.PatrolPoints.Length;
                Entity.ChangeState(Entity.Idle);
            }
        }

        public override void FixedTick() {
            Entity.MoveToward(CurrentPoint, Entity.Data.PatrolSpeed);
        }

        public override void Exit() {
            Entity.Stop();
        }
    }

}