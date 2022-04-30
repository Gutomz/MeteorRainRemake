using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class CameraFollow : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;
        public Transform target;

        [SerializeField, Range(0f, 1f)]
        private float smoothSpeed = 0.125f;

        [SerializeField]
        public Vector3 offset;

        private bool CanFollow {
            get {
                if (gameManagerMaster == null) return false;

                return gameManagerMaster.IsGameRunning;
            }
        }

        private void OnEnable()
        {
            gameManagerMaster = GameManagerMaster.Instance;
        }

        private void FixedUpdate()
        {
            if(CanFollow)
            {
                Vector3 desiredPosition = target.position + offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
            }
        }
    }
}
