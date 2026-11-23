using Isometric2DGame.Core;


namespace Isometric2DGame.Player {

    public class PlayerIdleState : State<PlayerController> {
        public PlayerIdleState(PlayerController entity) : base(entity) { }

        public override void Enter() {
            Entity.Movement.Stop();
        }

        public override void Tick() {
            if (Entity.Input.AttackPressed) {
                Entity.ChangeState(Entity.Attack);
            }

            if (Entity.Input.HasMoveInput) {
                Entity.ChangeState(Entity.Move);
            }
        }
    }

}