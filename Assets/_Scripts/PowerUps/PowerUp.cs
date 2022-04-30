using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorRain
{
    public class PowerUp : MonoBehaviour
    {
        [SerializeField]
        private PlayerMaster playerMaster;
        private Button button;

        [SerializeField]
        private PowerUpEffect effect;

        [SerializeField]
        private KeyCode activateKey;

        private void OnEnable()
        {
            SetInitialReferences();

            playerMaster.PlayerUpdateManaEvent += OnPlayerUpdateMana;
        }

        private void OnDisable()
        {
            playerMaster.PlayerUpdateManaEvent -= OnPlayerUpdateMana;
        }

        private void SetInitialReferences()
        {
            button = GetComponent<Button>();
        }

        public void Update()
        {
            if (Input.GetKeyUp(activateKey) && button.IsInteractable())
            {
                button.onClick.Invoke();
            }
        }

        public void Activate()
        {
            effect.Activate();
        }

        public void OnPlayerUpdateMana(float mana)
        {
            if (mana < effect.Cost)
            {
                DeactivateButton();
            } else
            {
                ActivateButton();
            }
        }

        private void DeactivateButton()
        {
            button.interactable = false;
        }

        private void ActivateButton()
        {
            button.interactable = true;
        }
    }
}
