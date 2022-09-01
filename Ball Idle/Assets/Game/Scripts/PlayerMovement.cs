using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public GameManager gameManager;

    public DynamicJoystick joystickScript;

    public float movementSpeedModifier;
    public float maxSpeed = 10f;

    Rigidbody rb;
    public Vector3 direction;

    public List<Transform> stack;
    public int maxStackSize;

    void Awake()
    {
        stack = new List<Transform>();
        stack.Add(transform);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("FreezeY", 0.5f);
    }

    void Update()
    {
        if (stack.Count == maxStackSize)
            gameManager.EnableSellIndicator();
        else
            gameManager.DisableSellIndicator();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float hor = joystickScript.Horizontal;
        float ver = joystickScript.Vertical;
        direction = new Vector3(hor, 0, ver);

        /*
        if (direction == Vector3.zero)
            rb.drag = 5;
        else
            rb.drag = 0;
        */

        //rb.AddForce(direction * Time.fixedDeltaTime * movementSpeedModifier * 10f, ForceMode.Acceleration);
        //rb.AddForce(direction * Time.fixedDeltaTime * movementSpeedModifier, ForceMode.VelocityChange);
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        rb.velocity = Vector3.ClampMagnitude(direction * Time.fixedDeltaTime * movementSpeedModifier * 10f, maxSpeed);

        /*
        if (direction != Vector3.zero)
        {
            //transform.LookAt(transform.position + direction);
            rb.MovePosition(transform.position + direction * Time.fixedDeltaTime * movementSpeedModifier);
            rb.MoveRotation(rb.rotation * Quaternion.Euler((transform.position + direction)));
        }
        */
    }

    public void AddToStack(Transform t)
    {
        stack.Add(t);
    }

    public void RemoveFromStack(Transform t)
    {
        stack.Remove(t);
    }

    void FreezeY()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    public bool isStackFree()
    {
        return maxStackSize > stack.Count;
    }
}
