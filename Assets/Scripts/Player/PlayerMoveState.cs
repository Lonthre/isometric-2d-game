using Isometric2DGame.Core;


namespace Isometric2DGame.Player {

    public class PlayerMoveState : State<PlayerController> {
        public PlayerMoveState(PlayerController entity) : base(entity) { }

        public override void Tick() {
            if (Entity.Input.HasMoveInput)
                Entity.SetFacingDirection(Entity.Input.MoveInput);
            else
                Entity.ChangeState(Entity.Idle);

            if (Entity.Input.AttackPressed || Entity.Weapon.ConsumeQueuedAttack())
                if (Entity.Weapon.CanAttack)
                    Entity.ChangeState(Entity.Attack);
        }

        public override void FixedTick() {
            Entity.Movement.Move(Entity.Input.MoveInput);
        }
    }

}