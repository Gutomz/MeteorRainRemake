using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class LevelDifficultySettings : MonoBehaviour
    {
        private LevelDifficultyMaster levelDifficultyMaster;

        [SerializeField]
        private LevelDifficultySettingsData[] levels;

        public LevelDifficultySettingsData CurrentLevel {  get { return currentLevel; } }
        private LevelDifficultySettingsData currentLevel;
        private int currentLevelIndex;

        private bool canIncreaseLevel = false;
        public bool CanIncreaseLevel { get { return canIncreaseLevel; } }

        private void OnEnable()
        {
            SetInitialReferences();

            levelDifficultyMaster.IncreaseLevelDifficultyEvent += OnIncreaseLevelDifficulty;
        }

        private void OnDisable()
        {
            levelDifficultyMaster.IncreaseLevelDifficultyEvent -= OnIncreaseLevelDifficulty;
        }

        private void SetInitialReferences()
        {
            levelDifficultyMaster = GetComponent<LevelDifficultyMaster>();
        }

        private void Start()
        {
            if (levels.Length == 0) {
                Debug.LogError("LevelDifficultySettings :: At least one level difficulty is required");
                return;
            }

            currentLevelIndex = 0;
            currentLevel = levels[currentLevelIndex];
            canIncreaseLevel = levels.Length > 1;
            levelDifficultyMaster.CallEventChangeLevelDifficulty(currentLevel);
        }

        private void OnIncreaseLevelDifficulty()
        {
            if (!canIncreaseLevel) return;

            currentLevelIndex += 1;
            currentLevel = levels[currentLevelIndex];
            canIncreaseLevel = currentLevelIndex + 1 < levels.Length;
            levelDifficultyMaster.CallEventChangeLevelDifficulty(currentLevel);
        }

        /// <summary>
        /// Get ReachPoint value for the next level if it exists.
        /// </summary>
        /// <returns>
        /// Returns ReachPoint value if succeeded.
        /// Returns -1 if fails.
        /// </returns>
        public int GetNextLevelReachPoint()
        {
            int output = -1;
            if (canIncreaseLevel)
            {
                output = levels[currentLevelIndex + 1].ReachPoint;
            }

            return output;
        }
    }
}
