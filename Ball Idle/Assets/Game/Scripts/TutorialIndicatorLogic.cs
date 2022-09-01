using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialIndicatorLogic : MonoBehaviour
{
    public Transform target;
    public float distanceFromPlayer;
    public float turnOffDistance;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(target.position, player.position) < turnOffDistance)
            Destroy(gameObject);

        Vector3 direction = (target.position - player.position).normalized;
        transform.position = player.position + direction * (player.localScale.x) + Vector3.up * 0.75f;
        //transform.position = player.position + direction * (player.localScale.x) + Vector3.up * 1.5f;
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }
}
