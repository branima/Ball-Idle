using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorLogic : MonoBehaviour
{

    public Transform target;
    public float distanceFromPlayer;
    public float turnOffDistance;
    Transform player;

    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent;
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(target.position, player.position) < turnOffDistance)
        {
            if (meshRenderer.enabled)
            {
                meshRenderer.enabled = false;
                //Destroy(gameObject);
            }
        }
        else if (!meshRenderer.enabled)
            meshRenderer.enabled = true;

        Vector3 direction = (target.position - player.position).normalized;
        transform.position = player.position + direction * (player.localScale.x) + Vector3.up;
        //transform.position = player.position + direction * (player.localScale.x) + Vector3.up * 2f;
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }

    public void SetMeshRenderer(MeshRenderer x)
    {
        meshRenderer = x;
    }
}
