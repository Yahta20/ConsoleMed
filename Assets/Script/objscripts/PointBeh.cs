using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBeh : MonoBehaviour
{
    private Vector2 propPos;

    public void SetProportion(Vector2 v) {
        propPos = v;
    }

    private void LateUpdate()
    {
        
    }
}
