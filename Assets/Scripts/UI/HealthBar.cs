using UnityEngine;
using UnityEngine.UI;


namespace Isometric2DGame.UI {

    public class HealthBar : MonoBehaviour {
        [SerializeField] private Combat.Health _health;
        [SerializeField] private Slider _slider;

        private void Awake() {
            _slider = GetComponent<Slider>();
        }
        
        private void OnEnable() {
            _health.Changed += OnHealthChanged;
            RefreshBar(_health.Current, _health.Max);
        }

        private void Start() {
            RefreshBar(_health.Current, _health.Max);
        }
        
        private void OnDisable() {
            if (_health != null) {
                _health.Changed -= OnHealthChanged;
            }
        }

        private void OnHealthChanged(int current, int max) {
            RefreshBar(current, max);
        }

        private void RefreshBar(int current, int max) {
            _slider.maxValue = max;
            _slider.value = current;
        }
    }

}