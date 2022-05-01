using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MeteorRain
{
    public class ToggleStartMenu : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;
        private bool isMobile;

        [SerializeField]
        private GameObject startMenuUI;

        [SerializeField]
        private TextMeshProUGUI startTextUI;

        [SerializeField]
        private TextMeshProUGUI countDownTextUI;

        [SerializeField, TextArea]
        private string mobileInitialText;

        [SerializeField, TextArea]
        private string computerInitialText;

        [SerializeField]
        private string countDownDoneText;

        [SerializeField]
        private int countDownTime = 3;

        [SerializeField]
        private float countDownDoneTimeout = 0.8f;

        private bool isStarting = false;

        private void OnEnable()
        {
            SetInitialReferences();

            startTextUI.text = isMobile ? mobileInitialText : computerInitialText;
            countDownTextUI.text = countDownTime.ToString();

            startTextUI.gameObject.SetActive(true);
            countDownTextUI.gameObject.SetActive(false);

            startMenuUI.SetActive(true);
        }

        private void SetInitialReferences()
        {
            gameManagerMaster = GameManagerMaster.Instance;
            isMobile = Application.platform == RuntimePlatform.Android;
        }

        private void Update()
        {
            if (!isStarting)
            {
                if (isMobile)
                {
                    int i = 0;
                    while (i < Input.touchCount)
                    {
                        if (Input.GetTouch(i).phase == TouchPhase.Ended)
                        {
                            isStarting = true;
                            StartCoroutine(OnStart());
                        }
                        ++i;
                    }
                }
                else
                {
                    if (Input.GetKey(KeyCode.J))
                    {
                        isStarting = true;
                        StartCoroutine(OnStart());
                    }
                }
            }
        }

        private IEnumerator OnStart()
        {
            int timeout = countDownTime;
            startTextUI.gameObject.SetActive(false);
            countDownTextUI.gameObject.SetActive(true);
            do
            {
                countDownTextUI.SetText(timeout.ToString());
                yield return new WaitForSecondsRealtime(1);
                timeout -= 1;
            } while (timeout > 0);
            countDownTextUI.SetText(countDownDoneText);
            yield return new WaitForSecondsRealtime(countDownDoneTimeout);
            gameManagerMaster.CallEventStartGame();
            startMenuUI.SetActive(false);
        }
    }
}
