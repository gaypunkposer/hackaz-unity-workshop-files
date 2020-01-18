using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Workshop.Player.Movement
{
    public class Walk : MonoBehaviour
    {
        //The PlayerController that we will use to move the player.
        public PlayerController controller;
        //How fast the player walks. Measured in m/s
        public float walkSpeed = 5;
        //How fast the player runs. Measured in m/s
        public float runSpeed = 10;
        
        // Start is called before the first frame update
        private void Start()
        {
            //If the PlayerController is not set in the inspector, disable this script to prevent further errors.
            if (!controller)
            {
                Debug.LogError("Controller not set on Walk! Disabling to prevent further errors.");
                enabled = false;
            }
        }

        //Update is called once per frame
        private void Update()
        {
            //If the player isn't on the ground, don't move them as if they were.
            if (!controller.IsGrounded()) return;
            
            //Get input, ignore jumping, and normalize to get a vector magnitude of one. 
            //Without the normalization, the player will move faster diagonally.
            Vector3 input = controller.GetInput();
            input.y = 0;
            input.Normalize();

            //Take the direction we want to move, and makes it relative to the direction the player is facing. 
            Vector3 desiredMove = transform.TransformDirection(input);

            //Multiply by the desired speed. The sprint button is defined in Project Settings -> Input.
            if (Input.GetButton("Sprint"))
                desiredMove *= runSpeed;
            else
                desiredMove *= walkSpeed;
            
            //Subtract the previous frame's velocity. 
            //This prevents the player from accelerating infinitely and allows them to stop if no input is given.
            controller.MoveCharacter(desiredMove - controller.Velocity);
        }
    }
}
