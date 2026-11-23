using Isometric2DGame.Core;


namespace Isometric2DGame.Player {

    public class PlayerAttackState : State<PlayerController> {
        public PlayerAttackState(PlayerController entity) : base(entity) { }

        public override void Enter() {
            Entity.Movement.Stop();
            Attack();
        }

        public override void Tick() {
            if (Entity.Input.AttackPressed) {
                Entity.Weapon.QueueAttack();
                Attack();
            }

            if (!Entity.Weapon.IsBusy) {
                Entity.ChangeState(Entity.Idle);
            }
        }

        public override void FixedTick() {
            Entity.Movement.Stop();
        }

        public override void Exit() {
            if (Entity.Weapon.IsBusy) {
                Entity.Weapon.Cancel();
            }
        }

        private void Attack() {
            var direction = Entity.GetAimDirection();
            Entity.SetFacingDirection(direction);
            Entity.Weapon.TryAttack(direction);
        }
    }

}