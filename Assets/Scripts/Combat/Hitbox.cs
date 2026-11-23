using System.Collections.Generic;
using UnityEngine;


namespace Isometric2DGame.Combat {

    /// <summary>Trigger capsule that is switched on during a swing and damages each target at most once.</summary>
    [RequireComponent(typeof(Collider2D))]
    public class Hitbox : MonoBehaviour {
        [SerializeField] private Transform _pivot;

        private Collider2D _collider;
        private int _damage;
        private Vector2 _direction;
        private GameObject _source;

        private void Awake() {
            _collider = GetComponent<Collider2D>();
            _collider.isTrigger = true;
            _collider.enabled = false;
        }

        private void OnDisable() {
            Deactivate();
        }

        public void Activate(int damage, Vector2 direction, GameObject source) {
            _damage = damage;
            _direction = direction;
            _source = source;
            _pivot.right = direction;
            _collider.enabled = true;
        }

        public void Deactivate() {
            if (_collider != null) {
                _collider.enabled = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (!CanHit(other, out Hurtbox hurtbox)) {
                return;
            }

            hurtbox.RegisterDamage(new HitInfo(_damage, _direction, _source));
        }

        private bool CanHit(Collider2D other, out Hurtbox hurtbox) {
            if (!other.TryGetComponent(out hurtbox)) {
                return false;
            }

            if (other.gameObject.layer == gameObject.layer) {
                return false;
            }

            return true;
        }
    }

}