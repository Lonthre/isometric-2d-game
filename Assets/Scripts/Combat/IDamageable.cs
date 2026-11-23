namespace Isometric2DGame.Combat {

    /// <summary>Any character/object/item that can receive damage.</summary>
    public interface IDamageable {
        void TakeDamage(HitInfo hit);
    }

}