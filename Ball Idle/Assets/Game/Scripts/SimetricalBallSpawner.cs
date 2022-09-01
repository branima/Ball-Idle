using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimetricalBallSpawner : MonoBehaviour
{
    public int numberOfObjects = 35;
    public float radius = 1;
    public int bigWaves;
    public int midWaves;
    public int smallWaves;
    public float elevation = 1f; ///15.5f

    public GameObject bigBall;
    public GameObject mediumBall;
    public GameObject smallBall;

    public AudioManager audioManager;
    public GameManager gameManager;
    public Transform player;
    public Transform ballsParentObject;

    public Material bigSphereMat;
    public Material midSphereMat;
    public Material smallSphereMat;

    void Awake()
    {
        for (int j = 0; j < bigWaves + midWaves + smallWaves; j++)
        {
            for (int i = 0; i < numberOfObjects + j * 10; i++)
            {
                float angle = i * Mathf.PI * 2 / (numberOfObjects + j * 10) + j * 1f / Mathf.PI;
                Vector3 pos = transform.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * (radius + j * 2.1f) + Vector3.up * elevation;
                GameObject sphere;
                if (j < bigWaves) ///2
                {
                    sphere = Instantiate(bigBall, pos, Quaternion.identity, ballsParentObject);
                    if (bigSphereMat != null)
                        sphere.GetComponent<Renderer>().material = bigSphereMat;
                }
                else if (j < bigWaves + midWaves) //5
                {
                    sphere = Instantiate(mediumBall, pos, Quaternion.identity, ballsParentObject);
                    if (midSphereMat != null)
                        sphere.GetComponent<Renderer>().material = midSphereMat;
                }
                else
                {
                    sphere = Instantiate(smallBall, pos, Quaternion.identity, ballsParentObject);
                    if (smallSphereMat != null)
                        sphere.GetComponent<Renderer>().material = smallSphereMat;
                }
                WaitingForCollection wfc = sphere.GetComponent<WaitingForCollection>();
                wfc.gameManager = gameManager;
                wfc.audioManager = audioManager;
                wfc.player = player;
                sphere.GetComponent<MoveToPlayer>().player = player;
                sphere.SetActive(true);
            }
        }
    }
}
