using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
using PathCreation;

public class PipeBalls : MonoBehaviour
{

    public float ballSpeed;
    public PathCreator path;

    HashSet<Transform> passedBalls;

    void Start()
    {
        passedBalls = new HashSet<Transform>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!passedBalls.Contains(other.transform))
        {
            passedBalls.Add(other.transform);
            other.GetComponent<TravelToTarget>().enabled = false;
            PathFollower pathScript = other.gameObject.AddComponent<PathFollower>();
            pathScript.speed = ballSpeed;
            pathScript.pathCreator = path;
            pathScript.endOfPathInstruction = EndOfPathInstruction.Stop;
        }
    }
}
