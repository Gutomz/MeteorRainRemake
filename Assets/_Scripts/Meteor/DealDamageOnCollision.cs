using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    public class DealDamageOnCollision : MonoBehaviour
    {
        private MeteorMaster meteorMaster;

        [SerializeField]
        private string playerTag = "Player";

        [SerializeField]
        private int damage = 1;

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
            if (collision.gameObject.tag.Equals(playerTag))
            {
                PlayerMaster playerMaster = collision.gameObject.GetComponent<PlayerMaster>();
                if (playerMaster != null)
                {
                    playerMaster.CallEventPlayerTakeDamage(damage);
                    enabled = false;
                }
            }
        }
    }
}
