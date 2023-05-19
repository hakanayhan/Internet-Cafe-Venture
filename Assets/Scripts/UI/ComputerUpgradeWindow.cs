using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerUpgradeWindow : Window
{
    [SerializeField] GameObject panel;
    [SerializeField] Text deviceNameText;
    [SerializeField] Text levelText;
    [SerializeField] Text costPerSecText;
    [SerializeField] Text upgradeCostText;
    [SerializeField] ProgressBar progressBar;
    [SerializeField] Image progressBarFill;
    public Button buttonObj;
    [SerializeField] private GameObject coinObj;
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private GameObject stars;
    public List<GameObject> starsList;
    Computers computer;
    float level;
    Currency cost;
    Currency upgradeCost;
    public static ComputerUpgradeWindow Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void LoadData(Computers computer)
    {
        this.computer = computer;
        Refresh();
    }

    public void Refresh()
    {
        int index = ComputerManager.Instance.computers.IndexOf(computer) + 1;
        string name = "Computer " + index;
        deviceNameText.text = name;
        level = computer.level;
        levelText.text = "Level " + level.ToString();
        cost = new Currency(computer.cost);
        costPerSecText.text = cost.ToString() + "/sec";
        upgradeCost = new Currency(computer.upgradeCost);
        upgradeCostText.text = upgradeCost.ToString();
        SetRank();
    }

    void SetRank()
    {
        if (!computer.isMaxLv)
        {
            int rank = computer.rank;
            float prevRankLv = ComputerManager.Instance.upgradeRanks[rank - 1].rankUpLevel;
            float rankLv = ComputerManager.Instance.upgradeRanks[rank].rankUpLevel;
            float progress = ((computer.level - prevRankLv) % (rankLv - prevRankLv)) / (rankLv - prevRankLv);
            progressBar.SetFillAmount(progress);
            buttonObj.interactable = true;
            coinObj.SetActive(true);
            SetStars(computer, rank);
        }
        else
        {
            int rank = computer.rank;
            upgradeCostText.text = "Max  ";
            progressBar.SetFillAmount(1);
            buttonObj.interactable = false;
            coinObj.SetActive(false);
            SetStars(computer, rank);
        }
    }

    void SetStars(Computers computer, float rank)
    {
        int ranksCount = ComputerManager.Instance.upgradeRanks.Count;
        ResetStars();

        string colorHex = "#42BDFF";
        if (rank > 6 && ranksCount > 6)
            colorHex = "#88DD4A";

        progressBarFill.color = HexToColor(colorHex);
        int fixedRanksCount = (ranksCount > 6) ? ranksCount - 5 : ranksCount;
        fixedRanksCount = (ranksCount > 6 && rank <= 6) ? 6 : fixedRanksCount;

        for (int i = 0; i < (fixedRanksCount - 1); i++)
        {
            GameObject obj = Instantiate(starPrefab, stars.transform);
            starsList.Add(obj);
        }

        starsList.ForEach(star => star.SetActive(true));

        float fixedRank = (rank > 6 && ranksCount > 6) ? rank - 5 : rank;
        for (int i = 0; i < fixedRank - 1; i++)
        {
            starsList[i].GetComponent<Image>().color = HexToColor(colorHex);
        }
    }

    void ResetStars()
    {
        starsList.Clear();
        foreach (Transform star in stars.transform)
        {
            Destroy(star.gameObject);
        }
    }

    public void LvUpButton()
    {
        if (Wallet.Instance.TryRemoveMoney(upgradeCost))
        {
            ComputerManager.Instance.LvUp(computer);
            Refresh();
        }
    }

    public void OpenWindow(Computers computer)
    {
        LoadData(computer);
        panel.SetActive(true);
        CloseWindowsOnClick.Instance.WindowOpened();
    }

    public override void CloseWindow()
    {
        panel.SetActive(false);
    }
    private Color HexToColor(string hex)
    {
        hex = hex.Replace("#", "");
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }
}
