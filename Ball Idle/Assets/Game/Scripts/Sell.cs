using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sell : MonoBehaviour
{

    public GameManager gameManager;
    public Transform player;
    PlayerMovement playerMovementScript;

    public GameObject moneyTextPrefab;
    Queue<GameObject> moneyTextInstances;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = player.GetComponent<PlayerMovement>();

        moneyTextInstances = new Queue<GameObject>();
        for (int i = 0; i < 80; i++)
        {
            GameObject moneyInstance = Instantiate(moneyTextPrefab, transform.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)), moneyTextPrefab.transform.rotation);
            moneyTextInstances.Enqueue(moneyInstance);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Sphere") && other.transform != player)
        {
            playerMovementScript.RemoveFromStack(other.transform);
            int rewardMoney = other.GetComponent<BallAttributesAndLogic>().GetRewardMoney();
            gameManager.AddMoney(rewardMoney);

            GameObject moneyInstance = moneyTextInstances.Dequeue();
            moneyInstance.GetComponent<TextMeshPro>().text = "+$" + rewardMoney.ToString();
            moneyInstance.transform.position = other.transform.position + Vector3.up + Vector3.forward;
            moneyInstance.SetActive(true);
            moneyTextInstances.Enqueue(moneyInstance);
        }
    }
}
