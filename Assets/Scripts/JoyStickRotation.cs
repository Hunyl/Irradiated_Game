using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickRotation : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform jStickLever;
    private RectTransform rectTransfrom;
    [SerializeField, Range(10f, 150f)]
    private float leverRange;

    public Vector2 inputVector;
    public bool isRInput;

    void Start()
    {
        rectTransfrom = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (isRInput)
        {
            InputControlVector();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoyStickLever(eventData);
        isRInput = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlJoyStickLever(eventData);
        isRInput = true;
    }

    private void ControlJoyStickLever(PointerEventData eventData)
    {
        var inputDir = eventData.position - rectTransfrom.anchoredPosition;
        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;
        jStickLever.anchoredPosition = clampedDir;

        inputVector = clampedDir / leverRange;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        jStickLever.anchoredPosition = Vector2.zero;
        isRInput = false;
    }

    public Vector3 InputControlVector()
    {
        float x = inputVector.x;
        float y = inputVector.y;
        Vector3 direction = new Vector3(x, 0, y);

        return direction;
    }
}