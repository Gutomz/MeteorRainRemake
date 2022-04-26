using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class TooglePauseMenu : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;
        private bool isMobile;

        [SerializeField]
        private GameObject pauseMenuUI;

        private void OnEnable()
        {
            SetInitialReferences();

            gameManagerMaster.TogglePauseGameEvent += TogglePauseMenu;
        }

        private void OnDisable()
        {
            gameManagerMaster.TogglePauseGameEvent -= TogglePauseMenu;
        }

        private void SetInitialReferences()
        {
            gameManagerMaster = GameManagerMaster.Instance;
            isMobile = Application.platform == RuntimePlatform.Android;
        }

        private void CheckPauseRequest()
        {
            if (!isMobile && Input.GetKeyUp(KeyCode.Space))
            {
                gameManagerMaster.CallEventTooglePauseGame();
            }
        }

        private void Start()
        {
            pauseMenuUI.SetActive(gameManagerMaster.IsGamePaused);
        }

        private void Update()
        {
            CheckPauseRequest();
        }

        private void TogglePauseMenu()
        {
            pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
        }
    }
}
