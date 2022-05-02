using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class ScoreSystem : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;
        private ScoreMaster scoreMaster;

        [SerializeField]
        private int initialEarnScore = 1;
        private int currentEarnScore;

        [SerializeField]
        private float initialTimeBetweenScore = 1;
        private float currentTimeBetweenScore;

        private bool isScoring = false;
        private float currentTime;
        private float nextScoreTime;

        private void OnEnable()
        {
            SetInitialReferences();

            gameManagerMaster.StartGameEvent += scoreMaster.CallEventStartScoring;
            gameManagerMaster.GameOverEvent += scoreMaster.CallEventStopScoring;
            scoreMaster.StartScoringEvent += OnStartScoring;
            scoreMaster.StopScoringEvent += OnStopScoring;

            currentEarnScore = initialEarnScore;
            currentTimeBetweenScore = initialTimeBetweenScore;
        }

        private void OnDisable()
        {
            gameManagerMaster.StartGameEvent -= scoreMaster.CallEventStartScoring;
            gameManagerMaster.GameOverEvent -= scoreMaster.CallEventStopScoring;
            scoreMaster.StartScoringEvent -= OnStartScoring;
            scoreMaster.StopScoringEvent -= OnStopScoring;
        }

        private void SetInitialReferences()
        {
            gameManagerMaster = GameManagerMaster.Instance;
            scoreMaster = GetComponent<ScoreMaster>();
        }

        private void Update()
        {
            if (gameManagerMaster.IsGameRunning)
            {
                if (isScoring)
                {
                    currentTime += Time.unscaledDeltaTime;

                    if (currentTime >= currentTimeBetweenScore)
                    {
                        scoreMaster.CallEventChangeScore(scoreMaster.CurrentScore + currentEarnScore);
                        currentTime = 0;
                    }
                }
            }
        }

        private void OnStartScoring()
        {
            isScoring = true;
        }

        private void OnStopScoring()
        {
            isScoring = false;
        }
    }
}
