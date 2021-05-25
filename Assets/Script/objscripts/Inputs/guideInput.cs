using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class guideInput : MonoBehaviour
{
    private Camera cam;

    [SerializeField]
    public float speed = 2;
    private Vector2 posit = Vector2.zero;

    private void Awake()
    {
        if (cam== null) {
            cam = GetComponent<Camera>();
        }
        transform.eulerAngles = posit;
    }
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            posit += new Vector2(-speed*Input.GetAxis("Mouse Y"),speed * Input.GetAxis("Mouse X"));
            transform.eulerAngles = posit;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //print($"{transform.rotation}");

        }

    }
}
