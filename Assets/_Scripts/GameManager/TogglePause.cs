using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class TogglePause : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;

        private float lastTimeScale;

        private void OnEnable()
        {
            SetInitialReferences();

            gameManagerMaster.TogglePauseGameEvent += OnTogglePause;
        }

        private void OnDisable()
        {
            gameManagerMaster.TogglePauseGameEvent -= OnTogglePause;
        }

        private void SetInitialReferences()
        {
            gameManagerMaster = GameManagerMaster.Instance;
        }

        private void OnTogglePause()
        {
            if (gameManagerMaster.IsGamePaused)
            {
                lastTimeScale = Time.timeScale;
                Time.timeScale = 0;
            } else
            {
                Time.timeScale = lastTimeScale;
            }
        }
    }
}
