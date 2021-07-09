using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class listMover : MonoBehaviour, IPointerDownHandler
{
    public RectTransform onGuide;
    public RectTransform self;
    //public GuideMapBeh guideBeh;

    void Awake()
    {
        self = GetComponent<RectTransform>();
    }

    void Start()
    {
        self.anchorMin = onGuide.anchorMin;
        self.anchorMax = onGuide.anchorMax;
        self.pivot = onGuide.pivot;
        self.sizeDelta = new Vector2(50, 50);
        self.anchoredPosition = new Vector2(onGuide.sizeDelta.x, self.anchoredPosition.y);
        //guideBeh = onGuide.gameObject.GetComponent<GuideMapBeh>();
    }
    void LateUpdate()
    {
        positionControl();

    }
    private void positionControl()
    {
        self.anchoredPosition = new Vector2(onGuide.sizeDelta.x + onGuide.anchoredPosition.x-self.sizeDelta.x,
            -(onGuide.sizeDelta.y + onGuide.anchoredPosition.y));
        /*
        if (guideBeh.State())
        {
         
        }
        */
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //guideBeh.changeVisible();

    }
}