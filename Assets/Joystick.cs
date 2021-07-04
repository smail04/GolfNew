using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject bound;
    public GameObject stick;
    private RectTransform boundTransform;
    private RectTransform stickTransform;
    private Image boundImage;
    private Image stickImage;
    public UnityEvent onBeginDrag;
    public UnityEvent onEndDrag;
    public Text debugText;
    private void Awake()
    {
        boundTransform = bound.GetComponent<RectTransform>();
        stickTransform = stick.GetComponent<RectTransform>();
        boundImage = bound.GetComponent<Image>();
        stickImage = stick.GetComponent<Image>();
        Hide();
    }

    private Vector2 firstPoint;
    private Vector2 lastPoint;
    bool swipeDown = false;

    private void Update()
    {
        debugText.text = "FPS: " + (1 / Time.deltaTime).ToString();
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.touches[0];

            if (!swipeDown)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    //if (boundTransform.rect.Contains(touch.position))
                    //{
                        firstPoint = touch.position;
                    //}
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    lastPoint = touch.position;

                    Vector2 directionToLastPoint = lastPoint - firstPoint;
                    if (Vector2.Dot((new Vector2(firstPoint.x, 0) - firstPoint).normalized, directionToLastPoint.normalized) > 0.7f)
                    {
                        SwipedDown();                        
                    }
                }
            }
            else
            {
                if (Input.touches.Length > 0)
                {
                    if (touch.phase == TouchPhase.Moved)
                    {
                        SetStickPosition(touch.position);                        
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        Hide();
                        swipeDown = false;
                        onEndDrag.Invoke();
                    }
                    else if (touch.phase == TouchPhase.Canceled)
                    {
                        swipeDown = false;
                    }
                }
            }
        }
        
    }

    void SwipedDown()
    {
        swipeDown = true;
        Show();
        onBeginDrag.Invoke();
    }

    public void Show()
    {
        boundImage.enabled = true;
        stickImage.enabled = true;
        stickTransform.position = boundTransform.position;
    }

    public void Hide()
    {
        boundImage.enabled = false;
        stickImage.enabled = false;
    }

    public void ReloadPosition()
    {
        stickTransform.position = boundTransform.position;
        Hide();
        onEndDrag.Invoke();
    }

    public void SetStickPosition(Vector2 position)
    {
        Vector2 directionRelativeCenter = position - (Vector2)boundTransform.position;
        float maxMagnitude = 500;
        float magnitude = directionRelativeCenter.magnitude;
        if (magnitude <= maxMagnitude)
        {
            stickTransform.position = position;
        }
        else
        {
            stickTransform.position = (Vector2)boundTransform.position + (directionRelativeCenter / magnitude * maxMagnitude);         
        }
    }

    public Vector2 GetPositionRelativeToCenter()
    {
        return stickTransform.position - boundTransform.position;    
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            Show(); 
            firstPoint = Input.mousePosition;
            onBeginDrag.Invoke();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            lastPoint = Input.mousePosition;
            Debug.DrawRay(firstPoint, (lastPoint - firstPoint).normalized * Vector2.Distance(firstPoint, lastPoint), Color.red);
            Debug.Log(Vector2.Dot(Vector2.down, (lastPoint - firstPoint).normalized));
            SetStickPosition(Input.mousePosition);
        }
    }   

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            Hide();
            onEndDrag.Invoke();
        }
    }

}
