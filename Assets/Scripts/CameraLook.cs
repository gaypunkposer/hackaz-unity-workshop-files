using UnityEngine;

namespace Workshop.Player
{
    /**
     * This script handles moving the camera relative to the mouse.
     */
    public class CameraLook : MonoBehaviour
    {
        //The root of the player object. This will rotate to make the player character look left and right.
        public Transform rootObject;
        
        // Start is called before the first frame update
        private void Start()
        {
            //Lock the cursor to the game window...
            Cursor.lockState = CursorLockMode.Locked;
            //and make it invisible
            Cursor.visible = false;
        }

        //Update is called once per frame
        private void Update()
        {
            //Get how far the mouse has moved since the last frame
            Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            //Get the rotation of this object relative to the parent object.
            Vector3 yangle = transform.localEulerAngles;
            //Subtract the mouse movement, or add to invert up/down looking
            yangle.x -= mouseInput.y;
            //Make the angles between -180 and 180, to make them easier to clamp
            yangle.x = (yangle.x > 180) ? yangle.x - 360 : yangle.x;
            //Then clamp between -90 and 90, to prevent gimble lock.
            yangle.x = Mathf.Clamp(yangle.x, -90, 90);
            
            //Get the root object's rotation relative to the world.
            Vector3 xangle = rootObject.eulerAngles;
            //Add the mouse movement.
            xangle.y += mouseInput.x;
            //Remove any up and down rotation, this will cause glitches.
            xangle.x = 0f;

            
            //Set the new rotations to the respective objects
            transform.localRotation = Quaternion.Euler(yangle);
            rootObject.rotation = Quaternion.Euler(xangle);
        }
    }
}