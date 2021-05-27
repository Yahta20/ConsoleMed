using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class MapObjBeh : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, 
    IDragHandler, IPointerExitHandler
{
    private Image currentImage;
    public GameObject pointToShow;
    private Vector3 positPres;
    private RectTransform parentRT;
    private RectTransform curerentRT;
    private bool mouseOn = false;

    public List<GameObject> pointList;

    private void Awake()
    {
        currentImage = GetComponent<Image>();
        curerentRT  = currentImage.rectTransform;
        var pt      = transform.parent;
        var gop     = pt.gameObject;
        parentRT    = gop.GetComponent<RectTransform>();
    }


    void Start()
    {
        var scale = parentRT.sizeDelta.x / curerentRT.sizeDelta.x;
        curerentRT.sizeDelta *= scale;
        //print($"{curerentRT.sizeDelta}-{scale}-{parentRT.sizeDelta}");
        CleearChildObj();
    }

    private void FixedUpdate()
    {
        if (mouseOn)
        {
            float mw = Input.GetAxis("Mouse ScrollWheel");
            var def = curerentRT.sizeDelta * mw * (1-mw);
            curerentRT.sizeDelta += def;
        }
    }

    private void Update()
    {
        var aspectP = parentRT.sizeDelta.x / parentRT.sizeDelta.y;
        var aspectI = curerentRT.sizeDelta.x / curerentRT.sizeDelta.y;

        var xRel = Mathf.Abs(parentRT.sizeDelta.x - curerentRT.sizeDelta.x)/2;
        var yRel = Mathf.Abs(parentRT.sizeDelta.y - curerentRT.sizeDelta.y)/2;

        var Psizex = parentRT.sizeDelta.x;
        var Psizey = parentRT.sizeDelta.y;

        var sizex = curerentRT.sizeDelta.x;
        var sizey = curerentRT.sizeDelta.y;

        var ancorsx = curerentRT.anchoredPosition.x;
        var ancorsy= curerentRT.anchoredPosition.y;

        var scale = 1.0f;

        if (Math.Abs(curerentRT.anchoredPosition.y) > yRel) {
            if (curerentRT.anchoredPosition.y>0) {
                ancorsy = Mathf.Lerp(curerentRT.anchoredPosition.y, yRel, Time.deltaTime*2);
            }
            if (curerentRT.anchoredPosition.y < 0)
            {
                ancorsy = Mathf.Lerp(curerentRT.anchoredPosition.y, -yRel, Time.deltaTime * 2);
            }
        }
        if (Math.Abs(curerentRT.anchoredPosition.x) > xRel)
        {
            if (curerentRT.anchoredPosition.x > 0)
            {
                ancorsx = 
                     Mathf.Lerp(curerentRT.anchoredPosition.x, xRel, Time.deltaTime * 2);
            }
            if (curerentRT.anchoredPosition.x < 0)
            {
                ancorsx =  Mathf.Lerp(curerentRT.anchoredPosition.x, -xRel, Time.deltaTime * 2);
            }
        }
        
        if (aspectP>=1) {

            if (Psizex >= sizex)
            {
                scale = Psizex / sizex;
            } 
            if (aspectI >= 1)
            {
                if (Psizey >= sizey)
                {
                    scale = Psizey / sizey;
                }
            }
        }

        if (aspectP<1)
        {

            if (Psizey >= sizey) { 
            scale = Psizey/sizey;
            }
            if (aspectI < 1)
            {
                 if (Psizex >= sizex)
                 {
                     scale = Psizex / sizex;
                 }
            }
        }


        
        
        curerentRT.sizeDelta *= scale;
        curerentRT.anchoredPosition = new Vector2(ancorsx,ancorsy);

    }
    




           


      
    public void CleearChildObj() {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void SetMapBackground(Sprite s) {
        currentImage.sprite = s;
        currentImage.rectTransform.sizeDelta = new Vector2(s.rect.width, s.rect.height);
        var scale = parentRT.sizeDelta.x / curerentRT.sizeDelta.x;
        curerentRT.sizeDelta *= scale;
        curerentRT.anchoredPosition = Vector2.zero;
    }
        
    public void AddPoint(Sprite sprite,Vector2 v2) {
        var go = Instantiate(pointToShow);
        go.GetComponent<Image>().sprite = sprite;
        go.GetComponent<Image>().rectTransform.anchoredPosition = v2;
        go.GetComponent<RectTransform>().SetParent(this.GetComponent<RectTransform>());
        //pointList.Add(go);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        positPres = Input.mousePosition;
        //print("x");
    }

    public void OnDrag(PointerEventData eventData)
    {
        var positPresNew = Input.mousePosition;
        var medx = positPres.x - positPresNew.x;
        var medy = positPres.y - positPresNew.y;
        //print($"x {medx} | y {medy}");
        curerentRT.anchoredPosition = new Vector3(
            curerentRT.anchoredPosition.x - medx, curerentRT.anchoredPosition.y - medy);
        positPres = Input.mousePosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       mouseOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      mouseOn = false;
    }




       
    

     
}