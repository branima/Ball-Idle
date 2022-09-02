using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeOnImpact : MonoBehaviour
{

    void OnCollisionEnter()
    {
        GetComponent<Rigidbody>().drag = 5f;
        Invoke("NextLevelPrompt", 1.5f);
    }

    void NextLevelPrompt() => GameObject.FindObjectOfType<GameManager>().EnableNextLevelPanel();
}
