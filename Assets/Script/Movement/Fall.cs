using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Workshop.Player.Movement
{
    public class Fall : MonoBehaviour
    {
        public PlayerController Controller;
        public float MoveSpeed;
        public float Weight;
        public float Gravity;
        public float AirResistance;

        void Start()
        {
            if (!Controller)
            {
                Debug.LogError("Controller not set on Fall! Disabling to prevent further errors.");
                enabled = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!Controller.IsGrounded())
            {
                Vector3 input = Controller.GetInput();
                input.y = 0;
                input.Normalize();

                Vector3 desiredMove = transform.TransformDirection(input) * MoveSpeed;
                desiredMove -= Controller.Velocity * AirResistance;
                desiredMove /= Weight / Time.deltaTime;
                desiredMove.y -= Gravity * Time.deltaTime;

                Controller.MoveCharacter(desiredMove);
            }
        }
    }
}