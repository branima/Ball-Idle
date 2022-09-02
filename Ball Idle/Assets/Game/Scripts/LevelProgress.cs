using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgress : MonoBehaviour
{

    public Slider progressBar;

    int startChildCount;

    public Transform player;
    public GameObject levelEndRamp;
    public Animator gateAnimator;

    void Start() => startChildCount = transform.childCount;

    void Update()
    {
        int childCnt = 0;
        foreach (Transform child in transform)
            if (child.gameObject.activeSelf)
                childCnt++;
        progressBar.value = 1.15f - childCnt * 1f / startChildCount;
        if (progressBar.value >= 1f && player.childCount > 3 && !player.GetChild(3).gameObject.activeSelf)
        {
            gateAnimator.SetTrigger("gateOpenTrigger");
            levelEndRamp.SetActive(true);
            player.GetChild(3).gameObject.SetActive(true);
            //player.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
