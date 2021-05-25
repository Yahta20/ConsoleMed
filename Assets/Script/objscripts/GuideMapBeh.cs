using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


public class GuideMapBeh : MonoBehaviour
{
    [System.Serializable]
    public class posList
    {
        public string[] name;
        public float[] cordin;
        public Vector2 GetPointPos() {
            return new Vector2(cordin[0], cordin[1]);
        }
    }

    [System.Serializable]
    public class floorMap
    {
        public string Build;
        public string Floor;
        public posList[] PosList;

        public posList GetPosLists(string room, string pos) {
            for (int i = 0; i < PosList.Length; i++)
            {
                if (room == PosList[i].name[0]& pos == PosList[i].name[1]) {
                    return PosList[i];
                }
            }
            return null;
        }
    }
    
    [System.Serializable]
    public class Map {
    
        public floorMap[] FloorMap;

        public floorMap getFloorInBuildByName(string name, string floor)
        {
            for (int i = 0; i < FloorMap.Length; i++)
            {
                if (name == FloorMap[i].Build & FloorMap[i].Floor == floor)
                {
                    return FloorMap[i];
                }
            }
            return null;
        }
        
    }





            


    public TextAsset jsonInstruction;
    public SpriteAtlas atlasMap;
    public Sprite currentPlace;
    public Sprite otherPlace;
    
    [Space]
    public MapObjBeh mapObj;
    public Image CurrentBacground;
    public GameObject pointToShow;
    [Space]
    public Map currentMap = new Map();
    public List<Image> pointsInRoom;
    public string[] position;
    

    private RectTransform rt;
    private Vector2 posit;


    // Start is called before the first frame update
    void Awake()
    {
        currentMap = JsonUtility.FromJson<Map>(jsonInstruction.text);
        pointsInRoom = new List<Image>();
        rt = GetComponent<RectTransform>();
        //var p    = MCUI.Instance.getCanvasSize()*0.32f;
        //new Vector2(p.x,p.y);
        
        currentPlace = atlasMap.GetSprite("pointPlace");
        otherPlace   = atlasMap.GetSprite("point");
        CurrentBacground = GetComponent<Image>();
    }

    private void OnEnable()
    {
    }
        


    void Start()
    {
        rt.sizeDelta = MCUI.Instance.getCanvasSize() * 0.32f;
        //rt.sizeDelta = new Vector2(p.y,p.x);
        updateMap();
    }

    private void updateMap()
    {
        foreach (var item in pointsInRoom)
        {
            Destroy(item);
        }
        pointsInRoom = new List<Image>();

    }

    public void changePlace(string[] args) {

        position = args;

        mapObj.SetMapBackground(atlasMap.GetSprite($"{args[0]}_{args[1]}"));

        //print(atlasMap.GetSprite($"{args[0]}_{args[1]}").rect);
        //CurrentBacground.sprite = atlasMap.GetSprite($"{args[0]}_{args[1]}");
        
        var floor = currentMap.getFloorInBuildByName(args[0],args[1]);

        for (int i = 0; i < floor.PosList.Length; i++)
        {
            
            if (floor.PosList[i].name[0] == args[0] & floor.PosList[i].name[1] == args[1])
            {
                mapObj.
                    AddPoint(currentPlace, floor.PosList[i].GetPointPos());
                
                //NewImage.sprite = currentPlace; //Set the Sprite of the Image Component on the new GameObject
            }
            else {
                mapObj.
                    AddPoint(otherPlace, floor.PosList[i].GetPointPos());
                //NewImage.sprite = otherPlace;
            }
            
        }
    
    }

    

    public void AddPoint(Sprite sprite, Vector2 v2)
    {
        var go = Instantiate(pointToShow);
        go.GetComponent<Image>().sprite = sprite;
        go.GetComponent<Image>().rectTransform.anchoredPosition = v2;
        go.transform.SetParent(this.mapObj.GetComponent<RectTransform>());
        //pointList.Add(go);
    }

    // Update is called once per frame
    void Update()
    {
        if (rt.sizeDelta != MCUI.Instance.getCanvasSize() * 0.32f) {
            rt.sizeDelta = MCUI.Instance.getCanvasSize() * 0.32f;
        }
    }


}
