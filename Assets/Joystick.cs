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
        Show();
        onBeginDrag.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetStickPosition(Input.mousePosition);
    }   

    public void OnEndDrag(PointerEventData eventData)
    {
        Hide();
        onEndDrag.Invoke();
    }
}
