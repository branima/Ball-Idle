using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPositionFreeze : MonoBehaviour
{

    public DynamicJoystick joystickScript;
    public Transform player;
    WaitingForCollection waitingForCollection;
    Quaternion initRot;

    void Start()
    {
        initRot = transform.rotation;
        waitingForCollection = GetComponent<WaitingForCollection>();
    }

    void LateUpdate()
    {
        if (waitingForCollection != null)
            transform.rotation = initRot;
        else
        {
            float hor = joystickScript.Horizontal;
            float ver = joystickScript.Vertical;
            if (hor != 0 || ver != 0)
                transform.LookAt(transform.position + new Vector3(hor, 0, ver));
            else
                transform.LookAt(player);
        }
    }

}
