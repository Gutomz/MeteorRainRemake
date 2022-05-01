using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class PlayerStatus : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;
        private PlayerMaster playerMaster;

        [Header("Health")]
        [SerializeField]
        private int initialHealth;
        [SerializeField]
        private int minHealth;
        [SerializeField]
        private int maxHealth;
        private int currentHealth;

        [Header("Speed")]
        [SerializeField]
        private float initialSpeed;
        [SerializeField]
        private float minSpeed;
        [SerializeField]
        private float maxSpeed;
        [SerializeField]
        private float slowSpeedScale;
        private float currentSpeed;

        [Header("Mana")]
        [SerializeField]
        private float initialMana;
        [SerializeField]
        private float minMana;
        [SerializeField]
        private float maxMana;
        private float currentMana;

        public int CurrentHealth { get { return currentHealth; } }
        public float CurrentSpeed { get { return currentSpeed; } }
        public float CurrentMana { get { return currentMana; } }

        private void OnEnable()
        {
            SetInitialReferences();

            gameManagerMaster.StartGameEvent += OnGameStart;
            gameManagerMaster.StartSlowTimeEvent += OnStartSlowTime;
            gameManagerMaster.StopSlowTimeEvent += OnStopSlowTime;
            playerMaster.PlayerHealEvent += OnHeal;
            playerMaster.PlayerTakeDamageEvent += OnTakeDamage;
            playerMaster.PlayerChangeSpeedEvent += OnChangeSpeed;
            playerMaster.PlayerIncreaseManaEvent += OnIncreaseMana;
            playerMaster.PlayerDecreaseManaEvent += OnDecreaseMana;
        }

        private void OnDisable()
        {
            gameManagerMaster.StartGameEvent -= OnGameStart;
            gameManagerMaster.StartSlowTimeEvent -= OnStartSlowTime;
            gameManagerMaster.StopSlowTimeEvent -= OnStopSlowTime;
            playerMaster.PlayerHealEvent -= OnHeal;
            playerMaster.PlayerTakeDamageEvent -= OnTakeDamage;
            playerMaster.PlayerChangeSpeedEvent -= OnChangeSpeed;
            playerMaster.PlayerIncreaseManaEvent -= OnIncreaseMana;
            playerMaster.PlayerDecreaseManaEvent -= OnDecreaseMana;
        }

        private void SetInitialReferences()
        {
            gameManagerMaster = GameManagerMaster.Instance;
            playerMaster = GetComponent<PlayerMaster>();
        }

        private void OnGameStart()
        {
            ResetHealth();
            ResetSpeed();
            ResetMana();
        }

        private void OnStartSlowTime(float duration, float slowPercentage)
        {
            playerMaster.CallEventPlayerChangeSpeed(currentSpeed * (1 + slowPercentage) * slowSpeedScale);
        }

        private void OnStopSlowTime()
        {
            ResetSpeed();
        }

        private void OnHeal(int amount)
        {
            if (currentHealth + amount > maxHealth)
            {
                currentHealth = maxHealth;
            } else
            {
                currentHealth += amount;
            }

            playerMaster.CallEventPlayerUpdateHealth(currentHealth);
        }

        private void OnTakeDamage(int amount)
        {
            if (currentHealth - amount <= minHealth)
            {
                currentHealth = minHealth;
                GameManagerMaster.Instance.CallEventGameOver();
            } else
            {
                currentHealth -= amount;
            }

            playerMaster.CallEventPlayerUpdateHealth(currentHealth);
        }

        private void OnChangeSpeed(float newSpeed)
        {
            if (newSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
            } else if (newSpeed < minSpeed)
            {
                currentSpeed = minSpeed;
            } else
            {
                currentSpeed = newSpeed;
            }
        }

        private void OnIncreaseMana(float amount)
        {
            if (currentMana + amount > maxMana)
            {
                currentMana = maxMana;
            }
            else
            {
                currentMana += amount;
            }

            playerMaster.CallEventPlayerUpdateMana(currentMana);
        }

        private void OnDecreaseMana(float amount)
        {
            if (currentMana - amount <= minMana)
            {
                currentMana = minMana;
            }
            else
            {
                currentMana -= amount;
            }

            playerMaster.CallEventPlayerUpdateMana(currentMana);
        }

        public void ResetHealth()
        {
            if (currentHealth < initialHealth)
            {
                playerMaster.CallEventPlayerHeal(initialHealth - currentHealth);
            } else if (currentHealth > initialHealth)
            {
                playerMaster.CallEventPlayerTakeDamage(currentHealth - initialHealth);
            }
        }

        public void ResetSpeed()
        {
            playerMaster.CallEventPlayerChangeSpeed(initialSpeed);
        }

        public void ResetMana()
        {
            if (currentMana < initialMana)
            {
                playerMaster.CallEventPlayerIncreaseMana(initialMana - currentMana);
            }
            else if (currentMana > initialMana)
            {
                playerMaster.CallEventPlayerDecreaseMana(currentMana - initialMana);
            }
        }
    }
}
