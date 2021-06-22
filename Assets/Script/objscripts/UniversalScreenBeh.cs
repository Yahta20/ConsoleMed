using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StateOfLoadScreen { 
    Loading = 0,
    Moving = 1,
    Look = 2,
    }
    

public class UniversalScreenBeh : MonoBehaviour
{
    public StateOfLoadScreen currState;

    public Image curentImage;

    public Image    Loadimg;
    public Text     LoadText;
    public VideoPlayerBeh curentPlayer;

    // [SerializeField]
    public bool ImageVisible;

    private void Awake()
    {
        //curentImage = GetComponent<Image>();
        currState = StateOfLoadScreen.Loading;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        setLastChild();
        
        switch (currState)
        {
            case StateOfLoadScreen.Loading:
                LoadingState();
                break;
            case StateOfLoadScreen.Moving:
                MovingState();
                break;
            case StateOfLoadScreen.Look:
                LookState();
            break;
        }

    }

    private void LookState()
    {
        var alpha = curentImage.color.a >= 0 ? 0 : curentImage.color.a - Time.deltaTime;
        curentImage.color = new Color
            (curentImage.color.r, curentImage.color.g, curentImage.color.b, alpha);
        Loadimg.color = new Color
            (Loadimg.color.r, Loadimg.color.g, Loadimg.color.b, alpha);
        LoadText.color = new Color
            (LoadText.color.r, LoadText.color.g, LoadText.color.b, alpha);
        //print(" fgrg ");
    }
    
    private void MovingState()
    {
        curentPlayer.showVideo();
        LoadingState();
    }

    private void LoadingState()
    {
        var alpha = curentImage.color.a >= 255 ? 255 : curentImage.color.a + Time.deltaTime;
        curentImage.color = new Color
            (curentImage.color.r, curentImage.color.g, curentImage.color.b, alpha);
        Loadimg.color = new Color
            (Loadimg.color.r, Loadimg.color.g, Loadimg.color.b, alpha);
        LoadText.color = new Color
            (LoadText.color.r, LoadText.color.g, LoadText.color.b, alpha);
    }
    
    private void setLastChild()
    {
        transform.SetSiblingIndex(MCUI.Instance.transform.childCount - 1);
    }


    public void SetColor(Color c) {
        var alpha = ImageVisible ? 255 : 0;
        curentImage.color = new Color (c.r,c.g,c.b,alpha);
    }


    public void SetSprate(Sprite s)
    {
        curentImage.sprite = s;
    }

    public void SetState(StateOfLoadScreen s) {
        currState = s;
        switch (currState)
        {
            case StateOfLoadScreen.Loading:
                curentImage.color = new Color
                    (curentImage.color.r, curentImage.color.g, curentImage.color.b, 255);
                Loadimg.color = new Color
                    (Loadimg.color.r, Loadimg.color.g, Loadimg.color.b, 255);
                LoadText.color = new Color
                    (LoadText.color.r, LoadText.color.g, LoadText.color.b, 255);
                break;
        }


    }
}
  