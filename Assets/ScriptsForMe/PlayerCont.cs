using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCont : MonoBehaviour
{
    CharacterController charCont;

    private Vector3 moveDirection = Vector3.zero;   //Creates a new vector3 variable that will be used to define how the player will move. Gives it an initial value of zero for all three values.
    public float speed;                       //Creates a new float variable that will store a speed modifier for the player. "f" denotes the number as a float value. "public" will create a little box in the inspector that you can modify.
    public float sprintSpeed;
    private float startSpeed;                       //This will hold the initial speed, so we know what value we should set speed to when the player finishes sprinting.
    public float gravity = 20f;
    public float jumpSpeed = 8f;


    private void Start()        //Anything here will happen at the start of the scene.
    {
        charCont = GetComponent<CharacterController>(); //Finds and stores the reference to the character controller component
        startSpeed = speed;     //set startspeed to speed, which is what we set in the inspector. We can use this to reset speed when player finishes sprinting.

    }


    void Update()              //Anything here happens once per frame.
    {
        PlayerMove();           //Call the function that handles movement
    }

    void PlayerMove()           //Handles all player movement
    {
        if (charCont.isGrounded)    //Checks to see if the player is on the ground.
        {   //Everything in here runs ONLY if the player is on the ground during the current frame.
            float moveX = Input.GetAxis("Horizontal");  //Stores keyboard input into float variables
            float moveZ = Input.GetAxis("Vertical");

            //Converts keyboard input stored in moveX and moveZ to a Vector3, so it can be used for movement.
            //Exercise: try making moveDirection = transform.right; Remove everything else. See what happens. Now try to make it transform.forward. See what happens. Enable both. Enable one, and put a zero. Enable one and put 100. Play around with this code and get a feel for what these do.
            moveDirection = transform.right * moveX + transform.forward * moveZ; //transform.right gets a numerical value for left/right movement. Forward gets forward/back movement. Multiply each by the input for X (horizontal) and Z (vertical) to build a Vector 3 for movement. Does not actually move the player yet.          
            moveDirection *= speed;                                              //Speed multiplier for movement.


            if (Input.GetButton("Jump"))        //If the jump button is pressed.
            {
                moveDirection.y = jumpSpeed;    //Adjust the y coordinates of the moveDirection Vector3 variable when the jump button is pressed.
            }

        }

        //This is where we apply gravity. Gravity is multipled by Time.deltaTime twice - here and below where we actually move the character controller. This is because gravity accelerates, and is defined in terms of meters per second squared. The squared part is why we multiply by deltaTime twice.  
        moveDirection.y -= gravity * Time.deltaTime;
        charCont.Move(moveDirection * Time.deltaTime);  //Actually moves the GameObject with the character controller on it.
    }
}
