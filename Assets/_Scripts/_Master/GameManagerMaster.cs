using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MeteorRain
{
    public class GameManagerMaster : MonoBehaviour
    {
        private static GameManagerMaster instance;

        public static GameManagerMaster Instance { get { return instance; } }

        private GameManagerMaster() {
            instance = this;
        }

        public delegate void GeneralEventHandler();
        public event GeneralEventHandler StartGameEvent;
        public event GeneralEventHandler GameOverEvent;
        public event GeneralEventHandler TogglePauseGameEvent;
        public event GeneralEventHandler RestartGameEvent;

        public bool IsGameStarted { get { return isGameStarted; } }
        private bool isGameStarted = false;

        public bool IsGamePaused { get { return isGamePaused; } }
        private bool isGamePaused = false;

        public bool IsGameOver { get { return isGameOver; } }
        private bool isGameOver = false;

        public bool IsGameRunning {
            get {
                return isGameStarted && !isGamePaused && !isGameOver;
            }
        }

        public void CallEventStartGame()
        {
            isGameStarted = true;
            StartGameEvent?.Invoke();
        }

        public void CallEventGameOver()
        {
            isGameOver = true;
            GameOverEvent?.Invoke();
        }

        public void CallEventTooglePauseGame()
        {
            if (isGameStarted && !isGameOver)
            {
                isGamePaused = !isGamePaused;
                TogglePauseGameEvent?.Invoke();
            }
        }

        public void RestartLevel()
        {
            RestartGameEvent?.Invoke();
            if (isGamePaused) CallEventTooglePauseGame();
            isGameOver = false;
            isGameStarted = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
