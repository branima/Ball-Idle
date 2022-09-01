using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{

    public Transform player;
    PlayerMovement playerMovementScript;
    public float spaceBetween = 1f;

    Rigidbody rb;
    [SerializeField]
    float movementSpeedModifier;
    float maxSpeed;

    [SerializeField]
    Transform closestNeighbour;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovementScript = player.GetComponent<PlayerMovement>();
        movementSpeedModifier = playerMovementScript.movementSpeedModifier;
        //movementSpeedModifier = playerMovementScript.movementSpeedModifier * 1.5f; ///WALL AD
        maxSpeed = playerMovementScript.maxSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (transform.position.y < 0.95f) { ///WALL AD

            List<Transform> neighbours = playerMovementScript.stack;

            closestNeighbour = null;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit))
            {
                if (neighbours.Contains(hit.transform))
                    closestNeighbour = hit.transform;
            }

            /*
            Transform closestNeighbour = null;
            float dist = float.MaxValue;
            */

            /*
            ///VERSION 2.1
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.1f);
            foreach (Collider hit in hitColliders)
            {
                if (hit.transform != transform && hit.transform.name.Contains("Sphere") && !hit.GetComponent<WaitingForCollection>())
                {
                    float newDist = Vector3.Distance(hit.transform.position, player.position);
                    if (newDist < dist)
                    {
                        closestNeighbour = hit.transform;
                        dist = newDist;
                    }
                }
            }
            */

            /*
            ///VERSION 2
            List<Transform> neighbours = playerMovementScript.stack;
            foreach (Transform hit in neighbours)
            {
                float newDist = Vector3.Distance(hit.position, player.position);
                if (newDist < dist)
                {
                    closestNeighbour = hit;
                    dist = newDist;
                }
            }
            //Debug.Log(transform.name + ", " + closestNeighbour.name);
            */
            Vector3 playerDirection = playerMovementScript.direction;
            if (playerDirection != Vector3.zero)
            {
                rb.AddForce(playerDirection * Time.fixedDeltaTime * movementSpeedModifier * 10f, ForceMode.Acceleration);
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }

            /*
            if (playerDirection == Vector3.zero)
                rb.drag = 0.25f;
            else
                rb.drag = 0;
            */

            if (closestNeighbour != null && Vector3.Distance(closestNeighbour.position, transform.position) > (spaceBetween + closestNeighbour.localScale.x / 2f + transform.localScale.x / 2f))
            {
                Vector3 direction = closestNeighbour.position - transform.position;

                rb.AddForce(direction * Time.fixedDeltaTime * movementSpeedModifier * 20f, ForceMode.Acceleration);
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }

            /*
            ///VERSION 1
             Vector3 playerDirection = playerMovementScript.direction;
            if (playerDirection != Vector3.zero)
            {
                rb.AddForce(playerDirection * Time.fixedDeltaTime * movementSpeedModifier * 10f, ForceMode.Acceleration);
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }
            if (Vector3.Distance(player.position, transform.position) > spaceBetween)
            {
                Vector3 direction = player.position - transform.position;

                rb.AddForce(direction * Time.fixedDeltaTime * movementSpeedModifier * 10f, ForceMode.Acceleration);
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }

            foreach (Transform neighbour in playerMovementScript.stack)
            {
                if (neighbour != transform && Vector3.Distance(neighbour.position, transform.position) < spaceBetween)
                {
                    Vector3 direction = transform.position - neighbour.position;

                    rb.AddForce(direction * Time.fixedDeltaTime * movementSpeedModifier * 10f, ForceMode.Acceleration);
                    rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
                }
            }
            */
        //} /// WALL AD
    }
}


