using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class InfoTableMoviBeh : MonoBehaviour
{
    public string Link = "";

    public VideoClip Image2Show;



    void OnMouseDown()
    {
        print($"Video {Link}");
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
