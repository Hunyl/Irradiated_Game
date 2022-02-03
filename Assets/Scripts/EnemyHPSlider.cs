using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPSlider : MonoBehaviour
{
    private Canvas canvas;
    private Camera hpCamera;
    private RectTransform rectParent;
    private RectTransform rectHp;

    public Vector3 offset;
    public Transform targetTr;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        hpCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHp = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);

        if (screenPos.z < 0.0f)
        {
            screenPos *= -1.0f;
        }

        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, hpCamera, out localPos);

        rectHp.localPosition = localPos;
    }
}
