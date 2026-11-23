using UnityEngine;


namespace Isometric2DGame.Combat {

    /// <summary>Trigger area which receives hits and forwards them to the owner's Health.</summary>
    [RequireComponent(typeof(Collider2D))]
    public class Hurtbox : MonoBehaviour {
        private Health _owner;

        private void Awake() {
            _owner = GetComponentInParent<Health>();
        }

        public void RegisterDamage(HitInfo hitInfo) {
            _owner.TakeDamage(hitInfo);
        }
    }

}