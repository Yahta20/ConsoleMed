using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;

public class GuideBeh : MonoBehaviour
{
    [System.Serializable]
    public class Interactiv {

        public string name;

        public float[]  pos;

        public float[]  rot;

        public float[]  scl;

        public string link;

        public Vector3 getPos()
        {
            return new Vector3(pos[0], pos[1], pos[2]);
        }

        public Vector3 getScl()
        {
            return new Vector3(scl[0], scl[1], scl[2]);
        }
        
        public Quaternion getRot()
        {
            return new Quaternion(rot[0], rot[1], rot[2], rot[3]);
        }
    }

        


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

        public float[] cordin;

        public Conects[] conects;

        public Interactiv[] interactiv;

        public int GetInteractivCount() {
            return interactiv.Length;
        }

        public Vector2 GetCordin() {
            return new Vector2(cordin[0], cordin[1]);
        }

        public Interactiv GetObjByName(string name) {
            foreach (var item in interactiv)
            {
                if (
                item.name==name
                    )
                {
                    return item;
                }

            }

            return null;
        }

        public Interactiv[] GetArrayByName(string name) {
            var l2r = new List<Interactiv>();
            foreach (var item in interactiv)
            {
                if (item.name==name)
                {
                    l2r.Add(item);
                }
            }

            return l2r.ToArray();
        }


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
                if (name == Building[i].Name & Building[i].Floor == floor)
                {
                    return Building[i];
                }
            }
            return null;
        }
        
        public Vector2[] ListPointsOfMap(string name, int floor) {

            List<Vector2> listVector = new List<Vector2>();
            var build  = getFloorInBuildByName(name, floor);

            foreach (var room in build.Rooms)
            {
                foreach (var point in room.point)
                {
                    listVector.Add(point.GetCordin());
                }
            }

            return listVector.ToArray();
        }
    }

    public BuldingList currentBuid = new BuldingList();
    public TextAsset jsonInstruction;
    //public Material skybox;
    public GuideMapBeh gMapbeh;
    public UniversalScreenBeh usBeh;
    public List<GameObject> Points2Draw;
    public List<GameObject> IntObj;
    public string CurrentBuild = "Main";
    public int CurrentFloor = 1;
    public string CurrentRoom = "Hall";
    public string CurrentPos = "pos0";
    [Space]

    //написать загрузку с адресабле
    public GameObject prefabPoint;

    private bool InteraktiveErect;
    

    private void Awake()
    {
        InteraktiveErect = false;
        
        currentBuid = JsonUtility.FromJson<BuldingList>(jsonInstruction.text);
        Points2Draw = new List<GameObject>();
        IntObj = new List<GameObject>();
    }

    private void Start()
    {
        updateEmbient();
        updateEnvironment();
    }


    private void Update()
    {
        
        var point = currentBuid
                    .getFloorInBuildByName(CurrentBuild, CurrentFloor)
                    .getRoomByName(CurrentRoom)
                    .getPointByName(CurrentPos);

        if (RenderSettings.skybox.name == point.skyBox)
        {
            if (gMapbeh.isCorect())
            {
                if (usBeh.currState !=StateOfLoadScreen.Moving)
                {
                    usBeh.SetState(StateOfLoadScreen.Look);

                }
            }
            else {
                updateEmbient();
            }
            
        }

        if (point.GetInteractivCount() == IntObj.Count & !InteraktiveErect)
        {
            InteraktiveErect = true;
            spawnInteraction();
        }


    }

                        



    private void spawnInteraction()
    {
        var point = currentBuid
            .getFloorInBuildByName(CurrentBuild, CurrentFloor)
            .getRoomByName(CurrentRoom)
            .getPointByName(CurrentPos);
        var cheker = new Dictionary<string,bool>();

        for (int i = 0; i < IntObj.Count; i++)
        {
            IntObj[i] = Instantiate(IntObj[i]);
            IntObj[i].name = IntObj[i].name.Replace("(Clone)", "");

            var objName = IntObj[i].name;
            var intobj = point.GetObjByName(IntObj[i].name);

            for (int j = 0; j < point.interactiv.Length; j++)
            {
                if (!cheker.ContainsKey(point.interactiv[j].link) &
                    point.interactiv[j].name == IntObj[i].name
                    ) { 
                    switch (objName)
                    {
                        case "InfoTableMovi":
                            var osV = IntObj[i].GetComponent<InfoTableMoviBeh>();
                            osV.Link = point.interactiv[j].link;
                            break;
                        case "InfoTableImage":
                            var osI = IntObj[i].GetComponent<InfoTableImageBeh>();
                            osI.Link = point.interactiv[j].link;
                            break;
                    }
                    cheker.Add(point.interactiv[j].link,true);
                    IntObj[i].transform.position    = point.interactiv[j].getPos();
                    IntObj[i].transform.rotation    = point.interactiv[j].getRot();
                    IntObj[i].transform.localScale  = point.interactiv[j].getScl();
                    IntObj[i].transform.SetParent(transform);
                    break;
                }
            }

        }
        

    }

    private void updateEmbient()
    {
        foreach (var item in Points2Draw)
        {
            Destroy(item);
        }

        Points2Draw = new List<GameObject>();
        var build = currentBuid.getFloorInBuildByName(CurrentBuild, CurrentFloor);
        var point = build.getRoomByName(CurrentRoom)
                         .getPointByName(CurrentPos);

        Consoleum.DeveloperConsole.Instance.ParseInput($"sky {point.skyBox}");
        usBeh.SetState(StateOfLoadScreen.Loading);

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

        gMapbeh.changePlace(new string[4] {CurrentBuild,CurrentFloor.ToString(),
                                CurrentRoom,CurrentPos }, build);

    }

    public void Moving(string[] args) {
        InteraktiveErect = false;
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
        updateEnvironment();
    }

    private void updateEnvironment()
    {
        if (IntObj.Count > 0) { 
            foreach (var item in IntObj)
            {
                Destroy(item);
            }
        }
        IntObj = new List<GameObject>();
        var build = currentBuid.getFloorInBuildByName(CurrentBuild, CurrentFloor);
        var point = build.getRoomByName(CurrentRoom)
                         .getPointByName(CurrentPos);
        if (point.GetInteractivCount()>0) {
            foreach (var item in point.interactiv)
            {
                Addressables.LoadAssetAsync<GameObject>(item.name).Completed += OnLoadAsset;
            }
        }


    }

    void OnLoadAsset(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            var gerbObject = handle.Result;
            var w   = gerbObject.name.Replace("(Clone)","");
            gerbObject.name = w; 
            IntObj.Add(gerbObject);
        }

        if (handle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogWarning("Spawn object faild");
        }

    }
}