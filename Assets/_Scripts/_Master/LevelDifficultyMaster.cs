using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class LevelDifficultyMaster : MonoBehaviour
    {
        public delegate void GeneralEventHandler();
        public event GeneralEventHandler IncreaseLevelDifficultyEvent;

        public delegate void LevelDifficultyEventHandler(LevelDifficultySettingsData data);
        public event LevelDifficultyEventHandler ChangeLevelDifficultyEvent;

        public void CallEventIncreaseLevelDifficulty()
        {
            IncreaseLevelDifficultyEvent?.Invoke();
        }

        public void CallEventChangeLevelDifficulty(LevelDifficultySettingsData data)
        {
            ChangeLevelDifficultyEvent?.Invoke(data);
        }
    }
}
