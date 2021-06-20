using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotationBar : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Spectator spectator;
    [Range(0, 40)] public float rotationSensetivity = 20;
    private float prevPoint = 0;
    public void OnBeginDrag(PointerEventData eventData)
    {
        prevPoint = Input.mousePosition.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (prevPoint != 0)
        {
            spectator.Rotate(new Vector3(0, -(prevPoint - Input.mousePosition.x), 0), rotationSensetivity * Time.deltaTime);
        }
        prevPoint = Input.mousePosition.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        prevPoint = 0;
    }
}
