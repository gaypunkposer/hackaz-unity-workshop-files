using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Workshop.Player.Movement;

namespace Workshop.Player.Movement
{
    public class Jump : MonoBehaviour
    {
        //The PlayerController we will use to move the player.
        public PlayerController controller;
        //How fast should the player jump? This is NOT the maximum height of the jump, this is the force that the player jumps off the ground with. Measured in m/s.
        public float jumpForce = 8;
        //The maximum number of times the player can jump before needing to touch the ground.
        public int maxJumps = 2;

        //How many times has the player jumped before hitting the ground?
        //Private instance variables should be prefaced by an underscore. This is Unity convention.
        private float _jumpCount;

        // Start is called before the first frame update
        private void Start()
        {
            //If the PlayerController is not set in the inspector, disable this script to prevent further errors.
            if (!controller)
            {
                Debug.LogError("Controller not set on Jump! Disabling to prevent further errors.");
                enabled = false;
            }
        }

        //Update is called once per frame
        private void Update()
        {
            //If the player is grounded, reset the number of times they can jump.
            if (controller.IsGrounded())
            {
                _jumpCount = 0;
            }

            Vector3 input = controller.GetInput();
            
            //If the jump button has been pressed and the player has more jumps remaining, jump.
            if (input.y > 0 && _jumpCount < maxJumps)
            {
                _jumpCount++;
                controller.MoveCharacter(Vector3.up * jumpForce);
            }
        }
    }
}
