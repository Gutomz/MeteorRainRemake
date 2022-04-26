using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class ScoreMaster : MonoBehaviour
    {
        private const string HIGH_SCORE_KEY = "highScore";

        private GameManagerMaster gameManagerMaster;

        public delegate void GeneralEventHandler();
        public delegate void ScoreEventHandler(int score);
        public event ScoreEventHandler ChangeScoreEvent;
        public event GeneralEventHandler StartScoringEvent;
        public event GeneralEventHandler StopScoringEvent;

        public int CurrentScore { get { return currentScore; } }
        private int currentScore = 0;

        public int LastHighScore { get { return lastHighScore; } }
        private int lastHighScore;

        public int CurrentHighScore { get { return currentHighScore; } }
        private int currentHighScore;

        public bool IsNewHighScore { get { return isNewHighScore; } }
        private bool isNewHighScore = false;

        private void OnEnable()
        {
            SetInitialReferences();
            currentHighScore = lastHighScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);

            gameManagerMaster.RestartGameEvent += SaveNewHighScore;
            gameManagerMaster.GameOverEvent += SaveNewHighScore;
        }

        private void OnDisable()
        {
            gameManagerMaster.RestartGameEvent -= SaveNewHighScore;
            gameManagerMaster.GameOverEvent -= SaveNewHighScore;
        }

        private void SetInitialReferences()
        {
            gameManagerMaster = GameManagerMaster.Instance;
        }

        public void CallEventChangeScore(int score)
        {
            currentScore = score;
            if (currentScore > currentHighScore)
            {
                isNewHighScore = true;
                currentHighScore = currentScore;
            }
            ChangeScoreEvent?.Invoke(score);
        }

        public void CallEventStartScoring()
        {
            StartScoringEvent?.Invoke();
        }

        public void CallEventStopScoring()
        {
            StopScoringEvent?.Invoke();
        }

        public void SaveNewHighScore()
        {
            if (isNewHighScore)
            {
                PlayerPrefs.SetInt(HIGH_SCORE_KEY, currentHighScore);
            }
        }
    }
}
