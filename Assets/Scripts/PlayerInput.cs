using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;

public class PlayerInput : MonoBehaviour
{
    public PlayerScript playerScript;
    
    public bool isSprinting = false;

    public float moveX;

    public float moveZ;

    public bool isJumping;

    public bool isAttacking;

    public bool isADS;

    public bool toggleMouse;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
        if (playerScript.playerMovement.isGrounded)
            isSprinting = false || Input.GetButton("Sprint");
        
        isJumping = false || Input.GetButton("Jump");
        if (Input.GetButtonDown("Attack") /*&& is holding sword or null*/)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
        //When holding left click and holding a ranged weapon, the player is aiming down sights
        if (Input.GetButton("Attack") /*&& is holding ranged weapon */)
        {
            isADS = true;
        }
        else
        {
            isADS = false;
        }

        toggleMouse = false || Input.GetButtonDown("CharacterMenu");
    }
}
