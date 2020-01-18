using System;
using UnityEngine;

namespace Workshop.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        public PlayerController controller;
        public Animator animator;
        
        //This takes each Animator property name and converts it to the hash it represents. This gives a slight performance boost on runtime.
        private static readonly int Grounded = Animator.StringToHash("Grounded");
        private static readonly int RightSpeed = Animator.StringToHash("RightSpeed");
        private static readonly int ForwardSpeed = Animator.StringToHash("ForwardSpeed");
        private static readonly int Sprinting = Animator.StringToHash("Sprinting");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Speed = Animator.StringToHash("Speed");

        //Start is called before the first frame update
        private void Start()
        {
            //If we don't have our controller set, disable this script to prevent further errors.
            if (!controller)
            {
                Debug.Log(
                    "PlayerController not set for PlayerAnimationController! Disabling to prevent further errors");
                enabled = false;
            }

            //If we don't have our animator set, disable this script to prevent further errors.
            if (!animator)
            {
                Debug.Log("Model Animator not set for PlayerAnimationController! Disabling to prevent further errors");
                enabled = false;
            }
        }

        //Update is called once per frame
        private void Update()
        {
            //Set booleans for the Animator, using the parameter specified in the AnimatorController
            animator.SetBool(Grounded, controller.IsGrounded());
            animator.SetBool(Sprinting, Input.GetButton("Sprint"));
            
            //Set a trigger for the Animator. A trigger is slightly different from a boolean, because a trigger is set back to 'false'
            //when a transition uses it. Keep in mind a trigger will stay 'true' until it is consumed by a transition.
            if (controller.GetInput().y > 0) animator.SetTrigger(Jump);
            
            //Set an integer for the Animator. Here, we're just setting the overall speed.
            animator.SetInteger(Speed, (int)new Vector3(controller.Velocity.x, 0, controller.Velocity.z).magnitude);
            
            //Set floats for the Animator. These are being used in a Blend Tree, which allows us to smoothly interpolate between several
            //similar animations (aka moving in 8 directions). The Dot product is used to get how far the character is moving in each direction.
            animator.SetFloat(RightSpeed, Vector3.Dot(controller.Velocity.normalized, controller.transform.right));
            animator.SetFloat(ForwardSpeed, Vector3.Dot(controller.Velocity.normalized, controller.transform.forward));
        }
    }
}