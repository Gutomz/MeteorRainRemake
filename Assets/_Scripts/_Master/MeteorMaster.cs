using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class MeteorMaster : MonoBehaviour
    {
        public delegate void MeteorGeneralEventHandler();
        public delegate void MeteorCollisionEventHandler(Collision collision);
        public event MeteorCollisionEventHandler MeteorCollideEvent;
        public event MeteorGeneralEventHandler OnDestroyMeteorEvent;

        public void CallEventMeteorCollide(Collision collision)
        {
            MeteorCollideEvent?.Invoke(collision);
        }

        public void CallEventOnDestroyMeteor()
        {
            OnDestroyMeteorEvent?.Invoke();
        }

        private void OnCollisionEnter(Collision collision)
        {
            CallEventMeteorCollide(collision);
        }
    }
}
