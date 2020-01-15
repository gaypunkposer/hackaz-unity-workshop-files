using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Workshop.Player
{
    public class PlayerController : MonoBehaviour
    {
        public Vector3 Velocity { get => m_velocity; }
        public bool ShouldReceiveInput = true;

        private Vector3 m_velocity;
        private CharacterController m_controller;
        void Start()
        {
            m_controller = GetComponent<CharacterController>();
        }

        public void MoveCharacter(Vector3 velocity)
        {
            //We need to keep track of our velocity, so we add the new movement to the current velocity.
            m_velocity += velocity;

            //Then we multiply our velocity by delta time to move our character in meters per second rather than meters per frame. This smooths out the movement.
            m_controller.Move(m_velocity * Time.deltaTime);
        }

        public Vector3 GetInput()
        {
            if (!ShouldReceiveInput) return Vector3.zero;

            //Get the axes of input, as defined in Project Settings->Input
            float right = Input.GetAxis("Horizontal");
            float forward = Input.GetAxis("Vertical");

            bool jump = Input.GetButtonDown("Jump");

            return new Vector3(right, (jump) ? 1 : 0, forward);
        }

        public bool IsGrounded()
        {
            RaycastHit GroundInfo;

            //This sends a sphere towards the ground, of the same dimensions of our CharacterController, to check if we're hitting the ground.
            //It takes into account Time.deltaTime to prevent issues with jumping at high framerates.
            //This is a code snippet from a game I'm working on. Shhhhhhh
            return Physics.SphereCast(transform.position + m_controller.center, m_controller.radius, Vector3.down, out GroundInfo, (m_controller.height / 2 - m_controller.radius) + Time.deltaTime * 5f, 1, QueryTriggerInteraction.Ignore);
        }
    }
}