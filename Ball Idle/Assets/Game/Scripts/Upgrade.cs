﻿using System.Collections;
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
    bool isEnabled;

    public float costConstantModifierWave1;
    public float costConstantModifierWave2;
    bool firstUpgradeWaveCleared;

    // Start is called before the first frame update
    void Start()
    {
        isEnabled = false;
        speedCostVisual.text = speedCost.ToString();
        sizeCostVisual.text = sizeCost.ToString();
        capacityCostVisual.text = capacityCost.ToString();

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

        if (money >= speedCost && !(speedLvl == 4 && !firstUpgradeWaveCleared) && speedLvl != 7)
            gameManager.SetGrayscaleSpeed(false);
        else
            gameManager.SetGrayscaleSpeed(true);

        if (money >= capacityCost && !(capacityLvl == 4 && !firstUpgradeWaveCleared) && capacityLvl != 7)
            gameManager.SetGrayscaleCapacity(false);
        else
            gameManager.SetGrayscaleCapacity(true);

        if (money >= sizeCost && !(sizeLvl == 4 && !firstUpgradeWaveCleared) && sizeLvl != 7)
            gameManager.SetGrayscaleSize(false);
        else
            gameManager.SetGrayscaleSize(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isEnabled && other.name.Contains("Player") && !other.isTrigger)
        {
            upgradeAnimator.SetTrigger("slideIn");
            isEnabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (isEnabled && other.name.Contains("Player") && !other.isTrigger)
        {
            upgradeAnimator.SetTrigger("slideOut");
            isEnabled = false;
        }
    }

    public void UpgradeSpeed()
    {
        int money = gameManager.GetMoney();
        if (money >= speedCost && gameManager.GetSpeedLevel() < 7)
        {
            if ((!firstUpgradeWaveCleared && gameManager.GetSpeedLevel() < 4) || (firstUpgradeWaveCleared && gameManager.GetSpeedLevel() < 7))
            {
                gameManager.AddMoney(-speedCost);
                money -= speedCost;

                if (!firstUpgradeWaveCleared)
                    speedCost = (int)(speedCost * costConstantModifierWave1);
                else
                    speedCost = (int)(speedCost * costConstantModifierWave2);

                if (speedCost < 1000)
                    speedCostVisual.text = speedCost.ToString();
                else if (speedCost < 1000000)
                    speedCostVisual.text = (speedCost * 1f / 1000).ToString("n2") + "K";
                else
                    speedCostVisual.text = (speedCost * 1f / 1000000).ToString("n2") + "M";

                gameManager.LevelUpSpeed();
                if (gameManager.GetSpeedLevel() == 4)
                {
                    if (gameManager.GetCapacityLevel() == 4 && gameManager.GetSizeLevel() == 4)
                    {
                        /*
                        gameManager.SetGrayscaleSpeed(false);
                        gameManager.SetGrayscaleCapacity(false);
                        gameManager.SetGrayscaleSize(false);
                        */
                        firstUpgradeWaveCleared = true;

                        if (speedCost < 1000)
                            speedCostVisual.text = speedCost.ToString();
                        else if (speedCost < 1000000)
                            speedCostVisual.text = (speedCost * 1f / 1000).ToString("n2") + "K";
                        else
                            speedCostVisual.text = (speedCost * 1f / 1000000).ToString("n2") + "M";

                        if (sizeCost < 1000)
                            sizeCostVisual.text = sizeCost.ToString();
                        else if (sizeCost < 1000000)
                            sizeCostVisual.text = (sizeCost * 1f / 1000).ToString("n2") + "K";
                        else
                            sizeCostVisual.text = (sizeCost * 1f / 1000000).ToString("n2") + "M";

                        if (capacityCost < 1000)
                            capacityCostVisual.text = capacityCost.ToString();
                        else if (capacityCost < 1000000)
                            capacityCostVisual.text = (capacityCost * 1f / 1000).ToString("n2") + "K";
                        else
                            capacityCostVisual.text = (capacityCost * 1f / 1000000).ToString("n2") + "M";
                    }
                    else
                        speedCostVisual.text = "MAX";
                }
                else if (gameManager.GetSpeedLevel() == 7)
                    speedCostVisual.text = "MAX";

                //if (gameManager.GetSpeedLevel() == 7 || (gameManager.GetSpeedLevel() == 4 && !firstUpgradeWaveCleared))
                //    gameManager.SetGrayscaleSpeed(true);

                if (!(money >= speedCost || money >= sizeCost || money >= capacityCost))
                    gameManager.DisableUpgradeIndicator();
            }
        }
    }

    public void UpgradeSize()
    {
        int money = gameManager.GetMoney();
        if (money >= sizeCost && gameManager.GetSizeLevel() < 7)
        {
            if ((!firstUpgradeWaveCleared && gameManager.GetSizeLevel() < 4) || (firstUpgradeWaveCleared && gameManager.GetSizeLevel() < 7))
            {
                gameManager.AddMoney(-sizeCost);
                money -= sizeCost;

                if (!firstUpgradeWaveCleared)
                    sizeCost = (int)(sizeCost * costConstantModifierWave1);
                else
                    sizeCost = (int)(sizeCost * costConstantModifierWave2);

                if (sizeCost < 1000)
                    sizeCostVisual.text = sizeCost.ToString();
                else if (sizeCost < 1000000)
                    sizeCostVisual.text = (sizeCost * 1f / 1000).ToString("n2") + "K";
                else
                    sizeCostVisual.text = (sizeCost * 1f / 1000000).ToString("n2") + "M";

                gameManager.LevelUpSize();
                if (gameManager.GetSizeLevel() == 4)
                {
                    if (gameManager.GetCapacityLevel() == 4 && gameManager.GetSpeedLevel() == 4)
                    {
                        /*
                        gameManager.SetGrayscaleSpeed(false);
                        gameManager.SetGrayscaleCapacity(false);
                        gameManager.SetGrayscaleSize(false);
                        */
                        firstUpgradeWaveCleared = true;

                        if (speedCost < 1000)
                            speedCostVisual.text = speedCost.ToString();
                        else if (speedCost < 1000000)
                            speedCostVisual.text = (speedCost * 1f / 1000).ToString("n2") + "K";
                        else
                            speedCostVisual.text = (speedCost * 1f / 1000000).ToString("n2") + "M";

                        if (sizeCost < 1000)
                            sizeCostVisual.text = sizeCost.ToString();
                        else if (sizeCost < 1000000)
                            sizeCostVisual.text = (sizeCost * 1f / 1000).ToString("n2") + "K";
                        else
                            sizeCostVisual.text = (sizeCost * 1f / 1000000).ToString("n2") + "M";

                        if (capacityCost < 1000)
                            capacityCostVisual.text = capacityCost.ToString();
                        else if (capacityCost < 1000000)
                            capacityCostVisual.text = (capacityCost * 1f / 1000).ToString("n2") + "K";
                        else
                            capacityCostVisual.text = (capacityCost * 1f / 1000000).ToString("n2") + "M";
                    }
                    else
                        sizeCostVisual.text = "MAX";
                }
                else if (gameManager.GetSizeLevel() == 7)
                    sizeCostVisual.text = "MAX";

                //if (gameManager.GetSizeLevel() == 7 || (gameManager.GetSizeLevel() == 4 && !firstUpgradeWaveCleared))
                //    gameManager.SetGrayscaleSize(true);

                if (!(money >= speedCost || money >= sizeCost || money >= capacityCost))
                    gameManager.DisableUpgradeIndicator();
            }
        }
    }

    public void UpgradeCapacity()
    {
        int money = gameManager.GetMoney();
        if (money >= capacityCost && gameManager.GetCapacityLevel() < 7)
        {
            if ((!firstUpgradeWaveCleared && gameManager.GetCapacityLevel() < 4) || (firstUpgradeWaveCleared && gameManager.GetCapacityLevel() < 7))
            {
                gameManager.AddMoney(-capacityCost);
                money -= capacityCost;

                if (!firstUpgradeWaveCleared)
                    capacityCost = (int)(capacityCost * costConstantModifierWave1);
                else
                    capacityCost = (int)(capacityCost * costConstantModifierWave2);

                if (capacityCost < 1000)
                    capacityCostVisual.text = capacityCost.ToString();
                else if (capacityCost < 1000000)
                    capacityCostVisual.text = (capacityCost * 1f / 1000).ToString("n2") + "K";
                else
                    capacityCostVisual.text = (capacityCost * 1f / 1000000).ToString("n2") + "M";

                gameManager.LevelUpCapacity();
                if (gameManager.GetCapacityLevel() == 4)
                {
                    if (gameManager.GetSpeedLevel() == 4 && gameManager.GetSizeLevel() == 4)
                    {
                        /*
                        gameManager.SetGrayscaleSpeed(false);
                        gameManager.SetGrayscaleCapacity(false);
                        gameManager.SetGrayscaleSize(false);
                        */
                        firstUpgradeWaveCleared = true;

                        if (speedCost < 1000)
                            speedCostVisual.text = speedCost.ToString();
                        else if (speedCost < 1000000)
                            speedCostVisual.text = (speedCost * 1f / 1000).ToString("n2") + "K";
                        else
                            speedCostVisual.text = (speedCost * 1f / 1000000).ToString("n2") + "M";

                        if (sizeCost < 1000)
                            sizeCostVisual.text = sizeCost.ToString();
                        else if (sizeCost < 1000000)
                            sizeCostVisual.text = (sizeCost * 1f / 1000).ToString("n2") + "K";
                        else
                            sizeCostVisual.text = (sizeCost * 1f / 1000000).ToString("n2") + "M";

                        if (capacityCost < 1000)
                            capacityCostVisual.text = capacityCost.ToString();
                        else if (capacityCost < 1000000)
                            capacityCostVisual.text = (capacityCost * 1f / 1000).ToString("n2") + "K";
                        else
                            capacityCostVisual.text = (capacityCost * 1f / 1000000).ToString("n2") + "M";
                    }
                    else
                        capacityCostVisual.text = "MAX";
                }
                else if (gameManager.GetCapacityLevel() == 7)
                    capacityCostVisual.text = "MAX";
                //if (gameManager.GetCapacityLevel() == 7 || (gameManager.GetCapacityLevel() == 4 && !firstUpgradeWaveCleared))
                //    gameManager.SetGrayscaleCapacity(true);

                if (!(money >= speedCost || money >= sizeCost || money >= capacityCost))
                    gameManager.DisableUpgradeIndicator();
            }
        }
    }
}
