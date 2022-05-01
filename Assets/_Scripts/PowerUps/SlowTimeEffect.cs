using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    [CreateAssetMenu(menuName = "Descripted Objects/PowerUps/SlowTimeEffect")]
    public class SlowTimeEffect : PowerUpEffect
    {
        [SerializeField]
        private float duration;

        [SerializeField, Range(0, 1)]
        private float slowPercentage;

        public override void Activate()
        {
            base.Activate();
            GameManagerMaster.Instance.CallEventStartSlowTime(duration, slowPercentage);
        }
    }
}
