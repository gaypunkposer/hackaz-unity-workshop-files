using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Workshop.Player.Movement
{
    public class Walk : MonoBehaviour
    {
        public PlayerController Controller;
        public float Speed;
        void Start()
        {
            if (!Controller)
            {
                Debug.LogError("Controller not set on Walk! Disabling to prevent further errors.");
                enabled = false;
            }
        }

        void Update()
        {
            if (Controller.IsGrounded())
            {
                //Get input, ignore jumping, and normalize to get a length of one. 
                //Without the normalization, the player will move faster diagonally.
                Vector3 input = Controller.GetInput();
                input.y = 0;
                input.Normalize();

                //Take the direction we want to move, and makes it relative to the world. 
                //This allows us to rotate the character and always move forward relative to the character.
                Vector3 desiredMove = transform.TransformDirection(input) * Speed;

                //Subtract the previous frame's velocity. 
                //This prevents the player from accelerating infinitely and allows them to stop if no input is given.
                Controller.MoveCharacter(desiredMove - Controller.Velocity);
            }
        }
    }
}
