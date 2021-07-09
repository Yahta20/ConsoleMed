using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URILoader : MonoBehaviour
{
    public string name;
    public float complit;
    private bool complite;
    private string uri = "https://drive.google.com/uc?export=download&id=1LVp3w6hiGGL0EuhRohxZeMbQ2n8DJ5ny";

    
    public List<object> loader;


    public AssetBundle mBundle;
    public Material skybox;
    public Shader shader;

    //https://drive.google.com/file/d/1LVp3w6hiGGL0EuhRohxZeMbQ2n8DJ5ny/view?usp=sharing
    //https://drive.google.com/uc?export=download&id=1LVp3w6hiGGL0EuhRohxZeMbQ2n8DJ5ny

    private void Start()
    {
        loader = new List<object>();
    }

    public bool isComplet() {
        return complite;
    }

    public void LoadAsset(string link, string name) {
        complite = false;
        uri = link;
        StartCoroutine(Download(name));
    }

    private IEnumerator Download(string fName)
    {
        if (mBundle != null)
        {
            mBundle.Unload(false); //scene is unload from here
        }
        yield return GetBundle();
     
        if (!mBundle)
        {
            print($"herovo");
            yield break;
        }
        skybox = mBundle.LoadAsset<Material>(fName);
         var load = mBundle.LoadAllAssets();
        print(load.Length);
        for (int i = 0; i < load.Length; i++)
        {
            loader.Add(load[i]);
            print(load[i].GetType());
        }
        complite = true;
    }
        


    private IEnumerator GetBundle() {
        WWW req = WWW.LoadFromCacheOrDownload(uri,0);
        while ( !req.isDone)
        {
            
            //print(req.progress);
            yield return null; 
        }
        if (req.error == null)
        {
            mBundle = req.assetBundle;
            complit = 1;
        }
        else {
            print(req.error);
        }
    }
}