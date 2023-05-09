using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public static Wallet Instance;
    public UIManager UIManager;

    public double startingMoneyAmount;
    private Currency _moneyAmount;

    [HideInInspector]public Currency moneyAmount
    {
        get { return _moneyAmount; }
        set
        {
            _moneyAmount = value;
            UIManager.SetMoneyText(_moneyAmount);
        }
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        moneyAmount = new Currency(startingMoneyAmount);
        UIManager = FindObjectOfType<UIManager>();
    }

    public void AddMoney(double amtToAdd)
    {
        moneyAmount += amtToAdd;
    }

    public bool TryRemoveMoney(double money)
    {
        if (moneyAmount < money)
            return false;

        moneyAmount -= money;
        return true;
    }

    public bool TryRemoveMoney(Currency money)
    {
        return TryRemoveMoney((double)money);
    }

    public Currency GetMoneyBalance()
    {
        return moneyAmount;
    }

    public void SetMoney(double amt)
    {
        moneyAmount = new Currency(amt);
    }
}
