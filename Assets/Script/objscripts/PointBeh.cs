using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBeh : MonoBehaviour
{
    private Vector2 propPos;
    private Vector2 parentSize;
    private RectTransform curRt;
    private RectTransform parRt;

    public string[] statement;
    public GuideBeh MasterGuide;

    private void Start()
    {
        curRt = GetComponent<RectTransform>();
        parRt = transform.parent.gameObject.GetComponent<RectTransform>();
        MasterGuide = FindObjectOfType<GuideBeh>();
    }
    
    private void LateUpdate()
    {
        curRt.anchoredPosition = parRt.sizeDelta * propPos;
        float w = parRt.sizeDelta.x > parRt.sizeDelta.y ? parRt.sizeDelta.x : parRt.sizeDelta.y;
        curRt.sizeDelta = new Vector2(w * 0.1f, w * 0.1f);
    }
    
    public void SetProportion(Vector2 v) {
        propPos = v;
    }

    private void OnMouseDown()
    {
        MasterGuide.Moving(statement);
    }
}




