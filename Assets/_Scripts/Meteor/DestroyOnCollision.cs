using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class DestroyOnCollision : MonoBehaviour
    {
        private MeteorMaster meteorMaster;

        [SerializeField]
        private float timeToDestroy = 0.125f;

        private void OnEnable()
        {
            SetInitialReferences();

            meteorMaster.MeteorCollideEvent += OnCollide;
        }

        private void OnDisable()
        {
            meteorMaster.MeteorCollideEvent -= OnCollide;
        }

        private void SetInitialReferences()
        {
            meteorMaster = GetComponent<MeteorMaster>();
        }

        private void OnCollide(Collision collision)
        {
            if (!collision.gameObject.tag.Equals(tag)) {    
                meteorMaster.CallEventOnDestroyMeteor();
                Destroy(gameObject, timeToDestroy);
            }
        }
    }
}
