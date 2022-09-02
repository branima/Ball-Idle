using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class BallDetacher : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PathFollower>().enabled = false;
        other.GetComponent<Rigidbody>().AddForce(Vector3.down * 30f, ForceMode.VelocityChange);
    }
}
