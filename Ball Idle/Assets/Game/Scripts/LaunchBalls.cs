using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
using PathCreation;

public class LaunchBalls : MonoBehaviour
{

    public PlayerMovement playerMovementScript;

    public float ballSpeed;
    public PathCreator path;

    public float intervalBetweenBalls;
    float oldTime;
    Queue<Transform> ballQueue;

    public Transform pipeStartPoint;

    // Start is called before the first frame update
    void Start()
    {
        ballQueue = new Queue<Transform>();
        oldTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - oldTime > intervalBetweenBalls)
        {
            oldTime = Time.time;
            if (ballQueue.Count > 0)
            {
                Transform ball = ballQueue.Dequeue();
                if (ball != null)
                {
                    ball.GetComponent<MoveToPlayer>().enabled = false;
                    Rigidbody rb = ball.GetComponent<Rigidbody>();
                    rb.isKinematic = false;
                    rb.useGravity = false;
                    rb.constraints = RigidbodyConstraints.None;

                    TravelToTarget ttt = ball.gameObject.AddComponent<TravelToTarget>();
                    ttt.SetTarget(pipeStartPoint);
                }
                /*
                PathFollower pathScript = ball.gameObject.AddComponent<PathFollower>();
                pathScript.speed = ballSpeed;
                pathScript.pathCreator = path;
                pathScript.endOfPathInstruction = EndOfPathInstruction.Stop;
                */
            }
        }
    }
    ///QUEUE
    void OnTriggerEnter(Collider other)
    {
        if (!other.name.Contains("Player") && other.GetComponent<TravelToTarget>() == null)
            ballQueue.Enqueue(other.transform);
        /*
        if(other.name.Contains("Player")){
            foreach (var item in collection)
            {
                
            }
        }
        */
    }

    void OnTriggerExit()
    {

    }
}
