using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideBeh : MonoBehaviour
{
    [System.Serializable]
    public class Conects {
        public string[] name;
        public float[] pos;
        public string[] drawTo;


        public Vector3 getPos() {
            return new Vector3(pos[0], pos[1], pos[2]);
        }
        public int Hierarchy() {
            return name.Length;
        }

    }

    [System.Serializable]
    public class Point {
        public string name;
        public string skyBox;
        public Conects[] conects;
    }
    
    [System.Serializable]
    public class rooms {

        public string name;
        public Point[] point;

        public Point getPointByName(string nam) {
            for (int i = 0; i < point.Length; i++)
            {
                if (nam == point[i].name)
                {
                    return point[i];
                }
            }
            return null;
        } 
    }

    [System.Serializable]
    public class building {
        public string Name;
        public int Floor;
        public rooms[] Rooms;
        
        public rooms getRoomByName(string name) {
            for (int i = 0; i < Rooms.Length; i++)
            {
                if (name == Rooms[i].name) {
                    return Rooms[i];
                }
            }
            return null;
        }
    }

    [System.Serializable]
    public class BuldingList { 
        public building[] Building;
        
        public building getBuildByName(string name) {
            for (int i = 0; i < Building.Length; i++)
            {
                if (name == Building[i].Name)
                {
                    return Building[i];
                }
            }
            return null;
        }

        public building getFloorInBuildByName(string name, int floor)
        {
            for (int i = 0; i < Building.Length; i++)
            {
                if (name == Building[i].Name & Building[i].Floor==floor)
                {
                    return Building[i];
                }
            }
            return null;
        }
    }

    public BuldingList currentBuid = new BuldingList();
    public TextAsset jsonInstruction;
    //public Material skybox;
    public GuideMapBeh gMapbeh;
    public List<GameObject> Points2Draw;
    public string CurrentBuild = "Main";
    public int CurrentFloor = 1;
    public string CurrentRoom = "Hall";
    public string CurrentPos = "pos0";
    [Space]
    //написать загрузку с адресабле
    public GameObject prefabPoint;

    private void Awake()
    {
        //var sw = new System.Diagnostics.Stopwatch();
        //sw.Start();
        currentBuid = JsonUtility.FromJson<BuldingList>(jsonInstruction.text);
        //sw.Stop();
        //print($"{sw.ElapsedTicks}/{sw.ElapsedMilliseconds}");
        Points2Draw = new List<GameObject>();
    }

    private void Start()
    {
        updateEmbient();
    }
        
    private void updateEmbient()
    {
        foreach (var item in Points2Draw)
        {
            Destroy(item);
        }
        Points2Draw = new List<GameObject>();
        var point = currentBuid.getFloorInBuildByName(CurrentBuild, CurrentFloor).getRoomByName(CurrentRoom).getPointByName(CurrentPos);
        Consoleum.DeveloperConsole.Instance.ParseInput($"sky {point.skyBox}");
        foreach (var p in point.conects)
        {
            var go = Instantiate(prefabPoint);
            var cb = go.AddComponent<ConnectBeh>();
            cb.MasterGuide = this;
            cb.statement = p;
            go.transform.SetParent(gameObject.transform);
            Points2Draw.Add(go);
            go.transform.position = p.getPos();
        }
        //gMapbeh.changePlace(new string[4] {CurrentBuild,CurrentFloor.ToString(),CurrentRoom,CurrentPos });
    }

    public void Moving(string[] args) {
        if (args.Length==1) {
            CurrentPos      = args[0];
        }
        if (args.Length == 2)
        {
            CurrentRoom     = args[0];
            CurrentPos      = args[1];
        }
        if (args.Length == 3)
        {
            CurrentFloor    = int.Parse(args[0]) ;
            CurrentRoom     = args[1];
            CurrentPos      = args[2];
        }
        if (args.Length == 4)
        {
           CurrentBuild     = args[0];
            CurrentFloor    = int.Parse(args[1]); 
            CurrentRoom     = args[2];
            CurrentPos      = args[3];
        }
        updateEmbient();
    }
}
