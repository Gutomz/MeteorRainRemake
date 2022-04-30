using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MeteorRain
{
    public abstract class PowerUpEffect : DescriptedObject
    {
        protected PlayerMaster playerMaster;

        [SerializeField]
        private float cost;
        public float Cost { get { return cost; } }

        protected virtual void SetInitialReferences()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerMaster = player?.GetComponent<PlayerMaster>();
        }

        public virtual void Activate()
        {
            SetInitialReferences();
            playerMaster.CallEventPlayerDecreaseMana(cost);
        }
    }
}
