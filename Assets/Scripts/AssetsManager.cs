
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetsManager : MonoBehaviour
{
    private static AssetsManager _assetsManager;
    public static AssetsManager Manager
    {
        get
        {
            if (_assetsManager == null)
            {
                var go = new GameObject("AssetManager");
                _assetsManager = go.AddComponent<AssetsManager>();
                DontDestroyOnLoad(go);
            }

            return _assetsManager;
        }
    }
    private Dictionary<string, object> _resDic;
    
    
    void Start()
    {
        _resDic = new Dictionary<string, object>();
    }

    public T Load<T>(string key)
    {
        var handle = Addressables.LoadAssetAsync<T>(key);
        handle.WaitForCompletion();
        return handle.Result;
    }

    public void LoadAssetAsync<T>(string key)
    {
        StartCoroutine(CorLoadAssetAsync<T>(key));
    }
    private IEnumerator CorLoadAssetAsync<T>(string key)
    {
        if (_resDic.ContainsKey(key))
        {
            // Debug.Log("重复加载资源：" +key);
            yield break;
        }
        var handle = Addressables.LoadAssetAsync<T>(key);
        yield return handle;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _resDic.TryAdd(key,handle.Result);
        }
        else
        {
            Debug.Log("加载资源失败：" + key);
        }
    }
    
    public T GetAsset<T>(string key)
    {
        if (_resDic.TryGetValue(key,out var asset))
        {
            return (T)asset;
        }
        else
        {
            Debug.LogError("获取资源失败！：" + key);
            return default;
        }
    }

    /// <summary>
    /// 会用反射获取T名字
    /// </summary>
    /// <param name="label"></param>
    /// <param name="callBack"></param>
    /// <typeparam name="T"></typeparam>
    public void LoadAssetsAsync<T>(string label, Action<T> callBack)
    {
        StartCoroutine(CorLoadAssetsAsync<T>(label, callBack));
    }
    
    private IEnumerator CorLoadAssetsAsync<T>(string label,Action<T> callBack)
    {
        AssetLabelReference assetLabelReference = new AssetLabelReference();
        assetLabelReference.labelString = label;
        var handle = Addressables.LoadAssetsAsync<T>(assetLabelReference,callBack);
        yield return handle;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (var t in handle.Result)
            {
                _resDic.TryAdd(t.GetType().Name,handle.Result);
            }
        }
        else
        {
            Debug.LogError("获取Label资源失败！：" + label);
        }
    }

    private IEnumerator DownLoadAssets(string key)
    {
       var handle = Addressables.DownloadDependenciesAsync(key);
       yield return handle;
       if (handle.Status == AsyncOperationStatus.Succeeded)
       {
           _resDic.TryAdd(key, handle.Result);
       }
    }
    
}
