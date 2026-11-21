using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText = null;
    [SerializeField] private Upgrades upgradesDatabase = null;
    [SerializeField] private MoneyData moneyData = null;

    private long points = 0;
    private float timer = 0;

    private Dictionary<Upgrades.Upgrade, int> upgrades = new Dictionary<Upgrades.Upgrade, int>();
    

    static private GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            UpdateScore();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;
            points += GetIdleAmount();
            UpdateScore();
        }
    }
    private void UpdateScore()
    {
        scoreText.text = "Score: " + points.ToString();
    }

    public void AddScore()
    {
        points += GetClickAmount();
        UpdateScore();
    }
    public void AddPoints(long amount)
    {
        points += amount; 
        UpdateScore();
    }
    public bool UsePoints(long amount)
    {
        if (amount < points)
        {
            points -= amount;
            return true;
        }
        return false;
    }
    public long GetPoints()
    {
        return points;
    }
    public List<Upgrades.Upgrade> GetUpgrades()
    {
        return upgradesDatabase.upgrades;
    }
    public List<MoneyData.MoneyPack> GetMoneyPacks()
    {
        return moneyData.packs;
    }

    public bool PurchaseUpgrade(Upgrades.Upgrade upgrade)
    {
        long cost = CalculateUpdateCost(upgrade);
        if (UsePoints(cost))
        {
            if (!upgrades.ContainsKey(upgrade))
            {
                upgrades.Add(upgrade, 0);
            }
            upgrades[upgrade]++;

            UpdateScore();
            return true;
        }
        return false;
    }
    private long GetClickAmount()
    {
        long total = 1;

        foreach (var item in upgrades)
        {
            if (item.Key.type == UpgradeType.Click)
            {
                total += item.Key.amount * item.Value;
            }


        }

        return total;
    }
    private long GetIdleAmount()
    {
        long total = 0;

        foreach (var item in upgrades)
        {
            if (item.Key.type == UpgradeType.Idle)
            {
                total += item.Key.amount * item.Value;
            }


        }

        return total;
    }

    public long CalculateUpdateCost(Upgrades.Upgrade upgrade)
    {
        int level = 0;

        if (upgrades.TryGetValue(upgrade, out level))
        {
            return (long)(upgrade.cost * Mathf.Pow(1.5f, level));
        }

        return upgrade.cost;
    }
}
