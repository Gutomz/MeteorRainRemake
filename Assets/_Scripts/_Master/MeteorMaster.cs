using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class MeteorMaster : MonoBehaviour
    {
        public delegate void MeteorCollisionEventHandler(Collision collision);
        public event MeteorCollisionEventHandler MeteorCollideEvent;

        public delegate void MeteorGeneralEventHandler();
        public event MeteorGeneralEventHandler DestroyMeteorEvent;

        public void CallEventMeteorCollide(Collision collision)
        {
            MeteorCollideEvent?.Invoke(collision);
        }

        public void CallEventDestroyMeteor(float timeout)
        {
            DestroyMeteorEvent?.Invoke();
            Destroy(gameObject, timeout);
        }

        private void OnCollisionEnter(Collision collision)
        {
            CallEventMeteorCollide(collision);
        }
    }
}
