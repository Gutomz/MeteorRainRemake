using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MeteorRain
{
    public class Spawner : MonoBehaviour
    {
        private SpawnerMaster spawnerMaster;
        private Coroutine mSpawnerCoroutine;

        [SerializeField]
        private float timeBetweenSpawns = 1f;

        [SerializeField]
        private Vector2 xBoundaries;

        [SerializeField]
        private SpawnerObject[] spawnerObjects;

        private void OnEnable()
        {
            SetInitialReferences();

            spawnerMaster.StartSpawnerEvent += OnStartSpawner;
            spawnerMaster.StopSpawnerEvent += OnStopSpawner;
            spawnerMaster.ChangeTimeBetweenSpawnsEvent += OnChangeTimeBetweenSpawns;
            spawnerMaster.UpdateSpawnerObjectsEvent += OnUpdateSpawnerObjects;
        }

        private void OnDisable()
        {
            spawnerMaster.StartSpawnerEvent -= OnStartSpawner;
            spawnerMaster.StopSpawnerEvent -= OnStopSpawner;
            spawnerMaster.ChangeTimeBetweenSpawnsEvent -= OnChangeTimeBetweenSpawns;
            spawnerMaster.UpdateSpawnerObjectsEvent -= OnUpdateSpawnerObjects;
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
                Instantiate(GetRandomMeteor(), GetRandomPosition(), GetRandomRotation(), transform);
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

        private void OnChangeTimeBetweenSpawns(float newTime)
        {
            timeBetweenSpawns = newTime;
        }

        private void OnUpdateSpawnerObjects(SpawnerObject[] newObjects)
        {
            spawnerObjects = newObjects;
        }

        private SpawnerObject GetRandomSpawnerObject(IEnumerable<SpawnerObject> pool)
        {
            double probabilities = pool.Sum(x => x.probability);
            double random = Random.Range(0f, 1f) * probabilities;

            double sum = 0;
            SpawnerObject output = pool.First();
            foreach (SpawnerObject spawnerObject in pool)
            {
                if (random <= (sum += spawnerObject.probability))
                {
                    output = spawnerObject;
                    break;
                }
            }

            return output;
        }

        private GameObject GetRandomMeteor()
        {
            SpawnerObject o = GetRandomSpawnerObject(spawnerObjects);
            return o.gameObject;
        }

        private Vector3 GetRandomPosition()
        {
            Vector3 position = transform.position;
            position.x = Random.Range(xBoundaries.x, xBoundaries.y);
            return position;
        }

        private Quaternion GetRandomRotation()
        {
            return Quaternion.identity;
        }
    }
}
