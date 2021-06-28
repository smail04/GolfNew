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

    private void Awake()
    {
        boundTransform = bound.GetComponent<RectTransform>();
        stickTransform = stick.GetComponent<RectTransform>();
        boundImage = bound.GetComponent<Image>();
        stickImage = stick.GetComponent<Image>();
        Hide();
    }

    private void Update()
    {        
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                Show();
                onBeginDrag.Invoke();
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                SetStickPosition(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Hide();
                onEndDrag.Invoke();
            }

        }
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
        stickTransform.position = position;
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
            onBeginDrag.Invoke();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
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
