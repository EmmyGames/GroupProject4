using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerScript playerScript;
    public Animator anim;

    private int _isMoving;
    private int _isSprinting;
    private int _isGrounded;
    void Start()
    {
        _isMoving = Animator.StringToHash("isMoving");
        _isSprinting = Animator.StringToHash("isSprinting");
        _isGrounded = Animator.StringToHash("isGrounded");
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool(_isMoving, playerScript.playerMovement.movement.GetDirection().magnitude > 0.1f);
        anim.SetBool(_isSprinting, playerScript.playerInput.isSprinting);
        anim.SetBool(_isGrounded, playerScript.playerMovement.isGrounded);
    }
}
