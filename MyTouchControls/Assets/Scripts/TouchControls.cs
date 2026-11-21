using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
public class TouchControls : MonoBehaviour
{

    public enum SwipeDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    [SerializeField] private float minHoldDuration = 0.5f;
    [SerializeField] private Color objectColor = Color.yellow;

    private Color defaltObjectColor = Color.white;
    private float holdDuration = 0;
    private bool isObjectSelected = false;
    private Vector2 touchPos = Vector2.zero;

    private Vector2 startingPosition = Vector2.zero;
    private GameObject selectedObject = null;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }
    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
    private void Update()
    {
        foreach (var touch in UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches)
        {

            switch (touch.phase)
            {
                case UnityEngine.InputSystem.TouchPhase.Began: 
                    OnPress();
                    touchPos = touch.screenPosition;
                    break;
                case UnityEngine.InputSystem.TouchPhase.Moved: 
                    OnHold();
                    touchPos = touch.screenPosition;
                    break;
                case UnityEngine.InputSystem.TouchPhase.Stationary:
                    OnHold();
                    touchPos = touch.screenPosition;
                    break;
                case UnityEngine.InputSystem.TouchPhase.Canceled:
                    OnRealease();

                    Vector2 releaseData = touch.screenPosition - touch.startScreenPosition;
                    OnSwipe(releaseData);
                    break;
                case UnityEngine.InputSystem.TouchPhase.Ended:
                    OnRealease();

                    Vector2 releaseDelta = touch.screenPosition - touch.startScreenPosition;
                    OnSwipe(releaseDelta);
                    break;


                    
                default: break;
            }

        }
        if(!isObjectSelected && holdDuration > minHoldDuration)
        {
            Ray screenRay = Camera.main.ScreenPointToRay(touchPos);
            RaycastHit hitInfo;
            if (Physics.Raycast(screenRay, out hitInfo, 100))
            {
                if (hitInfo.collider != null && hitInfo.collider.gameObject != null)
                {
                    MeshRenderer renderer = hitInfo.collider.gameObject.GetComponent<MeshRenderer>();
                    if (renderer != null)
                    {
                        defaltObjectColor = renderer.material.color;
                        renderer.material.color = objectColor;
                        selectedObject = hitInfo.collider.gameObject;
                    }
                }
            }
        }

    }

    private void OnPress()
    {
        holdDuration = 0.0f;
        isObjectSelected = false;
        Debug.Log("Touch is PRESSED");
    }
    private void OnRealease()
    {
        Debug.Log("Touch is RELEASED");
        if(selectedObject != null)
        {
            MeshRenderer renderer = selectedObject.GetComponent<MeshRenderer>();

        }
    }
    private void OnHold()
    {
        holdDuration += Time.deltaTime;

        Debug.Log("Touch is HELD " + holdDuration.ToString("F2"));

    }
    private void OnSwipe(Vector2 releasePosition)
    {
        Vector2 dir = releasePosition.normalized;
        float dotUp = Vector2.Dot(dir, Vector2.up);
        float dotRight = Vector2.Dot(dir, Vector2.right);
        if (Mathf.Abs(dotUp) >= Mathf.Abs(dotRight))
        {
            if (dotUp > 0)
            {
                ProcessSwipe(SwipeDirection.Up);
            }
            else
            {
                ProcessSwipe(SwipeDirection.Down);

            }
        }
        else
        {
            if (dotRight > 0)
            {
                ProcessSwipe(SwipeDirection.Right);
            }
            else
            {
                ProcessSwipe(SwipeDirection.Left);

            }
        }
        float speed = releasePosition.magnitude / holdDuration;
        Debug.Log("Swipe SPEED "+ speed.ToString("F2") + "pix/s");

    }
    private void ProcessSwipe(SwipeDirection direction)
    {
        switch (direction)
        {
            case SwipeDirection.Up: Debug.Log("Swipe UP"); break;
            case SwipeDirection.Down: Debug.Log("Swipe DOWN"); break;
            case SwipeDirection.Left: Debug.Log("Swipe LEFT"); break;
            case SwipeDirection.Right: Debug.Log("Swipe RIGHT"); break;
            default: break;
        }
    }
}
