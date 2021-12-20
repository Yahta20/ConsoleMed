using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Consoleum;

/// <summary>
/// Класс хранящий и реализующий взаимодействие с ними
/// </summary>

public class Comendant : MonoBehaviour
{
    public static Comendant Instance { get; private set; }

    public List<GameObject> ObjectOnScene;


    /// <summary>
    /// Добавление дочернего обьета
    /// </summary>
    /// <param name="go">Object for adding</param>
    public void AddObgetOnScene(GameObject go)
    {
        //ObjectOnScene.Add(go);
        go.transform.SetParent(this.gameObject.transform);
        ObjectOnScene = RollCall();
    }


    
    public string[] getAllObject() {
       var names = GetRecursChild(this.gameObject.transform,0);
       return names.ToArray();
    }
    
    private List<string> GetRecursChild(Transform t,int r)
    {
       List<string> ls = new List<string>();
       string[] folders = GetChildS(t,r);
        for (int i = 0; i < folders.Length; i++)
        {
           ls.Add(folders[i]+"|"+ r.ToString());
           ls.AddRange(GetRecursChild(t.GetChild(i),r+1));
        }

       return ls;
    }
    
    private string[] GetChildS(Transform t,int r)
    {
        var ret = new string[t.childCount];
        
        for (int i = 0; i < t.childCount; i++)
        {
            
            ret[i] = $"{tabs(r)}{t.GetChild(i).name}";
        }
        return ret;
    }

    private string tabs(int r) {
        var s = "";
        for (int i = 0; i < r; i++)
        {
            s += "\t";
        }
        return s;
    }

    private List<GameObject> RollCall() {
        return GetRecursGO(this.gameObject.transform);
    }

    private List<GameObject> GetRecursGO(Transform t)
    {
        var ls =new List<GameObject>();

        foreach (Transform item in t)
        {
            ls.Add(item.gameObject);
            ls.AddRange(GetRecursGO(item.transform));
        }

        return ls;
    }
    /// <summary>
    /// Print name of All Obj on Scene 
    /// </summary>
    /// <returns></returns>
    public string[] getOOSNames() {
        ObjectOnScene = RollCall();
        string[] sr = new string[ObjectOnScene.Count];
        var i = 0;
        foreach (var item in ObjectOnScene)
        {
            sr[i] = item.name;
            i++;
        }
        return sr;
    }




    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
        ObjectOnScene = new List<GameObject>();

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
