using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesListController : MonoBehaviour
{
    public static UpgradesListController Instance;
    public List<Upgrade> upgradesList;
    public GameObject upgradePrefab;
    public GameObject parentGameObject;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        ListUpgrades();
    }

    void ListUpgrades()
    {
        foreach (Upgrade upgrade in upgradesList)
        {
            GameObject obj = Instantiate(upgradePrefab, parentGameObject.transform);
            obj.transform.GetComponent<UpgradesListItemController>().upgrade = upgrade;
        }
    }
}

[Serializable]
public class Upgrade
{
    public Sprite icon;
    public string upgradeTitle;
    public string upgradeText;
    public double basePrice;
    public int level;
    public enum Features { popularity }
    public Features feature;
    public float currentRate = 1;
    public float multiplyRate = 1;
    public float addRate = 0;
}
