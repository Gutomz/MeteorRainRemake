using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class ToggleSlowTime : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;

        private bool isTimeSlower = false;
        private Coroutine slowTimeCoroutine;

        private void OnEnable()
        {
            SetInitialReferences();

            gameManagerMaster.StartSlowTimeEvent += OnStartSlowTime;
            gameManagerMaster.StopSlowTimeEvent += OnStopSlowTime;
        }

        private void OnDisable()
        {
            gameManagerMaster.StartSlowTimeEvent -= OnStartSlowTime;
            gameManagerMaster.StopSlowTimeEvent -= OnStopSlowTime;
        }

        private void SetInitialReferences()
        {
            gameManagerMaster = GameManagerMaster.Instance;
        }

        private void OnStartSlowTime(float duration, float slowPercentage)
        {
            if (!isTimeSlower)
            {
                isTimeSlower = true;
                slowTimeCoroutine = StartCoroutine(StartSlowTime(duration, slowPercentage));
            }
        }

        private void OnStopSlowTime()
        {
            if (slowTimeCoroutine != null)
                StopCoroutine(slowTimeCoroutine);
            isTimeSlower = false;
            Time.timeScale = 1;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

        private IEnumerator StartSlowTime(float duration, float slowPercentage)
        {
            Time.timeScale *= slowPercentage;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            yield return new WaitForSecondsRealtime(duration);
            gameManagerMaster.CallEventStopSlowTime();
        }
    }
}
