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

    // Start is called before the first frame update
    void Start()
    {
        lerpTime = 0f;
        travel = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!travel)
            return;

        lerpTime += Time.deltaTime * 3f;
        ball.position = Vector3.Lerp(ball.position, target, lerpTime);
        if (Vector3.Distance(ball.position, target) < 0.1f)
        {
            travel = false;
            ball.LookAt(pipe);
            if (walls != null)
                walls.SetActive(false);
            Rigidbody ballRB = ball.GetComponent<Rigidbody>();
            ballRB.velocity = Vector3.zero;
            ballRB.AddForce((transform.parent.forward * 3f + Vector3.up) * shootForceModifier, ForceMode.VelocityChange);
            //ball.transform.GetChild(3).gameObject.SetActive(false);
            Invoke("AddDrag", 0.75f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            progressBar.SetActive(false);
            foreach (Transform item in other.transform)
                Destroy(item.gameObject);
            other.GetComponent<PlayerMovement>().enabled = false;

            target = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);
            ball = other.transform;
            Rigidbody ballRB = ball.GetComponent<Rigidbody>();
            ballRB.constraints = RigidbodyConstraints.None;
            //ball.GetComponent<Rigidbody>().AddForce((Vector3.up + Vector3.forward) * shootForceModifier, ForceMode.Impulse);
            travel = true;
        }
    }

    void AddDrag()
    {
        ball.gameObject.AddComponent<BrakeOnImpact>();
    }
}
