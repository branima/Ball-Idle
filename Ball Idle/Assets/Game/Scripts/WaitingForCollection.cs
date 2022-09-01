using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using NiceVibrations.CrazyLabsExtension;
using UnityEngine;

public class WaitingForCollection : MonoBehaviour
{

    public AudioManager audioManager;
    public GameManager gameManager;
    public Transform player;
    PlayerMovement playerMovementScript;
    Rigidbody playerRB;
    Rigidbody rb;

    int requiredLevelForCollection;

    void Start()
    {
        //Debug.Log(gameManager + ", " + player);
        playerMovementScript = player.GetComponent<PlayerMovement>();
        Invoke("FreezeY", 1.5f);
        playerRB = player.GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();

        if (transform.name.Contains("Small"))
            requiredLevelForCollection = 1;
        else if (transform.name.Contains("Medium"))
            requiredLevelForCollection = 4;
        else
            requiredLevelForCollection = 7;
    }


    void Update()
    {
        //if (transform.position.y > 2.95f)                 ///WALL AD
        //    rb.constraints = RigidbodyConstraints.None;
    }

    /*
    void OnCollisionEnter(Collision other)
    {
        if (playerMovementScript.isStackFree() && playerMovementScript.stack.Contains(other.transform) && gameManager.GetSizeLevel() >= requiredLevelForCollection)
        {
            GetComponent<MoveToPlayer>().enabled = true;
            playerMovementScript.AddToStack(transform);
            Destroy(this);
        }
    }
    */

    void OnTriggerEnter(Collider other)
    {
        if (playerMovementScript.isStackFree() && playerMovementScript.stack.Contains(other.transform) && gameManager.GetSizeLevel() >= requiredLevelForCollection)
        {
            //rb.constraints = RigidbodyConstraints.None;  ///WALL AD
            GetComponent<MoveToPlayer>().enabled = true;
            playerMovementScript.AddToStack(transform);
            HapticFeedbackController.TriggerHaptics(HapticTypes.MediumImpact);
            audioManager.Play(gameObject, "pop2");
            Destroy(this);
        }
    }


    void FreezeY()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
    }
}
