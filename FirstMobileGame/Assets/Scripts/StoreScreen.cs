using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class StoreScreen : MonoBehaviour
{
    [SerializeField] RectTransform content;
    [SerializeField] private GameObject upgradeButtonPrefab;

    private void OnEnable()
    {
        if (GameManager.Instance == null)
            return;

        Debug.Log(upgradeButtonPrefab);
        Debug.Log(content);
        List<Upgrades.Upgrade> upgrades = GameManager.Instance.GetUpgrades();
        for (int i = 1; i <upgrades.Count; i++)
        {
            if (upgrades[i].type == UpgradeType.Idle)
                continue;
            GameObject button = Instantiate(upgradeButtonPrefab, content);
            UpgradeButton buttonData = button.GetComponent<UpgradeButton>();
            buttonData.SetData(upgrades[i]);
        }
    }

    private void OnDisable()
    {
        
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }

    }
    public void OnCloseScreen()
    {
        ScreenManager.Instance.CloseScreen();
    }
}
