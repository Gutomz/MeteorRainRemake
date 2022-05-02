using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MeteorRain
{
    public class Spawner : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;
        private SpawnerMaster spawnerMaster;

        [SerializeField]
        private float timeBetweenSpawns = 1f;
        private float currentSpawnTime;

        [SerializeField]
        private Vector2 xBoundaries;

        [SerializeField]
        private SpawnerObject[] spawnerObjects;

        private bool isSpawning = false;

        private void OnEnable()
        {
            SetInitialReferences();

            spawnerMaster.StartSpawnerEvent += OnStartSpawner;
            spawnerMaster.StopSpawnerEvent += OnStopSpawner;
            spawnerMaster.ChangeTimeBetweenSpawnsEvent += OnChangeTimeBetweenSpawns;
            spawnerMaster.UpdateSpawnerObjectsEvent += OnUpdateSpawnerObjects;
            spawnerMaster.DestroyAllEvent += OnDestroyAll;
        }

        private void OnDisable()
        {
            spawnerMaster.StartSpawnerEvent -= OnStartSpawner;
            spawnerMaster.StopSpawnerEvent -= OnStopSpawner;
            spawnerMaster.ChangeTimeBetweenSpawnsEvent -= OnChangeTimeBetweenSpawns;
            spawnerMaster.UpdateSpawnerObjectsEvent -= OnUpdateSpawnerObjects;
            spawnerMaster.DestroyAllEvent -= OnDestroyAll;
        }

        private void SetInitialReferences()
        {
            gameManagerMaster = GameManagerMaster.Instance;
            spawnerMaster = GetComponent<SpawnerMaster>();
        }

        private void Update()
        {
            if (gameManagerMaster.IsGameRunning)
            {
                if (isSpawning)
                {
                    currentSpawnTime += Time.deltaTime;

                    if (currentSpawnTime >= timeBetweenSpawns)
                    {
                        Instantiate(GetRandomMeteor(), GetRandomPosition(), GetRandomRotation(), transform);
                        currentSpawnTime = 0;
                    }
                }
            }
        }

        private void OnStartSpawner()
        {
            isSpawning = true;
        }

        private void OnStopSpawner()
        {
            isSpawning = false;
        }

        private void OnChangeTimeBetweenSpawns(float newTime)
        {
            timeBetweenSpawns = newTime;
        }

        private void OnUpdateSpawnerObjects(SpawnerObject[] newObjects)
        {
            spawnerObjects = newObjects;
        }

        private void OnDestroyAll(float offsetTime)
        {
            StartCoroutine(DestroyAll(offsetTime));
        }

        private IEnumerator DestroyAll(float offsetTime)
        {
            spawnerMaster.CallEventStopSpawner();
            MeteorMaster[] meteors = GetComponentsInChildren<MeteorMaster>();

            foreach (MeteorMaster meteor in meteors)
            {
                meteor.CallEventDestroyMeteor(0);
            }

            yield return new WaitForSecondsRealtime(offsetTime);

            spawnerMaster.CallEventStartSpawner();
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
