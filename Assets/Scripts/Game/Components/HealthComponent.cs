using System;
using TMPro;
using UnityEngine;

namespace Game.Components
{
    public class HealthComponent
    {
        private int _currentHealth;
        private int _temporaryHealth;
        
        
        private readonly TMP_Text _healthView;
        private readonly TMP_Text _temporaryHealthLabel;
        private readonly GameObject _temporaryIcon;
        
        public event Action onDie;

        public HealthComponent(
            TMP_Text healthView,
            TemporaryHealthView temporaryHealthView
            )
        {
            _healthView = healthView;
            _temporaryHealthLabel = temporaryHealthView.TemporaryLabel;
            _temporaryIcon = temporaryHealthView.TemporaryView;
            
            CheckTemporaryHealthActive();
        }
        
        public void SetHealth(int value)
        {
            _healthView.text = value.ToString();
            _currentHealth = value;
        }
        
        public void ApplyDamage(int damage)
        {
            if (_temporaryHealth > 0)
            {
                if (_temporaryHealth > damage)
                {
                    SetTemporaryHealth(_temporaryHealth - damage);
                    damage = 0;
                }
                else if(_temporaryHealth < damage)
                {
                    damage -= _temporaryHealth;
                    SetTemporaryHealth(0);
                }
                else
                {
                    SetTemporaryHealth(0);
                    damage = 0;
                }
            }
            
            SetHealth((int)Mathf.Clamp(_currentHealth - damage, 0, float.PositiveInfinity));
            CheckTemporaryHealthActive();
            
            if (_currentHealth <= 0)
            {
                onDie?.Invoke();
            }
        }
        
        public void AddHealth(int value)
        {
            var newHealth = _currentHealth + value;
            SetHealth(newHealth);
        }

        public void SetTemporaryHealth(int value)
        {
            _temporaryHealth = value;
            
            _temporaryHealthLabel.text = value.ToString();
            CheckTemporaryHealthActive();
        }

        private void CheckTemporaryHealthActive()
        {
            _temporaryIcon.SetActive(_temporaryHealth > 0);
        }
    }
}