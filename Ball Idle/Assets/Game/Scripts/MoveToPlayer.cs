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
        //if (transform.position.y < 0.95f){ ///WALL AD

            List<Transform> neighbours = playerMovementScript.stack;

            closestNeighbour = null;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit))
            {
                if (neighbours.Contains(hit.transform))
                    closestNeighbour = hit.transform;
            }

            Vector3 playerDirection = playerMovementScript.direction;
            if (playerDirection != Vector3.zero)
            {
                rb.AddForce(playerDirection * Time.fixedDeltaTime * movementSpeedModifier * 10f, ForceMode.Acceleration);
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }

            if (closestNeighbour != null && Vector3.Distance(closestNeighbour.position, transform.position) > (spaceBetween + closestNeighbour.localScale.x / 2f + transform.localScale.x / 2f))
            {
                Vector3 direction = closestNeighbour.position - transform.position;

                rb.AddForce(direction * Time.fixedDeltaTime * movementSpeedModifier * 20f, ForceMode.Acceleration);
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }

        //} /// WALL AD
    }
}


