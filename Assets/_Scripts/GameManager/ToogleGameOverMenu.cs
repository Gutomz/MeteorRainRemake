using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class ToogleGameOverMenu : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;

        [SerializeField]
        private GameObject gameOverMenuUI;

        private void OnEnable()
        {
            SetInitialReferences();

            gameManagerMaster.GameOverEvent += OnGameOver;
        }

        private void OnDisable()
        {
            gameManagerMaster.GameOverEvent -= OnGameOver;
        }

        private void SetInitialReferences()
        {
            gameManagerMaster = GameManagerMaster.Instance;
        }

        private void Start()
        {
            gameOverMenuUI.SetActive(gameManagerMaster.IsGamePaused);
        }

        private void OnGameOver()
        {
            gameOverMenuUI.SetActive(true);
        }
    }
}
