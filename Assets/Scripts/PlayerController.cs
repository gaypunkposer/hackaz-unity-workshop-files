using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Workshop.Player
{
    /**
     * This script is responsible for managing the player's movement. Other scripts will call it to add velocity to the player.
     */
    public class PlayerController : MonoBehaviour
    {
        //A C# property, effectively a getter and setter rolled into just a variable call.
        //Here, we only have a getter defined to prevent changing the velocity through improper channels.
        //The '=>' tells the getter to use the '_velocity' variable for the value.
        public Vector3 Velocity { get => _velocity; }
        //Should the player receive input?
        public bool shouldReceiveInput = true;

        //The player's current velocity. Measured in m/s.
        private Vector3 _velocity;
        //The CharacterController that we will use to move the player. 
        //CharacterControllers are good because they provide collision and movement without a rigidbody. Rigidbodies allow for physical interactions between objects.
        //The physics engine doesn't run at the same rate that the rest of Unity does, so the character can feel sluggish as the player's input
        //takes time to be processed through the physics engine.
        private CharacterController _controller;
        
        //Start is called before the first frame update
        void Start()
        {
            //Get the CharacterController component on this GameObject.
            //The CharacterController and this script must be on the same GameObject, or this will return null.
            _controller = GetComponent<CharacterController>();
        }

        public void MoveCharacter(Vector3 velocity)
        {
            //We need to keep track of our velocity, so we add the new movement to the current velocity.
            _velocity += velocity;

            //Then we multiply our velocity by delta time, the time between each frame rendering,
            //to move our character in meters per second rather than meters per frame. This smooths out the movement.
            _controller.Move(_velocity * Time.deltaTime);
        }

        public Vector3 GetInput()
        {
            //If we're not supposed to be receiving input, return no input.
            if (!shouldReceiveInput) return Vector3.zero;

            //Get the axes of input, as defined in Project Settings -> Input
            float right = Input.GetAxis("Horizontal");
            float forward = Input.GetAxis("Vertical");

            //GetButtonDown only returns true on the first frame a button is pressed down, great for a jump where we don't want the player to fly.
            bool jump = Input.GetButtonDown("Jump");

            //Create and return a new Vector3 with the input we just got from the player.
            return new Vector3(right, (jump) ? 1 : 0, forward);
        }

        public bool IsGrounded()
        {
            RaycastHit GroundInfo;

            //This sends a sphere towards the ground, of the same dimensions of our CharacterController, to check if we're hitting the ground.
            //It takes into account Time.deltaTime to prevent issues at high framerates where the player jumps, but doesn't get high enough by the next frame
            //to escape the ground check.
            return Physics.SphereCast(transform.position + _controller.center, _controller.radius, Vector3.down, out GroundInfo, (_controller.height / 2 - _controller.radius) + Time.deltaTime * 5f, 1, QueryTriggerInteraction.Ignore);
        }
    }
}