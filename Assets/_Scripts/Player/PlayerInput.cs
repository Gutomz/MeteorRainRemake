using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorRain
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerMaster playerMaster;

        private float screenWidth;
        private bool isAndroid;

        private void OnEnable()
        {
            SetInitialReferences();
        }

        void SetInitialReferences()
        {
            playerMaster = GetComponent<PlayerMaster>();
            screenWidth = Screen.width;
            isAndroid = Application.platform == RuntimePlatform.Android;
        }

        private void Update()
        {
            CheckInput();
        }

        private void CheckInput()
        {
            if (isAndroid)
            {
                int i = 0;
                while (i < Input.touchCount)
                {
                    if (Input.GetTouch(i).position.x > screenWidth / 2)
                    {
                        playerMaster.CallEventMovePlayerRight();
                    }

                    if (Input.GetTouch(i).position.x < screenWidth / 2)
                    {
                        playerMaster.CallEventMovePlayerLeft();
                    }
                    ++i;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    playerMaster.CallEventMovePlayerRight();
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    playerMaster.CallEventMovePlayerLeft();
                }
            }
        }
    }
}
