using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int numberOfLevels = 7;
    int levelNumber;
    public TextMeshProUGUI levelNumberText;

    public Transform player;
    Rigidbody playerRB;
    PlayerMovement playerMovementScript;
    public CameraFollow cameraFollowScript;

    [SerializeField]
    int money;
    public TextMeshProUGUI moneyCountVisual;

    int speedLevel;
    int sizeLevel;
    int capacityLevel;

    public TextMeshProUGUI speedLevelText;
    public TextMeshProUGUI sizeLevelText;
    public TextMeshProUGUI capacityLevelText;

    public Material uiGrayscaleMat;

    public Transform levelUpParticlesObject;
    ParticleSystem[] levelUpParticles;

    Vector3 newSize;
    float lerpTime;

    public GameObject smallerHoleMesh;
    public GameObject smallerHoleRing;
    public GameObject biggerHoleRing;

    public GameObject nextLevelPanel;

    // Start is called before the first frame update
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
        playerMovementScript = player.GetComponent<PlayerMovement>();

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

    public int GetMoney() => money;

    void ActivateLevelUpParticles()
    {
        levelUpParticlesObject.position = player.position;
        foreach (ParticleSystem item in levelUpParticles)
            item.Play();
    }

    public void LevelUpSpeed()
    {
        ActivateLevelUpParticles();
        speedLevel++;
        playerMovementScript.maxSpeed *= 1.1f;
        playerMovementScript.movementSpeedModifier *= 1.1f;
        speedLevelText.text = speedLevel.ToString();
    }

    public void LevelUpSpeed(float modif)
    {
        speedLevel++;
        playerMovementScript.maxSpeed = (int)(playerMovementScript.maxSpeed * modif);
        playerMovementScript.movementSpeedModifier = (int)(playerMovementScript.movementSpeedModifier * modif);
        speedLevelText.text = speedLevel.ToString();
    }

    public void LevelUpSize()
    {
        ActivateLevelUpParticles();
        sizeLevel++;
        lerpTime = 0f;
        newSize = player.transform.localScale * 1.12f;
        playerRB.mass *= 3.69f;
        playerRB.constraints = RigidbodyConstraints.None;
        sizeLevelText.text = sizeLevel.ToString();

        if (sizeLevel >= 4 && smallerHoleMesh != null && smallerHoleRing != null && biggerHoleRing != null)
        {
            smallerHoleMesh.SetActive(false);
            smallerHoleRing.SetActive(false);
            biggerHoleRing.SetActive(true);
            player.GetChild(0).GetComponent<IndicatorLogic>().target = biggerHoleRing.transform;
            smallerHoleRing = null;
        }

        if (smallerHoleRing != null)
        {
            BoxCollider boxCollider = smallerHoleRing.GetComponent<BoxCollider>();
            boxCollider.size = boxCollider.size * 1.1f;
        }
        else if (smallerHoleRing != null)
        {
            BoxCollider boxCollider = biggerHoleRing.GetComponent<BoxCollider>();
            boxCollider.size = boxCollider.size * 1.1f;
        }
    }

    public void LevelUpCapacity()
    {
        ActivateLevelUpParticles();
        capacityLevel++;
        playerMovementScript.maxStackSize += 6;
        capacityLevelText.text = capacityLevel.ToString();
    }

    public void LevelUpCapacity(float modif)
    {
        capacityLevel++;
        playerMovementScript.maxStackSize = (int)(playerMovementScript.maxStackSize * modif);
        capacityLevelText.text = capacityLevel.ToString();
    }

    void FreezeY() => playerRB.constraints = RigidbodyConstraints.FreezePositionY;


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

    public int GetSizeLevel() => sizeLevel;
    public int GetSpeedLevel() => speedLevel;
    public int GetCapacityLevel() => capacityLevel;

    public void EnableNextLevelPanel() => nextLevelPanel.SetActive(true);
    public void NextLevel() => LoadLevel((SceneManager.GetActiveScene().buildIndex + 1) % numberOfLevels);
    void LoadLevel(int levelIndex) => SceneManager.LoadScene(levelIndex);


    public void SetGrayscaleSpeed(bool x)
    {
        if (x)
            speedLevelText.GetComponentInParent<Image>().material = uiGrayscaleMat;
        else
            speedLevelText.GetComponentInParent<Image>().material = null;
    }

    public void SetGrayscaleSize(bool x)
    {
        if (x)
            sizeLevelText.GetComponentInParent<Image>().material = uiGrayscaleMat;
        else
            sizeLevelText.GetComponentInParent<Image>().material = null;
    }

    public void SetGrayscaleCapacity(bool x)
    {
        if (x)
            capacityLevelText.GetComponentInParent<Image>().material = uiGrayscaleMat;
        else
            capacityLevelText.GetComponentInParent<Image>().material = null;
    }

    public bool IsFullyUpgraded() => (capacityLevel == 7 && speedLevel == 7 && sizeLevel == 7);
}
