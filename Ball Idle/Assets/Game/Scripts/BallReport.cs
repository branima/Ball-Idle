using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReport : MonoBehaviour
{

    int small;
    int medium;
    int large;

    // Start is called before the first frame update
    void Start()
    {
        small = 0;
        medium = 0;
        large = 0;

        foreach (Transform item in transform)
        {
            if (!item.gameObject.activeSelf)
                continue;
            float mass = item.GetComponent<Rigidbody>().mass;
            if (mass == 1)
                small++;
            else if (mass == 50)
                medium++;
            else
                large++;
        }

        Debug.Log("Small: " + small + ", Medium: " + medium + ", Large: " + large);
    }
}
