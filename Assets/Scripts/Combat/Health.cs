using System;

using UnityEngine;


namespace Isometric2DGame.Combat {

    /// <summary>Keeps track of hit points and triggers Damaged and Died events.</summary>
    public class Health : MonoBehaviour, IDamageable {
        [SerializeField] [Min(1)] private int _maxHealth = 100;

        public int Current { get; private set; }

        public int Max => _maxHealth;
        public bool IsDead => Current <= 0;

        private void Awake() {
            Current = _maxHealth;
        }

        public void TakeDamage(int amount) {
            if (IsDead) return;

            Current = Mathf.Max(0, Current - amount);
            Damaged?.Invoke(amount);

            if (IsDead) Died?.Invoke();
        }

        public event Action<int> Damaged;
        public event Action Died;
    }

}