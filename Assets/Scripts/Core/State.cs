namespace Isometric2DGame.Core {

    /// <summary>Base class for a state owned by a context of TEntity.</summary>
    public abstract class State<TEntity> {
        protected readonly TEntity Entity;

        protected State(TEntity entity) {
            Entity = entity;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Tick() { }
        public virtual void FixedTick() { }
    }

}