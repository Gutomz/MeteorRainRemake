using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class ExplodesOnCollision : MonoBehaviour
    {
        private MeteorMaster meteorMaster;

        [SerializeField]
        private float chargingTime = 3f;

        [SerializeField]
        private float blastRadius = 3f;

        [SerializeField]
        private LayerMask explosionLayers;

        [SerializeField]
        private int explosionDamage = 2;

        [SerializeField]
        private float timeToDestroy = 0.125f;

        [SerializeField]
        private string playerTag = "Player";

        private bool isCharging = false;

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
            if (!isCharging && !collision.gameObject.CompareTag(tag))
            {
                isCharging = true;
                tag = "MeteorCharging";
                StartCoroutine(StartCharging());
            }
        }

        private IEnumerator StartCharging()
        {
            yield return new WaitForSeconds(chargingTime);
            Explode(transform.position);
        }

        private void Explode(Vector3 explosionPoint)
        {
            meteorMaster.CallEventOnDestroyMeteor();

            Collider[] colliders = Physics.OverlapSphere(explosionPoint, blastRadius, explosionLayers);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject == gameObject) continue;

                if (collider.CompareTag(playerTag)) {
                    collider.GetComponent<PlayerMaster>().CallEventPlayerTakeDamage(explosionDamage);
                }
            }

            Destroy(gameObject, timeToDestroy);
        }
    }
}
