using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTableImageBeh : MonoBehaviour
{

    public string Link = "";
    
    public Sprite Image2Show;



    void OnMouseDown()
    {
        print($"Image {Link}");
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
