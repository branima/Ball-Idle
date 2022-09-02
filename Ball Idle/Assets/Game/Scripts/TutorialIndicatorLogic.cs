using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialIndicatorLogic : MonoBehaviour
{
    public Transform target;
    public float turnOffDistance;
    Transform player;

    public Material arrowMat;

    // Start is called before the first frame update
    void Start() => player = transform.parent;

    void Update()
    {
        if (Vector3.Distance(target.position, player.position) < turnOffDistance)
            Destroy(gameObject);

        Vector3 newPos = new Vector3((player.position.x + target.position.x) / 2f, player.position.y, (player.position.z + target.position.z) / 2f);
        transform.position = newPos;
        transform.LookAt(new Vector3(target.position.x, player.position.y, target.position.z));
        transform.localScale = new Vector3(1f, 1f, Vector3.Distance(player.position, target.position));
        arrowMat.SetVector("_Tiles", new Vector4(1, Vector3.Distance(player.position, target.position), 0, 0));
    }
}
