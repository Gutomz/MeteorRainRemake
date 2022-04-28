using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    [System.Serializable]
    public class LevelDifficultySettingsData
    {
        [SerializeField]
        private int reachPoint;
        public int ReachPoint { get { return reachPoint; } }

        [SerializeField]
        private float timeBetweenSpawns;
        public float TimeBetweenSpawns { get { return timeBetweenSpawns; } }

        [SerializeField]
        private SpawnerObject[] objectsToSpawn;
        public SpawnerObject[] ObjectsToSpawn { get { return objectsToSpawn; } }
    }
}
