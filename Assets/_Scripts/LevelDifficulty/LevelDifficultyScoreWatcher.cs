using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class LevelDifficultyScoreWatcher : MonoBehaviour
    {
        private LevelDifficultyMaster levelDifficultyMaster;
        private LevelDifficultySettings levelDifficultySettings;

        [SerializeField]
        private ScoreMaster scoreMaster;

        [SerializeField]
        private SpawnerMaster spawnerMaster;

        private void OnEnable()
        {
            SetInitialReferences();

            scoreMaster.ChangeScoreEvent += OnChangeScore;
            levelDifficultyMaster.ChangeLevelDifficultyEvent += OnChangeLevelDifficulty;
        }

        private void OnDisable()
        {
            scoreMaster.ChangeScoreEvent -= OnChangeScore;
            levelDifficultyMaster.ChangeLevelDifficultyEvent -= OnChangeLevelDifficulty;
        }

        private void SetInitialReferences()
        {
            levelDifficultyMaster = GetComponent<LevelDifficultyMaster>();
            levelDifficultySettings = GetComponent<LevelDifficultySettings>();
        }

        private void OnChangeScore(int newScore)
        {
            if (!levelDifficultySettings.CanIncreaseLevel) return;
            int nextReachPoint = levelDifficultySettings.GetNextLevelReachPoint();

            if (newScore >= nextReachPoint)
            {
                levelDifficultyMaster.CallEventIncreaseLevelDifficulty();
            }
        }

        private void OnChangeLevelDifficulty(LevelDifficultySettingsData data)
        {
            spawnerMaster.CallEventChangeTimeBetweenSpawns(data.TimeBetweenSpawns);
            spawnerMaster.CallEventUpdateSpawnerObjects(data.ObjectsToSpawn);
        }
    }
}
