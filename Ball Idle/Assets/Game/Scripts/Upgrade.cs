using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrade : MonoBehaviour
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
    bool firstUpgradeWaveCleared;

    public float finalMaxLevel = 7;

    public float sizeModifier = 1.12f;

    // Start is called before the first frame update
    void Start()
    {
        upgradeUIEnabled = false;
        speedCostVisual.text = "$" + speedCost.ToString();
        sizeCostVisual.text = "$" + sizeCost.ToString();
        capacityCostVisual.text = "$" + capacityCost.ToString();

        firstUpgradeWaveCleared = false;
    }

    void Update()
    {
        int money = gameManager.GetMoney();
        if ((money >= speedCost || money >= sizeCost || money >= capacityCost) && !gameManager.IsFullyUpgraded())
            gameManager.EnableUpgradeIndicator();
        else
            gameManager.DisableUpgradeIndicator();
    }

    // Update is called once per frame
    void OnTriggerStay()
    {
        int money = gameManager.GetMoney();

        int speedLvl = gameManager.GetSpeedLevel();
        int sizeLvl = gameManager.GetSizeLevel();
        int capacityLvl = gameManager.GetCapacityLevel();

        if (money >= speedCost && !(speedLvl == 4 && !firstUpgradeWaveCleared) && speedLvl != finalMaxLevel)
            gameManager.SetGrayscaleSpeed(false);
        else
            gameManager.SetGrayscaleSpeed(true);

        if (money >= capacityCost && !(capacityLvl == 4 && !firstUpgradeWaveCleared) && capacityLvl != finalMaxLevel)
            gameManager.SetGrayscaleCapacity(false);
        else
            gameManager.SetGrayscaleCapacity(true);

        if (money >= sizeCost && !(sizeLvl == 4 && !firstUpgradeWaveCleared) && sizeLvl != finalMaxLevel)
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
            if ((!firstUpgradeWaveCleared && gameManager.GetSpeedLevel() < 4) || (firstUpgradeWaveCleared && gameManager.GetSpeedLevel() < finalMaxLevel))
            {
                gameManager.AddMoney(-speedCost);
                money -= speedCost;

                if (!firstUpgradeWaveCleared)
                    speedCost = (int)(speedCost * costConstantModifierWave1);
                else
                    speedCost = (int)(speedCost * costConstantModifierWave2);

                if (speedCost < 1000)
                    speedCostVisual.text = "$" + speedCost.ToString();
                else if (speedCost < 1000000)
                    speedCostVisual.text = "$" + (speedCost * 1f / 1000).ToString("n2") + "K";
                else
                    speedCostVisual.text = "$" + (speedCost * 1f / 1000000).ToString("n2") + "M";

                gameManager.LevelUpSpeed();
                if (gameManager.GetSpeedLevel() == 4)
                {
                    if (gameManager.GetCapacityLevel() == 4 && gameManager.GetSizeLevel() == 4)
                    {
                        firstUpgradeWaveCleared = true;

                        if (sizeCost < 1000)
                            sizeCostVisual.text = "$" + sizeCost.ToString();
                        else if (sizeCost < 1000000)
                            sizeCostVisual.text = "$" + (sizeCost * 1f / 1000).ToString("n2") + "K";
                        else
                            sizeCostVisual.text = "$" + (sizeCost * 1f / 1000000).ToString("n2") + "M";

                        if (capacityCost < 1000)
                            capacityCostVisual.text = "$" + capacityCost.ToString();
                        else if (capacityCost < 1000000)
                            capacityCostVisual.text = (capacityCost * 1f / 1000).ToString("n2") + "K";
                        else
                            capacityCostVisual.text = (capacityCost * 1f / 1000000).ToString("n2") + "M";
                    }
                    else
                        speedCostVisual.text = "MAX";
                }
                else if (gameManager.GetSpeedLevel() == finalMaxLevel)
                    speedCostVisual.text = "MAX";
            }
        }
    }

    public void UpgradeSize()
    {
        int money = gameManager.GetMoney();
        if (money >= sizeCost && gameManager.GetSizeLevel() < finalMaxLevel)
        {
            if ((!firstUpgradeWaveCleared && gameManager.GetSizeLevel() < 4) || (firstUpgradeWaveCleared && gameManager.GetSizeLevel() < finalMaxLevel))
            {
                gameManager.AddMoney(-sizeCost);
                money -= sizeCost;

                if (!firstUpgradeWaveCleared)
                    sizeCost = (int)(sizeCost * costConstantModifierWave1);
                else
                    sizeCost = (int)(sizeCost * costConstantModifierWave2);

                if (sizeCost < 1000)
                    sizeCostVisual.text = "$" + sizeCost.ToString();
                else if (sizeCost < 1000000)
                    sizeCostVisual.text = "$" + (sizeCost * 1f / 1000).ToString("n2") + "K";
                else
                    sizeCostVisual.text = "$" + (sizeCost * 1f / 1000000).ToString("n2") + "M";

                gameManager.LevelUpSize(sizeModifier);
                if (gameManager.GetSizeLevel() == 4)
                {
                    if (gameManager.GetCapacityLevel() == 4 && gameManager.GetSpeedLevel() == 4)
                    {
                        firstUpgradeWaveCleared = true;

                        if (speedCost < 1000)
                            speedCostVisual.text = "$" + speedCost.ToString();
                        else if (speedCost < 1000000)
                            speedCostVisual.text = "$" + (speedCost * 1f / 1000).ToString("n2") + "K";
                        else
                            speedCostVisual.text = "$" + (speedCost * 1f / 1000000).ToString("n2") + "M";

                        if (capacityCost < 1000)
                            capacityCostVisual.text = "$" + capacityCost.ToString();
                        else if (capacityCost < 1000000)
                            capacityCostVisual.text = "$" + (capacityCost * 1f / 1000).ToString("n2") + "K";
                        else
                            capacityCostVisual.text = "$" + (capacityCost * 1f / 1000000).ToString("n2") + "M";
                    }
                    else
                        sizeCostVisual.text = "MAX";
                }
                else if (gameManager.GetSizeLevel() == finalMaxLevel)
                    sizeCostVisual.text = "MAX";
            }
        }
    }

    public void UpgradeCapacity()
    {
        int money = gameManager.GetMoney();
        if (money >= capacityCost && gameManager.GetCapacityLevel() < finalMaxLevel)
        {
            if ((!firstUpgradeWaveCleared && gameManager.GetCapacityLevel() < 4) || (firstUpgradeWaveCleared && gameManager.GetCapacityLevel() < finalMaxLevel))
            {
                gameManager.AddMoney(-capacityCost);
                money -= capacityCost;

                if (!firstUpgradeWaveCleared)
                    capacityCost = (int)(capacityCost * costConstantModifierWave1);
                else
                    capacityCost = (int)(capacityCost * costConstantModifierWave2);

                if (capacityCost < 1000)
                    capacityCostVisual.text = "$" + capacityCost.ToString();
                else if (capacityCost < 1000000)
                    capacityCostVisual.text = "$" + (capacityCost * 1f / 1000).ToString("n2") + "K";
                else
                    capacityCostVisual.text = "$" + (capacityCost * 1f / 1000000).ToString("n2") + "M";

                gameManager.LevelUpCapacity();
                if (gameManager.GetCapacityLevel() == 4)
                {
                    if (gameManager.GetSpeedLevel() == 4 && gameManager.GetSizeLevel() == 4)
                    {
                        firstUpgradeWaveCleared = true;

                        if (speedCost < 1000)
                            speedCostVisual.text = "$" + speedCost.ToString();
                        else if (speedCost < 1000000)
                            speedCostVisual.text = "$" + (speedCost * 1f / 1000).ToString("n2") + "K";
                        else
                            speedCostVisual.text = "$" + (speedCost * 1f / 1000000).ToString("n2") + "M";

                        if (sizeCost < 1000)
                            sizeCostVisual.text = "$" + sizeCost.ToString();
                        else if (sizeCost < 1000000)
                            sizeCostVisual.text = "$" + (sizeCost * 1f / 1000).ToString("n2") + "K";
                        else
                            sizeCostVisual.text = "$" + (sizeCost * 1f / 1000000).ToString("n2") + "M";
                    }
                    else
                        capacityCostVisual.text = "MAX";
                }
                else if (gameManager.GetCapacityLevel() == finalMaxLevel)
                    capacityCostVisual.text = "MAX";
            }
        }
    }
}
