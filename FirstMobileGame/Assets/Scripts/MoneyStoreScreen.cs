using System.Collections.Generic;
using UnityEngine;

public class MoneyStoreScreen : MonoBehaviour
{
    [SerializeField] RectTransform content;
    [SerializeField] private GameObject moneySlotPrefab;

    private void OnEnable()
    {
        Debug.Log("A");

        if (GameManager.Instance == null)
        {
            Debug.Log("B");
            return;
        }

        Debug.Log("C");

        List<MoneyData.MoneyPack> moneyPacks = GameManager.Instance.GetMoneyPacks();

        Debug.Log("D");
        Debug.Log(moneySlotPrefab);
        Debug.Log(content);
        Debug.Log(GameManager.Instance);
        for (int i = 0; i < moneyPacks.Count; i++)
        {
            GameObject button = Instantiate(moneySlotPrefab, content);
            MoneySlot buttonData = button.GetComponent<MoneySlot>();
            buttonData.SetData(moneyPacks[i]);
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
