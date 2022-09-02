using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    Vector3 distanceToPlayer;
    
    void Start() =>  distanceToPlayer = transform.position - player.position;
    void LateUpdate() => transform.position = player.position + distanceToPlayer * player.localScale.x;
}
