using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    [CreateAssetMenu(menuName = "Descripted Objects/PowerUps/HealEffect")]
    public class HealEffect : PowerUpEffect
    {
        [SerializeField]
        private int amount;

        public override void Activate()
        {
            base.Activate();
            playerMaster?.CallEventPlayerHeal(amount);
        }
    }
}
