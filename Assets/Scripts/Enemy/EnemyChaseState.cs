using Isometric2DGame.Core;

namespace Isometric2DGame.Enemy {
    public class EnemyChaseState : State<EnemyController> {
        public EnemyChaseState(EnemyController entity) : base(entity) { }

        public override void Tick() {
            if (!Entity.CanSeePlayer(Entity.Config.LoseSightRange)) {
                Entity.ChangeState(Entity.Idle);
            }
            else if (Entity.DistanceToTarget() <= Entity.Config.AttackRange) {
                Entity.ChangeState(Entity.Attack);
            }
        }

        public override void FixedTick() {
            if (Entity.Target != null) {
                Entity.MoveToward(Entity.Target.position, Entity.Config.ChaseSpeed);
            }
        }

        public override void Exit() => Entity.Stop();
    }
}