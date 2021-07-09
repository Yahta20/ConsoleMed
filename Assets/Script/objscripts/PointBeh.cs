using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointBeh : MonoBehaviour, IPointerDownHandler
{
    private Vector2 propPos;
    private Vector2 parentSize;
    private RectTransform curRt;
    private RectTransform parRt;
    [Space]

    public string[] statement;
    //public GuideBeh MasterGuide;
    public GuideMaster MasterGuide;

    private void Start()
    {
        curRt = GetComponent<RectTransform>();
        parRt = transform.parent.gameObject.GetComponent<RectTransform>();
        MasterGuide = FindObjectOfType<GuideMaster>();
    }
        
    private void LateUpdate()
    {
        setSize();
    }
    
    public void SetProportion(Vector2 v) {
        propPos = v;
    }
    private void setSize() { 
    
        //float asp = thisSprite. //curRt.sizeDelta.x / curRt.sizeDelta.y;
        
        curRt.anchoredPosition = parRt.sizeDelta * propPos;
        float w = parRt.sizeDelta.x > parRt.sizeDelta.y ? parRt.sizeDelta.x : parRt.sizeDelta.y;
        w *= 0.03f;
        curRt.sizeDelta = new Vector2( w , w );
        //
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        MasterGuide.Moving(statement);
    }
        
}