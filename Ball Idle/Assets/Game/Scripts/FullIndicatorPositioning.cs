using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullIndicatorPositioning : MonoBehaviour
{

    Transform player;

    SpriteRenderer spriteRenderer;
    float lerpTime;

    // Start is called before the first frame update
    void Start() => player = transform.parent;

    void OnEnable()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        lerpTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + Vector3.up * player.localScale.x;
        transform.LookAt(Camera.main.transform);

        lerpTime += Time.deltaTime * 2f;
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.white, lerpTime);
    }
}
