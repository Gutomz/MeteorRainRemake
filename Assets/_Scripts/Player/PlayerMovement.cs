using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorRain
{
    [RequireComponent(typeof(PlayerMaster), typeof(PlayerStatus))]
    public class PlayerMovement : MonoBehaviour
    {
        private GameManagerMaster gameManagerMaster;
        private PlayerMaster playerMaster;
        private Rigidbody rb;

        private float speed;
        private Vector3 moveDirection;

        private void OnEnable()
        {
            SetInitialReferences();

            playerMaster.MovePlayerLeftEvent += OnMovePlayerLeft;
            playerMaster.MovePlayerRightEvent += OnMovePlayerRight;
            playerMaster.PlayerChangeSpeedEvent += OnChangeSpeed;
            gameManagerMaster.GameOverEvent += OnGameOver;
        }

        private void OnDisable()
        {
            playerMaster.MovePlayerLeftEvent -= OnMovePlayerLeft;
            playerMaster.MovePlayerRightEvent -= OnMovePlayerRight;
            playerMaster.PlayerChangeSpeedEvent -= OnChangeSpeed;
            gameManagerMaster.GameOverEvent -= OnGameOver;
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GameManagerMaster.Instance;
            playerMaster = GetComponent<PlayerMaster>();
            rb = GetComponent<Rigidbody>();

            moveDirection = Vector3.zero;
        }

        private void FixedUpdate()
        {
            rb.velocity = new Vector3(moveDirection.x * speed * Time.deltaTime, rb.velocity.y, rb.velocity.z);
        }

        private void OnMovePlayerLeft()
        {
            moveDirection = Vector3.left;
        }

        private void OnMovePlayerRight() 
        {
            moveDirection = Vector3.right;
        }

        private void OnChangeSpeed(float newSpeed)
        {
            speed = newSpeed;
        }

        private void OnGameOver()
        {
            moveDirection = Vector3.zero;
        }
    }
}
