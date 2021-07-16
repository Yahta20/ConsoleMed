using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class listMover : MonoBehaviour, IPointerDownHandler
{
    public RectTransform anchor;
    public RectTransform onGuide;
    public RectTransform self;
    public listOfPlaces guideBeh;

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
        guideBeh = onGuide.gameObject.GetComponent<listOfPlaces>();
    }
    void LateUpdate()
    {
        positionControl();

    }
    private void positionControl()
    {
        self.anchoredPosition = new Vector2(anchor.sizeDelta.x + anchor.anchoredPosition.x-self.sizeDelta.x,
            -anchor.sizeDelta.y);
        /*
        if (guideBeh.State())
        {
         
        }
        */
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        guideBeh.changeVisible();

    }
}