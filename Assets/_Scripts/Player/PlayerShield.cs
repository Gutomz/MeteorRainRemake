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
        private float endShieldTime;

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
            if (shield.activeSelf)
            {
                currentShieldTime += Time.deltaTime;

                if (currentShieldTime >= endShieldTime)
                {
                    playerMaster.CallEventPlayerStopShield();
                    RemoveShield();
                }
            }
        }

        private void OnApplyShield(float duration)
        {
            endShieldTime += duration;
            shield.SetActive(true);
        }

        private void RemoveShield()
        {
            shield.SetActive(false);
            currentShieldTime = 0;
            endShieldTime = 0;
        }
    }
}
