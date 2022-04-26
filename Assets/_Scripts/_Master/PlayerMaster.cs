using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class PlayerMaster : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;

        public delegate void PlayerMovementEventHandler();
        public event PlayerMovementEventHandler MovePlayerLeftEvent;
        public event PlayerMovementEventHandler MovePlayerRightEvent;

        public delegate void PlayerHealthEventHandler(int amount);
        public event PlayerHealthEventHandler PlayerTakeDamageEvent;
        public event PlayerHealthEventHandler PlayerHealEvent;
        public event PlayerHealthEventHandler PlayerUpdateHealthEvent;

        public delegate void PlayerSpeedEventHandler(float newSpeed);
        public event PlayerSpeedEventHandler PlayerChangeSpeedEvent;

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
    }
}
