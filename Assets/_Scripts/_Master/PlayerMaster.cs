using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class PlayerMaster : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;

        public delegate void PlayerGeneralEventHandler();
        public delegate void PlayerMovementEventHandler();
        public event PlayerMovementEventHandler MovePlayerLeftEvent;
        public event PlayerMovementEventHandler MovePlayerRightEvent;

        public delegate void PlayerHealthEventHandler(int amount);
        public event PlayerHealthEventHandler PlayerTakeDamageEvent;
        public event PlayerHealthEventHandler PlayerHealEvent;
        public event PlayerHealthEventHandler PlayerUpdateHealthEvent;

        public delegate void PlayerSpeedEventHandler(float newSpeed);
        public event PlayerSpeedEventHandler PlayerChangeSpeedEvent;

        public delegate void PlayerManaEventHandler(float value);
        public event PlayerSpeedEventHandler PlayerUpdateManaEvent;
        public event PlayerSpeedEventHandler PlayerIncreaseManaEvent;
        public event PlayerSpeedEventHandler PlayerDecreaseManaEvent;

        public delegate void ApplyShieldEventHandler(float duration);
        public event ApplyShieldEventHandler PlayerApplyShieldEvent;
        public event PlayerGeneralEventHandler PlayerStopShieldEvent;

        private void OnEnable()
        {
            gameManagerMaster = GameManagerMaster.Instance;
        }

        public void CallEventMovePlayerLeft()
        {
            if (gameManagerMaster.IsGameRunning)
            {
                MovePlayerLeftEvent?.Invoke();
            }
        }

        public void CallEventMovePlayerRight()
        {
            if (gameManagerMaster.IsGameRunning)
            {
                MovePlayerRightEvent?.Invoke();
            }
        }

        public void CallEventPlayerTakeDamage(int amount)
        {
            if (gameManagerMaster.IsGameRunning)
            {
                PlayerTakeDamageEvent?.Invoke(amount);
            }
        }

        public void CallEventPlayerHeal(int amount)
        {
            if (gameManagerMaster.IsGameRunning)
            {
                PlayerHealEvent?.Invoke(amount);
            }
        }

        public void CallEventPlayerUpdateHealth(int newHealth)
        {
            if (gameManagerMaster.IsGameRunning)
            {
                PlayerUpdateHealthEvent?.Invoke(newHealth);
            }
        }

        public void CallEventPlayerChangeSpeed(float newSpeed)
        {
            if (gameManagerMaster.IsGameRunning)
            {
                PlayerChangeSpeedEvent?.Invoke(newSpeed);
            }
        }

        public void CallEventPlayerUpdateMana(float newMana)
        {
            if (gameManagerMaster.IsGameRunning)
            {
                PlayerUpdateManaEvent?.Invoke(newMana);
            }
        }

        public void CallEventPlayerIncreaseMana(float amount)
        {
            if (gameManagerMaster.IsGameRunning)
            {
                PlayerIncreaseManaEvent?.Invoke(amount);
            }
        }

        public void CallEventPlayerDecreaseMana(float amount)
        {
            if (gameManagerMaster.IsGameRunning)
            {
                PlayerDecreaseManaEvent?.Invoke(amount);
            }
        }

        public void CallEventPlayerApplyShield(float duration)
        {
            if (gameManagerMaster.IsGameRunning)
            {
                PlayerApplyShieldEvent?.Invoke(duration);
            }
        }

        public void CallEventPlayerStopShield()
        {
            if (gameManagerMaster.IsGameRunning)
            {
                PlayerStopShieldEvent?.Invoke();
            }
        }
    }
}
