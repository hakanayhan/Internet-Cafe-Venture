using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerManager : MonoBehaviour
{
    public static ComputerManager Instance;
    public List<Computers> computers = new List<Computers>();
    public List<ComputerUpgradeRank> upgradeRanks;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void LvUp(Computers computer)
    {
        computer.level++;
        computer.computerUpgrades.costMultiplier += 0.1;
        computer.computerUpgrades.upgradeCostMultiplier += 0.15;
        computer.cost = computer.baseCost * computer.computerUpgrades.costMultiplier;
        computer.upgradeCost = computer.baseUpgradeCost * computer.computerUpgrades.upgradeCostMultiplier;
        if(computer.level == upgradeRanks[computer.rank].rankUpLevel)
        {
            if (upgradeRanks.Count > computer.rank)
                computer.rank++;
            if (upgradeRanks.Count <= computer.rank)
                computer.isMaxLv = true;
        }
    }
}

[Serializable] public class Computers
{
    public Computer computerObject;
    public bool isIdle = true;
    public float level;

    public double baseCost;
    public double baseUpgradeCost;
    public double unlockCost;

    public double cost;
    public double upgradeCost;
    
    public int usageTime;
    public double totalCost;

    public int rank;
    public bool isMaxLv;

    public CustomerStateMachine customer;
    public ComputerUpgrades computerUpgrades;
}

[Serializable] public class ComputerUpgrades
{
    public double costMultiplier = 1;
    public double upgradeCostMultiplier = 1;
}

[Serializable] public class ComputerUpgradeRank
{
    public int rankUpLevel;
}