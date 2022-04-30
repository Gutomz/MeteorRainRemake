using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    [CreateAssetMenu(menuName = "Descripted Objects/PowerUps/ShieldEffect")]
    public class ShieldEffect : PowerUpEffect
    {
        [SerializeField]
        private int duration;

        public override void Activate()
        {
            base.Activate();
            playerMaster.CallEventPlayerApplyShield(duration);
        }
    }
}
