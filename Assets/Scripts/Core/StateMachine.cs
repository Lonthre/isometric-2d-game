namespace Isometric2DGame.Core {

    /// <summary>Runs one active state at a time and handles the transition to another.</summary>
    public class StateMachine<TEntity> {
        public State<TEntity> Current { get; private set; }

        public void ChangeState(State<TEntity> next) {
            if (next == null || next == Current) return;

            Current?.Exit();
            Current = next;
            Current.Enter();
        }

        public void Tick() {
            Current?.Tick();
        }

        public void FixedTick() {
            Current?.FixedTick();
        }
    }

}