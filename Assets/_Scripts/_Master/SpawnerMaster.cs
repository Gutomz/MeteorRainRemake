using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class SpawnerMaster : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;

        public delegate void GenericSpawnerEventHandler();
        public event GenericSpawnerEventHandler StartSpawnerEvent;
        public event GenericSpawnerEventHandler StopSpawnerEvent;

        private void OnEnable()
        {
            SetInitialReferences();

            gameManagerMaster.StartGameEvent += CallEventStartSpawner;
        }

        private void OnDisable()
        {
            gameManagerMaster.StartGameEvent -= CallEventStartSpawner;
        }

        private void SetInitialReferences()
        {
            gameManagerMaster = GameManagerMaster.Instance;
        }

        public void CallEventStartSpawner()
        {
            if (StartSpawnerEvent != null)
            {
                StartSpawnerEvent();
            }
        }

        public void CallEventStopSpawner()
        {
            if (StopSpawnerEvent != null)
            {
                StopSpawnerEvent();
            }
        }
    }
}
