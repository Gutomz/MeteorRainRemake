using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class ScoreSystem : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;
        private ScoreMaster scoreMaster;
        private Coroutine mScoringCoroutine;

        [SerializeField]
        private int initialEarnScore = 1;
        private int currentEarnScore;

        [SerializeField]
        private float initialTimeBetweenScore = 1;
        private float currentTimeBetweenScore;

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

        private void OnStartScoring()
        {
            OnStopScoring();
            mScoringCoroutine = StartCoroutine(StartScoring());
        }

        private void OnStopScoring()
        {
            if (mScoringCoroutine != null)
            {
                StopCoroutine(mScoringCoroutine);
            }
        }

        IEnumerator StartScoring()
        {
            while (true)
            {
                yield return new WaitForSeconds(currentTimeBetweenScore);
                scoreMaster.CallEventChangeScore(scoreMaster.CurrentScore + currentEarnScore);
            }
        }
    }
}
