using System;
using System.Collections;
using System.Collections.Generic;
using Tabtale.TTPlugins;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static int numberOfLevels = 7;
    int levelNumber;
    public TextMeshProUGUI levelNumberText;

    public Transform player;
    Rigidbody playerRB;
    public CameraFollow cameraFollowScript;

    public TextMeshProUGUI moneyCountVisual;

    [SerializeField]
    int money;

    int speedLevel;
    int sizeLevel;
    int capacityLevel;

    public TextMeshProUGUI speedLevelText;
    public TextMeshProUGUI sizeLevelText;
    public TextMeshProUGUI capacityLevelText;

    public Material uiGrayscaleMat;
    public Material defaultUIMat;

    public Transform levelUpParticlesObject;
    ParticleSystem[] levelUpParticles;

    Vector3 newSize;
    float lerpTime;

    public GameObject smallerHole;
    public GameObject smallerHoleRing;
    public GameObject biggerHoleRing;

    public GameObject nextLevelPanel;

    // Start is called before the first frame update
    private void Awake()
    {
        TTPCore.Setup();
    }

    void Start()
    {
        levelNumber = SceneManager.GetActiveScene().buildIndex + 1;

        money = 0;
        moneyCountVisual.text = money.ToString();

        speedLevel = 1;
        sizeLevel = 1;
        capacityLevel = 1;

        newSize = Vector3.zero;

        lerpTime = 0f;

        playerRB = player.GetComponent<Rigidbody>();

        levelNumberText.text = levelNumber.ToString();

        levelUpParticles = levelUpParticlesObject.GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (newSize != Vector3.zero)
        {
            lerpTime += Time.deltaTime * 1f;
            player.transform.localScale = Vector3.Lerp(player.transform.localScale, newSize, lerpTime);
            if (Vector3.Distance(player.transform.localScale, newSize) < 0.01f)
            {
                player.transform.localScale = newSize;
                newSize = Vector3.zero;
                Invoke("FreezeY", 0.1f);
            }
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
        if (money < 1000)
            moneyCountVisual.text = money.ToString();
        else if (money < 1000000)
            moneyCountVisual.text = (money * 1f / 1000).ToString("n2") + "K";
        else
            moneyCountVisual.text = (money * 1f / 1000000).ToString("n2") + "M";

    }

    public int GetMoney()
    {
        return money;
    }

    public void LevelUpSpeed()
    {
        levelUpParticlesObject.position = player.position;
        foreach (ParticleSystem item in levelUpParticles)
            item.Play();
        speedLevel++;
        PlayerMovement playerMovementScript = player.GetComponent<PlayerMovement>();
        playerMovementScript.maxSpeed *= 1.1f;
        playerMovementScript.movementSpeedModifier *= 1.1f;
        speedLevelText.text = speedLevel.ToString();
    }

    public void LevelUpSpeed(float modif)
    {
        speedLevel++;
        PlayerMovement playerMovementScript = player.GetComponent<PlayerMovement>();
        playerMovementScript.maxSpeed = (int)(playerMovementScript.maxSpeed * modif);
        playerMovementScript.movementSpeedModifier = (int)(playerMovementScript.movementSpeedModifier * modif);
        speedLevelText.text = speedLevel.ToString();
    }

    public void LevelUpSize()
    {
        foreach (ParticleSystem item in levelUpParticles)
            item.Play();
        sizeLevel++;
        lerpTime = 0f;
        newSize = player.transform.localScale * 1.12f;
        //player.transform.localScale = player.transform.localScale * 1.225f;
        playerRB.mass *= 3.69f;
        playerRB.constraints = RigidbodyConstraints.None;
        //Invoke("FreezeY", 0.1f);        
        sizeLevelText.text = sizeLevel.ToString();

        if (sizeLevel >= 4 && smallerHole != null && smallerHoleRing != null && biggerHoleRing != null)
        {
            smallerHole.SetActive(false);
            smallerHoleRing.SetActive(false);
            biggerHoleRing.SetActive(true);
            player.GetChild(0).GetComponent<IndicatorLogic>().target = biggerHoleRing.transform;
        }

        levelUpParticlesObject.localScale = player.localScale;
        levelUpParticlesObject.position = player.position;
    }

    public void LevelUpCapacity()
    {
        levelUpParticlesObject.position = player.position;
        foreach (ParticleSystem item in levelUpParticles)
            item.Play();
        capacityLevel++;
        player.GetComponent<PlayerMovement>().maxStackSize += 6;
        capacityLevelText.text = capacityLevel.ToString();
    }

    public void LevelUpCapacity(float modif)
    {
        capacityLevel++;
        player.GetComponent<PlayerMovement>().maxStackSize = (int)(player.GetComponent<PlayerMovement>().maxStackSize * modif);
        capacityLevelText.text = capacityLevel.ToString();
    }

    void FreezeY()
    {
        playerRB.constraints = RigidbodyConstraints.FreezePositionY;
        //playerRB.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void EnableUpgradeIndicator()
    {
        if (!(speedLevel == 7 && capacityLevel == 7 && sizeLevel == 7))
            player.GetChild(1).gameObject.SetActive(true);
    }

    public void DisableUpgradeIndicator()
    {
        player.GetChild(1).gameObject.SetActive(false);
    }

    public void EnableSellIndicator()
    {
        player.GetChild(0).gameObject.SetActive(true);
        player.GetChild(2).gameObject.SetActive(true);
    }

    public void DisableSellIndicator()
    {
        player.GetChild(0).gameObject.SetActive(false);
        player.GetChild(2).gameObject.SetActive(false);
    }

    public int GetSizeLevel()
    {
        return sizeLevel;
    }

    public int GetSpeedLevel()
    {
        return speedLevel;
    }

    public int GetCapacityLevel()
    {
        return capacityLevel;
    }

    public void EnableNextLevelPanel()
    {
        nextLevelPanel.SetActive(true);
    }

    public void NextLevel()
    {
        LoadLevel((SceneManager.GetActiveScene().buildIndex + 1) % numberOfLevels);
    }

    void LoadLevel(int levelIndex)
    {
        //if (levelIndex == 0)
        //    levelIndex++;
        SceneManager.LoadScene(levelIndex);
    }

    public void SetGrayscaleSpeed(bool x)
    {
        if (x)
            speedLevelText.GetComponentInParent<Image>().material = uiGrayscaleMat;
        else
            speedLevelText.GetComponentInParent<Image>().material = defaultUIMat;
    }

    public void SetGrayscaleSize(bool x)
    {
        if (x)
            sizeLevelText.GetComponentInParent<Image>().material = uiGrayscaleMat;
        else
            sizeLevelText.GetComponentInParent<Image>().material = defaultUIMat;
    }

    public void SetGrayscaleCapacity(bool x)
    {
        if (x)
            capacityLevelText.GetComponentInParent<Image>().material = uiGrayscaleMat;
        else
            capacityLevelText.GetComponentInParent<Image>().material = defaultUIMat;
    }

    public bool IsFullyUpgraded()
    {
        return capacityLevel == 7 && speedLevel == 7 && sizeLevel == 7;
    }
}
