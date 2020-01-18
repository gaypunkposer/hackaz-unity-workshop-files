using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Workshop.Player.Movement
{
    public class Fall : MonoBehaviour
    {
        //The player controller that we will move the player through
        public PlayerController controller;
        //How fast the player moves in the air. This is affected by weight and air resistance, so it needs to be a large number.
        public float moveSpeed = 750;
        //This weight value allows the character to move smoothly between the ground and the height of the jump. Makes the character
        //feel more solid. The weight is in kilograms.
        public float weight = 100;
        //The acceleration of gravity on the player. This is measured in m/s^2
        public float gravity = 15;
        //How fast should we slow the player down? Good for feeling floaty in air, rather than having great stopping friction.
        public float airResistance = 0.05f;

        // Start is called before the first frame update
        private void Start()
        {
            //If the PlayerController is not set in the inspector, disable this script to prevent further errors.
            if (!controller)
            {
                Debug.LogError("Controller not set on Fall! Disabling to prevent further errors.");
                enabled = false;
            }
        }
        
        //Update is called once per frame
        public void Update()
        {
            //If the player is on the ground, we shouldn't move the player as if they're in the air.
            if (controller.IsGrounded()) return;
            
            //Get input and normalize. Prevents faster diagonal movement. Discard the jump value.
            Vector3 input = controller.GetInput();
            input.y = 0;
            input.Normalize();

            //Make the input relative to the direction the player is facing, apply the movement forces.
            Vector3 desiredMove = transform.TransformDirection(input) * moveSpeed;
            desiredMove -= controller.Velocity * airResistance;
            desiredMove /= weight / Time.deltaTime;
            desiredMove.y -= gravity * Time.deltaTime;

            //Move the character according to the new velocity.
            controller.MoveCharacter(desiredMove);
        }
    }
}