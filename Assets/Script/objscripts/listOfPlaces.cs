using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listOfPlaces : MonoBehaviour
{
    public RectTransform GuidesMap;

    RectTransform rt;
    bool isClose;
    // Start is called before the first frame update
    void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GuidesMap.sizeDelta.x != rt.sizeDelta.x &
            GuidesMap.sizeDelta.y + rt.sizeDelta.y != MCUI.Instance.getCanvasSize().y
            ) {
            rt.sizeDelta = new Vector2
                (GuidesMap.sizeDelta.x,   MCUI.Instance.getCanvasSize().y- GuidesMap.sizeDelta.y);
            rt.anchoredPosition = new Vector2(0,-GuidesMap.sizeDelta.y);
        }
        rt.anchoredPosition = new Vector2(GuidesMap.sizeDelta.x + GuidesMap.anchoredPosition.x - rt.sizeDelta.x,
            -(GuidesMap.sizeDelta.y + GuidesMap.anchoredPosition.y));

    }
}
