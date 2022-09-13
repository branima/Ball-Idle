using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LimitlessUpgrade : MonoBehaviour
{

    public GameManager gameManager;

    public int speedCost;
    public int sizeCost;
    public int capacityCost;

    public TextMeshProUGUI speedCostVisual;
    public TextMeshProUGUI sizeCostVisual;
    public TextMeshProUGUI capacityCostVisual;

    public Animator upgradeAnimator;
    bool upgradeUIEnabled;

    public float costConstantModifierWave1;
    public float costConstantModifierWave2;

    public float finalMaxLevel = 7;

    public float sizeModifier = 1.12f;

    // Start is called before the first frame update
    void Start()
    {
        upgradeUIEnabled = false;
        speedCostVisual.text = "$" + speedCost.ToString();
        sizeCostVisual.text = "$" + sizeCost.ToString();
        capacityCostVisual.text = "$" + capacityCost.ToString();
    }

    // Update is called once per frame
    void OnTriggerStay()
    {
        int money = gameManager.GetMoney();

        int speedLvl = gameManager.GetSpeedLevel();
        int sizeLvl = gameManager.GetSizeLevel();
        int capacityLvl = gameManager.GetCapacityLevel();

        if (money >= speedCost && speedLvl != finalMaxLevel)
            gameManager.SetGrayscaleSpeed(false);
        else
            gameManager.SetGrayscaleSpeed(true);

        if (money >= capacityCost && capacityLvl != finalMaxLevel)
            gameManager.SetGrayscaleCapacity(false);
        else
            gameManager.SetGrayscaleCapacity(true);

        if (money >= sizeCost && sizeLvl != finalMaxLevel)
            gameManager.SetGrayscaleSize(false);
        else
            gameManager.SetGrayscaleSize(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!upgradeUIEnabled && other.name.Contains("Player") && !other.isTrigger)
        {
            upgradeAnimator.SetTrigger("slideIn");
            upgradeUIEnabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (upgradeUIEnabled && other.name.Contains("Player") && !other.isTrigger)
        {
            upgradeAnimator.SetTrigger("slideOut");
            upgradeUIEnabled = false;
        }
    }

    public void UpgradeSpeed()
    {
        int money = gameManager.GetMoney();
        if (money >= speedCost && gameManager.GetSpeedLevel() < finalMaxLevel)
        {

            gameManager.AddMoney(-speedCost);
            money -= speedCost;
            speedCost = (int)(speedCost * costConstantModifierWave1);

            if (speedCost < 1000)
                speedCostVisual.text = "$" + speedCost.ToString();
            else if (speedCost < 1000000)
                speedCostVisual.text = "$" + (speedCost * 1f / 1000).ToString("n2") + "K";
            else
                speedCostVisual.text = "$" + (speedCost * 1f / 1000000).ToString("n2") + "M";

            gameManager.LevelUpSpeed(1.25f);
        }
    }

    public void UpgradeSize()
    {
        int money = gameManager.GetMoney();
        if (money >= sizeCost && gameManager.GetSizeLevel() < finalMaxLevel)
        {

            gameManager.AddMoney(-sizeCost);
            money -= sizeCost;
            sizeCost = (int)(sizeCost * costConstantModifierWave1);

            if (sizeCost < 1000)
                sizeCostVisual.text = "$" + sizeCost.ToString();
            else if (sizeCost < 1000000)
                sizeCostVisual.text = "$" + (sizeCost * 1f / 1000).ToString("n2") + "K";
            else
                sizeCostVisual.text = "$" + (sizeCost * 1f / 1000000).ToString("n2") + "M";

            gameManager.LevelUpSize(sizeModifier);
        }
    }

    public void UpgradeCapacity()
    {
        int money = gameManager.GetMoney();
        if (money >= capacityCost && gameManager.GetCapacityLevel() < finalMaxLevel)
        {

            gameManager.AddMoney(-capacityCost);
            money -= capacityCost;
            capacityCost = (int)(capacityCost * costConstantModifierWave1);

            if (capacityCost < 1000)
                capacityCostVisual.text = "$" + capacityCost.ToString();
            else if (capacityCost < 1000000)
                capacityCostVisual.text = "$" + (capacityCost * 1f / 1000).ToString("n2") + "K";
            else
                capacityCostVisual.text = "$" + (capacityCost * 1f / 1000000).ToString("n2") + "M";

            gameManager.LevelUpCapacity(1.5f);
        }
    }
}
