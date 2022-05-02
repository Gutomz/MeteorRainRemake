using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class PlayerShield : MonoBehaviour
    {
        private PlayerMaster playerMaster;

        [SerializeField]
        private GameObject shield;

        private float currentShieldTime;
        private float shieldDuration;

        private void OnEnable()
        {
            SetInitialReferences();

            playerMaster.PlayerApplyShieldEvent += OnApplyShield;
        }

        private void OnDisable()
        {
            playerMaster.PlayerApplyShieldEvent -= OnApplyShield;
        }

        private void SetInitialReferences()
        {
            playerMaster = GetComponent<PlayerMaster>();
        }

        private void Start()
        {
            RemoveShield();
        }

        private void Update()
        {
            if (GameManagerMaster.Instance.IsGameRunning)
            {
                if (shield.activeSelf)
                {
                    currentShieldTime += Time.unscaledDeltaTime;

                    if (currentShieldTime >= shieldDuration)
                    {
                        playerMaster.CallEventPlayerStopShield();
                        RemoveShield();
                    }
                }
            }
        }

        private void OnApplyShield(float duration)
        {
            shieldDuration += duration;
            shield.SetActive(true);
        }

        private void RemoveShield()
        {
            shield.SetActive(false);
            currentShieldTime = 0;
            shieldDuration = 0;
        }
    }
}
