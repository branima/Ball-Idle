using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelToTarget : MonoBehaviour
{
    [SerializeField]
    Transform target;
    Rigidbody rb;

    float lerpTime;

    float movementSpeedModifier = 12.5f;

    void Awake()
    {
        target = null;
        rb = GetComponent<Rigidbody>();
        lerpTime = 0f;
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        Vector3 direction = (target.position - transform.position + 2 * Vector3.down).normalized;

        //transform.LookAt(target);
        rb.MovePosition(transform.position + direction * Time.deltaTime * movementSpeedModifier);
        //transform.Translate(direction * Time.deltaTime * 3f, Space.World);
        if (Vector3.Distance(target.position, transform.position) < 2f) ///RUPA
                                                                        ///if (Vector3.Distance(target.position, transform.position) < 0.1f) /// CEV
        {
            rb.AddForce(direction * Time.fixedDeltaTime * movementSpeedModifier * 25f, ForceMode.VelocityChange);
            target = null;
            //transform.GetComponent<Collider>().enabled = false;
        }
    }

    /*
    ///ALSO RUPA SPECIFIC    
    void Update()
    {
        if (target == null)
            return;

        lerpTime += Time.deltaTime * 0.08f;
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, lerpTime);
    }
    */

    public void SetTarget(Transform target)
    {
        this.target = target;
        rb.drag = 0;
        //rb.isKinematic = true;
    }

    public Transform GetTarget()
    {
        return target;
    }
}
