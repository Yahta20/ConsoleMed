using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class guideInput : MonoBehaviour
{
    private Camera cam;
    public MapObjBeh mob;

    [SerializeField]
    public float speedView = 2;
    public float speedZoom = 2;
    public float FOV;

    private Vector2 posit = Vector2.zero;

    private void Awake()
    {
        if (cam== null) {
            cam = GetComponent<Camera>();
        }
        transform.eulerAngles = posit;
        FOV = cam.fieldOfView;
    }
    void Start()
    {

    }


    // Update is called once per frame
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