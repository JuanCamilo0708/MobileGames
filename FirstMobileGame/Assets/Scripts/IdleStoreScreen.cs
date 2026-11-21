using System.Collections.Generic;
using UnityEngine;

public class IdleStoreScreen : MonoBehaviour
{
    [SerializeField] RectTransform content;
    [SerializeField] private GameObject upgradeButtonPrefab;

    private void OnEnable()
    {
        Debug.Log("A");

        if (GameManager.Instance == null)
        {
            Debug.Log("B");
            return;
        }

        Debug.Log("C");

        List<Upgrades.Upgrade> upgrades = GameManager.Instance.GetUpgrades();

        Debug.Log("D");
        Debug.Log(upgradeButtonPrefab);
        Debug.Log(content);
        Debug.Log(GameManager.Instance);
        for (int i = 1; i < upgrades.Count; i++)
        {
            if (upgrades[i].type == UpgradeType.Click)
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
