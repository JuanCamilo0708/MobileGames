using DG.Tweening;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject inputBloker = null;
    [SerializeField] private List<GameObject> screens = new List<GameObject>();
    [SerializeField] private float transitionDuration = 1.0f;
    [SerializeField] private string initialScreenName = "";
    private GameObject topScreen = null;
    private Coroutine transitionCorutine = null;
    static ScreenManager instance = null;

    public static ScreenManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            foreach (var screen in screens)
            {
                screen.SetActive(initialScreenName == screen.name);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenScreen(string screenName)
    {
        topScreen = null;
        foreach (GameObject screen in screens)
        {
            if (screen.name == screenName)
            {
                topScreen = screen;
                break;
            }
        }
        if (transitionCorutine != null)
        {
            StopCoroutine(transitionCorutine);
        }
        transitionCorutine = StartCoroutine(OpenScreen(topScreen));
    }
    public void CloseScreen()
    {
        if (topScreen == null)
        {
            return;
        }
        if (transitionCorutine != null)
        {
            StopCoroutine(transitionCorutine);
        }
        transitionCorutine = StartCoroutine(CloseScreen(topScreen));
    }
    IEnumerator OpenScreen(GameObject go)
    {
        inputBloker.SetActive(true);
        RectTransform rectTransform = go.transform as RectTransform;
        rectTransform.anchoredPosition = new Vector2(-Screen.width, rectTransform.anchoredPosition.y);
        go.SetActive(true);

        Tween screenTween = rectTransform.DOAnchorPosX(0.0f, transitionDuration).SetEase(Ease.OutExpo);
        yield return screenTween.WaitForCompletion();

        screenTween.Kill();


        inputBloker.SetActive(false);
    }
    IEnumerator CloseScreen(GameObject go)
    {
        inputBloker.SetActive(true);
        RectTransform rectTransform = go.transform as RectTransform;

        Tween screenTween = rectTransform.DOAnchorPosX(-Screen.width, transitionDuration).SetEase(Ease.OutExpo);
        yield return screenTween.WaitForCompletion();

        screenTween.Kill();
        go.SetActive(false);

        inputBloker.SetActive(false);
    }

}
