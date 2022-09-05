using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShoot : MonoBehaviour
{

    float lerpTime;
    bool travel;
    Vector3 target;
    Transform ball;

    public float shootForceModifier;
    public Transform pipe;
    public GameObject walls;

    public GameObject progressBar;

    bool charge;
    Rigidbody ballRB;
    public float torqueModifier;

    // Start is called before the first frame update
    void Start()
    {
        lerpTime = 0f;
        travel = false;
        charge = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!travel)
            return;

        lerpTime += Time.deltaTime * 1f;
        ball.position = Vector3.Lerp(ball.position, target, lerpTime);
        if (Vector3.Distance(ball.position, target) < 0.1f)
        {
            travel = false;
            ball.LookAt(pipe);
            if (walls != null)
                walls.SetActive(false);

            ballRB = ball.GetComponent<Rigidbody>();
            ballRB.velocity = Vector3.zero;
            ballRB.constraints = RigidbodyConstraints.FreezePosition;
            charge = true;
            //ballRB.AddForce((transform.parent.forward * 3f + Vector3.up) * shootForceModifier, ForceMode.VelocityChange);
            Invoke("LaunchBall", 1f);
            //Invoke("AddDrag", 0.75f);
        }
    }

    void FixedUpdate()
    {
        if (!charge)
            return;

        ballRB.AddTorque(Vector3.right * 1000f * torqueModifier * Time.fixedDeltaTime, ForceMode.Acceleration);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.name.Contains("Player") && !other.isTrigger)
        {
            progressBar.SetActive(false);
            //foreach (Transform item in other.transform)
            //    Destroy(item.gameObject);
            other.GetComponent<CapsuleCollider>().enabled = false;
            PlayerMovement playerMovementScript = other.GetComponent<PlayerMovement>();
            playerMovementScript.StackClear();
            playerMovementScript.enabled = false;

            target = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);
            ball = other.transform;
            Rigidbody ballRB = ball.GetComponent<Rigidbody>();
            ballRB.constraints = RigidbodyConstraints.None;
            travel = true;
        }
    }

    void LaunchBall()
    {
        charge = false;
        ballRB.constraints = RigidbodyConstraints.None;
        ballRB.AddForce((transform.parent.forward * 3f + Vector3.up) * shootForceModifier, ForceMode.VelocityChange);
        Invoke("AddDrag", 0.75f);
    }

    void AddDrag() => ball.gameObject.AddComponent<BrakeOnImpact>();
}
