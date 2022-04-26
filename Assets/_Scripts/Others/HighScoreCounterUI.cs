using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MeteorRain
{
    public class HighScoreCounterUI : MonoBehaviour
    {
        [SerializeField]
        private ScoreMaster scoreMaster;
        private TextMeshProUGUI textMeshProUGUI;

        [SerializeField]
        private bool isLastHighScore = false;

        private void OnEnable()
        {
            SetInitialReferences();

            scoreMaster.ChangeScoreEvent += OnChange;

            textMeshProUGUI.SetText(GetHighScore().ToString());
        }

        private void OnDisable()
        {
            scoreMaster.ChangeScoreEvent -= OnChange;
        }

        private void SetInitialReferences()
        {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        private int GetHighScore()
        {
            return isLastHighScore ? scoreMaster.LastHighScore : scoreMaster.CurrentHighScore;
        }

        private void OnChange(int newScore)
        {
            textMeshProUGUI.SetText(GetHighScore().ToString());
        }
    }
}
