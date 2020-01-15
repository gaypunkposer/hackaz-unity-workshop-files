using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Workshop.Player.Movement;

namespace Workshop.Player.Movement
{
    public class Jump : MonoBehaviour
    {
        public PlayerController Controller;
        public float JumpThrust;
        public int MaxJumps;

        private float m_jumpCount;
        void Start()
        {
            if (!Controller)
            {
                Debug.LogError("Controller not set on Jump! Disabling to prevent further errors.");
                enabled = false;
            }
        }

        void Update()
        {
            //If the player is grounded, reset the number of times they can jump.
            if (Controller.IsGrounded())
            {
                m_jumpCount = 0;
            }

            Vector3 input = Controller.GetInput();
            
            //If the jump button has been pressed and we can jump more, jump.
            if (input.y > 0 && m_jumpCount < MaxJumps)
            {
                m_jumpCount++;
                Controller.MoveCharacter(new Vector3(0, JumpThrust, 0));
            }
        }
    }
}
