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

        [SerializeField]
        private string shieldTag = "Shield";

        private bool isCharging = false;
        private bool isExploding = false;
        private float currenTime;

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
            if (!isCharging && !isExploding && !collision.collider.CompareTag(tag))
            {
                if (collision.collider.CompareTag(shieldTag))
                {
                    DestroyMeteor();
                    return; 
                }

                isCharging = true;
                tag = "MeteorCharging";
            }
        }

        private void Update()
        {
            if (GameManagerMaster.Instance.IsGameRunning)
            {
                if (isCharging && !isExploding)
                {
                    currenTime += Time.unscaledDeltaTime;

                    if (currenTime >= chargingTime)
                    {
                        isExploding = true;
                        isCharging = false;
                        Explode(transform.position);
                    }
                }
            }
        }

        private void Explode(Vector3 explosionPoint)
        {
            Collider[] colliders = Physics.OverlapSphere(explosionPoint, blastRadius, explosionLayers);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject == gameObject) continue;

                if (collider.CompareTag(playerTag)) {
                    collider.GetComponent<PlayerMaster>().CallEventPlayerTakeDamage(explosionDamage);
                }
            }

            DestroyMeteor();
        }

        private void DestroyMeteor()
        {
            meteorMaster.CallEventDestroyMeteor(timeToDestroy);
        }
    }
}
