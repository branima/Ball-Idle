using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;

    Vector3 distanceToPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        distanceToPlayer = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position + distanceToPlayer * player.localScale.x;
    }
}
