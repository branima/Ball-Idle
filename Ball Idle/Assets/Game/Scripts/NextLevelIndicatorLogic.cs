using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelIndicatorLogic : MonoBehaviour
{
    public Transform target;
    Transform player;

    public Material arrowMat;

    // Start is called before the first frame update
    void Start() => player = transform.parent;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3((player.position.x + target.position.x) / 2f, player.position.y, (player.position.z + target.position.z) / 2f);
        transform.position = newPos;
        transform.LookAt(new Vector3(target.position.x, player.position.y, target.position.z));
        transform.localScale = new Vector3(1f, 1f, Vector3.Distance(player.position, target.position) / 2f);
        arrowMat.SetVector("_Tiles", new Vector4(1, Vector3.Distance(player.position, target.position) / 2f, 0, 0));

        //Debug.Log(Vector3.Distance(player.position, target.position));
        if (Vector3.Distance(player.position, target.position) < 5f)
            Destroy(gameObject);
    }
}
