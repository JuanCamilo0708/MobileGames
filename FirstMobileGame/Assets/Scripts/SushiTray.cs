using UnityEngine;

public class SushiTray : MonoBehaviour
{
    [SerializeField] private GameObject[] sushiObjects;

    public void SetSushiAmount(long amount)
    {
        amount = (long)Mathf.Clamp(amount, 0, sushiObjects.Length);

        for (int i = 0; i < sushiObjects.Length; i++)
        {
            sushiObjects[i].SetActive(i < amount);
        }
    }
}
