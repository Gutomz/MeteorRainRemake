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

        public delegate void ChangeTimeBetweenSpawnsEventHandler(float newTime);
        public event ChangeTimeBetweenSpawnsEventHandler ChangeTimeBetweenSpawnsEvent;

        public delegate void SpawnerObjectsEventHandler(SpawnerObject[] spawnerObjects);
        public event SpawnerObjectsEventHandler UpdateSpawnerObjectsEvent;

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

        public void CallEventChangeTimeBetweenSpawns(float newTime)
        {
            ChangeTimeBetweenSpawnsEvent?.Invoke(newTime);
        }

        public void CallEventUpdateSpawnerObjects(SpawnerObject[] spawnerObjects)
        {
            UpdateSpawnerObjectsEvent?.Invoke(spawnerObjects);
        }
    }
}
