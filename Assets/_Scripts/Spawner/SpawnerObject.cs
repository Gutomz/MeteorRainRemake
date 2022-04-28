using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    [System.Serializable]
    public class SpawnerObject
    {
        public GameObject gameObject;

        [Range(0f, 1f)]
        public float probability;
    }
}
