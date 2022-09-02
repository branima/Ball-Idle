using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorLogic : MonoBehaviour
{

    public Transform target;
    public float turnOffDistance;
    Transform player;

    MeshRenderer meshRenderer;
    public Material arrowMat;

    bool onboardingOn;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent;
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        onboardingOn = true;
    }

    void Update()
    {
        //Debug.Log(Vector3.Distance(target.position, player.position));
        if (Vector3.Distance(target.position, player.position) < turnOffDistance)
        {
            if (meshRenderer.enabled)
                meshRenderer.enabled = false;
        }
        else if (!meshRenderer.enabled)
            meshRenderer.enabled = true;

        if (onboardingOn)
        {
            Vector3 newPos = new Vector3((player.position.x + target.position.x) / 2f, player.position.y, (player.position.z + target.position.z) / 2f);
            transform.position = newPos;
            transform.LookAt(new Vector3(target.position.x, player.position.y, target.position.z));
            transform.localScale = new Vector3(1f, 1f, Vector3.Distance(player.position, target.position));
            arrowMat.SetVector("_Tiles", new Vector4(1, Vector3.Distance(player.position, target.position), 0, 0));
        }
        else
        {
            Vector3 direction = (target.position - player.position).normalized;
            transform.position = player.position + direction * (player.localScale.x) + Vector3.up;
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }
    }

    public void SetMeshRenderer(MeshRenderer x) => meshRenderer = x;

    public void OnDisable()
    {
        onboardingOn = false;
        transform.localScale = Vector3.one;
    }
}
