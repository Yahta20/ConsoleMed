using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class AssetLoader : MonoBehaviour
{
    [SerializeField] private string strin;
    public Material trying;
    public bool isComleat;


    // Start is called before the first frame update
    void Start()
    {
        isComleat = false;
        get(name);   
    }

    public void LoadMap(string iner) {
        isComleat = false;
        strin = iner;
        get(iner);
    }

    private async void get(string name)
    {
        //var loc = await Addressables.LoadResourceLocationsAsync(name).Task;//Task;
        //foreach (var locat in loc) {
        //    trying = await Addressables.LoadAssetAsync<Material>(name).Task;// += OnLoadAsset;
        //}
        Addressables.LoadResourceLocationsAsync(name).Completed += LRLA;



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LRLA(AsyncOperationHandle<IList<IResourceLocation>> handle) {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
           foreach (var item in handle.Result)
           {
                Addressables.LoadAssetAsync<Material>(strin).Completed += OnLoadAsset;
            }
        }
    
    }

    void OnLoadAsset(AsyncOperationHandle<Material> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            trying = handle.Result;
            isComleat = true;
        }
        if (handle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogWarning("Spawn object faild");
        }

    }
}
