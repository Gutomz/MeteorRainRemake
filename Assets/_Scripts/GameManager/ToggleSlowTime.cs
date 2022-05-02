using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class ToggleSlowTime : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;

        private bool isTimeSlower = false;

        private float lastTimeScale;
        private float lastFixedDeltaTime;

        private float currentTime;
        private float endTime;

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

        private void Update()
        {
            if (gameManagerMaster.IsGameRunning)
            {
                if (isTimeSlower)
                {
                    currentTime += Time.unscaledDeltaTime;

                    if (currentTime >= endTime)
                    {
                        isTimeSlower = false;
                        gameManagerMaster.CallEventStopSlowTime();
                    }
                }
            }
        }

        private void OnStartSlowTime(float duration, float slowPercentage)
        {
            if (!isTimeSlower)
            {
                StartSlowTime(duration, slowPercentage);
            }
        }

        private void OnStopSlowTime()
        {
            isTimeSlower = false;
            Time.timeScale = lastTimeScale;
            Time.fixedDeltaTime = lastFixedDeltaTime;
        }

        private void StartSlowTime(float duration, float slowPercentage)
        {
            currentTime = 0;
            endTime = duration;
            lastTimeScale = Time.timeScale;
            lastFixedDeltaTime = Time.fixedDeltaTime;
            Time.timeScale *= slowPercentage;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            isTimeSlower = true;
        }
    }
}
