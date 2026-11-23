using Isometric2DGame.Combat;
using UnityEngine;


namespace Isometric2DGame.Enemy {

    /// <summary>Removes the enemy when Health trigger Died event.</summary>
    [RequireComponent(typeof(Health))]
    public class EnemyDeath : MonoBehaviour {
        private Health _health;

        private void Awake() {
            _health = GetComponent<Health>();
        }

        private void OnEnable() {
            _health.Died += OnDied;
        }

        private void OnDisable() {
            _health.Died -= OnDied;
        }

        private void OnDied() {
            Destroy(gameObject);
        }
    }

}