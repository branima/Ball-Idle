using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelToTarget : MonoBehaviour
{
    [SerializeField]
    Transform target;
    Rigidbody rb;

    float movementSpeedModifier = 18f; ///15

    void Awake()
    {
        target = null;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        Vector3 direction = (target.position - transform.position + 2 * Vector3.down).normalized;
        rb.MovePosition(transform.position + direction * Time.deltaTime * movementSpeedModifier);
        if (Vector3.Distance(target.position, transform.position) < 1.5f)
        {
            rb.AddForce(direction * Time.fixedDeltaTime * movementSpeedModifier * 25f, ForceMode.VelocityChange);
            target = null;
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        rb.drag = 0;
    }

    public Transform GetTarget() => target;
}
