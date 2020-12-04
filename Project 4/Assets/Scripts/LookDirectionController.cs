using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDirectionController : MonoBehaviour
{
    public float mouseSensitivity = 5.0f;
    private Vector3 _look = Vector3.zero;

    private void Update()
    {
        _look.x = Input.GetAxis("Mouse X") * Time.deltaTime;
        _look.y = Input.GetAxis("Mouse Y") * Time.deltaTime * -1;


        transform.rotation *= Quaternion.AngleAxis(_look.x * mouseSensitivity, Vector3.up);
        transform.rotation *= Quaternion.AngleAxis(_look.y * mouseSensitivity, Vector3.right);

        var angles = transform.localEulerAngles;
        angles.z = 0;
        var angle = transform.localEulerAngles.x;

        if (angle > 180 && angle < 320)
        {
            angles.x = 320;
        }
        else if (angle < 180 && angle > 60)
        {
            angles.x = 60;
        }
        
        transform.localEulerAngles = angles;
    }
}
