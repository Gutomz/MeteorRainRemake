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

        private float currentSpeed;

        public int CurrentHealth { get { return currentHealth; } }
        public float CurrentSpeed { get { return currentSpeed; } }

        private void OnEnable()
        {
            SetInitialReferences();

            playerMaster.PlayerHealEvent += OnHeal;
            playerMaster.PlayerTakeDamageEvent += OnTakeDamage;
            playerMaster.PlayerChangeSpeedEvent += OnChangeSpeed;
            gameManagerMaster.StartGameEvent += OnGameStart;
        }

        private void OnDisable()
        {
            playerMaster.PlayerHealEvent -= OnHeal;
            playerMaster.PlayerTakeDamageEvent -= OnTakeDamage;
            playerMaster.PlayerChangeSpeedEvent -= OnChangeSpeed;
            gameManagerMaster.StartGameEvent -= OnGameStart;
        }

        private void SetInitialReferences()
        {
            gameManagerMaster = GameManagerMaster.Instance;
            playerMaster = GetComponent<PlayerMaster>();
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

        private void OnGameStart()
        {
            ResetHealth();
            ResetSpeed();
        }
    }
}
