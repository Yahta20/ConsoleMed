using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class guideInput : MonoBehaviour
{
    private Camera cam;
    public MapObjBeh mob;

    [SerializeField]
    float speedView = 2;
    float speedZoom = 2;
    float FOV;



    private Vector2 posit = Vector2.zero;

    private void Awake()
    {
        if (cam== null) {
            cam = GetComponent<Camera>();
        }
        transform.eulerAngles = posit;
        FOV = cam.fieldOfView;
    }

    public void setViewSpeed(float t) {
        speedView = t;
    }
    public void setZoomSpeed(float t)
    {
        speedZoom = t;
    }
    

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            posit += new Vector2(-speedView * Input.GetAxis("Mouse Y"), speedView * Input.GetAxis("Mouse X"));
            transform.eulerAngles = posit;
        }//Mouse ScrollWheel
        if (Input.GetAxis("Mouse ScrollWheel") !=0 & !mob.isOnPoint())
        {
            
            FOV += Input.GetAxis("Mouse ScrollWheel") * speedZoom;
            
            FOV = FOV > 60 ? 60 : FOV;

            FOV = FOV < 25 ? 25 : FOV;
        
        }

        cam.fieldOfView = FOV;
    }
}