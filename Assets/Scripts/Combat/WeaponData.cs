using UnityEngine;


namespace Isometric2DGame.Combat {

    /// <summary>Configurable stats for a weapon.</summary>
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/Weapon Data")]
    public class WeaponData : ScriptableObject {
        [Header("Damage")] [SerializeField] [Min(0)]
        private int _damage = 10;

        [Header("Timing")]
        [Tooltip("In seconds. Windup + Active + Recovery should match the attack clip length.")]
        [SerializeField]
        [Min(0f)]
        private float _windupTime = 0.1f;

        [SerializeField] [Min(0.04f)] private float _activeTime = 0.1f;
        [SerializeField] [Min(0f)] private float _recoveryTime = 0.2f;

        [Tooltip("Cooldown time between attacks of automatic weapons. Use 0 for manual weapons.")]
        [SerializeField]
        [Min(0f)]
        private float _cooldown;

        [Tooltip("Time buffer to remember input for the next attack.")] [SerializeField] [Min(0f)]
        private float _inputBufferTime = 0.15f;


        public int Damage => _damage;
        public float WindupTime => _windupTime;
        public float ActiveTime => _activeTime;
        public float RecoveryTime => _recoveryTime;
        public float Cooldown => _cooldown;
        public float InputBufferTime => _inputBufferTime;
    }

}