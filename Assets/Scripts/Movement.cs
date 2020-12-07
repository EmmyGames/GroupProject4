using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Movement : MonoBehaviour
{
    //Components
    private CharacterController _character;

    private const float Gravity = -9.81f;

    //Movement
    private Vector3 _direction;
    private Vector3 _lastDirection;

    public float jumpHeight = 1.3f;
    public float walkSpeed = 3.5f;
    public float walkBackMultiplier = 0.8f;
    public float sprintSpeed = 6f;
    public float speed;

    private Vector3 _moveDir;

    private float _targetAngle;
    private float _lookAngle;
    private float _angle;
    private const float JumpControlModifier = 1f;
    private const float TurnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;

    //Used for gravity calculation
    private Vector3 _velocity = Vector3.zero;

    private void Start()
    {
        _character = GetComponent<CharacterController>();
        speed = walkSpeed;
    }

    public void EntityMovement(bool isGrounded, bool isSprinting, bool isJumping, Transform lookDirTransform, float startRotation, Vector3 direction, bool isAttacking, bool isADS)
    {
        speed = isSprinting ? sprintSpeed : walkSpeed;
        direction = GetAxis(_lastDirection, direction);
        _lastDirection = direction;

        _direction = direction;

        if (isGrounded && _velocity.y < 0f)
        {
            //Reset the velocity that it had accumulated while on the ground
            _velocity.y = -2f;
        }

        //if not grounded (or most likely jumping), change movement
        if (!isGrounded)
        {
            _lookAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            _targetAngle = _lookAngle + lookDirTransform.eulerAngles.y - startRotation;
            transform.rotation = Quaternion.Euler(0f, _targetAngle, 0f);
            lookDirTransform.localEulerAngles = new Vector3(lookDirTransform.eulerAngles.x, -_lookAngle, 0);
            //Allows the player to have a slight amount of control while in the air determined by jumpControlModifier
            _moveDir += Quaternion.Euler(0f, _targetAngle, 0f) * direction * (JumpControlModifier * Time.deltaTime);
            _character.Move(speed * Time.deltaTime * _moveDir);
        }
        //Changes to walking 3rd person mode
        else if (isADS)
        {
            speed = walkSpeed;
            if (direction.z < 0.01f)
            {
                speed *= walkBackMultiplier;
            }

            //point player at camera
            _targetAngle = lookDirTransform.eulerAngles.y - startRotation;
            _moveDir = Quaternion.Euler(0f, _targetAngle, 0f) * direction;
            lookDirTransform.localEulerAngles = new Vector3(lookDirTransform.eulerAngles.x, 0, 0);
            //put if(direction.magnitude > 0.1f) to make the player not rotate while standing still.
            transform.rotation = Quaternion.Euler(0f, _targetAngle, 0f);
            _character.Move(speed * Time.deltaTime * _moveDir);
        }
        else if(!isAttacking)
        {
            //point player relative to the camera based on which direction they are moving
            _lookAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            _targetAngle = _lookAngle + lookDirTransform.eulerAngles.y - startRotation;
            //if the player isn't moving, this will be 0, 0, 0 which is intended
            _moveDir = Quaternion.Euler(0f, _targetAngle, 0f) * direction;

            if (direction.magnitude >= 0.1f)
            {
                //TODO: see if rotation can be smoother. Changed _angle to _targetAngle for the time being
                transform.rotation = Quaternion.Euler(0f, _targetAngle, 0f);
                //reset the look direction rotation because it is a child of Player
                _moveDir = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;
                lookDirTransform.localEulerAngles = new Vector3(lookDirTransform.eulerAngles.x, -_lookAngle, 0);
                //Debug.Log(lookDirTransform.localEulerAngles);
                _character.Move(speed * Time.deltaTime * _moveDir);
            }
        }

        //Jump Calculations
        //Player can jump smaller amounts by letting go of space
        if (isJumping && isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * Gravity);
        }

        _velocity.y += Gravity * Time.deltaTime;
        _character.Move(_velocity * Time.deltaTime);
    }

    public Vector3 GetDirection()
    {
        return _direction;
    }

    Vector3 a = Vector3.zero;
    Vector3 b = Vector3.zero;
    float t = 0f;

    private Vector3 GetAxis(Vector3 lastFrame, Vector3 thisFrame)
    {
        //How fast the acceleration and deceleration is
        const float acceleration = 7f;
        if (lastFrame != thisFrame)
        {
            a = lastFrame;
            b = thisFrame;
            t = 0f;
        }

        t += acceleration * Time.deltaTime;
        return Vector3.Lerp(a, b, t);
    }
}
