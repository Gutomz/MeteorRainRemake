using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MeteorRain
{
    public class HealthCounterUI : MonoBehaviour
    {
        [SerializeField]
        private PlayerMaster playerMaster;
        private TextMeshProUGUI textMeshProUGUI;

        private void OnEnable()
        {
            SetInitialReferences();

            playerMaster.PlayerUpdateHealthEvent += OnChange;
            textMeshProUGUI.SetText(playerMaster.GetComponent<PlayerStatus>().CurrentHealth.ToString());
        }

        private void OnDisable()
        {
            playerMaster.PlayerUpdateHealthEvent -= OnChange;
        }

        private void SetInitialReferences()
        {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        private void OnChange(int newHealh)
        {            
            textMeshProUGUI.SetText(newHealh.ToString());
        }
    }
}
