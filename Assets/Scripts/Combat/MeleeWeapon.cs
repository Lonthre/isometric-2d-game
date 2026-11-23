using UnityEngine;


namespace Isometric2DGame.Combat {

    /// <summary>Melee weapon which damages based on its hitbox during the Active phase of an attack.</summary>
    public class MeleeWeapon : Weapon {
        [SerializeField] private Hitbox _hitbox;

        protected override void OnActivePhaseStart(Vector2 direction) {
            _hitbox.Activate(Data.Damage, direction, transform.root.gameObject);
        }

        protected override void OnActivePhaseEnd() {
            _hitbox.Deactivate();
        }
    }

}