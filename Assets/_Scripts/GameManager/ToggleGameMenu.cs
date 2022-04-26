using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class ToggleGameMenu : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;

        [SerializeField]
        private GameObject gameMenuUI;

        private void OnEnable()
        {
            SetInitialReferences();

            gameManagerMaster.StartGameEvent += OnShowGameMenu;
            gameManagerMaster.GameOverEvent += OnHideGameMenu;
            gameManagerMaster.TogglePauseGameEvent += OnToggleGameMenu;
        }

        private void OnDisable()
        {
            gameManagerMaster.StartGameEvent -= OnShowGameMenu;
            gameManagerMaster.GameOverEvent -= OnHideGameMenu;
            gameManagerMaster.TogglePauseGameEvent -= OnToggleGameMenu;
        }

        private void Start()
        {
            gameMenuUI.SetActive(false);
        }

        private void SetInitialReferences()
        {
            gameManagerMaster = GameManagerMaster.Instance;
        }

        private void OnShowGameMenu()
        {
            gameMenuUI.SetActive(true);
        }

        private void OnHideGameMenu()
        {
            gameMenuUI.SetActive(false);
        }

        private void OnToggleGameMenu()
        {
            gameMenuUI.SetActive(!gameMenuUI.activeSelf);
        }
    }
}
