using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class Spawner : MonoBehaviour
    {
        private SpawnerMaster spawnerMaster;
        private Coroutine mSpawnerCoroutine;

        [SerializeField]
        private float timeBetweenSpawns = 1f;

        [System.Serializable]
        public class SpawnerObject
        {
            public GameObject gameObject;
            public float chance;
        }

        [SerializeField]
        private SpawnerObject[] spawnerObjects;

        private void OnEnable()
        {
            SetInitialReferences();

            spawnerMaster.StartSpawnerEvent += OnStartSpawner;
            spawnerMaster.StopSpawnerEvent += OnStopSpawner;
        }

        private void OnDisable()
        {
            spawnerMaster.StartSpawnerEvent -= OnStartSpawner;
            spawnerMaster.StopSpawnerEvent -= OnStopSpawner;
        }

        private void SetInitialReferences()
        {
            spawnerMaster = GetComponent<SpawnerMaster>();
        }

        private void OnStartSpawner()
        {
            mSpawnerCoroutine = StartCoroutine(StartSpawner());
        }

        IEnumerator StartSpawner()
        {
            while(true)
            {
                GameObject gameObject = Instantiate(getRandomMeteor(), getRandomPosition(), getRandomRotation(), transform);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }

        private void OnStopSpawner()
        {
            if (mSpawnerCoroutine != null)
            {
                StopCoroutine(mSpawnerCoroutine);
            }
        }

        private SpawnerObject getRandomSpawnerObject()
        {
            return spawnerObjects[Random.Range(0, spawnerObjects.Length)];
        }

        private GameObject getRandomMeteor()
        {
            SpawnerObject o = getRandomSpawnerObject();
            return o.gameObject;
        }

        private Vector3 getRandomPosition()
        {
            Vector3 position = transform.position;
            position.x = Random.Range(-11, 12);
            return position;
        }

        private Quaternion getRandomRotation()
        {
            return Quaternion.identity;
        }
    }
}
