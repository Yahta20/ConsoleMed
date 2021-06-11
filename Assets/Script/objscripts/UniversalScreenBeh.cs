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
    StateOfLoadScreen currState;

    private Image curentImage;

    public Image    Loadimg;
    public Text     LoadText;


    // [SerializeField]
    public bool ImageVisible;

    private void Awake()
    {
        curentImage = GetComponent<Image>();
        currState = StateOfLoadScreen.Loading;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        setLastChild();
        
        switch (currState)
        {
            case StateOfLoadScreen.Loading:
                var alpha = curentImage.color.a == 255 ? 255 : curentImage.color.a + Time.deltaTime;
                curentImage.color = new Color
                    (curentImage.color.r, curentImage.color.g, curentImage.color.b, alpha);
                Loadimg.color = new Color
                    (Loadimg.color.r, Loadimg.color.g, Loadimg.color.b, alpha);
                LoadText.color = new Color
                    (LoadText.color.r, LoadText.color.g, LoadText.color.b, alpha);
                break;
            case StateOfLoadScreen.Moving:
                
                
                break;
            case StateOfLoadScreen.Look:
                alpha = curentImage.color.a == 0 ? 0 : curentImage.color.a - Time.deltaTime;
                curentImage.color = new Color
                    (curentImage.color.r, curentImage.color.g, curentImage.color.b, alpha);
                Loadimg.color = new Color
                    (Loadimg.color.r, Loadimg.color.g, Loadimg.color.b, alpha);
                LoadText.color = new Color
                    (LoadText.color.r, LoadText.color.g, LoadText.color.b, alpha);
                break;
        }
        
                

        




    }

    private void setLastChild()
    {
        var childs = MCUI.Instance.transform.childCount;
        transform.SetSiblingIndex(childs-1);
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
    }



}
