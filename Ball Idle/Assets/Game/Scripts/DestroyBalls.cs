using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBalls : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);
        if (!other.name.Contains("Player"))
            other.gameObject.SetActive(false);
    }
}
