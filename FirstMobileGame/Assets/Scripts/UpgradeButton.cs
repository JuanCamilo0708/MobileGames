using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private TMP_Text costText = null;
    [SerializeField] private Image upgradeImage = null;
    [SerializeField] Button button = null;

    private Upgrades.Upgrade upgradeData = null;

    private long prevPoint = 0;

    public void SetData(Upgrades.Upgrade upgrade)
    {
        upgradeData = upgrade;
        prevPoint = GameManager.Instance.GetPoints();
        UpdateDisplay();
    }

    private void Update()
    {
        if (prevPoint!= GameManager.Instance.GetPoints())
        {
            UpdateDisplay();
            prevPoint = GameManager.Instance.GetPoints();
        }
    }
    public void UpdateDisplay()
    {
        costText.text = GameManager.Instance.CalculateUpdateCost(upgradeData).ToString();
        upgradeImage.sprite = upgradeData.sprite;
        button.interactable =GameManager.Instance.CalculateUpdateCost(upgradeData) < GameManager.Instance.GetPoints();
    }
    public void OnClick()
    {
        if(GameManager.Instance.PurchaseUpgrade(upgradeData))
        {
            UpdateDisplay();
        }
    }
}
