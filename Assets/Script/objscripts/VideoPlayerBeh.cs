using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerBeh : MonoBehaviour
{
    public VideoPlayer  currentPlayer;
    public RawImage     currentScreen;

    void Awake()
    {
        currentPlayer = GetComponent<VideoPlayer>();
        currentScreen = GetComponent<RawImage>();
    }
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (!currentPlayer.isPlaying)
        {
            hideVideo();
        }
    }


    public void PlayVideo() { 
        if (currentPlayer.isPrepared) { 
            currentPlayer.Play();
        }
    }
           
    public void LoadVideo(VideoClip vc) {
        currentPlayer.clip = vc;
    }
           
    public void showVideo() {
        currentScreen.enabled = true;
    }
    
    public void hideVideo() {
        currentScreen.enabled = false;
    }
    
}