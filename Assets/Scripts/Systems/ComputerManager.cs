using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerManager : MonoBehaviour
{
    public static ComputerManager Instance;
    public List<Computers> computers = new List<Computers>();

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
    }
}

[Serializable] public class Computers
{
    public Computer computerObject;
    public bool isIdle = true;
    public float level;
    public double baseCost;
    public double cost;
    public double baseUpgradeCost;
    public double upgradeCost;
    public int usageTime;
    public double totalCost;
    public CustomerStateMachine customer;
    public ComputerUpgrades computerUpgrades;
}

[Serializable] public class ComputerUpgrades
{
    public float unlockCost;
    public double costMultiplier = 1;
    public double upgradeCostMultiplier = 1;
}