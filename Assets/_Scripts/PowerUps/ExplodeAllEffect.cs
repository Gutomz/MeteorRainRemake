using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    [CreateAssetMenu(menuName = "Descripted Objects/PowerUps/ExplodeAllEffect")]
    public class ExplodeAllEffect : PowerUpEffect
    {
        public float offsetTime;

        public override void Activate()
        {
            base.Activate();
            SpawnerMaster spawnerMaster = FindObjectOfType<SpawnerMaster>();
            spawnerMaster.CallEventDestroyAll(offsetTime);
        }
    }
}
