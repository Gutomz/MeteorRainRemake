using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MeteorRain
{
    public class ScoreCounterUI : MonoBehaviour
    {
        [SerializeField]
        private ScoreMaster scoreMaster;
        private TextMeshProUGUI textMeshProUGUI;

        private void OnEnable()
        {
            SetInitialReferences();

            scoreMaster.ChangeScoreEvent += OnChange;
            textMeshProUGUI.SetText(scoreMaster.CurrentScore.ToString());
        }

        private void OnDisable()
        {
            scoreMaster.ChangeScoreEvent -= OnChange;
        }

        private void SetInitialReferences()
        {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        private void OnChange(int newScore)
        {
            textMeshProUGUI.SetText(newScore.ToString());
        }
    }
}
