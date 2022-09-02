using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualResize : MonoBehaviour
{

    public GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("BEN");
            gameManager.LevelUpSize();
            gameManager.LevelUpCapacity(1.3f);
            gameManager.LevelUpSpeed(1.1f);
        }
    }
}
