using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
using NiceVibrations.CrazyLabsExtension;

public class GravitateTowardsHole : MonoBehaviour
{

    public GameManager gameManager;
    public Transform player;
    PlayerMovement playerMovementScript;

    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = player.GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        //Physics.IgnoreLayerCollision(8, 8, true);
        if (playerMovementScript.stack.Contains(other.transform) && other.transform != player)
        {
            audioManager.Play(other.gameObject, "pop2");
            HapticFeedbackController.TriggerHaptics( HapticTypes.MediumImpact);
            other.gameObject.layer = LayerMask.NameToLayer("IgnorableBall");
            Rigidbody ballRB = other.GetComponent<Rigidbody>();
            ballRB.velocity = Vector3.zero;
            playerMovementScript.RemoveFromStack(other.transform);
            other.GetComponent<MoveToPlayer>().enabled = false;
            ballRB.constraints = RigidbodyConstraints.None;
            TravelToTarget ttt = other.gameObject.AddComponent<TravelToTarget>();
            ttt.SetTarget(transform);
        }
        else if (other.name.Contains("Sphere") && other.transform != player)
        {
            audioManager.Play(other.gameObject, "pop2");
            HapticFeedbackController.TriggerHaptics( HapticTypes.MediumImpact);
            other.gameObject.layer = LayerMask.NameToLayer("IgnorableBall");
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            TravelToTarget ttt = other.gameObject.AddComponent<TravelToTarget>();
            ttt.SetTarget(transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Physics.IgnoreLayerCollision(8, 8, false);
        if (other.name.Contains("Sphere") && other.transform != player && other.GetComponent<TravelToTarget>().GetTarget() == null)
        {
            other.gameObject.layer = LayerMask.NameToLayer("Default");
            WaitingForCollection wfc = other.gameObject.AddComponent<WaitingForCollection>();
            wfc.gameManager = gameManager;
            wfc.player = player;
        }
    }
}
