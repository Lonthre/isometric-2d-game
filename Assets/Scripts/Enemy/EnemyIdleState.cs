using Isometric2DGame.Core;

using UnityEngine;


namespace Isometric2DGame.Enemy {

    public class EnemyIdleState : State<EnemyController> {
        private float _timer;

        public EnemyIdleState(EnemyController entity) : base(entity) { }

        public override void Enter() {
            _timer = 0f;
            Entity.Stop();
        }

        public override void Tick() {
            if (Entity.CanSeePlayer(Entity.Data.DetectRange)) {
                Entity.EngageTarget();
                return;
            }

            if (Entity.PatrolPoints.Length < 2) return;

            _timer += Time.deltaTime;

            if (_timer >= Entity.Data.IdleTime) Entity.ChangeState(Entity.Patrol);
        }
    }

}