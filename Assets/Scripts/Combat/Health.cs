using System;
using System.Collections;
using UnityEngine;


namespace Isometric2DGame.Combat {

    /// <summary>Keeps track of hit points and triggers Damaged and Died events.</summary>
    public class Health : MonoBehaviour, IDamageable {
        [SerializeField] [Min(1)] private int _maxHealth = 100;
        [SerializeField] private Color _hitFlashColor = Color.red;
        [SerializeField] private SpriteRenderer[] _sprites;

        public int Current { get; private set; }
        public int Max => _maxHealth;
        public bool IsDead => Current <= 0;

        public event Action<int, int> Changed;
        public event Action<HitInfo> Damaged;
        public event Action Died;

        private void Awake() {
            Current = _maxHealth;
        }

        public void TakeDamage(HitInfo hit) {
            if (IsDead) {
                return;
            }

            StartCoroutine(ApplyHitFlash());

            Current = Mathf.Max(0, Current - hit.Damage);
            Damaged?.Invoke(hit);
            Changed?.Invoke(Current, _maxHealth);

            if (IsDead) {
                Died?.Invoke();
            }
        }

        private IEnumerator ApplyHitFlash() {
            foreach (SpriteRenderer sprite in _sprites) {
                sprite.color = _hitFlashColor;
            }

            yield return new WaitForSeconds(0.25f);

            foreach (SpriteRenderer sprite in _sprites) {
                sprite.color = Color.white;
            }
        }
    }

}