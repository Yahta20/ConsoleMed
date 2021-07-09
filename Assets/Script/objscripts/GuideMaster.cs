using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Interactiv
{
    public string name;

    public float[] pos;

    public float[] rot;

    public float[] scl;

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

    public void modTransform(Transform t) {
        pos = new float[3] { t.position.x, t.position.y, t.position.z };
        rot = new float[4] { t.rotation.x, t.rotation.y, t.rotation.z, t.rotation.w };
        scl = new float[3] { t.lossyScale.x, t.lossyScale.y, t.lossyScale.z };
    }

}

[System.Serializable]
public class Conects
{

    public string[] name;

    public float[] pos;

    public string[] drawTo;

    public Vector3 getPos()
    {
        return new Vector3(pos[0], pos[1], pos[2]);
    }

    public int Hierarchy()
    {
        return name.Length;
    }

    public void setPos(Transform t) {
        pos = new float[3] {t.position.x, t.position.y, t.position.z };
    }
}

[System.Serializable]
public class Point
{
    public string name;

    public string skyBox;

    public string skyLink;

    public float[] cordin;

    public Conects[] conects;

    public Interactiv[] interactiv;

    public int GetInteractivCount()
    {
        return interactiv.Length;
    }

    public Vector2 GetCordin()
    {
        return new Vector2(cordin[0], cordin[1]);
    }

    public Interactiv GetObjByName(string name)
    {
        foreach (var item in interactiv)
        {
            if (
            item.name == name
                )
            {
                return item;
            }

        }

        return null;
    }

    public Interactiv[] GetArrayByName(string name)
    {
        var l2r = new List<Interactiv>();
        foreach (var item in interactiv)
        {
            if (item.name == name)
            {
                l2r.Add(item);
            }
        }

        return l2r.ToArray();
    }
}

[System.Serializable]
public class rooms
{
    public string name;

    public Point[] point;

    public Point getPointByName(string nam)
    {
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
public class building
{
    public string Name;

    public int Floor;

    public rooms[] Rooms;

    public rooms getRoomByName(string name)
    {
        for (int i = 0; i < Rooms.Length; i++)
        {
            if (name == Rooms[i].name)
            {
                return Rooms[i];
            }
        }
        return null;
    }
}

[System.Serializable]
public class BuldingList
{
    public building[] Building;

    public building getBuildByName(string name)
    {
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

    public Vector2[] ListPointsOfMap(string name, int floor)
    {
        List<Vector2> listVector = new List<Vector2>();
        var build = getFloorInBuildByName(name, floor);
 
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

public class GuideMaster : MonoBehaviour
{
    public TextAsset jsonInstruction;
    public BuldingList currentBuid = new BuldingList();
    [Space]
    public string   CurrentBuild    = "Main";
    public int      CurrentFloor    = 1;
    public string   CurrentRoom     = "Hall";
    public string   CurrentPos      = "pos0";
    [Space]
    public GuideBeh     gb  ;
    public GuideMapBeh  gmb ;
    public MapObjBeh    mob ;
    [Space]
    public string path;

    //public GuideBeh gb;
    private void Awake()
    {
        currentBuid = JsonUtility.FromJson<BuldingList>(jsonInstruction.text);
        gb.setGM(this);
        gmb.setGM(this);
        mob.setGM(this);
    }

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void Moving(string[] args)
    {
        if (args.Length == 1)
        {
            CurrentPos = args[0];
        }
        if (args.Length == 2)
        {
            CurrentRoom = args[0];
            CurrentPos = args[1];
        }
        if (args.Length == 3)
        {
            CurrentFloor = int.Parse(args[0]);
            CurrentRoom = args[1];
            CurrentPos = args[2];
        }
        if (args.Length == 4)
        {
            CurrentBuild = args[0];
            CurrentFloor = int.Parse(args[1]);
            CurrentRoom = args[2];
            CurrentPos = args[3];
        }
        gb.Moving();
    }

    public string[] GetCurrentPos()
    {
        var posit =new string[4] {
            CurrentBuild,
            CurrentFloor.ToString(),
            CurrentRoom,
            CurrentPos
        };
        return posit;
    }

    public Point GetPointInfo() {
        var build = GetBuildInfo();
        var point = build.getRoomByName(CurrentRoom)
                         .getPointByName(CurrentPos);
        return point;
    }
    
    public building GetBuildInfo()
    {
        var build = currentBuid.getFloorInBuildByName(CurrentBuild, CurrentFloor);
        return build;
        //var point = build.getRoomByName(CurrentRoom)
        //                 .getPointByName(CurrentPos);
    }
    
    [ContextMenu("UpdateData")]
    public void SavePos() {
        var p       = GetPointInfo();
        var Np2d    = gb.Points2Draw;
        var Nio     = gb.IntObj;
        List<Conects> NConect = new List<Conects>();
        foreach (var item in Np2d)
        {
            var iCon = item.GetComponent<ConnectBeh>();
            var conect = iCon.statement;
            conect.setPos(item.transform);
            NConect.Add(conect);
        }
        List<Interactiv> NInteractive = new List<Interactiv>();
        foreach (var item in Nio)
        {
            var interactiv = new Interactiv();
            interactiv.name = item.name;
            interactiv.modTransform(item.transform);
            if (item.GetComponent<InfoTableImageBeh>())
            {
                interactiv.link =  item.GetComponent<InfoTableImageBeh>().Link;
            }
            if (item.GetComponent<InfoTableMoviBeh>())
            {
                
                interactiv.link = item.GetComponent<InfoTableMoviBeh>().Link;
            }
            NInteractive.Add(interactiv);
        }
        p.interactiv = NInteractive.ToArray();       
        p.conects = NConect.ToArray();
    }

    [ContextMenu("WriteToJson")]
    public void JsonWrite() {
        var njson = JsonUtility.ToJson(currentBuid,true);
        StreamWriter stream = new StreamWriter(path);
        stream.Write(njson);
    }


}