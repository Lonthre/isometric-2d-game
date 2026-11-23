using System;
using UnityEngine;


namespace Isometric2DGame.Combat {

    /// <summary>Base class for weapons. Manages a timed Windup-Active-Recovery sequence of attacking.</summary>
    public abstract class Weapon : MonoBehaviour {
        public enum Phase {
            Idle,
            Windup,
            Active,
            Recovery
        }

        [SerializeField] protected WeaponData Data;

        private Vector2 _direction;
        private float _phaseTimer;
        private float _nextAllowedTime;
        private float _queuedUntil = float.NegativeInfinity;

        public Phase CurrentPhase { get; private set; }
        public bool IsBusy => CurrentPhase != Phase.Idle;
        public bool CanAttack => !IsBusy && Time.time >= _nextAllowedTime;

        public event Action<Phase> PhaseChanged;

        protected abstract void OnActivePhaseStart(Vector2 direction);

        protected virtual void OnActivePhaseEnd() { }
        
        private void Update() {
            if (!IsBusy) {
                return;
            }

            _phaseTimer -= Time.deltaTime;

            if (_phaseTimer <= 0f) {
                AdvancePhase();
            }
        }
        
        /// <summary>Starts an attack along the given direction. Returns false if not ready to attack again.</summary>
        public bool TryAttack(Vector2 direction) {
            if (!CanAttack) {
                return false;
            }

            _direction = direction.normalized;
            _nextAllowedTime = Time.time + Data.Cooldown;
            _queuedUntil = float.NegativeInfinity;
            EnterPhase(Phase.Windup, Data.WindupTime);
            return true;
        }

        /// <summary>Remembers an attack time.</summary>
        public void QueueAttack() {
            _queuedUntil = Time.time + Data.InputBufferTime;
        }

        /// <summary>True if an attack was queued recently. The caller then fires TryAttack.</summary>
        public bool ConsumeQueuedAttack() {
            bool queued = Time.time <= _queuedUntil;
            _queuedUntil = float.NegativeInfinity;
            return queued;
        }

        /// <summary>Stops the swing immediately (for the case of death, knockback, etc.).</summary>
        public void Cancel() {
            if (CurrentPhase == Phase.Active) {
                OnActivePhaseEnd();
            }

            _queuedUntil = float.NegativeInfinity;
            SetPhase(Phase.Idle);
        }

        private void EnterPhase(Phase phase, float duration) {
            _phaseTimer = duration;
            SetPhase(phase);
        }

        private void SetPhase(Phase phase) {
            CurrentPhase = phase;
            PhaseChanged?.Invoke(phase);
        }
        
        private void AdvancePhase() {
            switch (CurrentPhase) {
                case Phase.Windup:
                    EnterPhase(Phase.Active, Data.ActiveTime);
                    OnActivePhaseStart(_direction);
                    break;

                case Phase.Active:
                    OnActivePhaseEnd();
                    EnterPhase(Phase.Recovery, Data.RecoveryTime);
                    break;

                case Phase.Recovery:
                    SetPhase(Phase.Idle);
                    break;
            }
        }
        
        private void OnDisable() {
            Cancel();
        }
    }

}