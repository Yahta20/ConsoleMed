using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class settingButton : MonoBehaviour, IPointerDownHandler
{
    public SettingPanel sp;
    public void OnPointerDown(PointerEventData eventData)
    {
        sp.changeState();
    }
}
