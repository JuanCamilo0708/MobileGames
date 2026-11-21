using UnityEngine;

public class GameScreen : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.Instance.AddScore();
    }

    public void GoToStore()
    {
        ScreenManager.Instance.OpenScreen("StoreScreen");
    }
    public void GoToIdleStore()
    {
        ScreenManager.Instance.OpenScreen("IdleStoreScreen");

    }
    public void GoToPointsStore()
    {
        ScreenManager.Instance.OpenScreen("PointsStoreScreen");
    }
}
