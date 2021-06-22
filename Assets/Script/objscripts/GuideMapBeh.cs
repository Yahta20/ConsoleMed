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
                if (room == PosList[i].name[0]&pos == PosList[i].name[1]) {
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

    public GuideBeh MasterGuide;

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
    public int pointOnFloar;

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
        pointOnFloar = 0;
    }
    void Start()
    {
        rt.sizeDelta = MCUI.Instance.getCanvasSize() * 0.32f;
        updateMap();
    }

    public bool isCorect() {
        return pointOnFloar == mapObj.pointList.Count;
    }


    private void updateMap()
    {
        foreach (var item in pointsInRoom)
        {
            Destroy(item);
        }
        pointsInRoom = new List<Image>();
    }

    public void changePlace(string[] args, GuideBeh.building build) {

        pointOnFloar = 0;
        mapObj.CleearChildObj();
        if (atlasMap.GetSprite($"{args[0]}_{args[1]}")!=null)
        {
             mapObj.SetMapBackground(atlasMap.GetSprite($"{args[0]}_{args[1]}"));
        }

        for (int i = 0; i < build.Rooms.Length; i++)
        {
            var roomName = build.Rooms[i].name;

            for (int j = 0; j < build.Rooms[i].point.Length; j++)
            {
                var pointName = build.Rooms[i].point[j].name;
                var go = Instantiate(pointToShow);
                var pb = go.GetComponent<PointBeh>();
                if (roomName == args[2] & pointName == args[3]) {
                    go.GetComponent<Image>().sprite = currentPlace;
                }
                pb.MasterGuide = MasterGuide;
                pb.statement = new string[4] {args[0],args[1],roomName,pointName };
                pb.SetProportion (build.Rooms[i].point[j].GetCordin());
                mapObj.AddPoint(go);
                pointOnFloar+=1;
            }

        }
       


    }
}