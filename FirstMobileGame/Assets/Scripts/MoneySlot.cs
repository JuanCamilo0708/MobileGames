using TMPro;
using UnityEngine;

public class MoneySlot : MonoBehaviour
{
    [SerializeField] private TMP_Text amountText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private SushiTray sushiTray;

    private long pointsGranted;

    public void SetData(MoneyData.MoneyPack moneyPack)
    {
        amountText.text = moneyPack.pointsGranted.ToString("N0");
        priceText.text = moneyPack.price.ToString();

        pointsGranted = moneyPack.pointsGranted;

        sushiTray.SetSushiAmount(moneyPack.sushiAmount);

    }


    public void BuyPack()
    {
        GameManager.Instance.AddPoints(pointsGranted);
    }
}
