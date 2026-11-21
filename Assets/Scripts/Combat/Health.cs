using System;
using UnityEngine;

namespace Isometric2DGame.Combat {
    /// <summary>Keeps track of hit points and triggers Damaged and Died events.</summary>
    public class Health : MonoBehaviour, IDamageable {
        [SerializeField, Min(1)] private int _maxHealth = 100;

        private int _current;

        public int Current => _current;
        public int Max => _maxHealth;
        public bool IsDead => _current <= 0;

        public event Action<int> Damaged;
        public event Action Died;

        private void Awake() {
            _current = _maxHealth;
        }

        public void TakeDamage(int amount) {
            if (IsDead) {
                return;
            }

            _current = Mathf.Max(0, _current - amount);
            Damaged?.Invoke(amount);

            if (IsDead) {
                Died?.Invoke();
            }
        }
    }
}