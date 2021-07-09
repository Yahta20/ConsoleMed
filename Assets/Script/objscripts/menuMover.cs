using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class menuMover : MonoBehaviour, IPointerDownHandler
{
    public RectTransform onGuide;
    public RectTransform self;
    public GuideMapBeh guideBeh;


    // Start is called before the first frame update
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
        guideBeh = onGuide.gameObject.GetComponent<GuideMapBeh>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        positionControl();

    }

    private void positionControl()
    {
        self.anchoredPosition = new Vector2(onGuide.sizeDelta.x+onGuide.anchoredPosition.x, 0);
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
